//--------------------------------------------------------------------------------------------
// <copyright file="F9017.cs" company="Congruent">
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
// 13 Sep 06        VINOTH              Created
//*********************************************************************************/

namespace D9000
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using System.Web;
    using Microsoft.Practices.ObjectBuilder;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Utilities;
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.SmartParts;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;

    #endregion

    /// <summary>
    /// F9016 Class
    /// </summary>
    public partial class F9017 : BaseSmartPart
    {
        #region Variables

        /// <summary>
        /// to check CAMA Skethc Form Already Opened
        /// </summary>
        private Boolean  formOpened = false;

        /// <summary>
        /// Object For  CAMA Skethc Form
        /// </summary>
        private Form camaForm = null;

        /// <summary>
        /// Created Instance for SupportFormData.GetFormDetailsDataTable
        /// </summary>
        private SupportFormData.GetFormDetailsDataTable getFormDetailsDataDetails = new SupportFormData.GetFormDetailsDataTable();

        /// <summary>
        /// Created Instance for SupportFormData.ListUsersDataTable
        /// </summary>
        private SupportFormData.ListUsersDataTable listUsers = new SupportFormData.ListUsersDataTable();

        /// <summary>
        /// Created Controller for F9016Controller
        /// </summary>
        private F9017Controller form9017Control;

        /// <summary>
        /// formLabelInfo string array
        /// </summary>
        private string[] formLabelInfo = new string[2];

        /// <summary>
        /// PermissionFields struct 
        /// </summary>
        private PermissionFields permissions;

        #endregion

        #region Constructor

        /// <summary>
        /// F9017 Constructor
        /// </summary>
        public F9017()
        {
            this.InitializeComponent();
        }

        #endregion

        #region EventPublication

        /// <summary>
        /// EventPublication for FormHeader
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_FormHeaderSmartPart_SetFormHeader, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string[]>> SetFormHeader;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the F9017 controll.
        /// </summary>
        /// <value>The F9017 controll.</value>
        [CreateNew]
        public F9017Controller F9017Controll
        {
            get { return this.form9017Control as F9017Controller; }
            set { this.form9017Control = value; }
        }

        #endregion        

        #region Events

        /// <summary>
        /// F9016 Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void F9016_Load(object sender, EventArgs e)
        {
            this.LoadWorkSpace();
            this.PreviewPanel.Visible = false;
            DataRow dr = this.listUsers.NewRow();
            dr[this.listUsers.Name_DisplayColumn.ColumnName] = "<Select one>";
            this.listUsers.Rows.Add(dr);
            this.listUsers.Merge(this.F9017Controll.WorkItem.ListUserNames());
            this.UserListCombo.DataSource = this.listUsers;
            this.UserListCombo.DisplayMember = "Name_Display";
            this.UserListCombo.ValueMember = "UserID";
            ////this.ParentForm.AcceptButton = this.PreviewButton;
            this.FormIDTextBox.Focus();            
        }        

        /// <summary>
        /// Preview Button
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.FormIDTextBox.Text.Trim()))
                {
                    if (this.UserListCombo.SelectedIndex > 0)
                    {
                        this.getFormDetailsDataDetails = this.F9017Controll.WorkItem.GetFormDetails(Convert.ToInt32(this.FormIDTextBox.Text.Trim()), Convert.ToInt32(this.UserListCombo.SelectedValue.ToString()));

                        if (this.getFormDetailsDataDetails.Rows.Count > 0)
                        {
                            this.PreviewPanel.Visible = true;
                            this.userViewLabel.Text = this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.USER_NAMESColumn.ColumnName].ToString();
                            this.FormFileViewLabel.Text = this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.FormFileColumn.ColumnName].ToString();
                            this.DescriptionViewLabel.Text = this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.DescriptionColumn.ColumnName].ToString();
                            this.MenuNameViewLabel.Text = this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.MenuNameColumn.ColumnName].ToString();
                            this.MenuOrderViewLabel.Text = this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.MenuOrderColumn.ColumnName].ToString();
                            this.MenuGroupIDViewPanel.Text = this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.MenuGroupIDColumn.ColumnName].ToString();
                            this.MenuViewLabel.Text = this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionMenuColumn.ColumnName].ToString();
                            this.OpenViewLabel.Text = this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionOpenColumn.ColumnName].ToString();
                            this.EditViewLabel.Text = this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionEditColumn.ColumnName].ToString();
                            this.AddViewLabel.Text = this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionAddColumn.ColumnName].ToString();
                            this.DeleteViewLabel.Text = this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionDeleteColumn.ColumnName].ToString();
                        }
                        else
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("InvalidForm"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.PreviewPanel.Visible = false;
                            this.FormIDTextBox.Focus();
                            ////this.FormIDTextBox.Text = string.Empty;
                        }
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("InvalidUser"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.PreviewPanel.Visible = false;
                        this.UserListCombo.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("EmptyFormValue"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.PreviewPanel.Visible = false;
                    this.FormIDTextBox.Focus();
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
        /// RunButton Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void RunButton_Click(object sender, EventArgs e)
        {            
            try
            {
                string formFile = string.Empty;
                string visibleName = string.Empty;
                FormInfo getPermissionForm = new FormInfo();
                bool isSlice;
                bool isRoverFormShow = true;
                if (this.FormIDTextBox.Text.Trim() == "3230" && !TerraScanCommon.IsFieldUser)
                    isRoverFormShow = false;
                if (isRoverFormShow && !string.IsNullOrEmpty(this.FormIDTextBox.Text.Trim()))
                {
                    if (this.UserListCombo.SelectedIndex > 0)
                    {
                        #region Debug Mode Code //Added by Jyothi

                        if (TerraScanCommon.activateDebugMode.Equals(true) && TerraScanCommon.debugConfiguration.Equals(true))
                        {
                            SupportFormData.GetFormManagementDetailsDataTable getFormSlicesDetails = new SupportFormData.GetFormManagementDetailsDataTable();
                            FormInfo getSliceFormPermission = new FormInfo();
                            string optionalValues = "";
                            string formFileSlice = string.Empty;
                            PermissionFields permissions1;

                            getFormSlicesDetails = F9017Controll.WorkItem.F9002_GetFormManagementDetails(Convert.ToInt32(FormIDTextBox.Text.Trim()), Convert.ToInt32(UserListCombo.SelectedValue.ToString()));

                            ////if (getFormSlicesDetails.Rows.Count == 3)
                            ////{
                            for (int count = 0; count < getFormSlicesDetails.Rows.Count; count++)
                            {
                                permissions1.newPermission = Convert.ToBoolean(getFormSlicesDetails.Rows[count][getFormSlicesDetails.IsPermissionAddColumn.ColumnName].ToString());
                                getSliceFormPermission.addPermission = Convert.ToInt32(Convert.ToBoolean(getFormSlicesDetails.Rows[count][getFormSlicesDetails.IsPermissionAddColumn.ColumnName]));

                                permissions1.openPermission = Convert.ToBoolean(getFormSlicesDetails.Rows[count][getFormSlicesDetails.IsPermissionOpenColumn.ColumnName].ToString());
                                getSliceFormPermission.openPermission = Convert.ToInt32(Convert.ToBoolean(getFormSlicesDetails.Rows[count][getFormSlicesDetails.IsPermissionOpenColumn.ColumnName]));

                                permissions1.editPermission = Convert.ToBoolean(getFormSlicesDetails.Rows[count][getFormSlicesDetails.IsPermissionEditColumn.ColumnName].ToString());
                                getSliceFormPermission.editPermission = Convert.ToInt32(Convert.ToBoolean(getFormSlicesDetails.Rows[count][getFormSlicesDetails.IsPermissionEditColumn.ColumnName]));

                                permissions1.deletePermission = Convert.ToBoolean(getFormSlicesDetails.Rows[count][getFormSlicesDetails.IsPermissionDeleteColumn.ColumnName].ToString());
                                getSliceFormPermission.deletePermission = Convert.ToInt32(Convert.ToBoolean(getFormSlicesDetails.Rows[count][getFormSlicesDetails.IsPermissionDeleteColumn.ColumnName]));

                                formFileSlice = getFormSlicesDetails.Rows[count][getFormSlicesDetails.FormFileColumn.ColumnName].ToString();
                                getSliceFormPermission.formFile = formFileSlice;

                                getSliceFormPermission.form = Convert.ToInt32(FormIDTextBox.Text.Trim());

                                if (getSliceFormPermission.optionalParameters != null)
                                {
                                    if (getSliceFormPermission.optionalParameters[0] != null)
                                    {
                                        optionalValues = optionalValues + "Key ID = " + getSliceFormPermission.optionalParameters[0] + "\n";
                                    }

                                    for (int iCount = 0; count < 3; iCount++)
                                    {
                                        if (getSliceFormPermission.optionalParameters[iCount] != null)
                                        {
                                            optionalValues = optionalValues + "Other Parameter " + iCount + " = " + getSliceFormPermission.optionalParameters[iCount] + "\n";
                                        }
                                    }
                                }

                                MessageBox.Show("Form: " + getSliceFormPermission.form + "\n" + "FormFile: " + getSliceFormPermission.formFile + "\n" + "Open Permission: " + Convert.ToBoolean(getSliceFormPermission.openPermission) + "\n" + "Add Permission: " + Convert.ToBoolean(getSliceFormPermission.addPermission) + "\n" + "Edit Permission: " + Convert.ToBoolean(getSliceFormPermission.editPermission) + "\n" + "Delete Permission: " + Convert.ToBoolean(getSliceFormPermission.deletePermission) + "\n" + optionalValues, ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            TerraScanCommon.debugSliceConfiguration = true;
                            ////}
                        }

                        #endregion Debug Mode Code ////Ended by Jyothi

                        this.getFormDetailsDataDetails = this.F9017Controll.WorkItem.GetFormDetails(Convert.ToInt32(FormIDTextBox.Text.Trim()), Convert.ToInt32(UserListCombo.SelectedValue.ToString()));
                        if (this.getFormDetailsDataDetails.Rows.Count > 0)
                        {
                            this.permissions.newPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionAddColumn.ColumnName].ToString());
                            getPermissionForm.addPermission = Convert.ToInt32(Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionAddColumn.ColumnName]));

                            this.permissions.openPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionOpenColumn.ColumnName].ToString());
                            getPermissionForm.openPermission = Convert.ToInt32(Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionOpenColumn.ColumnName]));

                            this.permissions.editPermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionEditColumn.ColumnName].ToString());
                            getPermissionForm.editPermission = Convert.ToInt32(Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionEditColumn.ColumnName]));

                            this.permissions.deletePermission = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionDeleteColumn.ColumnName].ToString());
                            getPermissionForm.deletePermission = Convert.ToInt32(Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][this.getFormDetailsDataDetails.IsPermissionDeleteColumn.ColumnName]));

                            formFile = this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.FormFileColumn.ColumnName].ToString();
                            getPermissionForm.formFile = formFile;

                            visibleName = this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.MenuNameColumn.ColumnName].ToString();
                            getPermissionForm.visibleName = visibleName;
                            getPermissionForm.form = Convert.ToInt32(FormIDTextBox.Text.Trim());
                            
                            isSlice = Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsSliceColumn.ColumnName].ToString());

                            if (!isSlice)
                            {
                                if (this.permissions.openPermission && Convert.ToBoolean(this.getFormDetailsDataDetails.Rows[0][getFormDetailsDataDetails.IsPermissionMenuColumn.ColumnName].ToString()))
                                {
                                    ////FormInfo getPermissionForm = TerraScan.Common.TerraScanCommon.GetFormInfo(Convert.ToInt32(FormIDTextBox.Text.Trim())); ////.GetForm(9002, optionalParameter, this.form9016Control.WorkItem);
                                    getPermissionForm.optionalParameters = new object[6];
                                    ////getPermissionForm.optionalParameters[0] = Convert.ToInt32(this.FormIDTextBox.Text.Trim());
                                    if (!string.IsNullOrEmpty(this.Parameter1TextBox.Text.Trim()))
                                    {
                                        ////string str1 = (str.Trim() = string.Empty ? "Hi" : "Bye");
                                        getPermissionForm.optionalParameters[0] = this.Parameter1TextBox.Text.Trim();

                                        if (!string.IsNullOrEmpty(this.Parameter2TextBox.Text.Trim()))
                                        {
                                            if (!string.IsNullOrEmpty(this.Parameter1TextBox.Text.Trim()))
                                            {
                                                getPermissionForm.optionalParameters[1] = this.Parameter2TextBox.Text.Trim();
                                            }
                                            else
                                            {
                                                MessageBox.Show(SharedFunctions.GetResourceString("InvalidKey"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                this.Parameter1TextBox.Focus();
                                                return;
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(this.Parameter3TextBox.Text.Trim()))
                                        {
                                            if (!string.IsNullOrEmpty(this.Parameter1TextBox.Text.Trim()))
                                            {
                                                getPermissionForm.optionalParameters[2] = this.Parameter3TextBox.Text.Trim();
                                            }
                                            else
                                            {
                                                MessageBox.Show(SharedFunctions.GetResourceString("InvalidKey"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                this.Parameter1TextBox.Focus();
                                                return;
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(this.Parameter4Textbox.Text.Trim()))
                                        {
                                            if (!string.IsNullOrEmpty(this.Parameter1TextBox.Text.Trim()))
                                            {
                                                getPermissionForm.optionalParameters[3] = this.Parameter4Textbox.Text.Trim();
                                            }
                                            else
                                            {
                                                MessageBox.Show(SharedFunctions.GetResourceString("InvalidKey"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                this.Parameter1TextBox.Focus();
                                                return;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(this.Parameter2TextBox.Text.Trim()))
                                        {
                                            if (!string.IsNullOrEmpty(this.Parameter1TextBox.Text.Trim()))
                                            {
                                                getPermissionForm.optionalParameters[1] = this.Parameter2TextBox.Text.Trim();
                                            }
                                            else
                                            {
                                                MessageBox.Show(SharedFunctions.GetResourceString("InvalidKey"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                this.Parameter1TextBox.Focus();
                                                return;
                                            }
                                        }
                                        else if (!string.IsNullOrEmpty(this.Parameter3TextBox.Text.Trim()))
                                        {
                                            if (!string.IsNullOrEmpty(this.Parameter1TextBox.Text.Trim()))
                                            {
                                                getPermissionForm.optionalParameters[2] = this.Parameter3TextBox.Text.Trim();
                                            }
                                            else
                                            {
                                                MessageBox.Show(SharedFunctions.GetResourceString("InvalidKey"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                this.Parameter1TextBox.Focus();
                                                return;
                                            }
                                        }
                                        else if (!string.IsNullOrEmpty(this.Parameter4Textbox.Text.Trim()))
                                        {
                                            if (!string.IsNullOrEmpty(this.Parameter1TextBox.Text.Trim()))
                                            {
                                                getPermissionForm.optionalParameters[3] = this.Parameter4Textbox.Text.Trim();
                                            }
                                            else
                                            {
                                                MessageBox.Show(SharedFunctions.GetResourceString("InvalidKey"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                this.Parameter1TextBox.Focus();
                                                return;
                                            }
                                        }
                                    }

                                    getPermissionForm.optionalParameters[4] = this.permissions;
                                    getPermissionForm.optionalParameters[5] = this.UserListCombo.SelectedValue.ToString();
                                    ////getPermissionForm.formFile = formFile;
                                    ////getPermissionForm.visibleName = visibleName;
                                    ////getPermissionForm.openPermission = Convert.ToInt32(this.permissions.openPermission);
                                    TerraScanCommon.SupportFormUserId = Convert.ToInt32(this.UserListCombo.SelectedValue);

                                    /* Code For CAMA Sketh*/
                                    Boolean CAMAForm = false;
                                    int objectID = 0;
                                    for (int CamaFormNo = 0; CamaFormNo < TerraScanCommon.CAMASketchFormDetails.Length; CamaFormNo++)
                                    {
                                        if (TerraScanCommon.CAMASketchFormDetails[CamaFormNo].Contains(FormIDTextBox.Text.Trim()))
                                        {
                                            CAMAForm = true;
                                        }

                                    }
                                    if (CAMAForm)
                                    {
                                    if (!String.IsNullOrEmpty(this.Parameter1TextBox.Text.Trim()))
                                    {
                                      Int32.TryParse(this.Parameter1TextBox.Text.Trim(), out objectID);
                                    }
                                    if(objectID > 0)
                                    {                                  
                                       /* if (!formOpened)
                                        {
                                            camaForm = new Form();
                                            camaForm = TerraScanCommon.ShowSketchForm(3200);
                                            Form mainFrm = new Form();
                                            mainFrm = (Form)((Form)this.ParentForm).ParentForm;
                                            foreach (Form f in mainFrm.MdiChildren)
                                            {
                                                if (f.Name == "CAMASketch")
                                                {
                                                    f.Close(); 
                                                    f.Dispose();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (formOpened)
                                            {
                                                //camaForm.Dispose(); 
                                                //camaForm.Close(); 
                                                camaForm = new Form();
                                                camaForm = TerraScanCommon.ShowSketchForm(3200);
                                                Form formAlreadyOpened = (Form)TerraScanCommon.GetObject(camaForm, "GetOpenedForm");
                                                formAlreadyOpened.Close();
                                                Boolean formClosed = (Boolean)TerraScanCommon.GetObject(camaForm, "FormOpened");
                                                formOpened = formClosed;
                                                if (!formClosed)
                                                {
                                                    camaForm = TerraScanCommon.ShowSketchForm(3200);                                                   
                                                }
                                            }
                                            else
                                            {
                                                Form mainFrm = new Form();
                                                mainFrm = (Form)((Form)this.ParentForm).ParentForm;
                                                foreach (Form f in mainFrm.MdiChildren)
                                                {
                                                    if (f.Name == "CAMASketch")
                                                    {
                                                        f.Dispose();
                                                    }

                                                }
                                            }
                                        }*/

                                        this.camaForm = new Form();
                                        this.camaForm = TerraScanCommon.ShowSketchForm(3200);
                                        Form formAlreadyOpened = (Form)TerraScanCommon.GetObject(this.camaForm, "GetOpenedForm");
                                        if (formAlreadyOpened != null)
                                        {
                                            formAlreadyOpened.Close();
                                            Boolean formClosed = (Boolean)TerraScanCommon.GetObject(this.camaForm, "FormOpened");
                                            this.formOpened = formClosed;
                                            if (!formClosed)
                                            {
                                                this.camaForm = TerraScanCommon.ShowSketchForm(3200);
                                            }
                                        }

                                        //if (!formOpened)
                                        //{
                                            TerraScanCommon.SetValue(camaForm, "SetObjectID", objectID);
                                            TerraScanCommon.SetValue(camaForm, "SetMDI", ((Form)((Form)this.ParentForm).ParentForm));
                                        //}
                                        formOpened = (Boolean)TerraScanCommon.GetObject(camaForm, "FormOpened");
                                    }
                                    }
                                    else
                                    {   ////Endshere                                     
                                        this.ShowForm(this, new DataEventArgs<FormInfo>(getPermissionForm));
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(SharedFunctions.GetResourceString("PermissionCheck"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    this.PreviewPanel.Visible = false;
                                    this.UserListCombo.Focus();
                                    ////this.FormIDTextBox.Text = string.Empty;                            
                                }
                            }
                            else
                            {
                                MessageBox.Show("Slice Form cannot be opened", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.PreviewPanel.Visible = false;
                                this.FormIDTextBox.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("InvalidForm"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.PreviewPanel.Visible = false;
                            this.FormIDTextBox.Focus();
                            ////this.FormIDTextBox.Text = string.Empty;                            
                        }
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("InvalidUser"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.PreviewPanel.Visible = false;
                        this.UserListCombo.Focus();
                        ////this.UserListCombo.Focus();
                    }
                }
                else if (string.IsNullOrEmpty(this.FormIDTextBox.Text.Trim()))
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("EmptyFormValue"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.PreviewPanel.Visible = false;
                    this.FormIDTextBox.Focus();
                    ////this.FormIDTextBox.Focus();
                }
                else
                {
                    MessageBox.Show("Current user do not have permission to acess this form.", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            finally
            {
                TerraScanCommon.SupportFormUserId = -1;
            }
        }

        #endregion

        #region LoadWorkSpace

        /// <summary>
        /// Loads the work space.
        /// </summary>
        private void LoadWorkSpace()
        {
            try
            {
                // To Load FormHeaderSmartPart into formHeaderSmartPartdeckWorkspace
                if (this.form9017Control.WorkItem.SmartParts.Contains(SmartPartNames.FormHeaderSmartPart))
                {
                    this.formHeaderSmartPartdeckWorkspace.Show(this.form9017Control.WorkItem.SmartParts.Get(SmartPartNames.FormHeaderSmartPart));
                }
                else
                {
                    this.formHeaderSmartPartdeckWorkspace.Show(this.form9017Control.WorkItem.SmartParts.AddNew<FormHeaderSmartPart>(SmartPartNames.FormHeaderSmartPart));
                }

                this.formLabelInfo[0] = "Support Form Call";
                this.formLabelInfo[1] = string.Empty;

                this.SetFormHeader(this, new DataEventArgs<string[]>(this.formLabelInfo));
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
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
    }
}