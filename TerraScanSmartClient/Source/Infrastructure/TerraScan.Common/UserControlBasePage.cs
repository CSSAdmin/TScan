// ---------------------------------------------------------------------
// <copyright file="UserControlBasePage.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This BasePage will Controls the UserControl Buttons Enable/Disable Based on Permissions of Each User</summary>
// ---------------------------------------------------------------------
// Author:  Shiva
// Date:    8th May 2006
// ---------------------------------------------------------------------
// Change History
// ---------------------------------------------------------------------
// Date             Author      Description
// ----------       ---------   ----------------------------------------
// 8th May 2006      Shiva       Created
// ---------------------------------------------------------------------
namespace TerraScan.Common
{
    ///using System;
    ///using System.Collections.Generic;
    ///using System.Text;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.Windows.Forms;

    /// <summary>
    /// Provides methods for managing the Action Buttons in Each UserControl Based On Permissions to User
    /// </summary>
    public class UserControlBasePage : UserControl
    {
        /// <summary>
        /// DataSet Which Holds The UserContorl Permissions
        /// </summary>
        private DataSet permissionDataset = new DataSet();

        /// <summary>
        /// List Which Holds the BasePage Buttons
        /// </summary>
        private List<TerraScan.UI.Controls.TerraScanButton> basePageButtons = new List<TerraScan.UI.Controls.TerraScanButton>();

        /// <summary>
        ///  Variable Holds the Edit Permission Value
        /// </summary>
        private int permissionEdit;

        /// <summary>
        /// Assigning Empty to parentFormId
        /// </summary>
        private int parentFormId = 0;

        /// <summary>
        /// Property to get the BasePage Buttons
        /// </summary>
        public List<TerraScan.UI.Controls.TerraScanButton> BasePageButtons
        {
            get { return this.basePageButtons; }
        }

        /// <summary>
        /// Public DataSet Which Holds the FormPermissions for Each User
        /// </summary>
        public DataSet PermissionDataset
        {
            get
            {
                return this.permissionDataset;
            }

            set
            {
                this.permissionDataset = value;
            }
        }

        /// <summary>
        /// Gets or sets the parent form id.
        /// </summary>
        /// <value>The parent form id.</value>
        public int ParentFormId
        {
            get { return this.parentFormId; }
            set { this.parentFormId = value; }
        }

        /// <summary>
        /// Set The Current Form Buttons to Enable/Disable On Each Action Mode
        /// </summary>
        /// <param name="currentForm">Current Form</param>
        /// <param name="mode">Action Mode</param>
        public void SetButtons(UserControl currentForm, int mode)
        {
            if (mode == (int)UserControlCommon.ButtonActionMode.NewMode)
            {
                this.GetButtonControls(currentForm);
                if (this.BasePageButtons.Count > 0)
                {
                    foreach (TerraScan.UI.Controls.TerraScanButton terraScanButton in this.BasePageButtons)
                    {
                        if ((int)terraScanButton.SetActionType == (int)UserControlCommon.ButtonActionType.New)
                        {
                            terraScanButton.Enabled = false;
                        }
                        else if ((int)terraScanButton.SetActionType == (int)UserControlCommon.ButtonActionType.Save)
                        {
                            terraScanButton.Enabled = true;
                        }
                        else if ((int)terraScanButton.SetActionType == (int)UserControlCommon.ButtonActionType.Cancel)
                        {
                            terraScanButton.Enabled = true;
                        }
                        else if ((int)terraScanButton.SetActionType == (int)UserControlCommon.ButtonActionType.Delete)
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
            else if (mode == (int)UserControlCommon.ButtonActionMode.SaveMode)
            {
                this.SetButtons(currentForm, (int)UserControlCommon.ButtonActionMode.CancelMode);
                //// New Button Enable/Disable (Based On User Permission On Form)
                //// Save Button Disabled 
                //// Cancel Button Disable
                //// Delete Button Enable/Disable (Based On User Permission On Form)
                //// Or 
                //// Call the Cancel Mode
            }
            else if (mode == (int)UserControlCommon.ButtonActionMode.EditMode)
            {
                this.GetButtonControls(currentForm);
                if (this.BasePageButtons.Count > 0)
                {
                    foreach (TerraScan.UI.Controls.TerraScanButton terraScanButton in this.BasePageButtons)
                    {
                        if ((int)terraScanButton.SetActionType == (int)UserControlCommon.ButtonActionType.New)
                        {
                            terraScanButton.Enabled = false;
                        }
                        else if ((int)terraScanButton.SetActionType == (int)UserControlCommon.ButtonActionType.Save)
                        {
                            terraScanButton.Enabled = true && Convert.ToBoolean(this.permissionEdit);
                        }
                        else if ((int)terraScanButton.SetActionType == (int)UserControlCommon.ButtonActionType.Cancel)
                        {
                            terraScanButton.Enabled = true;
                        }
                        else if ((int)terraScanButton.SetActionType == (int)UserControlCommon.ButtonActionType.Delete)
                        {
                            terraScanButton.Enabled = true && terraScanButton.ActualPermission;
                        }
                    }
                }

                this.BasePageButtons.Clear();
                //// New Button Disable
                //// Save Button Enable
                //// Cancel Button Enable
                //// Delete Button Enable/Disable (Based On User Permission On Form)
            }
            else if (mode == (int)UserControlCommon.ButtonActionMode.OpenMode)
            {
                this.SetButtons(currentForm, (int)UserControlCommon.ButtonActionMode.CancelMode);
                //// New Button Enable/Disable (Based On User Permission On Form)
                //// Save Button Disable
                //// Cancel Button Disable
                //// Delete Button Enable/Disable (Based On User Permission On Form)
                //// Or
                //// Cancel Mode
            }
            else if (mode == (int)UserControlCommon.ButtonActionMode.CancelMode)
            {
                this.GetButtonControls(currentForm);
                if (this.BasePageButtons.Count > 0)
                {
                    foreach (TerraScan.UI.Controls.TerraScanButton terraScanButton in this.BasePageButtons)
                    {
                        if ((int)terraScanButton.SetActionType == (int)UserControlCommon.ButtonActionType.New)
                        {
                            terraScanButton.Enabled = true && terraScanButton.ActualPermission;
                        }
                        else if ((int)terraScanButton.SetActionType == (int)UserControlCommon.ButtonActionType.Save)
                        {
                            terraScanButton.Enabled = false;
                        }
                        else if ((int)terraScanButton.SetActionType == (int)UserControlCommon.ButtonActionType.Cancel)
                        {
                            terraScanButton.Enabled = false;
                        }
                        else if ((int)terraScanButton.SetActionType == (int)UserControlCommon.ButtonActionType.Delete)
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
            else if (mode == (int)UserControlCommon.ButtonActionMode.DeleteMode)
            {
                this.SetButtons(currentForm, (int)UserControlCommon.ButtonActionMode.CancelMode);
                //// New Button Enable/Disable (Based On User Permission On Form)
                //// Save Button Disable
                //// Cancel Button Disable
                //// Delete Button Enable/Disable (Based On User Permission On Form)
                //// Or
                //// Cancel Mode
            }
        }

        /// <summary>
        /// the Form Permissions will be captured and Set the Button Action Modes
        /// </summary>
        /// <param name="e">OnLoad Even Args</param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.GetFormPermissions(this, this.PermissionDataset);
                this.SetButtons(this, (int)UserControlCommon.ButtonActionMode.CancelMode);
                base.OnLoad(e);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Method to Get the Button Controls in the Current Form
        /// </summary>
        /// <param name="currentForm">Current Form</param>
        private void GetButtonControls(UserControl currentForm)
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
                ////TODO: Check for generic UserControl type instead of a specific type
                if ((control.GetType().ToString() == "TerraScan.ReceiptEngine.ReceiptEngineUserControl"))
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
        /// Method to Get The UserControl  Permissions for each User from database
        /// </summary>
        /// <param name="currentForm">currentForm as UserControl</param>
        /// <param name="formPermissions">DataSet Which Holds the UserPermissions</param>
        private void GetFormPermissions(UserControl currentForm, DataSet formPermissions)
        {
            int permissionOpen, permissionAdd, permissionDelete;
            string stringExp = string.Concat("Form = ", this.parentFormId);
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
                    this.SetFormPermissions(currentForm, permissionOpen, permissionAdd, permissionDelete);
                }
            }
        }

        /// <summary>
        /// Method to Set The Buttons Based on Permissions of each User on form for Initial form Load
        /// </summary>
        /// <param name="currentForm">Current Form</param>
        /// <param name="permissionOpen">PermissionOpen</param>
        /// <param name="permissionAdd">PermissionAdd</param>
        /// <param name="permissionDelete">PermissionDelete</param>
        private void SetFormPermissions(UserControl currentForm, int permissionOpen, int permissionAdd, int permissionDelete)
        {
            this.GetButtonControls(currentForm);
            if (this.BasePageButtons.Count > 0)
            {
                foreach (TerraScan.UI.Controls.TerraScanButton terraScanButton in this.BasePageButtons)
                {
                    if ((int)terraScanButton.SetActionType == (int)UserControlCommon.ButtonActionType.Open)
                    {
                        terraScanButton.Enabled = Convert.ToBoolean(permissionOpen);
                        terraScanButton.ActualPermission = Convert.ToBoolean(permissionOpen);
                    }
                    else if ((int)terraScanButton.SetActionType == (int)UserControlCommon.ButtonActionType.New)
                    {
                        terraScanButton.Enabled = Convert.ToBoolean(permissionAdd);
                        terraScanButton.ActualPermission = Convert.ToBoolean(permissionAdd);
                    }
                    else if ((int)terraScanButton.SetActionType == (int)UserControlCommon.ButtonActionType.Delete)
                    {
                        terraScanButton.Enabled = Convert.ToBoolean(permissionDelete);
                        terraScanButton.ActualPermission = Convert.ToBoolean(permissionDelete);
                    }
                }
            }

            this.BasePageButtons.Clear();
        }
    }
}
