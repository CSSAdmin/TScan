//--------------------------------------------------------------------------------------------
// <copyright file="TestOperationSmartPart.cs" company="Congruent">
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
// 
//*********************************************************************************/
namespace TerraScan.SmartParts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Common;
    using System.Collections;

    /// <summary>
    /// TestOperationSmartPart class file
    /// </summary>
    public partial class TestOperationSmartPart : PrimaryBaseSmartPart
    {
        #region Private Variables

        /// <summary>
        /// permissionEdit
        /// </summary>
        private bool permissionEdit;

        /// <summary>
        /// makeButtonEnable
        /// </summary>
        private string[] makeButtonEnable = new string[2];

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TestOperationSmartPart"/> class.
        /// </summary>
        public TestOperationSmartPart()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Published Events

        /// <summary>
        /// EventPublication for OperationButtonClick
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> OperationButtonClick;

        /// <summary>
        /// EventPublication for LoadCancelButton
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_LoadCancelButton, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> LoadCancelButton;

        #endregion

        #region Subscribed Events

        /// <summary>
        /// Sets the button mode.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_SetButtonMode, Thread = ThreadOption.UserInterface)]
        public void SetButtonMode(object sender, DataEventArgs<Enum> e)
        {
            Enum mode = e.Data;
            if (mode.Equals(TerraScanCommon.ButtonActionMode.NewMode))
            {
                this.NewButton.Enabled = false;
                this.SaveButton.Enabled = true;
                this.CancelButton.Enabled = true;
                this.DeleteButton.Enabled = false;
                //// New Button Disable
                //// Save Button Enable
                //// Cancel Button Enable
                //// Delete Button Disable
            }
            else if (mode.Equals(TerraScanCommon.ButtonActionMode.SaveMode))
            {
                ////this.SetButtons(CurrentForm, TerraScanCommon.ButtonActionMode.CancelMode);

                NewButton.Enabled = true && NewButton.ActualPermission;
                SaveButton.Enabled = false;
                CancelButton.Enabled = false;
                DeleteButton.Enabled = true && DeleteButton.ActualPermission;

                //// New Button Enable/Disable (Based On User Permission On Form)
                //// Save Button Disabled 
                //// Cancel Button Disable
                //// Delete Button Enable/Disable (Based On User Permission On Form)
                //// Or 
                //// Call the Cancel Mode
            }
            else if (mode.Equals(TerraScanCommon.ButtonActionMode.EditMode))
            {
                NewButton.Enabled = false;
                SaveButton.Enabled = true && this.permissionEdit;
                CancelButton.Enabled = true;
                DeleteButton.Enabled = false;
                ////DeleteButton.Enabled = true && DeleteButton.ActualPermission;

                //// New Button Disable
                //// Save Button Enable
                //// Cancel Button Enable
                //// Delete Button Enable/Disable (Based On User Permission On Form)
            }
            else if (mode.Equals(TerraScanCommon.ButtonActionMode.CancelMode))
            {
                NewButton.Enabled = true && NewButton.ActualPermission;
                SaveButton.Enabled = false;
                CancelButton.Enabled = false;
                DeleteButton.Enabled = true && DeleteButton.ActualPermission;

                //// New Button Enable/Disable (Based On User Permission On Form)
                //// Save Button Disable
                //// Cancel Button Disable
                //// Delete Button Enable/Disable (Based On User Permission On Form)
            }
            else if (mode.Equals(TerraScanCommon.ButtonActionMode.DeleteMode))
            {
                ////this.SetButtons(CurrentForm, TerraScanCommon.ButtonActionMode.CancelMode);

                NewButton.Enabled = true && NewButton.ActualPermission;
                SaveButton.Enabled = false;
                CancelButton.Enabled = false;
                DeleteButton.Enabled = true && DeleteButton.ActualPermission;

                //// New Button Enable/Disable (Based On User Permission On Form)
                //// Save Button Disable
                //// Cancel Button Disable
                //// Delete Button Enable/Disable (Based On User Permission On Form)
                //// Or
                //// Cancel Mode
            }
        }

        /// <summary>
        /// Makes the button enable.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_MakeButtonEnable, Thread = ThreadOption.UserInterface)]
        public void MakeButtonEnable(object sender, DataEventArgs<string[]> e)
        {
            this.makeButtonEnable = e.Data;
            foreach (Control button in this.Controls)
            {
                if (button.Tag.ToString() == this.makeButtonEnable[0])
                {
                    button.Enabled = Convert.ToBoolean(this.makeButtonEnable[1]);
                }
            }
        }

        /// <summary>
        /// Gets the cancel button.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, Thread = ThreadOption.UserInterface)]
        public void GetCancelButton(object sender, DataEventArgs<string> e)
        {
            this.LoadCancelButton(this, new DataEventArgs<Button>(this.CancelButton));
        }

        /// <summary>
        /// Sets the permissions.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_SetPermissions, Thread = ThreadOption.UserInterface)]
        public void SetPermissions(object sender, DataEventArgs<PermissionFields> e)
        {
            PermissionFields permissionFields;
            permissionFields = e.Data;
            NewButton.Enabled = permissionFields.newPermission;
            NewButton.ActualPermission = permissionFields.newPermission;
            DeleteButton.Enabled = permissionFields.deletePermission;
            DeleteButton.ActualPermission = permissionFields.deletePermission;
            this.permissionEdit = permissionFields.editPermission;
        }

        /// <summary>
        /// Customizes the operation smart part.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription("topic://TerraScan.UI.Common.Controls/OperationSmartPart/CustomizeOperationSmartPart", Thread = ThreadOption.UserInterface)]
        public void CustomizeOperationSmartPart(object sender, DataEventArgs<OperationSmartPartFields> e)
        {
            OperationSmartPartFields operationSmartPartFields;
            operationSmartPartFields = e.Data;

            this.NewButton.Text = operationSmartPartFields.NewButtonText;
            this.NewButton.Visible = operationSmartPartFields.NewButtonVisible;

            this.SaveButton.Text = operationSmartPartFields.SaveButtonText;
            this.SaveButton.Visible = operationSmartPartFields.SaveButtonVisible;

            this.CancelButton.Text = operationSmartPartFields.CancelButtonText;
            this.CancelButton.Visible = operationSmartPartFields.CancelButtonVisible;

            this.DeleteButton.Text = operationSmartPartFields.DeleteButtonText;
            this.DeleteButton.Visible = operationSmartPartFields.DeleteButtonVisible;
        }

        #endregion

        #region Properties

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the Click event of the OperationButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OperationButton_Click(object sender, EventArgs e)
        {
            try
            {
                Control sourceControl = sender as Button;
                this.GetContainerControl().ActivateControl(sourceControl);
                this.OperationButtonClick(this, new DataEventArgs<string>(sourceControl.Tag.ToString().Trim()));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion
    }
}
