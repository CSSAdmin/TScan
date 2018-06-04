// -------------------------------------------------------------------------------------------------
// <copyright file="F36010WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------
namespace D36010
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
    public class F36010WorkItem : WorkItem
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

        #region F36010 Misc Improvement Catalog

        #region Get Misc Improvement Catalog

        /// <summary>
        /// Get Misc Improvement Catalog
        /// </summary>
        /// <param name="miscCodeId">miscCodeId</param>
        /// <returns>1</returns>
        public F36010MiscImprovementCatalog F36010_GetMiscImprovementCatalog(int miscCodeId)
        {
            return WSHelper.F36010_GetMiscImprovementCatalog(miscCodeId);
        }

        #endregion Get Misc Improvement Catalog

        #region Save Misc Improvement Catalog

        /// <summary>
        /// Save Misc Improvement Catalog
        /// </summary>
        /// <param name="miscCodeId">miscCodeID</param>
        /// <param name="miscCatalogItems">miscCatalogItems</param>
        /// <param name="userId">UserID</param>
        /// <returns>1</returns>
        public int F36010_SaveMiscImprovementCatalog(int miscCodeId, string miscCatalogItems, int userId, string miscCatalogChoiceItems)
        {
            return WSHelper.F36010_SaveMiscImprovementCatalog(miscCodeId, miscCatalogItems, userId, miscCatalogChoiceItems);
        }

        #endregion Save Misc Improvement Catalog

        #region Delete Misc Improvement Catalog

        /// <summary>
        /// F36010_s the delete misc improvement catalog.
        /// </summary>
        /// <param name="miscCodeId">The misc code ID.</param>
        /// <param name="userId">UserID</param>
        public void F36010_DeleteMiscImprovementCatalog(int miscCodeId, int userId)
        {
            WSHelper.F36010_DeleteMiscImprovementCatalog(miscCodeId, userId);
        }

        #endregion Delete Misc Improvement Catalog

        #region Check Misc Improvement Catalog

        /// <summary>
        /// F36010_s the check misc improvement catalog.
        /// </summary>
        /// <param name="miscCodeId">The misc code ID.</param>
        /// <param name="miscCode">The misc code.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>1</returns>
        public int F36010_CheckMiscImprovementCatalog(int miscCodeId, string miscCode, int rollYear)
        {
            return WSHelper.F36010_CheckMiscImprovementCatalog(miscCodeId, miscCode, rollYear);
        }

        #endregion Check Misc Improvement Catalog

        #region List DeprTable

        /// <summary>
        /// F36010_s the list depr table.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>miscImprovementData</returns>
        public F36010MiscImprovementCatalog F36010_ListDeprTable(int rollYear)
        {
            return WSHelper.F36010_ListDeprTable(rollYear);
        }

        #endregion List DeprTable

        #endregion F36010 Misc Improvement Catalog

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
