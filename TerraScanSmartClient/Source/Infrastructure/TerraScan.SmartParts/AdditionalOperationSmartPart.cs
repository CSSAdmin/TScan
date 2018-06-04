//--------------------------------------------------------------------------------------------
// <copyright file="AdditionalOperationSmartPart.cs" company="Congruent">
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
// 20/4/2009        Malliga             if the attachment file exceeds 2 digit, the value is not displaying properly(Bug Id : 5986)
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
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;

    /// <summary>
    /// AdditionalOperationSmartPart class file
    /// </summary>
    [SmartPart]
    public partial class AdditionalOperationSmartPart : PrimaryBaseSmartPart
    {
        #region Private Variables

        /// <summary>
        ///  Variable Holds the defaultAttachmentButtonBackColor
        /// </summary>
        private Color defaultAttachmentButtonBackColor = Color.FromArgb(174, 150, 94);
       
        /// <summary>
        ///  Variable Holds the defaultAttachmentButtonBackColor
        /// </summary>
        private Color defaultCommentButtonBackColor = Color.FromArgb(174, 150, 94);

        /// <summary>
        ///  Variable Holds the defaultAttachmentButtonBackColor
        /// </summary>
        private Color highPriorityCommentColor = Color.FromArgb(255, 0, 0);

        /// <summary>
        /// Variable Holds the formId
        /// </summary>
        private int currentFormId;

        /// <summary>
        /// Variable Holds the comment formId
        /// </summary>
        private int commentFormId;

        /// <summary>
        /// Variable Holds the keyId
        /// </summary>
        private int keyId;

        /// <summary>
        /// Holds the Parent FormId for Handling Permissions
        /// </summary>
        private int parentFormId;

        /// <summary>
        /// variable Holds the ParentWorkItem
        /// </summary>
        private WorkItem parentWorkItem;

        /// <summary>
        /// variable holds the vertivalButtons 
        /// </summary>
        private bool verticalButtons;

        /// <summary>
        /// variable holds the horizontalButtons
        /// </summary>
        private bool horizontalButtons;

        /// <summary>
        /// Used to store value for space between button
        /// </summary>
        private int spaceBetweenButton;

        /// <summary>
        /// Used to store value for height between button
        /// </summary>
        private int heightBetweenButton;

        /// <summary>
        /// Used to check whether it is used in formslice
        /// </summary>
        private bool commentFormIdDiffers;

        /// <summary>
        /// instance for AdditionalOperationCount Entity to access 
        /// </summary>
        private AdditionalOperationCountEntity additionalOperationCountEntity;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AdditionalOperationSmartPart"/> class.
        /// </summary>
        public AdditionalOperationSmartPart()
        {
            this.InitializeComponent();
            this.SpaceBetweenButton = 6;
            this.HeightBetweenButton = 5;
        }

        #endregion

        #region Properties       

        /// <summary>
        /// Gets or sets the color of the default comment button back.
        /// </summary>
        /// <value>The color of the default comment button back.</value>
        public Color DefaultCommentButtonBackColor
        {
            get 
            {
                return this.defaultCommentButtonBackColor; 
            }

            set 
            {
                this.defaultCommentButtonBackColor = value;
                this.CommentButton.BackColor = this.defaultCommentButtonBackColor;
            }
        }

        /// <summary>
        /// Gets or sets the type of the comment button.
        /// </summary>
        /// <value>The type of the comment button.</value>
        public TerraScanButton.ButtonType CommentButtonType
        {
            get
            {
                return this.CommentButton.SetButtonType;
            }

            set
            {
                this.CommentButton.SetButtonType = value;
                this.defaultCommentButtonBackColor = this.CommentButton.BackColor;
            }
        }

        /// <summary>
        /// Gets or sets the type of the comment button.
        /// </summary>
        /// <value>The type of the comment button.</value>
        public TerraScanButton.ButtonType AttachmentButtonType
        {
            get
            {
                return this.AttachmentButton.SetButtonType;
            }

            set
            {
                this.AttachmentButton.SetButtonType = value;
                this.defaultAttachmentButtonBackColor = this.AttachmentButton.BackColor;
            }
        }

        /// <summary>
        /// Gets or sets the color of the default attachment button back.
        /// </summary>
        /// <value>The color of the default attachment button back.</value>
        public Color DefaultAttachmentButtonBackColor
        {
            get 
            {
                return this.defaultAttachmentButtonBackColor; 
            }
            
            set
            {
                this.defaultAttachmentButtonBackColor = value;
                this.AttachmentButton.BackColor = this.defaultCommentButtonBackColor;
            }
        }

        /// <summary>
        /// Gets or sets the color of the high priority comment back.
        /// </summary>
        /// <value>The color of the high priority comment back.</value>
        public Color HighPriorityCommentColor
        {
            get { return this.highPriorityCommentColor; }
            set { this.highPriorityCommentColor = value; }
        }

        /// <summary>
        /// Property to Set and Get the Current FormId
        /// </summary>
        public int CurrntFormId
        {
            get { return this.currentFormId; }
            set { this.currentFormId = value; }
        }

        /// <summary>
        /// Property to Set and Get the Comment FormId
        /// </summary>
        public int CommentFormId
        {
            get { return this.commentFormId; }
            set { this.commentFormId = value; }
        }

        /// <summary>
        /// Property to Set and Get the ParentFormId
        /// </summary>
        public int ParentFormId
        {
            get { return this.parentFormId; }
            set { this.parentFormId = value; }
        }

        /// <summary>
        /// Property to Set and Get the keyId
        /// </summary>
        public int KeyId
        {
            get { return this.keyId; }
            set { this.keyId = value; }
        }

        /// <summary>
        /// Property to Set and Get the ParentWorkItem
        /// </summary>
        public WorkItem ParentWorkItem
        {
            get { return this.parentWorkItem; }
            set { this.parentWorkItem = value; }
        }

        /// <summary>
        /// Property to Access AdditionalOperationCountEntity
        /// </summary>
        public AdditionalOperationCountEntity AdditionalOperationCountEnt
        {
            get 
            {
                return this.additionalOperationCountEntity; 
            }

            set 
            {
                this.additionalOperationCountEntity = value;
                this.SetText(this.additionalOperationCountEntity);
            }
        }

        /// <summary>
        /// Property Holds the Horizontal Buttons
        /// </summary>
        public bool HorizontalButtons
        {
            get
            {
                return this.horizontalButtons;
            }

            set
            {
                this.horizontalButtons = value;
                this.SetHorizontalButtons();
            }
        }

        /// <summary>
        /// Property Holds the Vertical Buttons
        /// </summary>
        public bool VerticalButtons
        {
            get
            {
                return this.verticalButtons;
            }

            set
            {
                this.verticalButtons = value;
                this.SetVerticalButtons();
            }
        }

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
        /// Gets or sets the space between button.
        /// </summary>
        /// <value>The space between button.</value>
        public int HeightBetweenButton
        {
            get
            {
                return this.heightBetweenButton;
            }

            set
            {
                this.heightBetweenButton = value;
            }
        }

        /// <summary>
        /// Property for AttachmentButton visibility
        /// </summary>
        public bool AttachmentButtonVisible
        {
            get
            {
                return this.AttachmentButton.Visible;
            }

            set
            {
                this.AttachmentButton.Visible = value;
                this.SetButtonPosition();
            }
        }

        /// <summary>
        /// Property for AttachmentButton visibility
        /// </summary>
        public bool CommentButtonVisible
        {
            get
            {
                return this.CommentButton.Visible;
            }

            set
            {
                this.CommentButton.Visible = value;
                this.SetButtonPosition();
            }
        }

        /// <summary>
        /// Property to check wheather it is used in formslice
        /// </summary>
        public bool IsCommentFormIdDiffers
        {
            get
            {
                return this.commentFormIdDiffers;
            }

            set
            {
                this.commentFormIdDiffers = value;                
            }
        }

        #endregion

        #region Subscribed Events

        /// <summary>
        /// Sets the button text.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_AdditionalOperationSmartPart_SetButtonText, Thread = ThreadOption.UserInterface)]
        public void SetButtonText(object sender, DataEventArgs<AdditionalOperationCountEntity> e)
        {
            if (e.Data.AttachmentCount <= 0)
            {
                this.AttachmentButton.Text = "Attachment";
            }
            else
            {
                this.AttachmentButton.Text = "Attachment" + "(" + e.Data.AttachmentCount + ")";
            }

            if (e.Data.CommentCount <= 0)
            {
                this.CommentButton.Text = "Comment";
            }
            else
            {
                this.CommentButton.Text = "Comment" + "(" + e.Data.CommentCount + ")";
            }

            if (e.Data.HighPriority)
            {
                this.CommentButton.BackColor = this.highPriorityCommentColor;
                this.CommentButton.CommentPriority = true;
            }
            else
            {
                this.CommentButton.BackColor = this.defaultCommentButtonBackColor;
                this.CommentButton.CommentPriority = false;
            }
        }

        /// <summary>
        /// Enables the attachment and comments buttons.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e. (attachmentButtonEnabled for attachment button
        /// commentButtonEnabled for comment button 0 for disable and 1 for enabled)</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_AdditionalOperationSmartPart_EnableButtons, Thread = ThreadOption.UserInterface)]
        public void EnableButtons(object sender, DataEventArgs<AdditionalOperationEntity> e)
        {
            if (e.Data.AttachmentButtonEnabled == 0)
            {
                this.AttachmentButton.Enabled = false;
            }

            if (e.Data.AttachmentButtonEnabled == 1)
            {
                this.AttachmentButton.Enabled = true;
            }

            if (e.Data.CommentButtonEnabled == 0)
            {
                this.CommentButton.Enabled = false;
            }

            if (e.Data.CommentButtonEnabled == 1)
            {
                this.CommentButton.Enabled = true;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the Click event of the AttachmentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AttachmentButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                object[] optionalParameter = new object[] { this.currentFormId, this.keyId, this.parentFormId };

                Form attachmentForm = new Form();
                //attachmentForm.Icon = @"D:\Workarea\TerraScan-T.ico";
                if (Convert.ToBoolean(TerraScanCommon.GetFormInfo(this.currentFormId).openPermission))
                {
                    attachmentForm = TerraScanCommon.GetForm(9005, optionalParameter, this.ParentWorkItem);
                    attachmentForm.Tag = this.currentFormId;
                    if (attachmentForm != null)
                    {
                        attachmentForm.ShowDialog();

                        // Code Need to be Modified to Set the Text For Attachmnent/Comment Button (Get the Count,Flag From Attachment/Comment Form Makin Public Propertis.
                        AdditionalOperationCountEntity additionalOperationCountEnt;
                        additionalOperationCountEnt = new AdditionalOperationCountEntity(-999, -999, false);
                        additionalOperationCountEnt.AttachmentCount = Convert.ToInt16(TerraScanCommon.GetValue(attachmentForm, "AttachmentCount"));
                        this.SetText(additionalOperationCountEnt);
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("OpenPermission"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the CommentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CommentButton_Click(object sender, EventArgs e)
        {
           //// this.CommentButtonClick(this, new DataEventArgs<string>(string.Empty));
           
            try
            {
                this.Cursor = Cursors.WaitCursor; 
                object[] optionalParameter;
                int currentCommentForm;
                if (this.commentFormIdDiffers)
                {
                    currentCommentForm = this.commentFormId;
                }
                else
                {
                    currentCommentForm = this.currentFormId;
                }

                if (Convert.ToBoolean(TerraScanCommon.GetFormInfo(currentCommentForm).openPermission))
                {
                    optionalParameter = new object[] { currentCommentForm, this.keyId, this.parentFormId };

                    Form commentForm = new Form();
                    commentForm = TerraScanCommon.GetForm(9075, optionalParameter, this.ParentWorkItem);
                    commentForm.Tag = currentCommentForm;

                    if (commentForm != null)
                    {
                        commentForm.ShowDialog();

                        // Code Need to be Modified to Set the Text For Attachmnent/Comment Button (Get the Count,Flag From Attachment/Comment Form Makin Public Propertis.
                        AdditionalOperationCountEntity additionalOperationCountEnt;
                        additionalOperationCountEnt = new AdditionalOperationCountEntity(-999, -999, false);
                        additionalOperationCountEnt.CommentCount = Convert.ToInt16(TerraScanCommon.GetValue(commentForm, "CommentCount"));
                        additionalOperationCountEnt.HighPriority = Convert.ToBoolean(TerraScanCommon.GetValue(commentForm, "HighPriorityFlag"));
                        this.SetText(additionalOperationCountEnt);
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("OpenPermission"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="additionalOperationCountEntity">The additional operation count entity.</param>
        private void SetText(AdditionalOperationCountEntity additionalOperationCountEntity)
        {
            ////if not -999 reset text
            if (additionalOperationCountEntity.AttachmentCount != -999)
            {
                if (additionalOperationCountEntity.AttachmentCount <= 0)
                {
                    this.AttachmentButton.Text = "Attachment";
                }
                else
                {
                    this.AttachmentButton.Text = "Attachment" + "(" + additionalOperationCountEntity.AttachmentCount + ")";
                }
            }

            if (additionalOperationCountEntity.CommentCount != -999)
            {
                if (additionalOperationCountEntity.CommentCount <= 0)
                {
                    this.CommentButton.Text = "Comment";
                }
                else
                {
                    this.CommentButton.Text = "Comment" + "(" + additionalOperationCountEntity.CommentCount + ")";
                }

                if (additionalOperationCountEntity.HighPriority)
                {
                    this.CommentButton.BackColor = this.highPriorityCommentColor;
                    this.CommentButton.CommentPriority = true;
                }
                else
                {
                    this.CommentButton.BackColor = this.defaultCommentButtonBackColor;
                    this.CommentButton.CommentPriority = false;
                }
            }
        }

        /// <summary>
        /// Sets the Horizontal button position
        /// </summary>
        private void SetHorizontalButtons()
        {
            int nextButtonLeftPos = 5;
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
        /// Sets the vertical button position .
        /// </summary>
        private void SetVerticalButtons()
        {
            int nextButtonLeftPos = 5;
            int nextButtonTopPos = 5;
            foreach (Control button in this.Controls)
            {
                if (button.Visible == true)
                {
                    button.Left = nextButtonLeftPos;
                    button.Top = nextButtonTopPos;
                    nextButtonTopPos += button.Top + button.Height + this.HeightBetweenButton;
                }
            }
        }

        #endregion   

        #region ToBe Included For Generalization

        /*
        /// <summary>
        /// Handles the Click event of the CommentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CommentButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ////object[] optionalParameter = new object[] { (this.Tag.ToString()), Convert.ToInt32(this.CurrentImportId), Convert.ToInt32(this.Tag.ToString()) };
                object[] optionalParameter = new object[] { this.parentFormId, this.keyId, this.parentFormId };
                Form commentForm = new Form();
                commentForm = TerraScanCommon.GetForm(9075, optionalParameter, this.ParentWorkItem);
                if (commentForm != null)
                {
                    commentForm.ShowDialog();
                    // Code Need to be Modified to Set the Text For Attachmnent/Comment Button (Get the Count,Flag From Attachment/Comment Form Makin Public Propertis.
                    AdditionalOperationCountEntity additionalOperationCountEnt;
                    additionalOperationCountEnt = new AdditionalOperationCountEntity(WSHelper.GetAttachmentCount(ParentFormId, KeyId, TerraScanCommon.UserId), WSHelper.GetCommentsCount(parentFormId, KeyId, TerraScanCommon.UserId), true);
                    this.SetText(additionalOperationCountEnt);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
          
         */

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

        #endregion

        #region ToolTip For Attachment and Comment Button
        ////Coding Added for the issue 5986 by Malliga on 20/4/2009
        /// <summary>
        /// Handles the MouseHover event of the AttachmentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AttachmentButton_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.AttachmentToolTip.SetToolTip(this.AttachmentButton, this.AttachmentButton.Text);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
               
        #endregion

        #region Menu Events

        /// <summary>
        /// Handles the Click event of the CommentButtonMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CommentButtonMenuItem_Click(object sender, System.EventArgs e)
        {
            if (this.CommentButton.Enabled && this.CommentButton.Visible)
            {
                this.CommentButton.Focus();
                this.CommentButton_Click(this.CommentButton, null);
            }
        }

        /// <summary>
        /// Handles the Click event of the AttachmentButtonMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void AttachmentButtonMenuItem_Click(object sender, System.EventArgs e)
        {
            if (this.AttachmentButton.Enabled && this.AttachmentButton.Visible)
            {
                this.AttachmentButton.Focus();
                this.AttachmentButton_Click(this.AttachmentButton, null);
            }
        }

        #endregion Menu Events
    }
}
