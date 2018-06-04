// -------------------------------------------------------------------------------------------
// <copyright file="F9042TemplateNameComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F1440BatchButtonComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		            Description
// ----------		---------		        --------------------------------------------------
// 27/11/08         A.Shanmuga Sundaram     Create
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F9042TemplateNameComp
    /// </summary>
    public static class F9042TemplateNameComp
    {
        #region F9042 Analytics Template Selection

        /// <summary>
        /// F9042_s the get template.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns>F9042ExcelAnalyticsData</returns>
        public static F9042ExcelAnalyticsData F9042_GetTemplate(int templateId)
        {
            F9042ExcelAnalyticsData excelAnalyticsData = new F9042ExcelAnalyticsData();
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            Utility.LoadDataSet(excelAnalyticsData.GetTemplate, "f9042_pcget_Template", ht);
            return excelAnalyticsData;
        }

        /// <summary>
        /// F9042_s the list template.
        /// </summary>
        /// <param name="queryView">The query view.</param>
        /// <returns>F9042ExcelAnalyticsData</returns>
        public static F9042ExcelAnalyticsData F9042_ListTemplate(string queryView)
        {
            F9042ExcelAnalyticsData excelAnalyticsData = new F9042ExcelAnalyticsData();
            Hashtable ht = new Hashtable();
            ht.Add("@QueryView", queryView);
            Utility.LoadDataSet(excelAnalyticsData.ListTemplate, "f9042_pclst_Template", ht);
            return excelAnalyticsData;
        }

        #endregion F9042 Analytics Template Selection
    }
}
