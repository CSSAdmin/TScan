//----------------------------------------------------------------------------------
// <copyright file="F9042WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Recipting.
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			  Author		         Description
// ----------	  ----------		     -------------------------------------------
// 27/11/2008     A.Shanmuga Sundaram    Created
//*********************************************************************************/

namespace D9030
{
    #region NameSpaces

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

    #endregion NameSpaces

    /// <summary>
    /// F9041WorkItem
    /// </summary>
    public class F9042WorkItem : WorkItem 
    {
        #region F9042 Analytics Template Selection

        /// <summary>
        /// F9042_s the get template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>F9042ExcelAnalyticsData</returns>
        public F9042ExcelAnalyticsData F9042_GetTemplate(int templateId)
        {
            return WSHelper.F9042_GetTemplate(templateId);
        }

        /// <summary>
        /// F9042_s the list template.
        /// </summary>
        /// <param name="queryView">The query view.</param>
        /// <returns>F9042ExcelAnalyticsData</returns>
        public F9042ExcelAnalyticsData F9042_ListTemplate(string queryView)
        {
            return WSHelper.F9042_ListTemplate(queryView);
        }

        #endregion F9042 Analytics Template Selection

        #region GetConfigValue

        /// <summary>
        /// Gets the Image Patha
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The dataset containing the Config value.</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        #endregion GetConfigValue

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
