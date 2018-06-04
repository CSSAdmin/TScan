// ---------------------------------------------------------------------
// <copyright file="BasePage.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This BasePage will Controls the Form Buttons Enable/Disable Based on Permissions of Each User</summary>
// ---------------------------------------------------------------------
// Author:  Shiva
// Date:    8th May 2006
// ---------------------------------------------------------------------
// Change History
// ---------------------------------------------------------------------
// Date             Author      Description
// ----------       ---------   ----------------------------------------
// 8th May 2006      Shiva       Created
// 17th May 2006     Shiva       Modified (Added Varible ParentFormId for get the Permissions) 
// 18th Sep 2007     Guhan       Modified (getting  permission using currentForm.Tag )
// ---------------------------------------------------------------------

namespace TerraScan.Common
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.Windows.Forms;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// Provides methods for managing the Action Buttons in Each Form Based On Permissions to User
    /// </summary>
    public class BasePage : Form
    {
        #region Private Variables

        /// <summary>
        /// DataSet Which Holds the User Permissions on Each Form
        /// </summary>
        private static DataSet formPermissions;

        /// <summary>
        /// Variable Holds the Parent Form Id
        /// </summary>
        private int parentFormId;
        
        /// <summary>
        /// List Which Holds the BasePage Buttons
        /// </summary>
        private List<TerraScan.UI.Controls.TerraScanButton> basePageButtons = new List<TerraScan.UI.Controls.TerraScanButton>();

        /// <summary>
        ///  Variable Holds the Edit Permission Value
        /// </summary>
        private bool permissionEdit;

        /// <summary>
        /// Struct Which Defines the PermissionFields
        /// </summary>
        private PermissionFields formPermissionFields;

        #endregion

        #region Properties

        /// <summary>
        /// Holds the FormPermissions DataSet
        /// </summary>
        public static DataSet FormPermissions
        {
            get
            {
                return formPermissions;
            }

            set
            {
                formPermissions = value;
            }
        }

        /// <summary>
        /// Property to get the BasePage Buttons
        /// </summary>
        public List<TerraScan.UI.Controls.TerraScanButton> BasePageButtons
        {
            get { return this.basePageButtons; }
        }

        /// <summary>
        /// Property Which is Having Get and Set for ParentFormID
        /// </summary>
        public int ParentFormId
        {
            get { return this.parentFormId; }
            set { this.parentFormId = value; }
        }

        /// <summary>
        /// Gets or sets the form permission fields.
        /// </summary>
        /// <value>The form permission fields.</value>
        public PermissionFields FormPermissionFields
        {
            get { return this.formPermissionFields; }
            set { this.formPermissionFields = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set The Current Form Buttons to Enable/Disable On Each Action Mode
        /// </summary>
        /// <param name="currentForm">Current Form</param>
        /// <param name="mode">Action Mode</param>
        public void SetButtons(Form currentForm, int mode)
        {
            if (mode == (int)TerraScanCommon.ButtonActionMode.NewMode)
            {
                this.GetButtonControls(currentForm);
                if (this.BasePageButtons.Count > 0)
                {
                    foreach (TerraScan.UI.Controls.TerraScanButton terraScanButton in this.BasePageButtons)
                    {
                        if ((int)terraScanButton.SetActionType == (int)TerraScanCommon.ButtonActionType.New)
                        {
                            terraScanButton.Enabled = false;
                        }
                        else if ((int)terraScanButton.SetActionType == (int)TerraScanCommon.ButtonActionType.Save)
                        {
                            terraScanButton.Enabled = true;
                        }
                        else if ((int)terraScanButton.SetActionType == (int)TerraScanCommon.ButtonActionType.Cancel)
                        {
                            terraScanButton.Enabled = true;
                        }
                        else if ((int)terraScanButton.SetActionType == (int)TerraScanCommon.ButtonActionType.Delete)
                        {
                            terraScanButton.Enabled = false;
                        }
                    }
                }

                this.BasePageButtons.Clear();
                //// New Button Disable
                //// Save Button Enable
                //// Cancel Button Enable
                //// Delete Button Disable
            }
            else if (mode == (int)TerraScanCommon.ButtonActionMode.SaveMode)
            {
                this.SetButtons(currentForm, (int)TerraScanCommon.ButtonActionMode.CancelMode);
                //// New Button Enable/Disable (Based On User Permission On Form)
                //// Save Button Disabled 
                //// Cancel Button Disable
                //// Delete Button Enable/Disable (Based On User Permission On Form)
                //// Or 
                //// Call the Cancel Mode
            }
            else if (mode == (int)TerraScanCommon.ButtonActionMode.EditMode)
            {
                this.GetButtonControls(currentForm);
                if (this.BasePageButtons.Count > 0)
                {
                    foreach (TerraScan.UI.Controls.TerraScanButton terraScanButton in this.BasePageButtons)
                    {
                        if ((int)terraScanButton.SetActionType == (int)TerraScanCommon.ButtonActionType.New)
                        {
                            terraScanButton.Enabled = false;
                        }
                        else if ((int)terraScanButton.SetActionType == (int)TerraScanCommon.ButtonActionType.Save)
                        {
                            terraScanButton.Enabled = true && this.permissionEdit;
                        }
                        else if ((int)terraScanButton.SetActionType == (int)TerraScanCommon.ButtonActionType.Cancel)
                        {
                            terraScanButton.Enabled = true;
                        }
                        else if ((int)terraScanButton.SetActionType == (int)TerraScanCommon.ButtonActionType.Delete)
                        {
                            ////terraScanButton.Enabled = true && terraScanButton.ActualPermission;
                            terraScanButton.Enabled = false;
                        }
                    }
                }

                this.BasePageButtons.Clear();
                //// New Button Disable
                //// Save Button Enable
                //// Cancel Button Enable
                //// Delete Button Enable/Disable (Based On User Permission On Form)
            }
            else if (mode == (int)TerraScanCommon.ButtonActionMode.OpenMode)
            {
                this.SetButtons(currentForm, (int)TerraScanCommon.ButtonActionMode.CancelMode);
                //// New Button Enable/Disable (Based On User Permission On Form)
                //// Save Button Disable
                //// Cancel Button Disable
                //// Delete Button Enable/Disable (Based On User Permission On Form)
                //// Or
                //// Cancel Mode
            }
            else if (mode == (int)TerraScanCommon.ButtonActionMode.CancelMode)
            {
                this.GetButtonControls(currentForm);
                if (this.BasePageButtons.Count > 0)
                {
                    foreach (TerraScan.UI.Controls.TerraScanButton terraScanButton in this.BasePageButtons)
                    {
                        if ((int)terraScanButton.SetActionType == (int)TerraScanCommon.ButtonActionType.New)
                        {
                            terraScanButton.Enabled = true && terraScanButton.ActualPermission;
                        }
                        else if ((int)terraScanButton.SetActionType == (int)TerraScanCommon.ButtonActionType.Save)
                        {
                            terraScanButton.Enabled = false;
                        }
                        else if ((int)terraScanButton.SetActionType == (int)TerraScanCommon.ButtonActionType.Cancel)
                        {
                            terraScanButton.Enabled = false;
                        }
                        else if ((int)terraScanButton.SetActionType == (int)TerraScanCommon.ButtonActionType.Delete)
                        {
                            terraScanButton.Enabled = true && terraScanButton.ActualPermission;
                        }
                    }
                }

                this.BasePageButtons.Clear();
                //// New Button Enable/Disable (Based On User Permission On Form)
                //// Save Button Disable
                //// Cancel Button Disable
                //// Delete Button Enable/Disable (Based On User Permission On Form)
            }
            else if (mode == (int)TerraScanCommon.ButtonActionMode.DeleteMode)
            {
                this.SetButtons(currentForm, (int)TerraScanCommon.ButtonActionMode.CancelMode);
                //// New Button Enable/Disable (Based On User Permission On Form)
                //// Save Button Disable
                //// Cancel Button Disable
                //// Delete Button Enable/Disable (Based On User Permission On Form)
                //// Or
                //// Cancel Mode
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// the Form Permissions will be captured and Set the Button Action Modes
        /// </summary>
        /// <param name="e">OnLoad Even Args</param>
        protected override void OnLoad(EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Text)) 
            { 
                return; 
            }
            
            if (this.IsMdiContainer == false)
            {
                if (this.parentFormId == 0)
                {
                    this.parentFormId = Convert.ToInt32(this.Tag);
                }

                this.GetFormPermissions(this);
                this.SetButtons(this, (int)TerraScanCommon.ButtonActionMode.CancelMode);
            }

            base.OnLoad(e);
        }

        /// <summary>
        /// Clears The BasePageButtons List When the form is Unloaded
        /// </summary>
        /// <param name="e">OnClosed Event Args</param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this.BasePageButtons.Clear();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Method to Get the Button Controls in the Current Form
        /// </summary>
        /// <param name="currentForm">Current Form</param>
        private void GetButtonControls(Form currentForm)
        {
            foreach (Control control in currentForm.Controls)
            {
                this.CheckTerraScanButton(control);
            }
        }

        /// <summary>
        /// Method to Check the Control as TerraScanButton Control
        /// </summary>
        /// <param name="control">Button Control</param>
        private void CheckTerraScanButton(Control control)
        {
            if (control is TerraScan.UI.Controls.TerraScanButton)
            {
                this.AddBasePageButton(control);
            }
            else if (control.HasChildren)
            {
                if (!(control.GetType().ToString().Equals("TerraScan.ReceiptEngine.ReceiptEngineUserControl")))
                {
                    foreach (Control currentctrl in control.Controls)
                    {
                        this.CheckTerraScanButton(currentctrl);
                    }
                }
            }
        }

        /// <summary>
        /// Method to Add The TerraScanButton to BasePageButtons List
        /// </summary>
        /// <param name="button">Button As TerraScanButton Control</param>
        private void AddBasePageButton(Control button)
        {
           this.BasePageButtons.Add((TerraScan.UI.Controls.TerraScanButton)button);
        }

        /// <summary>
        /// Method to Get The Form Permissions for each User from database
        /// </summary>
        /// <param name="currentForm">Current Form</param>
        private void GetFormPermissions(Form currentForm)
        {
            //// Code Added for Immediate Permissions Effect when Changes Made

            int formId = 0;
            if (currentForm != null)
            {
                ////formId = currentForm.Tag.ToString();

                if (this.parentFormId != 0)
                {
                    formId = this.parentFormId;
                }
                else if (currentForm.Tag != null)
                {

                    int.TryParse(currentForm.Tag.ToString(), out formId);
                }
                else
                {
                    formId = 0;
                }
            }
           
            SupportFormData.GetFormDetailsDataTable permissionDataTable = new SupportFormData.GetFormDetailsDataTable();
            permissionDataTable = TerraScanCommon.GetFormPermissionDetails(formId, TerraScanCommon.UserId);

            if (permissionDataTable.Rows.Count > 0)
            {
                this.formPermissionFields.openPermission = Convert.ToBoolean(permissionDataTable.Rows[0]["IsPermissionOpen"]);
                this.formPermissionFields.newPermission = Convert.ToBoolean(permissionDataTable.Rows[0]["IsPermissionAdd"]);
                this.formPermissionFields.editPermission = Convert.ToBoolean(permissionDataTable.Rows[0]["IsPermissionEdit"]);
                this.permissionEdit = this.formPermissionFields.editPermission;
                this.formPermissionFields.deletePermission = Convert.ToBoolean(permissionDataTable.Rows[0]["IsPermissionDelete"]);

                this.SetFormPermissions(currentForm, this.formPermissionFields.openPermission, this.formPermissionFields.newPermission, this.formPermissionFields.deletePermission);
            }

            //// End 

            #region Commented the Code Existing Permissions Handling
            /*
            FormPermissions = TerraScanCommon.FormPermissionsDataSet;

            string formId = String.Empty;
            if (currentForm != null)
            {
                ////formId = currentForm.Tag.ToString();
                formId = this.parentFormId.ToString();
            }

            int permissionOpen, permissionAdd, permissionDelete;
            string stringExp = "Form =" + formId.Trim();
            DataRow[] permissionSet;
            if (formPermissions != null)
            {
                permissionSet = formPermissions.Tables[0].Select(stringExp);
                if (permissionSet.Length > 0)
                {
                    permissionOpen = (int)permissionSet[0]["IsPermissionOpen"];
                    permissionAdd = (int)permissionSet[0]["IsPermissionAdd"];
                    this.permissionEdit = (int)permissionSet[0]["IsPermissionEdit"];
                    permissionDelete = (int)permissionSet[0]["IsPermissionDelete"];

                    //// Added Code to Store the Permission Details in the Struct
                    this.formPermissionFields.openPermission = Convert.ToBoolean(permissionSet[0]["IsPermissionOpen"]);
                    this.formPermissionFields.newPermission = Convert.ToBoolean(permissionSet[0]["IsPermissionAdd"]);
                    this.formPermissionFields.editPermission = Convert.ToBoolean(permissionSet[0]["IsPermissionEdit"]);
                    this.formPermissionFields.deletePermission = Convert.ToBoolean(permissionSet[0]["IsPermissionDelete"]);

                    this.SetFormPermissions(currentForm, permissionOpen, permissionAdd, permissionDelete);
                }
           }
             */
            #endregion
        }

        /// <summary>
        /// Method to Set The Buttons Based on Permissions of each User on form for Initial form Load
        /// </summary>
        /// <param name="currentForm">Current Form</param>
        /// <param name="permissionOpen">PermissionOpen</param>
        /// <param name="permissionAdd">PermissionAdd</param>
        /// <param name="permissionDelete">PermissionDelete</param>
        private void SetFormPermissions(Form currentForm, bool permissionOpen, bool permissionAdd, bool permissionDelete)
        {
            this.GetButtonControls(currentForm);
            if (this.BasePageButtons.Count > 0)
            {
                foreach (TerraScan.UI.Controls.TerraScanButton terraScanButton in this.BasePageButtons)
                {
                    if ((int)terraScanButton.SetActionType == (int)TerraScanCommon.ButtonActionType.Open)
                    {
                        terraScanButton.Enabled = permissionOpen;
                        terraScanButton.ActualPermission = permissionOpen;
                    }
                    else if ((int)terraScanButton.SetActionType == (int)TerraScanCommon.ButtonActionType.New)
                    {
                        terraScanButton.Enabled = permissionAdd;
                        terraScanButton.ActualPermission = permissionAdd;
                    }
                    else if ((int)terraScanButton.SetActionType == (int)TerraScanCommon.ButtonActionType.Delete)
                    {
                        terraScanButton.Enabled = permissionDelete;
                        terraScanButton.ActualPermission = permissionDelete;
                    }
                    else if ((int)terraScanButton.SetActionType == (int)TerraScanCommon.ButtonActionType.Edit)
                    {
                        terraScanButton.Enabled = this.permissionEdit;
                        terraScanButton.ActualPermission = this.permissionEdit;
                    }
                }
            }

            this.BasePageButtons.Clear();
        }

        #endregion
    }
}
