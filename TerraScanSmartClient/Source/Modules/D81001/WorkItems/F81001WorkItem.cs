// -------------------------------------------------------------------------------------------
// <copyright file="F81001WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F81001</summary>
// Release history
//**********************************************************************************
// Date			    Author		         Description
// ----------		---------		     -----------------------------------------------------
// 07/11/2007       D.Latha Maheswari    Created
// 
// -------------------------------------------------------------------------------------------
namespace D81001
{
    #region NameSpace

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
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    #endregion NameSpace

    /// <summary>
    /// F36010WorkItem Class file
    /// </summary>
    public class F81001WorkItem : WorkItem
    {
        #region BusinessProcesses

        #region To Get Configuration Roll Year

        /// <summary>
        /// Gets the config Roll Year.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>GetConfigDetails</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        #endregion To Get Configuration Roll Year

        #region F81001 Misc Improvement Catalog

        #region Get Misc Improvement Catalog

        /// <summary>
        /// Get Event Fee Catalog
        /// </summary>
        /// <param name="feeCatId">feeCatId</param>
        /// <returns>DataSet</returns>
        public F81001FeeCatalogData F81001_GetEventFeeCatalog(int feeCatId)
        {
            return WSHelper.F81001_GetEventFeeCatalog(feeCatId);
        }

        #endregion Get Misc Improvement Catalog

        #region Save Misc Improvement Catalog

        /// <summary>
        /// Save Event Fee Catalog
        /// </summary>
        /// <param name="feeCatId">feeCatId</param>
        /// <param name="feeCatalogItems">feeCatalogItems</param>
        /// <param name="userId">UserID</param>
        /// <returns>integer</returns>
        public int F81001_SaveEventFeeCatalog(int feeCatId, string feeCatalogItems, int userId)
        {
            return WSHelper.F81001_SaveEventFeeCatalog(feeCatId, feeCatalogItems, userId);
        }

        #endregion Save Misc Improvement Catalog

        #region Delete Misc Improvement Catalog

        /// <summary>
        /// Delete Event Fee Catalog
        /// </summary>
        /// <param name="feeCatId">feeCatId</param>
        /// <param name="userId">UserID</param>
        public void F81001_DeleteEventFeeCatalog(int feeCatId, int userId)
        {
            WSHelper.F81001_DeleteEventFeeCatalog(feeCatId, userId);
        }

        #endregion Delete Misc Improvement Catalog

        #region Check Misc Improvement Catalog

        /// <summary>
        /// Check Event Fee Catalog
        /// </summary>
        /// <param name="feeCatId">feeCatId</param>
        /// <param name="formNumber">formNumber</param>
        /// <param name="effectiveDate">effectiveDate</param>
        /// <returns>Integer</returns>
        public int F81001_CheckEventFeeCatalog(int feeCatId, string formNumber, DateTime effectiveDate)
        {
            return WSHelper.F81001_CheckEventFeeCatalog(feeCatId, formNumber, effectiveDate);
        }

        #endregion Check Misc Improvement Catalog

        #endregion F81001 Misc Improvement Catalog

        #endregion BusinessProcesses

        #region WorkItemEvents

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

        #endregion WorkItemEvents
    }
}
