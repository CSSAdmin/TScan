//--------------------------------------------------------------------------------------------
// <copyright file="AffidavitFormSmartPart.cs" company="Congruent">
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
// 25 July 06		KARTHIKEYAN V	    Created
//*********************************************************************************/

namespace D1100
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.Common;

    /// <summary>
    /// AffidavitFormSmartPart class file
    /// </summary>
    public partial class AffidavitFormSmartPart : UserControl
    {
        #region Variable

        /// <summary>
        /// Used to store value for space between button
        /// </summary>
        private int spaceBetweenButton;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AffidavitFormSmartPart"/> class.
        /// </summary>
        public AffidavitFormSmartPart()
        {
            this.InitializeComponent();
            this.SpaceBetweenButton = 6;
        }

        #endregion        
        
        #region Event Publication

        /// <summary>
        /// EventPublication for AffidavitFormButtonClick
        /// </summary>
        [EventPublication(EventTopics.D1100_F1107_AffidavitFormSmartPart_AffidavitFormButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> AffidavitFormButtonClick;

        /// <summary>
        /// EventPublication for ReceiptFormButtonClick
        /// </summary>
        [EventPublication(EventTopics.D1100_F1107_AffidavitFormSmartPart_ReceiptFormButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> ReceiptFormButtonClick;

        /// <summary>
        /// EventPublication for ViewAfdvtButtonClick
        /// </summary>
        [EventPublication(EventTopics.D1100_F1107_AffidavitFormSmartPart_ViewAfdvtButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> ViewAfdvtButtonClick;

        /// <summary>
        /// EventPublication for ViewAfdvtButtonClick
        /// </summary>
        [EventPublication(EventTopics.D1100_F1108_AffidavitFormSmartPart_ReportButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> ReportButtonClick;

        /// <summary>
        /// EventPublication for SubmitQueueButtonClick
        /// </summary>
        [EventPublication(EventTopics.D1100_F1109_AffidavitFormSmartPart_SubmitQueueButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> SubmitQueueButtonClick;

        #endregion

        #region Property

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
        /// Property for User Id
        /// </summary>
        public bool ReportButtonVisible
        {
            get
            {
                return this.ReportButton.Visible;
            }

            set
            {
                this.ReportButton.Visible = value;
                this.SetButtonPosition();
            }
        }

        /// <summary>
        /// Property for User Id
        /// </summary>
        public bool SubmitQueueButtonVisible
        {
            get
            {
                return this.SubmitQueueButton.Visible;
            }

            set
            {
                this.SubmitQueueButton.Visible = value;
                this.SetButtonPosition();
            }
        }

        #endregion

        #region Event Subscription

        /// <summary>
        /// Buttons the status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D1100_F1107_AffidavitFormSmartPart_DisableButtons, Thread = ThreadOption.UserInterface)]
        public void DisableButtons(object sender, DataEventArgs<string> e)
        {
            this.AffidavitFormButton.Enabled = false;
            this.ReceiptFormButton.Enabled = false;
            this.ViewAfdvtButton.Enabled = false;            
            ////this.SubmitQueueButton.Enabled = false;
        }

        /// <summary>
        /// Buttons the status.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D1100_F1107_AffidavitFormSmartPart_EnableButtons, Thread = ThreadOption.UserInterface)]
        public void EnableButtons(object sender, DataEventArgs<string> e)
        {
            this.AffidavitFormButton.Enabled = true;
            this.ReceiptFormButton.Enabled = true;
            this.ViewAfdvtButton.Enabled = true;            
            ////this.SubmitQueueButton.Enabled = true;
        }

        #endregion       

        #region Methods

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

        #region Events

        /// <summary>
        /// Handles the Click event of the AffidavitFormButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AffidavitFormButton_Click(object sender, EventArgs e)
        {
            this.AffidavitFormButtonClick(this, new DataEventArgs<string>("0"));
        }

        /// <summary>
        /// Handles the Click event of the ReceiptFormButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReceiptFormButton_Click(object sender, EventArgs e)
        {
            this.ReceiptFormButtonClick(this, new DataEventArgs<string>("0"));
        }

        /// <summary>
        /// Handles the Click event of the ViewAfdvtButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ViewAfdvtButton_Click(object sender, EventArgs e)
        {
            this.ViewAfdvtButtonClick(this, new DataEventArgs<string>("0"));
        }

        /// <summary>
        /// Handles the Click event of the ReportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ReportButton_Click(object sender, EventArgs e)
        {
            this.ReportButtonClick(this, new DataEventArgs<string>("0"));
        }

        /// <summary>
        /// Handles the Click event of the SubmitQueueButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SubmitQueueButton_Click(object sender, EventArgs e)
        {
            this.SubmitQueueButtonClick(this, new DataEventArgs<string>("0"));
        }

        #endregion
    }
}
