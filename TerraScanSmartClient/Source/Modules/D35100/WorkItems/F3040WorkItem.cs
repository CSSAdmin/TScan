//--------------------------------------------------------------------------------------------
// <copyright file="F3040WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F3040WorkItem.cs. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15/05/2007   	M.Vijayakumar      Created
//*********************************************************************************/

namespace D35100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using System.Windows.Forms;
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F3040WorkItem Class file 
    /// </summary>
    public class F3040WorkItem : WorkItem
    {
        #region F3040 Zoning

        #region F3040 Get Zoning

        /// <summary>
        /// Used to Get the Zoning Details
        /// </summary>
        /// <returns>Gets Typed DataSet containing the Zoning Details.</returns>
        public F3040ZoningData F3040_GetZoningDetails()
        {
            return WSHelper.F3040_GetZoningDetails();
        }

        #endregion F3040 Get Zoning

        #region F3040 Save Zoning

        /// <summary>
        /// Used to Save the Zoning Details
        /// </summary>
        /// <param name="zoningDetails">The zoning details.</param>
        /// <returns>Typed DataSet containing the Zoning Details to Save.</returns>
        public int F3040_SaveZoningDetails(string zoningDetails,int userId)
        {
            return WSHelper.F3040_SaveZoningDetails(zoningDetails, userId);
        }

        #endregion F3040 Save Zoning

        #endregion F3040 Zoning

        #region Attachment

        /// <summary>
        /// Gets the Attachments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns> The count of comments.</returns>
        public int GetAttachmentCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetAttachmentCount(formId, keyId, userId);
        }

        #endregion Attachment

        #region Comments

        /// <summary>
        /// Gets the comments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns> The count of comments.</returns>
        public CommentsData.GetCommentsCountDataTable GetCommentsCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetCommentsCount(formId, keyId, userId).GetCommentsCount;
        }

        #endregion Comments

        #region WorkItem Common Methods

        /// <summary>
        /// Fires the <see cref="RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="Run"/>
        /// method is called on the <see cref="WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Fires the <see cref="Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        #endregion WorkItem Common Methods
    }
}
