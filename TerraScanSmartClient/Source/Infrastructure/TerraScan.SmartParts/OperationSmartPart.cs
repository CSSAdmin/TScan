//--------------------------------------------------------------------------------------------
// <copyright file="OperationSmartPart.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains the Permissions for Operation SmartPart based On User
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10 July 06       Shiva              Created
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
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.UI .Controls ;
    using TerraScan.Infrastructure.Interface.Constants;
    
    /// <summary>
    /// This Smart Part Contains Action button like NEW, Save,Cancel and Delete
    /// </summary>
    [SmartPart]
    public partial class OperationSmartPart : PrimaryBaseSmartPart
    {
        #region Private Variables

        /// <summary>
        ///  Variable Holds the Edit Permission Value
        /// </summary>
        private bool permissionEdit;

        /// <summary>
        /// makeButtonEnable
        /// </summary>
        private string[] makeButtonEnable = new string[2];

        /// <summary>
        /// Used to store value for space between button
        /// </summary>
        private int spaceBetweenButton;

        /// <summary>
        /// tool tip for parcel lock details
        /// </summary>
        private string savButtonTooltip;

        #endregion       

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:OperationSmartPart"/> class.
        /// </summary>
        public OperationSmartPart()
        {
            this.InitializeComponent();
            this.SpaceBetweenButton = 6;
            ////NewButtonMenuItem.Click += new EventHandler(this.NewButtonMenuItem_Click);
            ////SaveButtonMenuItem.Click += new EventHandler(this.SaveButtonMenuItem_Click);
        }        

        #endregion

        #region Published Events

        /// <summary>
        /// Event trigered when button is clicked
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_OperationButtonClick, PublicationScope.Descendants)]
        public event EventHandler<DataEventArgs<string>> OperationButtonClick;

        /// <summary>
        /// LoadCancelButton
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_LoadCancelButton, PublicationScope.Descendants)]
        public event EventHandler<DataEventArgs<Button>> LoadCancelButton;

        /// <summary>
        /// Occurs when [on form master_ visible forms].
        /// </summary>
        [EventPublication(EventTopicNames.FormMaster_VisibleForms, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<EnablePanelEventArgs>> OnFormMaster_VisibleForms;

        #endregion       

        #region Properties

        #region New Button Properties

        /// <summary>
        /// Property for NewButton Text
        /// </summary>
        public string NewButtonText
        {
            get
            {
                return this.NewButton.Text;
            }

            set
            {
                this.NewButton.Text = value;
                ////this.SetButtonPosition();
            }
        }

        /// <summary>
        /// Property for User Id
        /// </summary>
        public bool NewButtonVisible
        {
            get
            {
                return this.NewButton.Visible;
            }

            set
            {
                this.NewButton.Visible = value;
                this.SetButtonPosition();
            }
        }

        /// <summary>
        /// Property for NewButton Enable/Disable
        /// </summary>
        public bool NewButtonEnable
        {
            get
            {
                return this.NewButton.Enabled;
            }

            set
            {
                this.NewButton.Enabled = value;
            }
        }

        public bool NewButtonAutoSize
        {
            get
            {
                return this.NewButton.AutoSize;
            }

            set 
            {
                this.NewButton.AutoSize = value;
                this.SetButtonPosition();
            }
        }

        /// <summary>
        /// Property for CancelButton Text
        /// </summary>
        public TerraScanButton RetrieveNewButton
        {
            get
            {
                return this.NewButton;
            }
        }

        #endregion

        #region Save Button Properties

        /// <summary>
        /// Property for SaveButton Text
        /// </summary>
        public string SaveButtonText
        {
            get
            {
                return this.SaveButton.Text;
            }

            set
            {
                this.SaveButton.Text = value;
                ////this.SetButtonPosition();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [save button visible].
        /// </summary>
        /// <value><c>true</c> if [save button visible]; otherwise, <c>false</c>.</value>
        public bool SaveButtonVisible
        {
            get
            {
                return this.SaveButton.Visible;
            }

            set
            {
                this.SaveButton.Visible = value;
                this.SetButtonPosition();
            }
        }

        /// <summary>
        /// Property for SaveButton Enable/Disable
        /// </summary>
        public bool SaveButtonEnable
        {
            get
            {
                return this.SaveButton.Enabled;
            }

            set
            {
                this.SaveButton.Enabled = value;
            }
        }

        /// <summary>
        /// SaveButtonBackColor
        /// </summary>
        public Color SaveButtonBackColor
        {
            get
            {
                return this.SaveButton.BackColor;
            }

            set
            {
                this.SaveButton.BackColor = value;
            }
        }

        public TerraScanButton.ButtonType SaveButtonType
        {
            get
            {
                return this.SaveButton.SetButtonType;
            }

            set
            {
                this.SaveButton.SetButtonType = value;
            }
        }




        /// <summary>
        /// SaveButtonForeColor
        /// </summary>
        public Color SaveButtonForeColor
        {
            get
            {
                return this.SaveButton.ForeColor;
            }
            set
            {
                this.SaveButton.ForeColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the save button tooltip.
        /// </summary>
        /// <value>The save button tooltip.</value>
        public string SaveButtonTooltip
        {
            get
            {
                return this.savButtonTooltip;
            }
            set
            {
                this.savButtonTooltip = value;
            }
        }

        #endregion

        #region Cancel Button Properties

        /// <summary>
        /// Gets or sets a value indicating whether [cancel button visible].
        /// </summary>
        /// <value><c>true</c> if [cancel button visible]; otherwise, <c>false</c>.</value>
        public bool CancelButtonVisible
        {
            get
            {
                return this.CancelButton.Visible;
            }

            set
            {
                this.CancelButton.Visible = value;
                this.SetButtonPosition();
            }
        }

        /// <summary>
        /// Property for CancelButton Enable/Disable
        /// </summary>
        public bool CancelButtonEnable
        {
            get
            {
                return this.CancelButton.Enabled;
            }

            set
            {
                this.CancelButton.Enabled = value;
            }
        }

        /// <summary>
        /// Property for CancelButton Text
        /// </summary>
        public string CancelButtonText
        {
            get
            {
                return this.CancelButton.Text;
            }

            set
            {
                this.CancelButton.Text = value;
                ////this.SetButtonPosition();
            }
        }

        /// <summary>
        /// Property for CancelButton Text
        /// </summary>
        public  TerraScanButton RetrieveCancelButton
        {
            get
            {
                return this.CancelButton;
            }
        }

        #endregion

        #region Delete Button Properties

        /// <summary>
        /// Gets or sets a value indicating whether [delete button visible].
        /// </summary>
        /// <value><c>true</c> if [delete button visible]; otherwise, <c>false</c>.</value>
        public bool DeleteButtonVisible
        {
            get
            {
                return this.DeleteButton.Visible;
            }

            set
            {
                this.DeleteButton.Visible = value;
                this.SetButtonPosition();
            }
        }

        /// <summary>
        /// Property for DeleteButton Enable/Disable
        /// </summary>
        public bool DeleteButtonEnable
        {
            get
            {
                return this.DeleteButton.Enabled;
            }

            set
            {
                this.DeleteButton.Enabled = value;
            }
        }

        /// <summary>
        /// Property for CancelButton Text
        /// </summary>
        public string DeleteButtonText
        {
            get
            {
                return this.DeleteButton.Text;
            }

            set
            {
                this.DeleteButton.Text = value;
                ////this.SetButtonPosition();
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets the space between button.
        /// </summary>
        /// <value>The space between button.</value>
        public int SpaceBetweenButton
        {
            get
            {
                return this.spaceBetweenButton;
            }

            set
            {
                this.spaceBetweenButton = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether [on off short cut keys].
        /// </summary>
        /// <value><c>true</c> if [on off short cut keys]; otherwise, <c>false</c>.</value>
        public bool OnOffShortCutKeys
        {
            set
            {
                if (value)
                {
                    this.NewButtonMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
                    this.SaveButtonMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
                }
                else
                {
                    this.NewButtonMenuItem.ShortcutKeys = Keys.None;
                    this.SaveButtonMenuItem.ShortcutKeys = Keys.None;
                }
            }
        }

        #endregion

        #region Subscribed Events

        /// <summary>
        /// Subscribed Method will Handle the Operation SmartPart Buttons Enable/Disable Based on Permissions
        /// </summary>
        /// <param name="sender">name of the SmartPart</param>
        /// <param name="e">event as Mode</param>
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
                this.NewButton.Enabled = true && NewButton.ActualPermission;
                this.SaveButton.Enabled = false;
                this.CancelButton.Enabled = false;
                this.DeleteButton.Enabled = true && DeleteButton.ActualPermission;

                //// New Button Enable/Disable (Based On User Permission On Form)
                //// Save Button Disabled 
                //// Cancel Button Disable
                //// Delete Button Enable/Disable (Based On User Permission On Form)
                //// Or 
                //// Call the Cancel Mode
            }
            else if (mode.Equals(TerraScanCommon.ButtonActionMode.EditMode))
            {
                this.NewButton.Enabled = false;
                this.SaveButton.Enabled = true && this.permissionEdit;
                this.CancelButton.Enabled = true;
                this.DeleteButton.Enabled = false;
                ////DeleteButton.Enabled = true && DeleteButton.ActualPermission;

                //// New Button Disable
                //// Save Button Enable
                //// Cancel Button Enable
                //// Delete Button Enable/Disable (Based On User Permission On Form)
            }
            else if (mode.Equals(TerraScanCommon.ButtonActionMode.CancelMode))
            {
                this.NewButton.Enabled = true && NewButton.ActualPermission;
                this.SaveButton.Enabled = false;
                this.CancelButton.Enabled = false;
                this.DeleteButton.Enabled = true && DeleteButton.ActualPermission;

                //// New Button Enable/Disable (Based On User Permission On Form)
                //// Save Button Disable
                //// Cancel Button Disable
                //// Delete Button Enable/Disable (Based On User Permission On Form)
            }
            else if (mode.Equals(TerraScanCommon.ButtonActionMode.DeleteMode))
            {
                this.NewButton.Enabled = true && NewButton.ActualPermission;
                this.SaveButton.Enabled = false;
                this.CancelButton.Enabled = false;
                this.DeleteButton.Enabled = true && DeleteButton.ActualPermission;

                //// New Button Enable/Disable (Based On User Permission On Form)
                //// Save Button Disable
                //// Cancel Button Disable
                //// Delete Button Enable/Disable (Based On User Permission On Form)
                //// Or
                //// Cancel Mode
            }
            else if (mode.Equals(TerraScanCommon.ButtonActionMode.NullRecordMode))
            {
                this.NewButton.Enabled = true && NewButton.ActualPermission;
                this.SaveButton.Enabled = false;
                this.CancelButton.Enabled = false;
                this.DeleteButton.Enabled = false;

                //// this Mode is For NoRecord in the Form 
                //// New Button Enable/Disable (Based On User Permission On Form)
                //// Save Button Disable
                //// Cancel Button Disable
                //// Delete Button Disable
            }
            else if (mode.Equals(TerraScanCommon.ButtonActionMode.DisableAllContorlsMode))
            {
                this.NewButton.Enabled = false;
                this.SaveButton.Enabled = false;
                this.CancelButton.Enabled = false;
                this.DeleteButton.Enabled = false;

                //// this Mode is For NoRecord in the Form and No Controls Enabled
                //// New Button Disable
                //// Save Button Disable
                //// Cancel Button Disable
                //// Delete Button Disable
            }
        }

        /// <summary>
        /// Subscribed Method will Handle the Permissions on Buttons based on User
        /// </summary>
        /// <param name="sender">Sender As SmartPart</param>
        /// <param name="e">EventArgs as PermissionFields</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_SetPermissions, Thread = ThreadOption.UserInterface)]
        public void SetPermissions(object sender, DataEventArgs<PermissionFields> e)
        {
            PermissionFields permissionFields;
            permissionFields = e.Data;
            this.NewButton.Enabled = permissionFields.newPermission;
            this.NewButton.ActualPermission = permissionFields.newPermission;
            this.DeleteButton.Enabled = permissionFields.deletePermission;
            this.DeleteButton.ActualPermission = permissionFields.deletePermission;
            this.permissionEdit = permissionFields.editPermission;
        }

        /// <summary>
        /// used to enable or disbale buttons
        /// </summary>
        /// <param name="sender"> Action buttons</param>
        /// <param name="e">Event Args </param>
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
        /// Get cancel button
        /// </summary>
        /// <param name="sender">Cancel buttons</param>
        /// <param name="e">Event Args</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_OperationSmartPart_GetCancelButton, Thread = ThreadOption.UserInterface)]
        public void GetCancelButton(object sender, DataEventArgs<string> e)
        {
            this.LoadCancelButton(this, new DataEventArgs<Button>(this.CancelButton));
        }

        #endregion

       #region Private Methods

        /// <summary>
        /// Sets the button position.
        /// </summary>
        private void SetButtonPosition()
        {
            int nextButtonLeftPos = 1;
            foreach (Control button in this.Controls)
            {
                if (button.Visible == true)
                {
                    button.Left = nextButtonLeftPos;
                    nextButtonLeftPos += button.Width + this.spaceBetweenButton;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the OperationButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OperationButton_Click(object sender, EventArgs e)
        {
            try
            {
                //EnablePanelEventArgs visibleInfo;
                //visibleInfo.IsSlice = false;
                //visibleInfo.IsVisible = false;
                //this.OnFormMaster_VisibleForms(this, new DataEventArgs<EnablePanelEventArgs>(visibleInfo));
                this.Cursor = Cursors.WaitCursor;
                Control sourceControl = sender as Button;
                this.GetContainerControl().ActivateControl(sourceControl);
                this.OperationButtonClick(this, new DataEventArgs<string>(sourceControl.Tag.ToString().Trim()));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                //EnablePanelEventArgs visibleInfo;
                //visibleInfo.IsSlice = false;
                //visibleInfo.IsVisible = true;
                //this.OnFormMaster_VisibleForms(this, new DataEventArgs<EnablePanelEventArgs>(visibleInfo));
            }
        }

        #endregion

        #region Private Method Constructors

        /// <summary>
        /// Handles the NewButtonMenuItem click for ShortCut of New Button
        /// </summary>
        /// <param name="sender">sender as menustrip item</param>
        /// <param name="e">MenuStrip Event Args</param>
        private void SaveButtonMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = this.OperationSmartPartMenuStrip;

                if (this.ActiveControl != null)
                {
                    if (this.SaveButton.Enabled && this.SaveButton.Visible)
                    {
                        this.SaveButton.Focus();
                        this.OperationButtonClick(this, new DataEventArgs<string>("SAVE"));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the SaveButtonMenuItem click for ShortCut of Save Button
        /// </summary>
        /// <param name="sender">sender as menustrip item</param>
        /// <param name="e">MenuStrip Event Args</param>
        private void NewButtonMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.NewButton.Enabled && this.NewButton.Visible)
                {
                    this.NewButton.Focus();
                    this.OperationButtonClick(this, new DataEventArgs<string>("NEW"));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Private Method Constructors

        private void SaveButton_MouseHover(object sender, EventArgs e)
        {
            // todo:
           //// this.savButtonTooltip = "aaaaa";
            this.toolTip1.SetToolTip(this.SaveButton, this.savButtonTooltip);
        }
    }
}
