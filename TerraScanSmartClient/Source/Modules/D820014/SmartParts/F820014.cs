//--------------------------------------------------------------------------------------------
// <copyright file="F820014.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F820014.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//*********************************************************************************/

namespace D820014
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.BusinessEntities;
    using TerraScan.UI.Controls;
    using TerraScan.Helper;

    /// <summary>
    /// F820014
    /// </summary>
    [SmartPart]
    public partial class F820014 : BaseSmartPart
    {
        #region Member Variables

        private TerraScanCommon.PageModeTypes pageMode;
        private bool isNewRecord = false;
        private List<ComboSQLText> comboTextCollection;
        private List<DataSetCollection> dataSetCollection;
        private bool formMasterPermissionEdit;
        private int masterFormNo;
        private int keyId;
        private PermissionFields slicePermissionField;
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();
        private DataSet formDetailsDataSet = new DataSet();
        private List<Control> lockControlsCollection = new List<Control>();
        private int actualHeight;
        private bool pageLoadStatus;

        #endregion Member Variables

        /// <summary>
        /// Initializes a new instance of the <see cref="F820014"/> [ERROR: invalid expression DeclaringTypeKind].
        /// </summary>
        public F820014()
        {
            this.Tag = 820014;
            InitializeComponent();
            this.actualHeight = this.Height;
            this.comboTextCollection = new List<ComboSQLText>();
            this.dataSetCollection = new List<DataSetCollection>();
            this.r820014_Vmg_Development.Image = ExtendedGraphics.GenerateVerticalLabelText(this.r820014_Vmg_Development.Height, this.r820014_Vmg_Development.Width, this.r820014_Vmg_Development.Text, this.r820014_Vmg_Development.BackColor, this.r820014_Vmg_Development.Font);
            this.r820014_Vmg_Development.Text = string.Empty;
            this.AddDataSetValues("r820014_FloodPlain_TypeOfDevelopment", "StoredProcedure", "r820014_Get");
        }

        /// <summary>
        /// Handles the Load event of the F820014 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F820014_Load(object sender, EventArgs e)
        {
            this.FlagSliceForm = true;
            this.Height = this.actualHeight;
            this.pageLoadStatus = true;
            this.GetLockControls();
            this.ClearFields();
            this.PopulateFormFields();
            this.pageLoadStatus = false;
        }

        /// <summary>
        /// Handles the Resize event of the BaseSmartPart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void BaseSmartPart_Resize(object sender, EventArgs e)
        {
            this.Height = this.actualHeight;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F820014"/> [ERROR: invalid expression DeclaringTypeKind].
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        public F820014(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit)
        {
            this.Tag = 820014;
            InitializeComponent();
            this.actualHeight = this.Height;
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.comboTextCollection = new List<ComboSQLText>();
            this.dataSetCollection = new List<DataSetCollection>();
            this.r820014_Vmg_Development.Image = ExtendedGraphics.GenerateVerticalLabelText(this.r820014_Vmg_Development.Height, this.r820014_Vmg_Development.Width, this.r820014_Vmg_Development.Text, this.r820014_Vmg_Development.BackColor, this.r820014_Vmg_Development.Font);
            this.r820014_Vmg_Development.Text = string.Empty;
            this.AddDataSetValues("r820014_FloodPlain_TypeOfDevelopment", "StoredProcedure", "r820014_Get");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F820014"/> [ERROR: invalid expression DeclaringTypeKind].
        /// </summary>
        /// <param name="masterform">The masterform.</param>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyID">The key ID.</param>
        /// <param name="red">The red.</param>
        /// <param name="green">The green.</param>
        /// <param name="blue">The blue.</param>
        /// <param name="tabText">The tab text.</param>
        /// <param name="permissionEdit">if set to <c>true</c> [permission edit].</param>
        /// <param name="featureClassID">The feature class ID.</param>
        public F820014(int masterform, int formNo, int keyID, int red, int green, int blue, string tabText, bool permissionEdit, int featureClassID)
        {
            this.Tag = 820014;
            InitializeComponent();
            this.actualHeight = this.Height;
            this.formMasterPermissionEdit = permissionEdit;
            this.masterFormNo = masterform;
            this.keyId = keyID;
            this.comboTextCollection = new List<ComboSQLText>();
            this.dataSetCollection = new List<DataSetCollection>();
            this.r820014_Vmg_Development.Image = ExtendedGraphics.GenerateVerticalLabelText(this.r820014_Vmg_Development.Height, this.r820014_Vmg_Development.Width, this.r820014_Vmg_Development.Text, this.r820014_Vmg_Development.BackColor, this.r820014_Vmg_Development.Font);
            this.r820014_Vmg_Development.Text = string.Empty;
            this.AddDataSetValues("r820014_FloodPlain_TypeOfDevelopment", "StoredProcedure", "r820014_Get");
        }
                
        ////private OperationSmartPart operationSmartPart;
        private F820014Controller form820014control;

        /// <summary>
        /// Gets or sets the form820014control.
        /// </summary>
        /// <value>The form820014control.</value>
        [CreateNew]        
        public F820014Controller Form820014control
        {
            get { return this.form820014control as F820014Controller; }
            set { this.form820014control = value; }
        }
        
        [EventPublication(EventTopicNames.FormSlice_EditEnabled, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<int>> FormSlice_EditEnabled;

        [EventPublication(EventTopicNames.FormSlice_ValidationAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceValidationFields>> FormSlice_ValidationAlert;

        [EventPublication(EventTopicNames.D9030_F9030_ReloadAfterSave, PublicationScope.Global)]
        public event EventHandler<TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>> D9030_F9030_ReloadAfterSave;

        [EventPublication(EventTopicNames.FormSlice_FormCloseAlert, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<SliceFormCloseAlert>> FormSlice_FormCloseAlert;

        /// <summary>
        /// Called when [ERROR: invalid expression End][D9030_ F9030_ set slice permission].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SetSlicePermission, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SetSlicePermission(object sender, DataEventArgs<SlicePermissionReload> eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                    {
                        this.slicePermissionField.deletePermission = this.PermissionFiled.deletePermission;
                        this.slicePermissionField.editPermission = this.PermissionFiled.editPermission;
                        this.slicePermissionField.newPermission = this.PermissionFiled.newPermission;
                        this.slicePermissionField.openPermission = this.PermissionFiled.openPermission;
                    }

                    if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    {
                        this.LockControls(false);
                    }
                    else
                    {
                        this.LockControls(true);
                    }

                    if (this.formDetailsDataSet.Tables.Count > 0)
                    {
                        if (this.formDetailsDataSet.Tables[0].Rows.Count > 0)
                        {
                            eventArgs.Data.FlagInvalidSliceKey = false;
                        }
                        else
                        {
                            this.LockControls(true);
                               //// Coding Added for the issue 4212 0n 30/5/2009.
                        //// Last Slice does not have a record also it will not return invalid slice
                            if (eventArgs.Data.FlagInvalidSliceKey)
                            {
                                eventArgs.Data.FlagInvalidSliceKey = true;
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
        /// Called when [ERROR: invalid expression End][D9030_ F9030_ load slice details].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_LoadSliceDetails, ThreadOption.UserInterface)]
        public void OnD9030_F9030_LoadSliceDetails(object sender, DataEventArgs<SliceReloadActiveRecord> eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    if (this.masterFormNo == eventArgs.Data.MasterFormNo)
                    {
                        this.keyId = eventArgs.Data.SelectedKeyId;
                        
                        if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                        {
                            this.LockControls(false);
                        }
                        else
                        {
                            this.LockControls(true);
                        }

                        this.pageLoadStatus = true;
                        this.ClearFields();
                        this.PopulateFormFields();
                        this.pageLoadStatus = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [ERROR: invalid expression End][D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    if (this.pageMode == TerraScanCommon.PageModeTypes.View)
                    {
                        if (this.slicePermissionField.newPermission)
                        {
                            this.Cursor = Cursors.WaitCursor;
                            this.pageLoadStatus = true;
                            this.pageMode = TerraScanCommon.PageModeTypes.New;
                            this.ClearFields();
                            this.isNewRecord = true;
                            this.LockControls(false);
                            this.pageLoadStatus = false;
                            this.Cursor = Cursors.Default;
                        }
                        else
                        {
                            this.LockControls(true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [ERROR: invalid expression End][D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    this.Cursor = Cursors.WaitCursor;
                    
                    if (this.formMasterPermissionEdit && this.slicePermissionField.editPermission)
                    {
                        this.LockControls(false);
                    }
                    else
                    {
                        this.LockControls(true);
                    }

                    this.pageMode = TerraScanCommon.PageModeTypes.View;
                    this.pageLoadStatus = true;
                    this.ClearFields();
                    this.PopulateFormFields();
                    this.pageLoadStatus = false;
                    this.isNewRecord = false;
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [ERROR: invalid expression End][D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, EventArgs eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    if (this.slicePermissionField.deletePermission)
                    {
                        ////this.DeleteButton_Click(); 
                        RdlToCodeData.ParameterDataDataTable dt = new RdlToCodeData.ParameterDataDataTable();
                        dt.Rows.Add(new object[] { "EventID", this.keyId });
                        string deleteXML = TerraScanCommon.GetXmlString(dt);
                        this.form820014control.WorkItem.RdlToCode_Delete(deleteXML, "r820014");
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
            }
        }

        /// <summary>
        /// Called when [ERROR: invalid expression End][D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, DataEventArgs<int> eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                    {
                        SliceValidationFields sliceValidationFields = new SliceValidationFields();
                        sliceValidationFields.FormNo = eventArgs.Data;
                        ////this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(this.CheckErrors(eventArgs.Data))); 
                        this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
                    }
                    else
                    {
                        SliceValidationFields sliceValidationFields = new SliceValidationFields();
                        sliceValidationFields.FormNo = eventArgs.Data;
                        this.FormSlice_ValidationAlert(this, new DataEventArgs<SliceValidationFields>(sliceValidationFields));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [ERROR: invalid expression End][D9030_ F9030_ save confirmed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveConfirmed, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveConfirmed(object sender, EventArgs eventArgs)
        {
            try
            {
                if (this != null && this.IsDisposed != true)
                {
                    if ((this.pageMode == TerraScanCommon.PageModeTypes.Edit && this.slicePermissionField.editPermission) || (this.pageMode == TerraScanCommon.PageModeTypes.New && this.slicePermissionField.newPermission))
                    {
                        int currentKeyId = this.SaveForm(this.isNewRecord);
                        this.isNewRecord = false; 
                        
                        if (currentKeyId != -1)
                        {
                            SliceReloadActiveRecord currentSliceInfo;
                            currentSliceInfo.MasterFormNo = this.masterFormNo;
                            currentSliceInfo.SelectedKeyId = currentKeyId;
                            this.OnD9030_F9030_ReloadAfterSave(new TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord>(currentSliceInfo));
                            this.pageMode = TerraScanCommon.PageModeTypes.View;
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
        /// Called when [ERROR: invalid expression End][D9030_ F9030_ reload after save].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnD9030_F9030_ReloadAfterSave(TerraScan.Infrastructure.Interface.EventArgs<SliceReloadActiveRecord> eventArgs)
        {
            if (this.D9030_F9030_ReloadAfterSave != null)
            {
                this.D9030_F9030_ReloadAfterSave(this, eventArgs);
            }
        }

        /// <summary>
        /// Called when [ERROR: invalid expression End][form slice_ form close alert].
        /// </summary>
        /// <param name="eventArgs">The event args.</param>
        protected virtual void OnFormSlice_FormCloseAlert(DataEventArgs<SliceFormCloseAlert> eventArgs)
        {
            if (this.FormSlice_FormCloseAlert != null)
            {
                this.FormSlice_FormCloseAlert(this, eventArgs);
            }
        }

        /// <summary>
        /// Adds the data set values.
        /// </summary>
        /// <param name="dsName">Name of the ds.</param>
        /// <param name="commandType">Type of the command.</param>
        /// <param name="commandText">The command text.</param>
        private void AddDataSetValues(string dsName, string commandType, string commandText)
        {
            DataSetCollection tempDataSetCollection;
            tempDataSetCollection.dataSetName = dsName;
            tempDataSetCollection.commandType = commandType;
            tempDataSetCollection.commandText = commandText;
            this.dataSetCollection.Add(tempDataSetCollection);
        }

        /// <summary>
        /// Adds the combo values.
        /// </summary>
        /// <param name="comboName">Name of the combo.</param>
        /// <param name="sqlText">The SQL text.</param>
        private void AddComboValues(string comboName, string sqlText)
        {
            ComboSQLText comboSQLText;
            comboSQLText.ComboName = comboName;
            comboSQLText.SqlText = sqlText;
            this.comboTextCollection.Add(comboSQLText);
        }

        /// <summary>
        /// Populates the form fields.
        /// </summary>
        private void PopulateFormFields()
        {
            ////DataSet ds = new DataSet();
            RdlToCodeData.ParameterDataDataTable dt = new RdlToCodeData.ParameterDataDataTable();
            dt.Rows.Add(new object[] { "EventID", this.keyId });
            try
            {
                this.formDetailsDataSet = this.form820014control.WorkItem.RdlToCode_Get(TerraScanCommon.GetXmlString(dt), "r820014");
                ////ds = TerraScan.Helper.WSHelper.RdlToCode_Get(dt,"r820014");
            }
            catch (Exception ex1)
            {
            }

            if (this.formDetailsDataSet != null)
            {
                if (this.formDetailsDataSet.Tables.Count > 0)
                {
                    if (this.formDetailsDataSet.Tables[0].Rows.Count > 0)
                    {
                        foreach (Control c in this.Controls)
                        {
                            try
                            {
                                if (c.Name.EndsWith("subreport"))
                                {
                                    string[] formName = c.Name.Split(new char[] { '_' });
                                    DataSet subReportDataset = new DataSet();
                                    ////subReportDataset = TerraScan.Helper.WSHelper.RdlToCode_Get(dt, formName[2]);
                                    if (c.HasChildren)
                                    {
                                        if (c is Panel)
                                        {
                                            ////(c as Panel).BorderStyle = BorderStyle.None; 
                                        }

                                        this.AddValuesToControl(subReportDataset, c.Controls);
                                    }
                                }
                                else if (c.HasChildren)
                                {
                                    this.AddValuesToControl(this.formDetailsDataSet, c.Controls);
                                }
                                else if (c.GetType() == typeof(TerraScan.UI.Controls.TerraScanTextBox))
                                {
                                    if (!string.IsNullOrEmpty(c.Tag.ToString()))
                                    {
                                        c.Text = this.formDetailsDataSet.Tables[0].Rows[0][c.Tag.ToString()].ToString();
                                    }
                                }
                                else if (c.GetType() == typeof(TerraScan.UI.Controls.TerraScanCheckBox))
                                {
                                    if (!string.IsNullOrEmpty(c.Tag.ToString()))
                                    {
                                        string result = this.formDetailsDataSet.Tables[0].Rows[0][c.Tag.ToString()].ToString();
                                        if (result == "True")
                                        {
                                            ((CheckBox)c).Checked = true;
                                        }
                                    }
                                }
                                else if (c.GetType() == typeof(RadioButton))
                                {
                                    if (!string.IsNullOrEmpty(c.Tag.ToString()))
                                    {
                                        string result = this.formDetailsDataSet.Tables[0].Rows[0][c.Tag.ToString()].ToString();
                                        if (c.AccessibleName.ToString().EndsWith(result))
                                        {
                                            ((RadioButton)c).Checked = true;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    else
                    {
                        this.LockControls(true);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the border style.
        /// </summary>
        /// <param name="currentControl">The current control.</param>
        private void SetBorderStyle(Control currentControl)
        {
            if ((currentControl != null) && (currentControl.Parent != null) && (currentControl.Parent is Panel))
            {
                if ((currentControl.Parent as Panel).BorderStyle != BorderStyle.FixedSingle)
                {
                    ////(currentControl.Parent as Panel).BorderStyle = BorderStyle.FixedSingle; 
                }
            }
        }

        /// <summary>
        /// Gets the lock controls.
        /// </summary>
        private void GetLockControls()
        {
            foreach (Control c in this.Controls)
            {
                if (c.Name.EndsWith("subreport"))
                {
                    string[] formName = c.Name.Split(new char[] { '_' });
                    if (c.HasChildren)
                    {
                        this.AddLockControlsCollection(c.Controls);
                    }
                }
                else if (c.HasChildren)
                {
                    this.AddLockControlsCollection(c.Controls);
                }
                else if (c.GetType().Equals(typeof(TerraScan.UI.Controls.TerraScanTextBox)))
                {
                    (c as TerraScanTextBox).BorderStyle = BorderStyle.None;
                    (c as TerraScanTextBox).ApplyFocusColor = true;
                    (c as TerraScanTextBox).SetFocusColor = Color.FromArgb(255, 255, 121);
                    this.SetBorderStyle(c);
                    this.lockControlsCollection.Add(c);
                }
                else if (c.GetType().Equals(typeof(TerraScan.UI.Controls.TerraScanComboBox)))
                {
                    (c as TerraScanComboBox).FlatStyle = FlatStyle.Popup;
                    this.SetBorderStyle(c);
                    this.lockControlsCollection.Add(c);
                }
                else if (c.GetType().Equals(typeof(TerraScan.UI.Controls.TerraScanCheckBox)))
                {
                    this.SetBorderStyle(c);
                    this.lockControlsCollection.Add(c);
                }
                else if (c.GetType().Equals(typeof(RadioButton)))
                {
                    this.SetBorderStyle(c);
                    this.lockControlsCollection.Add(c);
                }
            }
        }

        /// <summary>
        /// Adds the lock controls collection.
        /// </summary>
        /// <param name="c">The c.</param>
        private void AddLockControlsCollection(Control.ControlCollection c)
        {
            foreach (Control c1 in c)
            {
                if (c1.HasChildren)
                {
                    this.AddLockControlsCollection(c1.Controls);
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanTextBox))
                {
                    (c1 as TerraScanTextBox).BorderStyle = BorderStyle.None;
                    (c1 as TerraScanTextBox).ApplyFocusColor = true;
                    (c1 as TerraScanTextBox).SetFocusColor = Color.FromArgb(255, 255, 121);
                    this.SetBorderStyle(c1);
                    this.lockControlsCollection.Add(c1);
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanComboBox))
                {
                    (c1 as TerraScanComboBox).FlatStyle = FlatStyle.Popup;
                    this.SetBorderStyle(c1);
                    this.lockControlsCollection.Add(c1);
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanCheckBox))
                {
                    this.SetBorderStyle(c1);
                    this.lockControlsCollection.Add(c1);
                }
                else if (c1.GetType() == typeof(RadioButton))
                {
                    this.SetBorderStyle(c1);
                    this.lockControlsCollection.Add(c1);
                }
            }
        }

        /// <summary>
        /// Locks the controls.
        /// </summary>
        /// <param name="lockValue">if set to <c>true</c> [lock value].</param>
        private void LockControls(bool lockValue)
        {
            if (this.lockControlsCollection != null && this.lockControlsCollection.Count > 0)
            {
                for (int iCount = 0; iCount < this.lockControlsCollection.Count; iCount++)
                {
                    Control tempControl = this.lockControlsCollection[iCount];
                    if (tempControl.GetType().Equals(typeof(TerraScanTextBox)))
                    {
                        (tempControl as TerraScanTextBox).LockKeyPress = lockValue;
                    }
                    else if (tempControl.GetType().Equals(typeof(TerraScanComboBox)))
                    {
                        (tempControl as TerraScanComboBox).Enabled = !lockValue;
                    }
                    else if (tempControl.GetType().Equals(typeof(TerraScanCheckBox)))
                    {
                        (tempControl as TerraScanCheckBox).Enabled = !lockValue;
                    }
                    else if (tempControl.GetType().Equals(typeof(RadioButton)))
                    {
                        (tempControl as RadioButton).Enabled = !lockValue;
                    }
                }
            }
        }

        /// <summary>
        /// Adds the values to control.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <param name="c">The c.</param>
        private void AddValuesToControl(DataSet ds, Control.ControlCollection c)
        {
            foreach (Control c1 in c)
            {
                if (c1.HasChildren)
                {
                    if (c1 is Panel)
                    {
                        ////(c1 as Panel).BorderStyle = BorderStyle.None; 
                    }

                    this.AddValuesToControl(ds, c1.Controls);
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanTextBox))
                {
                    if (!string.IsNullOrEmpty(c1.Tag.ToString()))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            c1.Text = ds.Tables[0].Rows[0][c1.Tag.ToString()].ToString();
                        }
                    }
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanComboBox))
                {
                    DataSet dsCombo = new DataSet();
                    for (int ctrlCount = 0; ctrlCount < this.dataSetCollection.Count; ctrlCount++)
                    {
                        if (this.dataSetCollection[ctrlCount].dataSetName.ToLower().Equals(c1.Tag.ToString().ToLower()))
                        {
                            dsCombo = this.form820014control.WorkItem.RdlToCode_FillCombo(this.dataSetCollection[ctrlCount].commandText);
                        }
                    }

                    ((TerraScan.UI.Controls.TerraScanComboBox)c1).DataSource = dsCombo.Tables[0];
                    ((TerraScan.UI.Controls.TerraScanComboBox)c1).DisplayMember = dsCombo.Tables[0].Columns[1].ToString();
                    ((TerraScan.UI.Controls.TerraScanComboBox)c1).ValueMember = dsCombo.Tables[0].Columns[0].ToString();
                    ////((TerraScan.UI.Controls.TerraScanComboBox)c1).SelectedValue = ds.Tables[0].Rows[0][c1.Tag.ToString()].ToString();
                    
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        this.SetComboSelectedValue(((TerraScan.UI.Controls.TerraScanComboBox)c1), dsCombo, ds);
                    }
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanCheckBox))
                {
                    if (!string.IsNullOrEmpty(c1.Tag.ToString()))
                    {
                        string result = string.Empty;
                        
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            result = ds.Tables[0].Rows[0][c1.Tag.ToString()].ToString();
                        }

                        if (result == "True")
                        {
                            ((CheckBox)c1).Checked = true;
                        }
                    }
                }
                else if (c1.GetType() == typeof(RadioButton))
                {
                    if (!string.IsNullOrEmpty(c1.Tag.ToString()))
                    {
                        string result = string.Empty;
                        
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            result = ds.Tables[0].Rows[0][c1.Tag.ToString()].ToString();
                        }
                        ////To Fix Bug #4263 by khaja
                        foreach (Char c6 in result)
                        {
                            if (c1.AccessibleName.ToString().EndsWith(c6.ToString()))
                            {
                                ((RadioButton)c1).Checked = true;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Edits the enabled event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EditEnabledEvent(object sender, EventArgs e)
        {
            this.SetEditRecord();
        }

        /// <summary>
        /// Sets the edit record.
        /// </summary>
        private void SetEditRecord()
        {
            if (this.slicePermissionField.editPermission && this.formMasterPermissionEdit)
            {
                if (!this.pageLoadStatus)
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
        /// Saves the form.
        /// </summary>
        /// <param name="newRecord">if set to <c>true</c> [new record].</param>
        /// <returns>Record</returns>
        private int SaveForm(bool newRecord)
        {
            int result = -1;
            DataSet da = new DataSet();
            RdlToCodeData.ParameterDataDataTable dt = new RdlToCodeData.ParameterDataDataTable();
            foreach (Control c in this.Controls)
            {
                try
                {
                    if (c.Name.EndsWith("subreport"))
                    {
                        string[] formName = c.Name.Split(new char[] { '_' });
                        DataSet subReportDataset = new DataSet();
                        if (c.HasChildren)
                        {
                            RdlToCodeData.ParameterDataDataTable subReportDatatable = new RdlToCodeData.ParameterDataDataTable();
                            this.UpdateValuesToControl(subReportDatatable, c.Controls);
                            result = this.SaveOperation(subReportDatatable, newRecord, formName[2]);
                        }
                    }
                    else if (c.HasChildren)
                    {
                        this.UpdateValuesToControl(dt, c.Controls);
                    }
                    else if (c.GetType() == typeof(TerraScan.UI.Controls.TerraScanTextBox))
                    {
                        if (!string.IsNullOrEmpty(c.Tag.ToString()))
                        {
                            dt.Rows.Add(new object[] { c.Tag.ToString(), c.Text });
                        }
                    }
                    else if (c.GetType() == typeof(TerraScan.UI.Controls.TerraScanCheckBox))
                    {
                        if (!string.IsNullOrEmpty(c.Tag.ToString()))
                        {
                            if (((TerraScan.UI.Controls.TerraScanCheckBox)c).Checked == true)
                            {
                                dt.Rows.Add(new object[] { c.Tag.ToString(), true });
                            }
                            else
                            {
                                dt.Rows.Add(new object[] { c.Tag.ToString(), false });
                            }
                        }
                    }
                    else if (c.GetType() == typeof(RadioButton))
                    {
                        if (!string.IsNullOrEmpty(c.Tag.ToString()))
                        {
                            if (((RadioButton)c).Checked == true)
                            {
                                string[] value = c.AccessibleName.Split('_');
                                string actualValue = value[value.Length - 1].ToString();
                                dt.Rows.Add(new object[] { c.Tag.ToString(), actualValue });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            result = this.SaveOperation(dt, newRecord, "r820014");
            return result;
        }

        /// <summary>
        /// Updates the values to control.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="c">The c.</param>
        private void UpdateValuesToControl(RdlToCodeData.ParameterDataDataTable dt, Control.ControlCollection c)
        {
            foreach (Control c1 in c)
            {
                string nae = c1.Name;
                if (c1.HasChildren)
                {
                    this.UpdateValuesToControl(dt, c1.Controls);
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanTextBox))
                {
                    if (!string.IsNullOrEmpty(c1.Tag.ToString()))
                    {
                        dt.Rows.Add(new object[] { c1.Tag.ToString(), c1.Text });
                    }
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanComboBox))
                {
                    if (!string.IsNullOrEmpty(c1.Tag.ToString()))
                    {
                        dt.Rows.Add(new object[] { c1.Tag.ToString(), ((TerraScan.UI.Controls.TerraScanComboBox)c1).SelectedValue });
                    }
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanCheckBox))
                {
                    if (!string.IsNullOrEmpty(c1.Tag.ToString()))
                    {
                        if (((TerraScan.UI.Controls.TerraScanCheckBox)c1).Checked == true)
                        {
                            dt.Rows.Add(new object[] { c1.Tag.ToString(), true });
                        }
                        else
                        {
                            dt.Rows.Add(new object[] { c1.Tag.ToString(), false });
                        }
                    }
                }
                else if (c1.GetType() == typeof(RadioButton))
                {
                    if (!string.IsNullOrEmpty(c1.Tag.ToString()))
                    {
                        if (((RadioButton)c1).Checked == true)
                        {
                            string[] value = c1.AccessibleName.Split('_');
                            string actualValue = value[value.Length - 1].ToString();
                            dt.Rows.Add(new object[] { c1.Tag.ToString(), actualValue });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Clears the fields.
        /// </summary>
        private void ClearFields()
        {
            foreach (Control c in this.Controls)
            {
                if (c.Name != "subreport1")
                {
                    if (c.HasChildren)
                    {
                        this.ClearFieldValues(c.Controls);
                    }
                }
            }
        }

        /// <summary>
        /// Clears the field values.
        /// </summary>
        /// <param name="c">The c.</param>
        private void ClearFieldValues(Control.ControlCollection c)
        {
            foreach (Control c1 in c)
            {
                if (c1.HasChildren)
                {
                    this.ClearFieldValues(c1.Controls);
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanTextBox))
                {
                    if (!string.IsNullOrEmpty(c1.Tag.ToString()))
                    {
                        c1.Text = "";
                    }
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanCheckBox))
                {
                    ((CheckBox)c1).Checked = false;
                }
                else if (c1.GetType() == typeof(TerraScan.UI.Controls.TerraScanComboBox))
                {
                    DataSet dsCombo = new DataSet();
                    for (int ctrlCount = 0; ctrlCount < this.dataSetCollection.Count; ctrlCount++)
                    {
                        if (this.dataSetCollection[ctrlCount].dataSetName.ToLower().Equals(c1.Tag.ToString().ToLower()))
                        {
                            dsCombo = this.form820014control.WorkItem.RdlToCode_FillCombo(this.dataSetCollection[ctrlCount].commandText);
                        }
                    }

                    if (dsCombo != null && dsCombo.Tables.Count > 0)
                    {
                        ((TerraScan.UI.Controls.TerraScanComboBox)c1).DataSource = dsCombo.Tables[0];
                        ((TerraScan.UI.Controls.TerraScanComboBox)c1).DisplayMember = dsCombo.Tables[0].Columns[1].ToString();
                        ((TerraScan.UI.Controls.TerraScanComboBox)c1).ValueMember = dsCombo.Tables[0].Columns[0].ToString();
                        ////((TerraScan.UI.Controls.TerraScanComboBox)c1).SelectedValue = ds.Tables[0].Rows[0][c1.Tag.ToString()].ToString();
                    }

                    ((TerraScan.UI.Controls.TerraScanComboBox)c1).SelectedIndex = -1;
                }
                else if (c1.GetType().Equals(typeof(RadioButton)))
                {
                    ((RadioButton)c1).Checked = false;
                }
            }
        }

        /// <summary>
        /// Saves the operation.
        /// </summary>
        /// <param name="dt">The dt.</param>
        /// <param name="newRecord">if set to <c>true</c> [new record].</param>
        /// <param name="formName">Name of the form.</param>
        /// <returns>Record</returns>
        private int SaveOperation(RdlToCodeData.ParameterDataDataTable dt, bool newRecord, string formName)
        {
            int errorId = -1;
            if (!newRecord)
            {
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Add(new object[] { "EventID", this.keyId });
                    string saveXML = TerraScanCommon.GetXmlString(dt);
                    errorId = this.form820014control.WorkItem.RdlToCode_Save(saveXML, formName);
                    this.keyId = errorId;
                    ////MessageBox.Show("Record Updated Successfully for " + formName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    string saveXML = TerraScanCommon.GetXmlString(dt);
                    errorId = this.form820014control.WorkItem.RdlToCode_Save(saveXML, formName);
                    this.keyId = errorId;
                    ////MessageBox.Show("New Record Created Successfully for " + formName, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            return errorId;
        }

        /// <summary>
        /// Sets the combo selected value.
        /// </summary>
        /// <param name="combobox">The combobox.</param>
        /// <param name="comboDataSet">The combo data set.</param>
        /// <param name="getDataSet">The get data set.</param>
        private void SetComboSelectedValue(TerraScanComboBox combobox, DataSet comboDataSet, DataSet getDataSet)
        {
            try
            {
                string columnName = string.Empty;
                string value = string.Empty;
                columnName = this.GetColumnName(combobox.ValueMember, getDataSet);
                if (columnName.Equals(string.Empty))
                {
                    columnName = this.GetColumnName(combobox.DisplayMember, getDataSet);
                    if (columnName.Equals(string.Empty))
                    {
                        this.SetNoneAsDefault(combobox, comboDataSet);
                    }
                    else
                    {
                        value = getDataSet.Tables[0].Rows[0][columnName].ToString();
                        value = this.CheckValue(value, combobox, false, comboDataSet);
                        if (!string.IsNullOrEmpty(value))
                        {
                            combobox.SelectedValue = value;
                        }
                        else
                        {
                            this.SetNoneAsDefault(combobox, comboDataSet);
                        }
                    }
                }
                else // if column name is value member 
                {
                    value = getDataSet.Tables[0].Rows[0][columnName].ToString();
                    if (!string.IsNullOrEmpty(this.CheckValue(value, combobox, true, comboDataSet)))
                    {
                        combobox.SelectedValue = value;
                    }
                    else // if the value from get method not present at combo box(combo DataSet) 
                    {
                        this.SetNoneAsDefault(combobox, comboDataSet);
                    }
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Checks the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="combo">The combo.</param>
        /// <param name="flagValueMember">if set to <c>true</c> [flag value member].</param>
        /// <param name="comboDataSet">The combo data set.</param>
        /// <returns>string</returns>
        private string CheckValue(string value, ComboBox combo, bool flagValueMember, DataSet comboDataSet)
        {
            string columnName = string.Empty;
            
            if (flagValueMember)
            {
                columnName = combo.ValueMember;
            }
            else
            {
                columnName = combo.DisplayMember;
            }

            DataRow[] getSelctedRows = comboDataSet.Tables[0].Select(columnName + "= '" + value + "'");
            
            if ((getSelctedRows != null) && (getSelctedRows.Length > 0))
            {
                return getSelctedRows[0][combo.ValueMember].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Sets the none as default.
        /// </summary>
        /// <param name="combo">The combo.</param>
        /// <param name="comboDataSet">The combo data set.</param>
        private void SetNoneAsDefault(ComboBox combo, DataSet comboDataSet)
        {
            DataRow newRow = comboDataSet.Tables[0].NewRow();
            newRow[combo.DisplayMember] = string.Empty;
            newRow[combo.ValueMember] = 0;
            comboDataSet.Tables[0].Rows.Add(newRow);
            combo.SelectedValue = 0;
        }

        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="getDataSet">The get data set.</param>
        /// <returns>String</returns>
        private string GetColumnName(string columnName, DataSet getDataSet)
        {
            foreach (DataColumn column in getDataSet.Tables[0].Columns)
            {
                if (columnName == column.ColumnName)
                {
                    return column.ColumnName;
                }
            }

            return string.Empty;
        }        
    }
}