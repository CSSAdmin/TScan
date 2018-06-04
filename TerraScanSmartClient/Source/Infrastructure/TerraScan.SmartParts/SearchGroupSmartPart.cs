//--------------------------------------------------------------------------------------------
// <copyright file="SearchGroupSmartPart.cs" company="Congruent">
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

namespace TerraScan.SmartParts
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
    /// SearchGroupSmartPart class file
    /// </summary>
    public partial class SearchGroupSmartPart : UserControl
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:SearchGroupSmartPart"/> class.
        /// </summary>
        public SearchGroupSmartPart()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// D9001_TerrascanSmartParts_SearchGroupSmartPart_SearchButtonClick
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_SearchGroupSmartPart_SearchButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> SearchButtonClick;

        /// <summary>
        /// D9001_TerrascanSmartParts_SearchGroupSmartPart_ClearButtonClickClick
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_SearchGroupSmartPart_ClearButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> ClearButtonClick;

        /// <summary>
        /// D9001_TerrascanSmartParts_SearchGroupSmartPart_CancelButtonClick
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_SearchGroupSmartPart_CancelButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<string>> CancelButtonClick;

        #endregion

        #region Events

        /// <summary>
        /// Handles the Click event of the SearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.SearchButtonClick(this, new DataEventArgs<string>("0"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the ClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ClearButtonClick(this, new DataEventArgs<string>("0"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.CancelButtonClick(this, new DataEventArgs<string>("0"));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion      
    }
}
