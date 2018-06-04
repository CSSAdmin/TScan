//--------------------------------------------------------------------------------------------
// <copyright file="F81004WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F81004WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 26 Dec 08        Sadha Shivudu M    Created
//*********************************************************************************/

namespace D81003
{
    #region namespace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    #endregion namespace

    /// <summary>
    /// F81004WorkItem
    /// </summary>
    public class F81004WorkItem : WorkItem
    {
        #region CRUD Methods

        /// <summary>
        /// F81004_s the get selection details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="form">The form.</param>
        /// <returns>selection dataset</returns>
        public F81004SelectionData F81004_GetSelectionDetails(int eventId, int form)
        {
            return WSHelper.F81004_GetSelectionDetails(eventId, form);
        }

        /// <summary>
        /// F81004_s the get selection catalog details.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns>selection catalog data table</returns>
        public F81004SelectionData.GetSelectionCatalogDetailsDataTable F81004_GetSelectionCatalogDetails(int categoryId)
        {
            return WSHelper.F81004_GetSelectionCatalogDetails(categoryId);
        }

        /// <summary>
        /// F81004_s the save selection items.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="selectionItemsXml">The selection items XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>eventId</returns>
        public int F81004_SaveSelectionItems(int eventId, string selectionItemsXml, int userId)
        {
            return WSHelper.F81004_SaveSelectionItems(eventId, selectionItemsXml, userId);
        }

        #endregion CRUD Methods

        #region Base Methods

        /// <summary>
        /// Called when [run started].
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Called when [activated].
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        #endregion Base Methods.
    }
}
