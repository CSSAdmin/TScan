
//----------------------------------------------------------------------------------
// <copyright file="F29660.cs" company="Congruent">
// Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F29660 Form Slice - TIF Event
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		         Description
// ----------		---------		     -------------------------------------------
//
//
//***********************************************************************************



namespace D24660
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Web.Services.Protocols;   
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.SmartParts;
    using TerraScan.Utilities;
    using TerraScan.Infrastructure.Interface.Constants;    
 

    /// <summary>
    /// F29660-TIF EVENT FORM
    /// </summary>
   
    [SmartPart]
    public partial class F29660 : BaseSmartPart
    {
        #region Member Variables
        
        /// <summary>
        /// pageMode variable used to set the mode of page new/edit/view.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// form27081Controll variable.
        /// </summary>
        private F29660Controller form29660Controll;

        /// <summary>
        /// instrumentHeaderDataSetObject
        /// </summary>
        private F29660TIFEventData tifEventData = new F29660TIFEventData();

        private F29660TIFEventData.F29660TIFEventDataTableRow getTifEventData;  

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private bool formLoad;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private bool saveChanged;

        /// <summary>
        /// formMasterPermissionEdit variable used to store form master editpermission value.
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// slicePermissionField variable used to store the slice permissionFields.
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// masterFormNo variable used to store the masterForm Number.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// pageLoadStatus variable is used to store the flag value for PageLoad.
        /// </summary>
        private bool pageLoadStatus;

        /// <summary>
        /// flagLoadOnProcess
        /// </summary>
        private bool flagLoadOnProcess;
        
        /// <summary>
        /// keyId variable used to srored the keyId value.
        /// </summary>
        private int keyId;

        /// <summary>
        /// ExcessValue
        /// </summary>
        private decimal ExcessValue = 0.00M;

        /// <summary>
        /// TIFEventId variable is used to store the TIFEventId value default value is Null.
        /// </summary>
        private int? EventId = null;

        /// <summary>
        /// Used to store the BaseValue
        /// </summary>
        private decimal BaseValue;

        /// <summary>
        /// Used to store the CurrentAssessed
        /// </summary>
        private decimal CurrentAssessed;

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F29660"/> class.
        /// </summary>
        public F29660()
        {
            this.InitializeComponent();
        }
         /// <summary>
        /// Initializes a new instance of the <see cref="T:F15005"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F29660(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.keyId = keyID;
            this.pictureBox1.Image = ExtendedGraphics.GenerateVerticalImage(this.pictureBox1.Height, this.pictureBox1.Width, string.Empty, red, green, blue);
            this.formLoad = false;
        }

        #endregion

        #region Event Publication

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
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Declare the event D9030_F9030_ReloadAfterSave
        /// </summary>
        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;


        #endregion

        #region Properities

        /// <summary>
        /// Gets or sets the form29660 controll.
        /// </summary>
        /// <value>The form29660 controll.</value>
        [CreateNew]
        public F29660Controller Form29660Control
        {
            get { return this.form29660Controll as F29660Controller; }
            set { this.form29660Controll = value; }
        }


        ///// <summary>
        ///// Gets or sets the TIF ID.
        ///// </summary>
        ///// <value>The TIF ID.</value>
        //public int? TIFEventIdentity
        //{
        //    get
        //    {
        //        return this.TIFEventId;
        //    }

        //    set
        //    {
        //        this.TIFEventId = value;
        //    }
        //}


        #endregion

        #region Event Subscribtion
        
        /// <summary>
        /// Called when [D9030_ F9030_ set slice permission].
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
                    //if (this.subFundMgmtDataSet.SubFundDetails.Rows.Count > 0)
                    //{
                    eventArgs.Data.FlagInvalidSliceKey = false;
                    //}
                    //else
                    //{
                    //    //// Coding Added for the issue 4212 0n 30/5/2009.
                    //    //// Last Slice does not have a record also it will not return invalid slice
                    //    if (eventArgs.Data.FlagInvalidSliceKey)
                    //    {
                    //        eventArgs.Data.FlagInvalidSliceKey = true;
                    //    }
                    //}
                }

                if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                {
                    if (this.tifEventData.F29660TIFEventDataTable.Rows.Count > 0)
                    {
                    this.LockControls(false);
                    }
                    else
                    {
                        this.LockControls(true);
                    }
                }
                else
                {
                    this.LockControls(true);
                }
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
            {
               if (this.slicePermissionField.editPermission)
                {
                    SliceValidationFields sliceValidationFields = new SliceValidationFields();
                    sliceValidationFields.FormNo = eventArgs.Data;
                    this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
                }
            }
            else
            {
                SliceValidationFields sliceValidationFields = new SliceValidationFields();
                sliceValidationFields.FormNo = eventArgs.Data;
                this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
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
            if (this.pageMode == TerraScanCommon.PageModeTypes.Edit || this.pageMode == TerraScanCommon.PageModeTypes.New)
            {
                if (this.slicePermissionField.editPermission || this.slicePermissionField.newPermission)
                {
                    this.SaveTIFEvent();
                    this.saveChanged = false;
                     //this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
            else
            {
                this.LockControls(true);
                //// ToDo : FormLoad Events should happen (refresh)
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            this.ClearTIFEventText();
            this.LockControls(true);
               
        }

        /// <summary>
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            //if (this.formMasterPermissionEdit && this.PermissionFiled.editPermission)
            //{
            //    this.LockControls(true);
            //}
            //else
            //{
            //    this.LockControls(false); 
            //}
            this.LoadEventDetails();
            this.saveChanged = false;
            this.TIFDistrictComboBox.Select();
            this.TIFDistrictComboBox.Focus();   
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

        // <summary>
        /// Called when [D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.New;   

        }
        /// <summary>
        /// Called when [D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.masterFormNo == eventArgs.Data.MasterFormNo)
            {
                    this.pageLoadStatus = true;
                    this.formLoad = false;
                    this.keyId = eventArgs.Data.SelectedKeyId;
                    this.LoadEventDetails();
                    this.pageLoadStatus = false;
            }
          
        }



        #endregion

        #region FormLoad

        /// <summary>
        /// Handles the Load event of the F27081 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F29660_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.pageLoadStatus = true;
                this.LoadEventDetails();
                this.pageLoadStatus = false;
                
                //this.PopulateTIFDistrictFundDetails();
            }

            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="lockValue">if set to <c>true</c> [lock value].</param>
        private void LockControls(bool lockvalue)
        {
            this.TIFDistrictSubFundpanel.Enabled = !lockvalue;
            this.TIFCitypanel.Enabled = !lockvalue;
            this.CurrentAssessedpanel.Enabled = !lockvalue;
            this.TIFExcessValuepanel.Enabled = !lockvalue;
            this.BaseValuepanel.Enabled = !lockvalue;
            this.TIFDistrictpanel.Enabled = !lockvalue;   
        }
         /// <summary>
        /// Clears the TIF Text.
        /// </summary>
        private void ClearTIFEventText()
        {
            this.TIFDistrictComboBox.SelectedIndex = -1;
            this.TIFCityTextBox.Text = string.Empty;
            this.CurrentAssessedTextBox.Text = string.Empty;
            this.TIFSubFund.Text = string.Empty;
            this.BaseValueTextBox.Text = string.Empty;
            this.TIFExcessTextBox.Text = string.Empty;  
        }


         /// <summary>
        /// CalculateExcessValue
        /// </summary>
        private void CalculateExcessValue()
        {
            decimal currentAssessvalue = 0;
            decimal baseValue = 0;
            decimal.TryParse(this.CurrentAssessedTextBox.Text.Replace("$", "").Trim(), out currentAssessvalue);
            decimal.TryParse(this.BaseValueTextBox.Text.Replace("$", "").Trim(), out baseValue);
            this.ExcessValue = currentAssessvalue - baseValue;
            this.TIFExcessTextBox.Text = Convert.ToDecimal(this.ExcessValue.ToString()).ToString("#,##0");  

        }

               
        /// <summary>
        /// To save TIFDistrict
        /// </summary>
       private void SaveTIFEvent()
        {
            int returnValue=-1;
            int TIFID;
            decimal BaseValue;
           decimal.TryParse(this.BaseValueTextBox.Text.Replace("$","").Trim(), out BaseValue);
            string  district;
            district = this.TIFDistrictComboBox.SelectedValue.ToString();  
            DataRow[] tifID = this.tifEventData.DistrictComboboxDataTable.Select("TIFID=" + district);
            if (tifID.Length > 0)
            {
             TIFID = ((TerraScan.BusinessEntities.F29660TIFEventData.DistrictComboboxDataTableRow)tifID[0]).TIFID; 	
             returnValue = this.Form29660Control.WorkItem.F29660_SaveEventDetails(this.keyId, TIFID , BaseValue, TerraScanCommon.UserId);
            }
            if (returnValue != -1)
            {
                SliceReloadActiveRecord currentSliceInfo;
                currentSliceInfo.MasterFormNo = this.masterFormNo;
                currentSliceInfo.SelectedKeyId = this.keyId;
                this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
            }
           //tifid = this.TIFDistrictComboBox.SelectedValue.ToString();   
        }

        /// <summary>
        /// Validates the slice form.
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        private void ValidateSliceForm(DataEventArgs<int> eventArgs)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = eventArgs.Data;
            this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data)));
        }

       
          /// <summary>
        /// EditRecord
        /// </summary>
        private void EditRecord()
        {

            if (this.saveChanged && !this.flagLoadOnProcess && this.pageMode == TerraScanCommon.PageModeTypes.View && this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
            }
        }


          /// <summary>
        /// LoadEventDetails
        /// </summary>
        private void LoadEventDetails()
        {
            this.pageLoadStatus = true;
            this.tifEventData = this.form29660Controll.WorkItem.F29660_GetTIFEventDetails(this.keyId, TerraScanCommon.UserId);
            //this.tifEventData = this.form29660Controll.WorkItem.F29660_GetTIFEventDetails(this.keyId, TerraScanCommon.UserId);
            if (this.tifEventData.DistrictComboboxDataTable.Rows.Count > 0)
            {
                this.TIFDistrictComboBox.DataSource = this.tifEventData.DistrictComboboxDataTable;
                this.TIFDistrictComboBox.DisplayMember = this.tifEventData.DistrictComboboxDataTable.NameColumn.ColumnName;
                this.TIFDistrictComboBox.ValueMember = this.tifEventData.DistrictComboboxDataTable.TIFIDColumn.ColumnName;
                this.TIFDistrictComboBox.SelectedValue = -1;
            }
            //// load the form f29660 using keyid and userid          
            if (this.tifEventData.F29660TIFEventDataTable.Rows.Count > 0)
            {
                this.getTifEventData = (F29660TIFEventData.F29660TIFEventDataTableRow)this.tifEventData.F29660TIFEventDataTable.Rows[0];
                if (!this.getTifEventData.IsTIFIDNull())
                {
                    this.TIFDistrictComboBox.SelectedValue = this.tifEventData.F29660TIFEventDataTable.Rows[0][this.tifEventData.F29660TIFEventDataTable.TIFIDColumn.ColumnName].ToString();       
                }
                else
                {
                    this.TIFDistrictComboBox.SelectedValue = -1; 
                }
                if (!this.getTifEventData.IsTIFDistrictNull())
                {
                    //this.TIFDistrictComboBox.SelectedValue = this.tifEventData.F29660TIFEventDataTable.Rows[0][this.tifEventData.F29660TIFEventDataTable.TIFDistrictColumn.ColumnName].ToString();
                }
                else
                {
                    this.TIFDistrictComboBox.SelectedValue = -1;
                }
                if (!this.getTifEventData.IsSubFundNull())
                {
                    this.TIFSubFund.Text = this.tifEventData.F29660TIFEventDataTable.Rows[0][this.tifEventData.F29660TIFEventDataTable.SubFundColumn.ColumnName].ToString();
                }
                else
                {
                    this.TIFSubFund.Text = string.Empty; 
                }
                if (!this.getTifEventData.IsCityNull())
                {
                    this.TIFCityTextBox.Text = this.tifEventData.F29660TIFEventDataTable.Rows[0][this.tifEventData.F29660TIFEventDataTable.CityColumn.ColumnName].ToString();
                }
                else
                {
                    this.TIFCityTextBox.Text = string.Empty;  
                }
                        
                this.CurrentAssessedTextBox.Text = this.tifEventData.F29660TIFEventDataTable.Rows[0][this.tifEventData.F29660TIFEventDataTable.CurrentAssessedValueColumn.ColumnName].ToString();
                this.BaseValueTextBox.Text = this.tifEventData.F29660TIFEventDataTable.Rows[0][this.tifEventData.F29660TIFEventDataTable.BaseValueColumn.ColumnName].ToString();
                //this.CalculateExcessValue();  
                    //this.LockControls(true); 
                this.pageMode = TerraScanCommon.PageModeTypes.View;
            }
            else
            {
                this.ClearTIFEventText();
                this.LockControls(true);
            }
            this.formLoad = true; 
            //this.pageMode = TerraScanCommon.PageModeTypes.View;
            //this.pageLoadStatus = false;

        }


         /// <summary>
        /// Checks the errors.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <returns>returns formNo</returns>
        private SliceValidationFields CheckErrors(int formNo)
        {
            SliceValidationFields sliceValidationFields = new SliceValidationFields();
            sliceValidationFields.FormNo = formNo;
            if (this.TIFDistrictComboBox.SelectedValue.Equals(0))
            {
                DialogResult TIFID = MessageBox.Show("You must select a TIF District to save this record.", "TerraScan – Select TIF District", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (TIFID.Equals(DialogResult.OK))
                {
                    sliceValidationFields.RequiredFieldMissing = false;
                    sliceValidationFields.DisableNewMethod = true;
                    this.TIFDistrictComboBox.Focus();
                    return sliceValidationFields;
                }
                return sliceValidationFields;
            }
            decimal.TryParse(this.BaseValueTextBox.Text.Replace("$","").Trim(), out this.BaseValue);
            decimal.TryParse(this.CurrentAssessedTextBox.Text.Replace("$", "").Trim(), out this.CurrentAssessed);
            if(this.BaseValue>this.CurrentAssessed)
            {
               DialogResult result = MessageBox.Show("You are about to save a TIF Event that has a greater Base Value than Assessed Value. \r\nAre you sure you want to Save this record?", "TerraScan – Base Value Over Assessed Value", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
              if (result.Equals(DialogResult.No))
              {
                     sliceValidationFields.RequiredFieldMissing = false;
                     sliceValidationFields.DisableNewMethod = true;
                     this.BaseValueTextBox.Focus();  
                     return sliceValidationFields;

               }
               else
                {
                   return sliceValidationFields;

                }

            }
            
            return sliceValidationFields; 
        }

        #endregion


        private void TIFDistrictComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void TIFDistrictComboBox_TextChanged(object sender, EventArgs e)
        {
            this.TIFSubFund.Text = string.Empty;
            this.TIFCityTextBox.Text = string.Empty;
            this.saveChanged = true; 
            this.EditRecord(); 
        }

        private void TIFDistrictComboBox_Leave(object sender, EventArgs e)
        {

        }

        private void TIFDistrictComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.flagLoadOnProcess)
                {
                    this.EditRecord();
                    if (this.TIFDistrictComboBox.SelectedValue != null)
                    {
                        string subfund;
                        string City;
                        int tifId;
                        int.TryParse(this.TIFDistrictComboBox.SelectedValue.ToString(), out tifId);
                        DataRow[] districtDetails = this.tifEventData.DistrictComboboxDataTable.Select("TifID=" + tifId);
                        if (districtDetails.Length > 0)
                        {
                            subfund = ((TerraScan.BusinessEntities.F29660TIFEventData.DistrictComboboxDataTableRow)(districtDetails[0])).SubFund;
                            City = ((TerraScan.BusinessEntities.F29660TIFEventData.DistrictComboboxDataTableRow)(districtDetails[0])).City;
                            this.TIFSubFund.Text = subfund;
                            this.TIFCityTextBox.Text = City;
                        }
                        else
                        {
                            this.TIFSubFund.Text = string.Empty;
                            this.TIFCityTextBox.Text = string.Empty;  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void BaseValueTextBox_TextChanged(object sender, EventArgs e)
        {
           decimal assessValue;
           decimal baseValue;
           decimal excessValue;
           decimal.TryParse(this.CurrentAssessedTextBox.Text.Replace('$',' ').Trim(), out  assessValue);
           decimal.TryParse(this.BaseValueTextBox.Text.Replace('$',' ').Trim () , out baseValue);
           excessValue  = assessValue - baseValue;
           this.TIFExcessTextBox.Text = Convert.ToDecimal(excessValue.ToString()).ToString("#,##0");
           this.saveChanged = true;
           this.EditRecord();  
        }

        private void TIFSubFund_TextChanged(object sender, EventArgs e)
        {
            
        }

    }
}
