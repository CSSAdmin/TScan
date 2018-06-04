// ---------------------------------------------------------------------
// <copyright file="BaseSmartPart.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This BasePage will Controls the Form Buttons Enable/Disable Based on Permissions of Each User</summary>
// ---------------------------------------------------------------------
// Author:  Shiva
// Date:    28th July 2006
// ---------------------------------------------------------------------
// Change History
// ---------------------------------------------------------------------
// Date             Author      Description
// ----------       ---------   ----------------------------------------
// 28th July 2006      Shiva       Created
// ---------------------------------------------------------------------

namespace TerraScan.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Holds the Permissions for SmartPart
    /// </summary>
    [SmartPart]
    [ComVisible(true)]
    public partial class BaseSmartPart : UserControl
    {
        #region Private Variables

        /// <summary>
        /// DataSet Which Holds The UserContorl Permissions
        /// </summary>
        private DataSet permissionDataset = new DataSet();

        /// <summary>
        /// Struct Which Defines the PermissionFields
        /// </summary>
        public PermissionFields permissionFields;

        /// <summary>
        /// Variable Holds the Null Records Value
        /// </summary>
        private bool nullRecords;

        /// <summary>
        /// Variable Holds the all Controls disable
        /// </summary>
        private bool disableAllControls;

       /// <summary>
        ///  Variable Holds the Edit Permission Value
        /// </summary>
        private bool permissionEdit;

        /// <summary>
        /// Variable Holds the Parent Form Id
        /// </summary>
        private int parentFormId;

        /// <summary>
        /// flag for identifying wehter it is a slice form
        /// </summary>
        private bool flagSliceForm;

        private bool flagParametirized;

        public bool FlagParametirized
        {
            get { return flagParametirized; }
            set { flagParametirized = value; }
        }

        ////public bool PermissionEditVal
        ////{
        ////    get { return this.permissionFields.editPermission; }
        ////    set { this.permissionFields.editPermission = value; }
        ////}

        ////public bool PermissionNewVal
        ////{
        ////    get { return this.permissionFields.newPermission; }
        ////    set { this.permissionFields.newPermission = value; }
        ////}

        ////public bool PermissionOpenVal
        ////{
        ////    get { return this.permissionFields.openPermission; }
        ////    set { this.permissionFields.openPermission = value; }
        ////}

        ////public bool PermissionDeleteVal
        ////{
        ////    get { return this.permissionFields.deletePermission; }
        ////    set { this.permissionFields.deletePermission = value; }
        ////}

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor For BaseSmartPart
        /// </summary>
        public BaseSmartPart()
        {
            InitializeComponent();
            this.flagSliceForm = false;
        }

        #endregion

        #region Publish Events

        /// <summary>
        /// Published event for Setting the Button Mode in OperationSmartPart
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_SetButtonMode, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Enum>> SetButtonMode;

        /// <summary>
        /// Published event for Setting the Permissions in OperationSmartPart
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_SetPermissions, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<PermissionFields>> SetPermissions;

        /// <summary>
        /// Published event for FormClose
        /// </summary>
        [EventPublication(EventTopics.D9001_BaseSmartPart_formClose, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<string>> FormClose;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [flag slice form].
        /// </summary>
        /// <value><c>true</c> if [flag slice form]; otherwise, <c>false</c>.</value>
        public bool FlagSliceForm
        {
            get { return this.flagSliceForm; }
            set { this.flagSliceForm = value; }
        }

        /// <summary>
        /// Property Which is Having Get and Set for PermissionEdit
        /// </summary>
        public bool PermissionEdit
        {
            get { return this.permissionEdit; }
            set { this.permissionEdit = value; }
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
        /// Property Which is Having Get and Set for PermissionFields Struct
        /// </summary>
        public PermissionFields PermissionFiled
        {
            get { return this.permissionFields; }
            set { this.permissionFields = value; }
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
        /// Property Holds the Null Record Value
        /// </summary>
        public bool NullRecords
        {
            get { return this.nullRecords; }
            set { this.nullRecords = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [disable all controls].
        /// </summary>
        /// <value><c>true</c> if [disable all controls]; otherwise, <c>false</c>.</value>
        public bool DisableAllControls
        {
            get { return this.disableAllControls; }
            set { this.disableAllControls = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Mothod which Retrives the Permissions for the Currnt SmartPart
        /// </summary>
        /// <param name="formId">SmartPart Id to get permissions</param>
        /// <returns>PermissionFilds Struc</returns>
        public PermissionFields GetFormPermissions(int formId)
        {
            SupportFormData.GetFormDetailsDataTable permissionDataTable = new SupportFormData.GetFormDetailsDataTable();

            if (TerraScanCommon.SupportFormUserId != -1)
            {
                permissionDataTable = TerraScanCommon.GetFormPermissionDetails(formId, TerraScanCommon.SupportFormUserId);
            }
            else
            {
                permissionDataTable = TerraScanCommon.GetFormPermissionDetails(formId, TerraScanCommon.UserId);
            }
            
            if (permissionDataTable.Rows.Count > 0)
            {
                this.permissionFields.openPermission = Convert.ToBoolean(permissionDataTable.Rows[0]["IsPermissionOpen"]);
                this.permissionFields.newPermission = Convert.ToBoolean(permissionDataTable.Rows[0]["IsPermissionAdd"]);
                this.permissionFields.editPermission = Convert.ToBoolean(permissionDataTable.Rows[0]["IsPermissionEdit"]);
                this.permissionFields.deletePermission = Convert.ToBoolean(permissionDataTable.Rows[0]["IsPermissionDelete"]);
            }

            ////string stringExp = "Form =" + formId;
            ////DataRow[] permissionSet;
            ////if (TerraScanCommon.formPermissionsDataSet != null)
            ////{
            ////    permissionSet = TerraScanCommon.formPermissionsDataSet.Tables[0].Select(stringExp);
            ////    if (permissionSet.Length > 0)
            ////    {
            ////        this.permissionFields.openPermission = Convert.ToBoolean((int)permissionSet[0]["IsPermissionOpen"]);
            ////        this.permissionFields.newPermission = Convert.ToBoolean((int)permissionSet[0]["IsPermissionAdd"]);
            ////        this.permissionFields.editPermission = Convert.ToBoolean((int)permissionSet[0]["IsPermissionEdit"]);
            ////        this.permissionFields.deletePermission = Convert.ToBoolean((int)permissionSet[0]["IsPermissionDelete"]);
            ////    }
            ////}

            return this.permissionFields;
        }

        /// <summary>
        /// Method to Set the Button Modes
        /// </summary>
        /// <param name="buttonActionMode">Mode of the Button</param>
        public void SetButtons(Enum buttonActionMode)
        {
            if (this.SetButtonMode != null)
            {
                this.SetButtonMode(this, new DataEventArgs<Enum>(buttonActionMode));
            }
        }

        /// <summary>
        /// Method will set the Permissions for the SmartPart
        /// </summary>
        /// <param name="permissionField">PermissionField Struct</param>
        public void SetFormPermissions(PermissionFields permissionField)
        {
            this.SetPermissions(this, new DataEventArgs<PermissionFields>(permissionField));
        }

        /// <summary>
        /// Handles the FormClosing event of the ParentForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        public void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.flagSliceForm)
            {
                if (TerraScanCommon.FormName == string.Empty)
                {
                    if (e.CloseReason == CloseReason.MdiFormClosing)
                    {
                        e.Cancel = true;
                        TerraScanCommon.FormName = string.Empty;
                    }
                    else if (e.CloseReason == CloseReason.ApplicationExitCall)
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        this.FormClose(this, new DataEventArgs<string>(e.CloseReason.ToString()));
                        e.Cancel = true;
                    }
                }
                else
                {

                    //if (sender is Form)
                    //{
                    //    (sender as Form).Dispose();
                    //}
                    //if (this != null)
                    //{
                    //    this.DestroyHandle();
                    //}
                    TerraScanCommon.FormName = string.Empty;
                }
            }
            //else
            //{
            //    if (TerraScanCommon.FormName == string.Empty)
            //    {
            //        if (this.Name == "F32012")
            //        {
            //            this.DestroyHandle();
            //        }
            //    }
            //}
            
        }

        /// <summary>
        /// Handles the Resize event of the ParentForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        public void ParentForm_Resize(object sender, EventArgs e)
        {
            if (this.ParentForm != null)
            {
                this.Size = this.ParentForm.Size;
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
            try
            {
                ////Check for default value
                if (this.parentFormId == 0)
                {
                    this.parentFormId = Convert.ToInt32(this.Tag);
                }

                this.ParentForm.FormClosing += new FormClosingEventHandler(this.ParentForm_FormClosing);
                this.ParentForm.Resize += new EventHandler(this.ParentForm_Resize);
                this.permissionFields = this.GetFormPermissions(this.parentFormId);
                this.permissionEdit = this.permissionFields.editPermission;
                this.Size = this.ParentForm.Size;
               
                this.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right))));

                base.OnLoad(e);

                if (!this.flagSliceForm)
                {
                    this.ParentForm.MinimumSize = this.MinimumSize;
                }               

                this.SetFormPermissions(this.permissionFields);
                if (this.NullRecords)
                {
                    this.SetButtons(TerraScanCommon.ButtonActionMode.NullRecordMode);
                }
                else if (this.DisableAllControls)
                {
                    this.SetButtons(TerraScanCommon.ButtonActionMode.DisableAllContorlsMode);
                }
                else
                {
                    this.SetButtons(TerraScanCommon.ButtonActionMode.CancelMode);
                }
            }
            catch
            {
            }
        }
        #endregion
    }
}
