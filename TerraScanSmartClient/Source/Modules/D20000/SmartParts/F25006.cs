namespace D20000
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.SmartParts;
    using System.Collections;
    using System.IO;
    using TerraScan.Utilities;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Helper;
    using TerraScan.UI.Controls;
    using Microsoft.Practices.ObjectBuilder;


    [SmartPart]
    public partial class F25006 : BaseSmartPart
    {
        private int parcelId;
        private int masterFormNo;
        private string tabText;
        private int redColorCode;
        private int greenColorCode;
        private int blueColorCode;
        private bool formMasterPermissionEdit;
        private int nextParcelId;
        private int previousParcelId;
        private F25006Controller form25006Control;
        private F25006ParcelNavigation parcelDetailData = new F25006ParcelNavigation();
        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        public F25006()
        {
            InitializeComponent();
        }

         public F25006(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.parcelId = keyID;
            this.Tag = formNo;
            this.tabText = tabText;
            this.redColorCode = red;
            this.greenColorCode = green;
            this.blueColorCode = blue;
            this.ParcelNavigationPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.ParcelNavigationPictureBox.Height, this.ParcelNavigationPictureBox.Width, this.tabText, this.redColorCode, this.greenColorCode, this.blueColorCode);
        }

         #region Event Publication

         /// <summary>
         /// event publication for panel link label click
         /// </summary>
         [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
         public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

         /// <summary>
         /// event publication for getting the form status
         /// </summary>
         [EventPublication(EventTopics.D9001_ShellForm_GetFormStatus, PublicationScope.Global)]
         public event EventHandler<DataEventArgs<string>> GetFormStatus;

         /// <summary>
         /// Get Cancel Button
         /// </summary>
         [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, PublicationScope.WorkItem)]
         public event EventHandler<DataEventArgs<string>> GetCancelButton;

         /// <summary>
         /// Declare the event FormSlice_SectionIndicatorClick        
         /// </summary>
         [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
         public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

         /// <summary>
         /// Declare the event D9030_F9030_ReloadAfterSave
         /// </summary>
         [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
         public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

         /// <summary>
         /// Declare the event D84700_F84722_OnSave_SetKeyId
         /// </summary>
         [EventPublication(EventTopicNames.D84700_F84722_OnSave_SetKeyId, PublicationScope.Global)]
         public event EventHandler<DataEventArgs<SliceReloadActiveRecord>> FormSlice_OnSave_SetKeyId;

         /// <summary>
         /// Declare the event FormSlice_ValidationAlert        
         /// </summary>
         [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
         public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

         /// <summary>
         /// Declare the event FormSlice_EditEnabled        
         /// </summary>
         [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
         public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

         #endregion Event Publication
         #region Property
         /// <summary>
         /// For F25006Control
         /// </summary>
         [CreateNew]
         public F25006Controller Form25006Control
         {
             get { return this.form25006Control as F25006Controller; }
             set { this.form25006Control = value; }
         }
         #endregion Property

        
         /// <summary>
         /// OnD9030_F9030_LoadSliceDetails
         /// </summary>
         /// <param name="sender">sender</param>
         /// <param name="eventArgs">eventArgs</param>
         [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
         public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
         {
             // Checks the masterform no is same  
             if (this.masterFormNo == eventArgs.Data.MasterFormNo)
             {
                 // Coding Added for while navigating the record will not get display because keyid is passing as -99.
                 // if we add this code it will get a selected keyid.and it will display a record.
                 this.parcelId = eventArgs.Data.SelectedKeyId;
                 // Added coding ends here

                 // Checks its not in View Mode 
                 if (this.pageMode != TerraScanCommon.PageModeTypes.View)
                 {
                     this.parcelId = eventArgs.Data.SelectedKeyId;


                     this.FlagSliceForm = true;
                     this.LoadParcelDetails();
                     this.pageMode = TerraScanCommon.PageModeTypes.View;
                 }
                 else if (this.parcelId == eventArgs.Data.SelectedKeyId)  // IF the id is same from parcel owner then needs to refresh the form
                 {

                     this.LoadParcelDetails();
                     this.pageMode = TerraScanCommon.PageModeTypes.View;

                 }

             }

         }
       
        private void ParcelNavigationPictureBox_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>("D20000.F25006"));
            //}
            //catch (Exception e1)
            //{
            //    ExceptionManager.ManageException(e1, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            //}
        }

        private void ParcelNavigationPictureBox_MouseEnter(object sender, EventArgs e)
        {
            this.ParcelNavigationToolTip.SetToolTip(this.ParcelNavigationPictureBox, Utility.GetFormNameSpace(this.Name));
        }
        
        private void F25006_Load(object sender, EventArgs e)
        {
            this.pageMode = TerraScanCommon.PageModeTypes.View;
            this.FlagSliceForm = true;
            LoadParcelDetails();

        }
        public void LoadParcelDetails()
        {
            this.NextButtonPanel.BringToFront();
            this.PreviousButton.BackgroundImage = global::D20000.Properties.Resources.Arrow_Left;
            this.NextButton.BackgroundImage = global::D20000.Properties.Resources.Arrow_Right;
       
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            bool IsNext = true;
            if (this.parcelId > 0)
            {
               
                this.parcelDetailData = this.form25006Control.WorkItem.GetParcelDetails(this.parcelId,IsNext);//,this.IsNext
                parcelId = Convert.ToInt32(this.parcelDetailData.f25006_ParcelNavigation.Rows[0][this.parcelDetailData.f25006_ParcelNavigation.ParcelIDColumn].ToString());
                //nextParcelId = Convert.ToInt32(this.parcelDetailData.f25006_ParcelNavigation.Rows[0][this.parcelDetailData.f25006_ParcelNavigation.NextParcelIDColumn].ToString());
                //this.parcelId = this.nextParcelId;
                if (this.parcelDetailData != null)
                {
                   
                    if (this.parcelDetailData.Tables[0].Rows.Count == 0)
                    {
                        this.NextButton.BackgroundImage = global::D20000.Properties.Resources.Arrow_Right_Disabled;
                        this.PreviousButton.BackgroundImage = global::D20000.Properties.Resources.Arrow_Left_Disabled;
                    }
                    else
                    {
                        this.PreviousButton.BackgroundImage = global::D20000.Properties.Resources.Arrow_Left;
                        this.NextButton.BackgroundImage = global::D20000.Properties.Resources.Arrow_Right;
                        this.Cursor = Cursors.WaitCursor;
                        FormInfo formInfo;
                        formInfo = TerraScanCommon.GetFormInfo(30000);
                        formInfo.optionalParameters = new object[1];
                        //formInfo.optionalParameters[0] = this.masterFormNo;
                        formInfo.optionalParameters[0] = this.parcelId;
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                        
      
                    }
                   
                }
            }
        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            bool IsNext = false;
           
            if (this.parcelId > 0)
            {
                this.parcelDetailData = this.form25006Control.WorkItem.GetParcelDetails(this.parcelId,IsNext);//,this.IsNext
                parcelId = Convert.ToInt32(this.parcelDetailData.f25006_ParcelNavigation.Rows[0][this.parcelDetailData.f25006_ParcelNavigation.ParcelIDColumn].ToString());
                //this.parcelId = this.nextParcelId;
                if (this.parcelDetailData != null)
                {

                    if (this.parcelDetailData.Tables[0].Rows.Count == 0)
                    {
                        this.NextButton.BackgroundImage = global::D20000.Properties.Resources.Arrow_Right_Disabled;
                        this.PreviousButton.BackgroundImage = global::D20000.Properties.Resources.Arrow_Left_Disabled;
                    }
                    else
                    {
                        this.PreviousButton.BackgroundImage = global::D20000.Properties.Resources.Arrow_Left;
                        this.NextButton.BackgroundImage = global::D20000.Properties.Resources.Arrow_Right;
                        this.Cursor = Cursors.WaitCursor;
                        FormInfo formInfo;
                        formInfo = TerraScanCommon.GetFormInfo(30000);
                        formInfo.optionalParameters = new object[1];
                        //formInfo.optionalParameters[0] = this.masterFormNo;
                        formInfo.optionalParameters[0] = this.parcelId;
                        this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                    }
                   

                }
            }  
        }
    }
}
