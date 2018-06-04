
namespace D24530
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
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Utilities;
    using Infrastructure.Interface;

    /// <summary>
    /// F29531
    /// </summary>
    [SmartPart]
    public partial class F29531 : BaseSmartPart
    {
       #region  Variables

        /// <summary>
        /// keyId
        /// </summary>
        private int keyId;

        /// <summary>
        /// Used to store the asscoationId(
        /// </summary>
        private int asscoationId;

        /// <summary>
        /// controller F29531
        /// </summary>
        private F29531Controller form29531Control;

         /// <summary>
        /// masterFormNo Local variable.
        /// </summary>
        private int masterFormNo;

        /// <summary>
        /// Used to store the statementId(keyid)
        /// </summary>
        private int formId;

        /// <summary>
        /// slicePermissionfield which neglects the basesmartpart permission field
        /// </summary>
        private PermissionFields slicePermissionField;

        /// <summary>
        /// enum variable used to find PageMode
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// Form Roll year.
        /// </summary>
        public static int RollYear = 0;

       /// F29531AssciationLinkData
        /// </summary>
        private F29531AssciationLinkData linkData = new F29531AssciationLinkData();

        /// F29531AssciationLinkData
        /// </summary>
        private F29531AssciationLinkData linktypeData = new F29531AssciationLinkData();


        /// <summary>
        /// associationEventCount
        /// </summary>
        private int associationEventCount;


        /// <summary>
        /// Instance For Dataset
        /// </summary>
        private F29531AssciationLinkData.AssociationDataTableDataTable linkDataCollection = new F29531AssciationLinkData.AssociationDataTableDataTable();

        private F29531AssciationLinkData.UpdateAssociationDataTableDataTable dt = new F29531AssciationLinkData.UpdateAssociationDataTableDataTable();


        /// <summary>
        /// formMasterPermissionEdit
        /// </summary>
        private bool formMasterPermissionEdit;

        /// <summary>
        /// linkselectionform
        /// </summary>
        private int linkselectionform = 0;

        /// <summary>
        /// selectionform
        /// </summary>
        private int selectionform = 0;

        /// <summary>
        /// destinationform
        /// </summary>
        private int destinationform = 0;

        /// <summary>
        /// linktextSQL
        /// </summary>
        private string linktextSQL = string.Empty;

        private int activekeyid;

        private bool flag = false;
        //public D9030.F9030 auditlinktext;
        private bool formidflag = false;

        private bool linkclickflag = false;
        
        private int formno;

        private int selectionformactiveid;

        /// <summary>
        /// To Store AssociationId
        /// </summary>
        private int gridassociationid;
        /// <summary>
        /// To store the section indicator text
        /// </summary>
        private string tabText;
        /// <summary>
        /// To store the red color
        /// </summary>
        private int redColor;
        /// <summary>
        /// To store the green color
        /// </summary>
        private int greenColor;
        /// <summary>
        /// To store the blue color
        /// </summary>
        private int blueColor;
        /// <summary>
        /// To identify the form load
        /// </summary>
        private bool isFormLoad;

        /// <summary>
        /// Roll Year Static Variable.
        /// </summary>
        public static string rollYearval;
        
        private DataGridViewTextBoxEditingControl editingControl;
      
      #endregion

       #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="T:F29531"/> class.
        /// </summary>
        public F29531()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F29531"/> class.
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
         public F29531(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.InitializeComponent();
            this.masterFormNo = masterform;
            this.Tag = formNo;
            this.formId = keyID;
            this.formMasterPermissionEdit = permissionEdit;
            this.tabText = tabText;
            this.redColor = red;
            this.greenColor = green;
            this.blueColor = blue;
            this.AssociationLinkpictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AssociationLinkpictureBox.Height, this.AssociationLinkpictureBox.Width, tabText, red , green , blue );
         }

        #endregion

       #region Event Publication
         /// <summary>
         /// Declare the event FormSlice_SectionIndicatorClick        
         /// </summary>
         [EventPublication(EventTopicNames.FormSlice_SectionIndicatorClick, PublicationScope.Global)]
         public event EventHandler<EventArgs> FormSlice_SectionIndicatorClick;

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
         /// <summary>
         /// Declare the event FormSlice_Resize        
         /// </summary>
         [EventPublication(EventTopicNames.FormSlice_Resize, PublicationScope.Global)]
         public event EventHandler<DataEventArgs<SliceResize>> FormSlice_Resize;

         /// <summary>
         /// Declare the event FormSlice_EditEnabled        
         /// </summary>
         [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
         public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

      #endregion

       #region Property

         /// <summary>
         /// Gets or sets the form36032 control.
         /// </summary>
         /// <value>The form36032 control.</value>
         [CreateNew]
         public F29531Controller Form29531Control
         {
             get { return this.form29531Control as F29531Controller; }
             set { this.form29531Control = value; }
         }

         #endregion Property

       #region Event Subscription
        
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
                 }

                 if (this.AssociationLinksGridView.OriginalRowCount > 0)
                 {
                     //this.AssociationLinksGridView.Rows[0].Selected = true;
                     TerraScanCommon.SetDataGridViewPosition(this.AssociationLinksGridView, 0);
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

        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_FooterSmartPart_GetActiveKeyid, Thread = ThreadOption.UserInterface)]
        public void SetActiveKeyId(object sender, DataEventArgs<int[]> e)
        {
            // Master form number has been chanecked to fix #11690(TSBG 30000 Real Property Summary - Form breaks after adding Association Link)
            if (!linkclickflag && this.masterFormNo == e.Data[1])
            {
                selectionformactiveid = e.Data[0];
                this.activekeyid = e.Data[0];
                if (!formidflag)
                {
                    this.formId = e.Data[1];
                }
                formidflag = true;
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
             try
             {
                 if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                 {
                     ////Commented by Biju on 26-Nov-2010 to fix #9549
                     ////this.selectionform = this.formId;
                     ////Added by Biju on 26-Nov-2010 to fix #9549
                     this.selectionform = this.masterFormNo ;
                     ////Code added to set keyid - Latha
                     this.selectionformactiveid = eventArgs.Data.SelectedKeyId;
                     this.isFormLoad = true;
                     this.LoadAssociationEventGrid();
                     this.pageMode = TerraScanCommon.PageModeTypes.View;
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


         [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
         public void OnD9030_F9030_CancelSliceInformation(object sender, EventArgs eventArgs)
         {
             try
             {
                 this.pageMode = TerraScanCommon.PageModeTypes.View;
                 this.LoadAssociationEventGrid();

             }
             catch (Exception ex)
             {
                 ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
             }
         }


         [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
         public void OnD9030_F9030_SaveConfirmed(object sender, DataEventArgs<int> eventArgs)
         {
             if (this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission)
             {
                 this.SaveButtonClick();
             }
         }
         /// <summary>
         /// Forms the close.
         /// </summary>
         /// <param name="sender">The sender.</param>
         /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_BaseSmartPart_formClose, Thread = ThreadOption.UserInterface)]
        public void FormClose(object sender, DataEventArgs<string> e)
        {
            if (e.Data == "ApplicationExitCall")
            {
                TerraScanCommon.FormName = string.Empty;
            }
            else if (e.Data == "UserClosing")
            {
                //this.LinkToTextBox.Text = activekeyid.ToString();
            }
            formidflag = false;
        }
         #endregion

       #region PictureBox Events
         /// <summary>
         /// Handles the Click event of the AssociationLinkpictureBox control.
         /// </summary>
         /// <param name="sender">The source of the event.</param>
         /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AssociationLinkpictureBox_Click(object sender, EventArgs e)
        {
            this.FormSlice_SectionIndicatorClick(this, new DataEventArgs<string>(Utility.GetFormNameSpace(this.Name)));            
        }

        /// <summary>
        /// Handles the MouseHover event of the AssociationLinkpictureBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AssociationLinkpictureBox_MouseHover(object sender, EventArgs e)
        {
            this.AssociationLinkToolTip.SetToolTip(this.AssociationLinkpictureBox, Utility.GetFormNameSpace(this.Name));
        }
     #endregion

       #region Methods
        private void GotoColumnLinkForeColor(DataGridViewCellFormattingEventArgs e)
        {

            if (e.ColumnIndex == this.AssociationLinksGridView.Columns[this.linkData.AssociationDataTable.LinkTextColumn.ColumnName].Index)
            {
                if (this.AssociationLinksGridView.Rows[e.RowIndex].Selected || this.AssociationLinksGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
                {
                    (this.AssociationLinksGridView.Rows[e.RowIndex].Cells[this.linkData.AssociationDataTable.LinkTextColumn.ColumnName] as DataGridViewLinkCell).LinkColor = Color.White;
                    (this.AssociationLinksGridView.Rows[e.RowIndex].Cells[this.linkData.AssociationDataTable.LinkTextColumn.ColumnName] as DataGridViewLinkCell).ActiveLinkColor = Color.White;
                    (this.AssociationLinksGridView.Rows[e.RowIndex].Cells[this.linkData.AssociationDataTable.LinkTextColumn.ColumnName] as DataGridViewLinkCell).VisitedLinkColor = Color.White;
                }
                else
                {
                    (this.AssociationLinksGridView.Rows[e.RowIndex].Cells[this.linkData.AssociationDataTable.LinkTextColumn.ColumnName] as DataGridViewLinkCell).LinkColor = Color.Blue;
                    (this.AssociationLinksGridView.Rows[e.RowIndex].Cells[this.linkData.AssociationDataTable.LinkTextColumn.ColumnName] as DataGridViewLinkCell).ActiveLinkColor = Color.Red;
                    (this.AssociationLinksGridView.Rows[e.RowIndex].Cells[this.linkData.AssociationDataTable.LinkTextColumn.ColumnName] as DataGridViewLinkCell).VisitedLinkColor = Color.Blue;
                }
            }
        }

             /// <summary>
        /// LoadAssociationEventGrid()
        /// </summary>
        private void LoadAssociationEventGrid()
        {
            this.linkData.AssociationDataTable.Clear();
            this.linkData = this.form29531Control.WorkItem.F29531_FillAssociationLinkGrid(this.selectionformactiveid,this.selectionform);            
            this.associationEventCount = this.linkData.AssociationDataTable.Rows.Count;
            if (this.associationEventCount > 0)
            {
                this.LinkText.DataPropertyName = this.linkData.AssociationDataTable.LinkTextColumn.ColumnName;
                this.Description.DataPropertyName = this.linkData.AssociationDataTable.DescriptionColumn.ColumnName;
                this.Form.DataPropertyName = this.linkData.AssociationDataTable.DestinationFormColumn.ColumnName;
                this.Param1.DataPropertyName = this.linkData.AssociationDataTable.Param1Column.ColumnName;
                this.Param2.DataPropertyName = this.linkData.AssociationDataTable.Param2Column.ColumnName;
                this.Param3.DataPropertyName = this.linkData.AssociationDataTable.Param3Column.ColumnName;
                this.AssociationID.DataPropertyName = this.linkData.AssociationDataTable.AssociationIDColumn.ColumnName;
                this.AssociationLinksGridView.AutoGenerateColumns = false;
                if (this.associationEventCount == 1)
                {
                    this.AssociationLinksGridView.NumRowsVisible = 1;
                    this.AssociationGridPanel.Height = 90 - 44;
                    this.AssociationLinkGridVscrollBar.Height = 90 - 44;
                    this.AssociationLinksGridView.Height = 90 - 44;
                    if (this.LinkTypeComboBox.SelectedIndex > 0)
                    {
                        this.AssociationLinkpictureBox.Height = 87;
                    }
                    else
                    {
                        this.AssociationLinkpictureBox.Height = 87;
                    }
                    this.AssociationLinkpictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AssociationLinkpictureBox.Height+20, this.AssociationLinkpictureBox.Width, this.tabText, this.redColor, this.greenColor, this.blueColor);
                }
                else if (this.associationEventCount.Equals(2))
                {
                    this.AssociationLinksGridView.NumRowsVisible = 3;
                    this.AssociationLinkpictureBox.Height = 87 + 41;
                    this.AssociationGridPanel.Height = this.AssociationLinkpictureBox.Height - 41;
                    this.AssociationLinkGridVscrollBar.Height = this.AssociationLinkpictureBox.Height - 38;
                    this.AssociationLinksGridView.Height = this.AssociationLinkpictureBox.Height - 38;
                   //  this.AssociationLinkpictureBox.Height = 130;
                    this.Height = this.AssociationLinkpictureBox.Height + 23;
                        SliceResize sliceResize;
                        sliceResize.MasterFormNo = this.masterFormNo;
                        ////sliceResize.SliceFormName = "D24500.F29510";
                        sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                        sliceResize.SliceFormHeight = this.Height;
                        this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                        this.AssociationLinkpictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AssociationLinkpictureBox.Height, this.AssociationLinkpictureBox.Width, this.tabText, this.redColor, this.greenColor, this.blueColor);
                   
                 }
                    ////added by Biju on 27-Oct-2010 to implement #8835
                else if (this.associationEventCount <= 12)
                {
                    this.AssociationLinksGridView.NumRowsVisible = this.associationEventCount ;
                    this.AssociationLinkpictureBox.Height = Convert.ToInt32(87.0 + ((this.associationEventCount-1)*22));
                    this.AssociationGridPanel.Height = this.AssociationLinkpictureBox.Height -41;
                    this.AssociationLinkGridVscrollBar.Height = this.AssociationLinkpictureBox.Height - 40;
                    this.AssociationLinksGridView.Height = this.AssociationLinkpictureBox.Height - 40;
                    this.Height = this.AssociationLinkpictureBox.Height+23;

                    SliceResize sliceResize;
                    sliceResize.MasterFormNo = this.masterFormNo ;
                    ////sliceResize.SliceFormName = "D24500.F29510";
                    sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                    sliceResize.SliceFormHeight = this.Height;
                    this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                    this.AssociationLinkpictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AssociationLinkpictureBox.Height, this.AssociationLinkpictureBox.Width, this.tabText, this.redColor, this.greenColor, this.blueColor);
                    
                }
                else if (this.associationEventCount > 12)
                {
                    this.AssociationLinksGridView.NumRowsVisible = 12;
                    this.AssociationLinkpictureBox.Height = Convert.ToInt32(87.0 + (11 * 22));
                    this.AssociationGridPanel.Height = this.AssociationLinkpictureBox.Height - 41;
                    this.AssociationLinkGridVscrollBar.Height = this.AssociationLinkpictureBox.Height - 40;
                    this.AssociationLinksGridView.Height = this.AssociationLinkpictureBox.Height - 40;
                    this.Height = this.AssociationLinkpictureBox.Height + 23;
                    if (this.isFormLoad)
                    {
                        SliceResize sliceResize;
                        sliceResize.MasterFormNo = this.masterFormNo;
                        ////sliceResize.SliceFormName = "D24500.F29510";
                        sliceResize.SliceFormName = Utility.GetFormNameSpace(this.Name);
                        sliceResize.SliceFormHeight = this.Height;
                        this.OnFormSlice_Resize(new DataEventArgs<SliceResize>(sliceResize));
                        this.AssociationLinkpictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.AssociationLinkpictureBox.Height, this.AssociationLinkpictureBox.Width, this.tabText, this.redColor, this.greenColor, this.blueColor);
                        this.isFormLoad = false;
                    }
                }////till here
                this.AssociationLinksGridView.DataSource = this.linkData.AssociationDataTable.DefaultView;
                this.AssociationLinksGridView.Focus();
                //this.AssociationLinksGridView.Rows[0].Selected = true;
                TerraScanCommon.SetDataGridViewPosition(this.AssociationLinksGridView, 0);

                if (this.linkData.AssociationDataTable.Rows.Count > this.AssociationLinksGridView.NumRowsVisible)
                {
                    this.AssociationLinkGridVscrollBar.Visible = false;
                }
                else
                {
                    this.AssociationLinkGridVscrollBar.Enabled = false;
                    this.AssociationLinkGridVscrollBar.Visible = true;
                }
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

        #endregion

       #region Grid Events

        /// <summary>
        /// Handles the CellFormatting event of the AssociationLinksGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> instance containing the event data.</param>
        private void AssociationLinksGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            this.GotoColumnLinkForeColor(e);
        }

        /// <summary>
        /// Handles the CellContentClick event of the AssociationLinksGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AssociationLinksGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex >=0)
                {
                    if (this.AssociationLinksGridView.Rows.Count > 0)
                    {
                        this.linkclickflag = true;
                        string tempvalue = this.AssociationLinksGridView.Rows[e.RowIndex].Cells["Form"].Value.ToString();
                        FormInfo getForm = TerraScan.Common.TerraScanCommon.GetFormInfo(Convert.ToInt32(this.AssociationLinksGridView.Rows[e.RowIndex].Cells["Form"].Value.ToString()));
                        getForm.optionalParameters = new object[2];
                        int opp1;
                        int opp2;
                        int opp3;

                        if (!string.IsNullOrEmpty(this.AssociationLinksGridView.Rows[e.RowIndex].Cells[this.linkDataCollection.Param1Column.ColumnName].Value.ToString()))
                        {
                            int.TryParse(this.AssociationLinksGridView.Rows[e.RowIndex].Cells[this.linkDataCollection.Param1Column.ColumnName].Value.ToString(), out opp1);
                            getForm.optionalParameters[0] = opp1;
                        }
                        else
                        {
                            getForm.optionalParameters[0] = null;
                        }

                        if (!string.IsNullOrEmpty(this.AssociationLinksGridView.Rows[e.RowIndex].Cells[this.linkDataCollection.Param2Column.ColumnName].Value.ToString()))
                        {
                            int.TryParse(this.AssociationLinksGridView.Rows[e.RowIndex].Cells[this.linkDataCollection.Param2Column.ColumnName].Value.ToString(), out opp2);
                            getForm.optionalParameters[1] = opp2;
                        }
                        else
                        {
                            getForm.optionalParameters[1] = null;
                        }

                        if (!string.IsNullOrEmpty(this.AssociationLinksGridView.Rows[e.RowIndex].Cells[this.linkDataCollection.Param3Column.ColumnName].Value.ToString()))
                        {
                            int.TryParse(this.AssociationLinksGridView.Rows[e.RowIndex].Cells[this.linkDataCollection.Param3Column.ColumnName].Value.ToString(), out opp3);
                            ////getForm.optionalParameters[2] = opp3;
                        }
                        else
                        {
                            //// getForm.optionalParameters[2] = null;
                        }

                        this.ShowForm(this, new DataEventArgs<FormInfo>(getForm));
                    }
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
        /// Handles the KeyDown event of the AssociationLinksGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void AssociationLinksGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 46)
            {
                if (gridassociationid > 0)
                {
                    this.form29531Control.WorkItem.F29531_DeleteAssociationLink(gridassociationid, TerraScanCommon.UserId);
                    this.LoadAssociationEventGrid();
                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                }
            }
        }

        /// <summary>
        /// Handles the RowEnter event of the AssociationLinksGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void AssociationLinksGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.AssociationLinksGridView[this.AssociationID.Name, e.RowIndex].Value.ToString().Trim()))
                {
                    int.TryParse(this.AssociationLinksGridView[this.AssociationID.Name, e.RowIndex].Value.ToString().Trim(), out this.gridassociationid);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }


        #endregion

       #region LinkType Combo and Button
        
        /// <summary>
        /// Handles the SelectionChangeCommitted event of the LinkTypeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LinkTypeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.linkclickflag = false;
            this.LinkToTextBox.Text = string.Empty;
            this.DescriptionTextBox.Text = string.Empty;
            this.DescriptionTextBox.Enabled = false;
            this.CreateButton.Enabled = false;
            this.LinkToTextBox.Enabled = false;
              
            if (this.LinkTypeComboBox.SelectedIndex == 0)
            {
                this.LinkToTextBox.Text = string.Empty;   
                this.LinkToButton.Enabled = false;
                this.CreateButton.Enabled = false;
                this.DescriptionTextBox.Text = string.Empty;
                this.DescriptionTextBox.Enabled = false;
                //selectionform = 0;
                //this.LoadAssociationEventGrid();
             }
           else
            {
                this.LinkToButton.Enabled = true;
                this.LinkToTextBox.Enabled = true;
                this.LinkToTextBox.LockKeyPress = true; 
                this.LinkToTextBox.ForeColor = Color.Black;
               // this.DescriptionTextBox.Enabled = true;
                //this.CreateButton.Enabled = true;
                DataTable linktypeDetails = new DataTable("Table");
                linktypeDetails.Columns.AddRange(new DataColumn[] { new DataColumn("AssociationCfgID"), new DataColumn("AssociationType"), new DataColumn("SelectionForm"), new DataColumn("DestinationForm"), new DataColumn("LinkTextSQL") });
                linkselectionform = 0;
                destinationform = 0;
                linktextSQL = string.Empty;
                for (int i = 0; i <= this.linktypeData.AssociationLinkTypeDataTable.Rows.Count - 1; i++)
                {
                    if (this.linktypeData.AssociationLinkTypeDataTable.Rows[i].ItemArray[0].ToString() == this.LinkTypeComboBox.SelectedValue.ToString())
                    {
                        asscoationId = Convert.ToInt32(this.linktypeData.AssociationLinkTypeDataTable.Rows[i].ItemArray[0].ToString());
                        linkselectionform = Convert.ToInt32(this.linktypeData.AssociationLinkTypeDataTable.Rows[i].ItemArray[2].ToString());
                        destinationform = Convert.ToInt32(this.linktypeData.AssociationLinkTypeDataTable.Rows[i].ItemArray[3].ToString());
                        linktextSQL = this.linktypeData.AssociationLinkTypeDataTable.Rows[i].ItemArray[4].ToString();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the LinkToButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LinkToButton_Click(object sender, EventArgs e)
        {
            Form formInfo = new Form();
            object[] optionalParameter = new object[] { RollYear,this.masterFormNo.ToString() };
            formInfo = this.form29531Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(linkselectionform, optionalParameter, this.form29531Control.WorkItem);
            if (formInfo != null)
            {
                if (formInfo.ShowDialog() == DialogResult.OK)
                {
                    int parcelid;
                    parcelid = Convert.ToInt32(TerraScanCommon.GetValue(formInfo, "CommandResult").ToString());
                    this.activekeyid = parcelid;
                    string associationlink = F29531WorkItem.F29531_GetLinkText(asscoationId, this.activekeyid);
                    this.LinkToTextBox.Text = associationlink;
                   // this.LoadAssociationEventGrid();
                }
            }
         }

        /// <summary>
        /// Handles the TextChanged event of the LinkToTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void LinkToTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.LinkTypeComboBox.SelectedIndex > 0)
            {
               
                if ((this.LinkTypeComboBox.SelectedIndex == 0) && (string.IsNullOrEmpty(this.LinkToTextBox.Text)))
                {
                    this.CreateButton.Enabled = false;
                    this.DescriptionTextBox.Enabled = false;
                }
                else
                {
                    this.CreateButton.Enabled = true;
                    this.DescriptionTextBox.Enabled = true;
                }
                //if (this.LinkToTextBox.Text != "")
                //{
                //    for (int i = 0; i <= this.AssociationLinksGridView.OriginalRowCount - 1; i++)
                //    {
                //        this.linkData.AssociationDataTable.Rows[i][this.linkData.AssociationDataTable.LinkTextColumn.ColumnName] = this.LinkTypeComboBox.Text + ": " + this.LinkToTextBox.Text;
                //    }
                //    this.linkData.AssociationDataTable.AcceptChanges();
                //}
            }
        }

        #endregion

       #region Form Load
        /// <summary>
        /// Handles the Load event of the F29531 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F29531_Load(object sender, EventArgs e)
        {
            try
            {
                this.FlagSliceForm = true;
                this.isFormLoad = true;
                ////For Combox Box - Association-Link Type
                this.linktypeData = this.form29531Control.WorkItem.F29531AssociationLinkType(TerraScanCommon.UserId);
                this.LinkTypeComboBox.DataSource = this.linktypeData.AssociationLinkTypeDataTable;
                this.LinkTypeComboBox.DisplayMember = this.linktypeData.AssociationLinkTypeDataTable.AssociationTypeColumn.ColumnName;
                this.LinkTypeComboBox.ValueMember = this.linktypeData.AssociationLinkTypeDataTable.AssociationCfgIDColumn.ColumnName;
                this.LinkTypeComboBox.SelectedIndex = 0;
                this.LinkTypeComboBox.Focus(); 
                //this.LinkTypeComboBox.Text = "<<Select>>"; 
                this.selectionform = this.masterFormNo;
                selectionformactiveid = this.formId; 
                this.LoadAssociationEventGrid();
                this.LinkToButton.Enabled = false;
                RollYear = Convert.ToInt32(F29531.rollYearval);
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
        #endregion

       #region Create Button Click

        /// <summary>
        /// Handles the Click event of the CreateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CreateButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            string associationLinkItems = string.Empty;
            //for (int i = 0; i <= this.AssociationLinksGridView.OriginalRowCount - 1; i++)
            //{
            //    this.linkData.AssociationDataTable.Rows[0][this.linkData.AssociationDataTable.DescriptionColumn.ColumnName]  = this.DescriptionTextBox.Text;
            //    this.linkData.AssociationDataTable.Rows[i][this.linkData.AssociationDataTable.FormColumn.ColumnName] = this.formId;  
            //}
            //this.linkData.AssociationDataTable.AcceptChanges(); 
            DataTable dt = new DataTable();
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("Associationcfgid");
                dt.Columns.Add("keyId");
                dt.Columns.Add("LinkText");  
                dt.Columns.Add("Description");
                dt.Columns.Add("CurrentForm");
                dt.Columns.Add("Param1");
                dt.Columns.Add("Param2");
                dt.Columns.Add("Param3"); 
             }
             DataRow dr;
             dr = dt.NewRow();
             dr["Associationcfgid"] = this.LinkTypeComboBox.SelectedValue;
            // dr["keyId"] = this.activekeyid;
             dr["keyId"] = selectionformactiveid;
             dr["LinkText"] = this.LinkTypeComboBox.Text + ": " + this.LinkToTextBox.Text;
             dr["Description"] = this.DescriptionTextBox.Text;
            ////Modified by Biju on 26-Nov-2010 to fix #9549
             dr["CurrentForm"] = this.masterFormNo;//// this.formId;
             dr["Param1"] = this.activekeyid;
             //dr["Param2"] = this.activekeyid;
             //dr["Param3"] = this.activekeyid;
             dt.Rows.Add(dr);
             associationLinkItems = TerraScanCommon.GetXmlString(dt);
            int returnValue = this.form29531Control.WorkItem.F29531_SaveAssociationLink(asscoationId, associationLinkItems, TerraScanCommon.UserId);
            this.LinkTypeComboBox.SelectedIndex = 0;
            this.flag = true;
            this.LinkToTextBox.Text = string.Empty;  
            this.DescriptionTextBox.Text = string.Empty;
            this.CreateButton.Enabled = false;
            this.LinkToButton.Enabled = false;
            this.DescriptionTextBox.Enabled = false; 
            SliceReloadActiveRecord sliceReloadActiveRecord;
            sliceReloadActiveRecord.MasterFormNo = this.masterFormNo;
            sliceReloadActiveRecord.SelectedKeyId = this.selectionformactiveid;
            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
            this.OnD9030_F9030_LoadSliceDetails(this,new DataEventArgs<SliceReloadActiveRecord>(sliceReloadActiveRecord));
            this.Cursor = Cursors.Default;
        }

        #endregion
        #region


        public void SaveButtonClick()
        {
            F29531AssciationLinkData UpdateAssociationData = new F29531AssciationLinkData();
            F29531AssciationLinkData.UpdateAssociationDataTableRow dr = UpdateAssociationData.UpdateAssociationDataTable.NewUpdateAssociationDataTableRow();
            DataTable tempTable = dt.Clone();
            foreach (DataRow dtRow in this.linkData.AssociationDataTable)
            {
                tempTable.ImportRow(dtRow);
            }
            string xmlValue = TerraScan.Utilities.Utility.GetXmlString(tempTable);
            xmlValue = xmlValue.Replace("Root", "root");
            xmlValue= xmlValue.Replace("Table","Association");
            this.form29531Control.WorkItem.UpdateAssociationLinkDetails(xmlValue, TerraScanCommon.UserId);
            this.LoadAssociationEventGrid();
            this.pageMode = TerraScanCommon.PageModeTypes.View;
        }
        /// <summary>
        /// Called when [form slice_ resize].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_Resize(DataEventArgs<SliceResize> eventArgs)
        {
            if (this.FormSlice_Resize != null)
            {
                this.FormSlice_Resize(this, eventArgs);
            }
        }


        private void SetEditRecord()
        {
            //if (this.permissionFields.editPermission && this.formMasterPermissionEdit)
            //{
            //    //if (!this.flagLoadOnProcess)
            //    //{
                    if (!string.Equals(this.pageMode, TerraScanCommon.PageModeTypes.Edit))
                    {
                        this.pageMode = TerraScanCommon.PageModeTypes.Edit;
                        this.FormSlice_EditEnabled(this, new DataEventArgs<int>(this.masterFormNo));
                    }
                //}
           // }
        }

        #endregion 

        private void AssociationLinksGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            editingControl = (DataGridViewTextBoxEditingControl)e.Control;
            editingControl.TextChanged += new EventHandler(editingControl_TextChanged);
        }

        public void editingControl_TextChanged(object sender, EventArgs e)
        {
            SetEditRecord();
        }
   

        private void AssociationLinksGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            editingControl.TextChanged -= new EventHandler(editingControl_TextChanged);
            editingControl = null;
        }
    }

}
