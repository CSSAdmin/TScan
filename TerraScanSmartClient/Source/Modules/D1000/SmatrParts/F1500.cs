namespace D1000
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.Web.Services.Protocols;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;



    /// <summary>
    /// 
    /// </summary>
    [SmartPart]
    public partial class F1500 : BaseSmartPart
    {
        #region Varibles
        /// <summary>
        /// PageMode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;
        /// <summary>
        /// MasterFormNo
        /// </summary>
        private int masterFormNo;
        /// <summary>
        /// FormID
        /// </summary>
        private int keyID;
        /// <summary>
        /// FormMaster PermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;
        /// <summary>
        /// Permission for Open,Edit,Add,Delete
        /// </summary> 
        private PermissionFields slicePermissionField;
      
        /// <summary>
        /// Sample Form Dat from busines form entity
        /// </summary>
        private F1500SampleForm sampleFormData;
        /// <summary>
        /// Flag on Load Process
        /// </summary>
        private bool flagLoadOnProcess;
          /// <summary>
          /// 
          /// </summary>
        private F1500SampleForm sampleForm = new F1500SampleForm();

        private F1500SampleForm.SampleFormApplicationIdTableDataTable sample1Form = new F1500SampleForm.SampleFormApplicationIdTableDataTable(); 

        private F1500SampleForm.SampleFormMenuGroupTableDataTable sample2Form = new F1500SampleForm.SampleFormMenuGroupTableDataTable();
          /// <summary>
          /// 
          /// </summary>
        private F1500Controller form1500Controller;
       
        /// <summary>
        /// Navigation Flag
        /// </summary>
        private bool navigationFlag;
        private int tempAppId;
        private int tempMenuId;
        #endregion
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F1500"/> class.
        /// </summary>
        public F1500()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F1500"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F1500(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {

            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.keyID = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            //this.FormHeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.FormHeaderPictureBox.Height, this.FormHeaderPictureBox.Width, tabText, red, green, blue);
            this.HeaderPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.HeaderPictureBox.Height, this.HeaderPictureBox.Width, tabText, red, green, blue);
            this.Tag = formNo;

        } 
        #endregion


        #region Property


        /// <summary>
        /// Gets or sets the form1500 controller.
        /// </summary>
        /// <value>The form1500 controller.</value>
        [CreateNew]
        public F1500Controller Form1500Controller
        {
            get { return this.form1500Controller as F1500Controller; }
            set { this.form1500Controller = value; }

        }
        #endregion Property
        #region EventPublication
        /// <summary>
        /// Get Cancel Button
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> GetCancelButton;

        /// <summary>
        /// Occurs when [form slice_ edit enabled].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        /// <summary>
        /// Declare the event FormSlice_ValidationAlert        
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;


        [EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;

        /// <summary>
        /// Occurs when [form slice_ section indicator click].
        /// </summary>
        [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
        public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;


        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

     
        #endregion




        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            try
            {
                if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                {
                    this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                    this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                    this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                    this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;
                    ////business entity datasource and returning data table
                    if (this.sampleForm.FormSliceDetails.Rows.Count > 0)
                    {
                        eventArgs.Data.FlagInvalidSliceKey = false;
                    }
                    else
                    {
                      
                        
                        if (eventArgs.Data.FlagInvalidSliceKey)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #region Event Subscription



        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Microsoft.Practices.CompositeUI.Utility.DataEventArgs&lt;TerraScan.Common.SliceReloadActiveRecord&gt;"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            // Checks the masterform no is same  
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
              
                this.keyID = eventArgs.Data.SelectedKeyId;
               

                // Checks its not in View Mode 
                if (this.pageMode != TerraScanCommon.PageModeTypes.View)
                {
                    this.keyID = eventArgs.Data.SelectedKeyId;
                    this.flagLoadOnProcess = true;
                    this.navigationFlag = false;
                    this.FlagSliceForm = true;
                    this.LoadDefaultView();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.flagLoadOnProcess = false;
                }
                else if (this.keyID == eventArgs.Data.SelectedKeyId)  
                {
                    this.flagLoadOnProcess = true;
                    this.navigationFlag = false;
                    this.FlagSliceForm = true;
                    this.LoadDefaultView();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.flagLoadOnProcess = false;
                }

            }

        }

        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, EventArgs eventArgs)
        {
            if (this.slicePermissionField.newPermission)
            {
                // Clear all the controls value and make it enable
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.F1500ClearSmapleForm();
                
                this.ShowControls(true);
                this.ShowPanel(true);
                // Set focus on firs editanle field
                this.FormIDTextBox.Focus();
            }
            else
            {
                // Clear all the controls and make it disable
                this.pageMode = TerraScanCommon.PageModeTypes.New;
                this.F1500ClearSmapleForm();
                 this.ShowControls(false);
                this.ShowPanel(false);
               
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            this.flagLoadOnProcess = true;
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.F1500ClearSmapleForm();
            this.LoadDefaultView();
            this.flagLoadOnProcess = false;
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if ((this.pageMode.Equals(TerraScanCommon.PageModeTypes.Edit) && this.slicePermissionField.editPermission)
                 || (this.pageMode.Equals(TerraScanCommon.PageModeTypes.New) && this.slicePermissionField.newPermission))
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission)
            {
                this.SaveSampleFormDetails();
            }
        }

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

        #endregion Event Subscription

        #region Enable Edit


        /// <summary>
        /// Handles the Click event of the SeniorExemptPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void SeniorExemptPictureBox_Click(object sender, EventArgs e)
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
        /// Activates the Edit button in Master form according to the conditions specified.
        /// </summary>
        private void EditEnabled()
        {
            if (!this.flagLoadOnProcess && this.pageMode.Equals(TerraScanCommon.PageModeTypes.View)
                && this.PermissionFiled.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;

                // Event publication for enable Save,Cancel button in Form Master
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                
            }
        }

        #endregion Enable Edit

        #region Edit button event
        ///<summary>
        /// Enables the edit button in master form.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EnableEditButtonInMasterForm(object sender, EventArgs e)
        {
            try
            {
                // Enable Form Master Save/Cancel button
                this.EditEnabled();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        #endregion

        #region vaidation
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            if (string.IsNullOrEmpty(FormIDTextBox.Text))
            {
                sliceValidationFields.ErrorMessage = SharedFunctions.GetResourceString ( "Required Field");
                sliceValidationFields.RequiredFieldMissing = true;
            }
            return sliceValidationFields;
        } 
        #endregion

        /// <summary>
        /// Loads the default view.
        /// </summary>
        private void LoadDefaultView()
        {
            this.ShowPanel(true);

            this.ShowControls(true);

            this.F1500GetSampleFormData();
        }
        /// <summary>
        /// Shows the panel.
        /// </summary>
        /// <param name="show">if set to <c>true</c> [show].</param>
        private void ShowPanel(bool show)
        {
            this.FormIDPanel.Enabled = show;
            this.FormFilePanel.Enabled = show;
            this.DescriptionPanel.Enabled = show;
            this.MenuNamePanel.Enabled = show;
            this.WebHeightPanel.Enabled = show;
            this.MenuOrderPanel.Enabled = show;
            this.MenuGroupIDPanel.Enabled = show;
            this.GroupIDPanel.Enabled = show;
            this.ReportPanel.Enabled = show;
            this.ApplacationIDPanel.Enabled = show;
            this.IsPermissionOpenpanel.Enabled = show;
            this.IsPermissionMenupanel.Enabled = show;
            this.IsPermissiomAddPanel.Enabled = show;
            this.IsPermissionEditpanel.Enabled = show;
            this.IsPermissionDeletepanel.Enabled = show;
        }
        /// <summary>
        /// Shows the controls.
        /// </summary>
        /// <param name="show">if set to <c>true</c> [show].</param>
        private void ShowControls(bool show)
        {
            this.FormIDTextBox.Enabled = show;
            this.FormFileTextBox.Enabled = show;
            this.DescriptionTextBox.Enabled = show;
            this.MenuNameTextBox1.Enabled = show;
            this.WebHeightTextBox.Enabled = show;
            this.MenuOrderTextBox.Enabled = show;
            this.ReportTextBox.Enabled = show;
            this.GroupIDTextBox.Enabled = show;
            this.ApplicationIDComboBox.Enabled = show;
            this.MenuGroupIdComboBox.Enabled = show;
            this.IsPermissionMenuComboBox.Enabled = show;
            this.IsPermissionOpenComboBox.Enabled = show;
            this.IsPermissionAddComboBox.Enabled = show;
            this.IsPermissionEditComboBox.Enabled = show;
            this.IsPermissionDeleteComboBox.Enabled = show;
        }

        private void SaveSampleFormDetails()
        {
           
            F1500SampleForm.FormSliceDetailsDataTable dtSampleForm = new F1500SampleForm.FormSliceDetailsDataTable();
            F1500SampleForm.FormSliceDetailsRow drSampleForm = dtSampleForm.NewFormSliceDetailsRow();

            drSampleForm.Form = Int32.Parse(this.FormIDTextBox.Text);
            drSampleForm.FormFile = this.FormFileTextBox.Text;
            drSampleForm.MenuName = this.MenuNameTextBox1.Text;
            drSampleForm.Description =this. DescriptionTextBox.Text;
            int menugroupId;
            int.TryParse(this.MenuGroupIdComboBox.SelectedValue.ToString(),out menugroupId);
            drSampleForm.MenuGroupID = menugroupId;
            int appId;
            int.TryParse(this.ApplicationIDComboBox.SelectedValue.ToString(), out appId);
            drSampleForm.ApplicationID = appId;
            drSampleForm.Report = int.Parse(ReportTextBox.Text);
            int temp;
            int.TryParse(this.MenuOrderTextBox.Text, out temp);
            drSampleForm.MenuOrder = temp;
            int tempWebHeight;
            int.TryParse(this.WebHeightTextBox.Text, out tempWebHeight);
            drSampleForm.WebHeight = tempWebHeight;
            //drSampleForm.WebHeight = int.Parse(WebHeightTextBox.Text);
            int tempGroupId;
            int.TryParse(this.GroupIDTextBox.Text, out tempGroupId);
            drSampleForm.GroupID=tempGroupId;
            
            if(this.IsPermissionMenuComboBox.SelectedIndex.Equals(0))
            {
                drSampleForm.IsPermissionMenu =  true;
            }
            else
            {
                drSampleForm.IsPermissionMenu = false;
            }
            
            
            if (this.IsPermissionOpenComboBox.SelectedIndex.Equals(0))
            {
                drSampleForm.IsPermissionOpen = true;
            }
            else
            {
                drSampleForm.IsPermissionOpen = false;
            }
            if (this.IsPermissionAddComboBox.SelectedIndex.Equals(0))
            {
                drSampleForm.IsPermissionAdd = true;
            }
            else
            {
                drSampleForm.IsPermissionAdd = false;
            }
            if (this.IsPermissionEditComboBox.SelectedIndex.Equals(0))
            {
                drSampleForm.IsPermissionEdit = true;
            }
            else
            {
                drSampleForm.IsPermissionEdit = false;
            }
            if (this.IsPermissionDeleteComboBox.SelectedIndex.Equals(0))
            {
                drSampleForm.IsPermissionDelete = true;
            }
            else
            {
                drSampleForm.IsPermissionDelete = false;
            }
            
            dtSampleForm.Rows.Add(drSampleForm);
            DataSet tempDataSet = new DataSet("Root");
            tempDataSet.Tables.Add(dtSampleForm.Copy());
            tempDataSet.Tables[0].TableName="Table";
            string tempXmlData= TerraScanCommon.GetXmlString(dtSampleForm);

            int returnValue = this.Form1500Controller.WorkItem.InsertSampleFormDetails(drSampleForm.Form, tempXmlData, TerraScanCommon.UserId);
            

            
            SliceReloadActiveRecord currentKeyIdInfo;
            currentKeyIdInfo.MasterFormNo = this.masterFormNo;
            currentKeyIdInfo.SelectedKeyId = returnValue;
            this.FormSlice_OnSave_SetKeyId(this, new DataEventArgs<SliceReloadActiveRecord>(currentKeyIdInfo));
            SliceReloadActiveRecord sliceReloadActiveRecord = new SliceReloadActiveRecord();
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = returnValue;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
 
        }


        private void F1500GetSampleFormData()
        {
            this.sampleForm = (this.form1500Controller.WorkItem.F1500_GetSampleFormDetails(keyID));
            //this.sample1Form = (this.form1500Controller.WorkItem.GetApplicationId()).SampleFormApplicationIdTable;
            //this.sample2Form = (this.form1500Controller.WorkItem.GetMenuIdDetails()).SampleFormMenuGroupTable;

            if (this.sampleForm != null && this.sampleForm.SampleFormApplicationIdTable.Rows.Count > 0)
            {
                this.ApplicationIDComboBox.DataSource = this.sampleForm.SampleFormApplicationIdTable;
                this.ApplicationIDComboBox.ValueMember = this.sampleForm.SampleFormApplicationIdTable.ApplicationIDColumn.ColumnName;
                this.ApplicationIDComboBox.DisplayMember = this.sampleForm.SampleFormApplicationIdTable.ApplicationColumn.ColumnName.ToString();
               
            }
            if (this.sampleForm != null && this.sampleForm.SampleFormMenuGroupTable.Rows.Count > 0)
            {
                this.MenuGroupIdComboBox.DataSource = this.sampleForm.SampleFormMenuGroupTable;
                this.MenuGroupIdComboBox.DisplayMember = this.sampleForm.SampleFormMenuGroupTable.MenuGroupColumn.ColumnName;
                this.MenuGroupIdComboBox.ValueMember = this.sampleForm.SampleFormMenuGroupTable.MenuGroupIDColumn.ColumnName;
            }

         
            if (this.sampleForm.FormSliceDetails.Rows.Count > 0)
            {
                
                //this.FormIDTextBox.Text = this.sampleForm.FormSliceDetails.Rows[0][this.sampleForm.FormSliceDetails.FormColumn].ToString();
                //this.FormFileTextBox.Text = this.sampleForm.FormSliceDetails.Rows[0][this.sampleForm.FormSliceDetails.FormFileColumn].ToString();
                //this.MenuNameTextBox1.Text = this.sampleForm.FormSliceDetails.Rows[0][this.sampleForm.FormSliceDetails.MenuNameColumn].ToString();
                //this.DescriptionTextBox.Text = this.sampleForm.FormSliceDetails.Rows[0][this.sampleForm.FormSliceDetails.DescriptionColumn].ToString();
                ////this.MenuGroupIdComboBox.SelectedValue = this.sampleForm.SampleFormMenuGroupTable.Rows[0][this.sampleForm.SampleFormMenuGroupTable.MenuGroupColumn];
                ////this.ApplicationIDComboBox.SelectedValue = this.sampleForm.SampleFormApplicationIdTable.Rows[0][this.sampleForm.SampleFormApplicationIdTable.ApplicationColumn].ToString();
                //this.ReportTextBox.Text = this.sampleForm.FormSliceDetails.Rows[0][this.sampleForm.FormSliceDetails.ReportColumn].ToString();
                //this.MenuOrderTextBox.Text = this.sampleForm.FormSliceDetails.Rows[0][this.sampleForm.FormSliceDetails.MenuOrderColumn].ToString();
                //this.WebHeightTextBox.Text = this.sampleForm.FormSliceDetails.Rows[0][this.sampleForm.FormSliceDetails.WebHeightColumn].ToString();
                //this.GroupIDTextBox.Text = this.sampleForm.FormSliceDetails.Rows[0][this.sampleForm.FormSliceDetails.GroupIDColumn].ToString();
                //this.IsPermissionMenuComboBox.Text = this.sampleForm.FormSliceDetails.Rows[0][this.sampleForm.FormSliceDetails.IsPermissionMenuColumn].ToString();
                //this.IsPermissionOpenComboBox.Text = this.sampleForm.FormSliceDetails.Rows[0][this.sampleForm.FormSliceDetails.IsPermissionOpenColumn].ToString();
                //this.IsPermissionAddComboBox.Text = this.sampleForm.FormSliceDetails.Rows[0][this.sampleForm.FormSliceDetails.IsPermissionAddColumn].ToString();
                //this.IsPermissionEditComboBox.Text = this.sampleForm.FormSliceDetails.Rows[0][this.sampleForm.FormSliceDetails.IsPermissionEditColumn].ToString();
                //this.IsPermissionDeleteComboBox.Text = this.sampleForm.FormSliceDetails.Rows[0][this.sampleForm.FormSliceDetails.IsPermissionDeleteColumn].ToString();
                //F1500SampleForm.FormSliceDetailsRow objCurrentRow = (F1500SampleForm.FormSliceDetailsRow)this.sampleForm.FormSliceDetails.Rows[0];
               // F1500SampleForm.SampleFormApplicationIdTableRow objAppidcurrentRow=(F1500SampleForm.SampleFormApplicationIdTableRow)this.sampleForm.SampleFormApplicationIdTable.Rows[0];
                //F1500SampleForm.SampleFormMenuGroupTableRow objMenuidCurrentRow =(F1500SampleForm.SampleFormMenuGroupTableRow)this.sampleForm.SampleFormMenuGroupTable.Rows[0];


                F1500SampleForm.FormSliceDetailsRow objCurrentRow = (F1500SampleForm.FormSliceDetailsRow)this.sampleForm.FormSliceDetails.Rows[0];
                this.FormIDTextBox.Text = objCurrentRow.Form.ToString(); 
                this.FormFileTextBox.Text = objCurrentRow.FormFile.ToString();
                this.MenuNameTextBox1.Text = objCurrentRow.MenuName.ToString();
                this.DescriptionTextBox.Text = objCurrentRow.Description.ToString();
                this.MenuGroupIdComboBox.SelectedValue = objCurrentRow.MenuGroupID;
                this.ApplicationIDComboBox.SelectedValue = objCurrentRow.ApplicationID;
                this.ReportTextBox.Text = objCurrentRow.Report.ToString();
                this.MenuOrderTextBox.Text = objCurrentRow.MenuOrder.ToString();

                if (!objCurrentRow.IsWebHeightNull())
                {
                    this.WebHeightTextBox.Text = objCurrentRow.WebHeight.ToString();
                }
                else
                {
                    this.WebHeightTextBox.Text = string.Empty;
                }
                
                this.GroupIDTextBox.Text = objCurrentRow.GroupID.ToString();
                

                this.IsPermissionMenuComboBox.SelectedValue = objCurrentRow.IsPermissionMenu;
                if (objCurrentRow.IsPermissionMenu)
                {

                    this.IsPermissionMenuComboBox.SelectedIndex = 0;
                }
                else
                {
                    this.IsPermissionMenuComboBox.SelectedIndex = 1;
                }
                this.IsPermissionOpenComboBox.SelectedValue = objCurrentRow.IsPermissionOpen;
                if (objCurrentRow.IsPermissionOpen)
                {
                    this.IsPermissionOpenComboBox.SelectedIndex =0;

                }
                else
                {
                    this.IsPermissionOpenComboBox.SelectedIndex = 1;
                }
                this.IsPermissionAddComboBox.SelectedItem = objCurrentRow.IsPermissionAdd;
                if (objCurrentRow.IsPermissionAdd)
                {
                    this.IsPermissionAddComboBox.SelectedIndex =0;

                }
                else
                {
                    this.IsPermissionAddComboBox.SelectedIndex = 1;
                }
                this.IsPermissionEditComboBox.SelectedValue = objCurrentRow.IsPermissionEdit;
                if (objCurrentRow.IsPermissionEdit)
                {
                    this.IsPermissionEditComboBox.SelectedIndex = 0;

                }
                else
                {
                    this.IsPermissionEditComboBox.SelectedIndex = 1;
                }
                this.IsPermissionDeleteComboBox.SelectedValue = objCurrentRow.IsPermissionDelete;
                if (objCurrentRow.IsPermissionDelete)
                {
                    this.IsPermissionDeleteComboBox.SelectedIndex=0;
                }
                else
                {
                    this.IsPermissionDeleteComboBox.SelectedIndex= 1;
                }


                if (!string.IsNullOrEmpty(this.sampleForm.SampleFormApplicationIdTable.Rows[0][this.sampleForm.SampleFormApplicationIdTable.ApplicationColumn].ToString()))
                {
                    this.ApplicationIDComboBox.Text = this.sampleForm.SampleFormApplicationIdTable.Rows[0][this.sampleForm.SampleFormApplicationIdTable.ApplicationColumn].ToString();

                     int.TryParse(this.ApplicationIDComboBox.SelectedValue.ToString(), out this.tempAppId) ;
                }
                else
                {
                    this.ApplicationIDComboBox.SelectedValue = 0;
                }

                if (!string.IsNullOrEmpty(this.sampleForm.SampleFormMenuGroupTable.Rows[0][this.sampleForm.SampleFormMenuGroupTable.MenuGroupColumn].ToString()))
                {
                    this.MenuGroupIdComboBox.Text = this.sampleForm.SampleFormMenuGroupTable.Rows[0][this.sampleForm.SampleFormMenuGroupTable.MenuGroupColumn].ToString();
                    int.TryParse(this.MenuGroupIdComboBox.SelectedValue.ToString(), out this.tempMenuId);
                }
                else
                {
                    this.MenuGroupIdComboBox.SelectedValue = 0;
                }
                if (!this.PermissionFiled.editPermission || !this.formMasterPermissionEdit)
                {
                    this.ShowControls(false);


                }
                else
                {
                    this.ShowPanel(true);
                    this.ShowControls(true);

                }
            }
            else
            {
                F1500ClearSmapleForm();
                this.ShowPanel(false);
                this.ShowControls(false);

            }
            this.FormIDTextBox.Focus();
        }


        /// <summary>
        /// Handles the Load event of the F1500 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F1500_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.flagLoadOnProcess = true;
                this.LoadDefaultView();
                this.pageMode = TerraScanCommon.PageModeTypes.View;
                this.flagLoadOnProcess = false;
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
        /// F1500s the clear smaple form.
        /// </summary>
        private void F1500ClearSmapleForm()
        {
            this.FormIDTextBox.Text = string.Empty;
            this.FormFileTextBox.Text = string.Empty;
            this.MenuNameTextBox1.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.MenuGroupIdComboBox.SelectedIndex = -1;
            this.ApplicationIDComboBox.SelectedIndex = -1;
            this.MenuOrderTextBox.Text = string.Empty;
            this.ReportTextBox.Text = string.Empty;
            this.WebHeightTextBox.Text = string.Empty;
            this.GroupIDTextBox.Text = string.Empty;
            this.IsPermissionMenuComboBox.SelectedIndex = -1;
            this.IsPermissionOpenComboBox.SelectedIndex = -1;
            this.IsPermissionAddComboBox.SelectedIndex = -1;
            this.IsPermissionEditComboBox.SelectedIndex = -1;
            this.IsPermissionDeleteComboBox.SelectedIndex = -1;
            
        }
        private void SetEditRecord()
        {
            if (this.permissionFields.editPermission && this.formMasterPermissionEdit)
            {
                if (!this.flagLoadOnProcess)
                {
                    if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    }
                }
            }
        }


        /// <summary>
        /// Handles the Click event of the ParcelHeaderPictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ParcelHeaderPictureBox_Click(object sender, EventArgs e)
        {
            try
            {
                this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D20000.F25000"));
            }
            catch (Exception e1)
            {
                ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
       
       

        /// <summary>
        /// Handles the Validating event of the ApplicationIDComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ApplicationIDComboBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (!this.tempAppId.Equals(this.ApplicationIDComboBox.SelectedValue))
                {
                    
                     this.SetEditRecord();

                     if (this.ApplicationIDComboBox.SelectedValue == null)
                     {
                         this.ApplicationIDComboBox.SelectedValue = 0;
                         this.ApplicationIDComboBox.Text = "";
                         //this.tempAppId =string.Empty;
                     }
                     else
                     {
                         this.ApplicationIDComboBox.Text = this.ApplicationIDComboBox.SelectedText;
                       // this.tempAppId = (int)this.ApplicationIDComboBox.SelectedValue;
                     }
                }
            }


            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void MenuGroupIdComboBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (!this.tempMenuId.Equals(this.MenuGroupIdComboBox.SelectedValue))
                {

                    this.SetEditRecord();

                    if (this.MenuGroupIdComboBox.SelectedValue == null)
                    {
                        this.MenuGroupIdComboBox.SelectedValue = 0;
                        this.MenuGroupIdComboBox.Text = "";
                        
                    }
                    else
                    {
                        this.MenuGroupIdComboBox.Text = this.MenuGroupIdComboBox.SelectedText;
                        
                    }
                }
            }


            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void FormIDTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(FormIDTextBox.Text))
            {
                this.FormIDTextBox.Focus();

            }
            else
            {

            }
        }

        private void FormFileTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(FormFileTextBox.Text))
            {
                this.FormFileTextBox.Focus();
            }
        }

        //private void ApplicationIDComboBox_SelectionChangeCommitted_1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        this.SetEditRecord();
        //        this.tempAppId = (int)this.ApplicationIDComboBox.SelectedValue;
        //    }
        //    catch (Exception e1)
        //    {
        //        ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
        //    }
        //}

       
    }
}
