//--------------------------------------------------------------------------------------------
// <copyright file="ReportActionSmartPart.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the ReportActionSmartPart.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 27 JUL 06		RANJANI JG	    Created
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
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;

    /// <summary>
    /// ReportAction SmartPart 
    /// </summary>
    [SmartPart]
    public partial class ReportActionSmartPart : PrimaryBaseSmartPart
    {
        /// <summary>
        /// Used to store value for space between button
        /// </summary>
        private int spaceBetweenButton;

        /// <summary>
        /// length of visible buttons
        /// </summary>
        private int visibleButtonLength;       

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ReportActionSmartPart"/> class.
        /// </summary>
        public ReportActionSmartPart()
        {
            this.InitializeComponent();
            this.SpaceBetweenButton = 6;
        }

        #region Properties

        /// <summary>
        /// Property for PrintButton visibility
        /// </summary>
        public bool PrintButtonVisible
        {
            get
            {
                return this.PrintButton.Visible;
            }

            set
            {
                this.PrintButton.Visible = value;
                this.SetButtonPosition();
            }
        }

        /// <summary>
        /// Property for PreviewButton visibility
        /// </summary>
        public bool PreviewButtonVisible
        {
            get
            {
                return this.PreviewButton.Visible;
            }

            set
            {
                this.PreviewButton.Visible = value;
                this.SetButtonPosition();
            }
        }

        /// <summary>
        /// Property for EmailButton visibility
        /// </summary>
        public bool EmailButtonVisible
        {
            get
            {
                return this.EmailButton.Visible;
            }

            set
            {
                this.EmailButton.Visible = value;
                this.SetButtonPosition();
            }
        }

        /// <summary>
        /// Property for DetailsButton visibility
        /// </summary>
        public bool DetailsButtonVisible
        {
            get
            {
                return this.DetailsButton.Visible;
            }

            set
            {
                this.DetailsButton.Visible = value;
                this.SetButtonPosition();
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
        /// Gets or sets the length of the visible button.
        /// </summary>
        /// <value>The length of the visible button.</value>
        public int VisibleButtonLength
        {
            get { return visibleButtonLength; }
        }

        #endregion Properties

        #region Publications and Subcriptions

        /// <summary>
        /// PrintButtonClick Event
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_PrintButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PrintButtonClick;

        /// <summary>
        /// PreviewButtonClick Event
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_PreviewButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> PreviewButtonClick;

        /// <summary>
        /// EmailButtonClick Event
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_EmailButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> EmailButtonClick;

        /// <summary>
        /// DetailsButtonClick Event
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_ReportActionSmartPart_DetailsButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> DetailsButtonClick;

        #endregion

        /// <summary>
        /// Handles the Click event of the PrintButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PrintButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.PrintButtonClick(this, new DataEventArgs<Button>(sender as Button));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the PreviewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.PreviewButtonClick(this, new DataEventArgs<Button>(sender as Button));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the EmailButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EmailButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.EmailButtonClick(this, new DataEventArgs<Button>(sender as Button));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the DetailsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DetailsButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.DetailsButtonClick(this, new DataEventArgs<Button>(sender as Button));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the EmailButtonMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EmailButtonMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.EmailButton.Enabled && this.EmailButton.Visible)
                {
                    this.EmailButton.Focus();
                    this.EmailButtonClick(this, new DataEventArgs<Button>(this.EmailButton));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the PreviewButtonMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PreviewButtonMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.PreviewButton.Enabled && this.PreviewButton.Visible)
                {
                    this.PreviewButton.Focus();
                    this.PreviewButtonClick(this, new DataEventArgs<Button>(this.PreviewButton));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the PrintButtonMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void PrintButtonMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.PrintButton.Enabled && this.PrintButton.Visible)
                {
                    this.PrintButton.Focus();
                    this.PrintButtonClick(this, new DataEventArgs<Button>(this.PrintButton));
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

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

            this.visibleButtonLength = nextButtonLeftPos;
        }
    }
}
