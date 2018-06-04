// -------------------------------------------------------------------------------------------
// <copyright file="F9070ReportListingComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access receipt header related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    #region Namespace
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;
    #endregion Namespace

    /// <summary>
    /// F9070ReportListingComp
    /// </summary>
    public static class F9070ReportListingComp
    {
        /// <summary>
        /// F9070s the get report listing details.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>form9070ReportListingData</returns>
        public static F9070ReportListingData F9070GetReportListingDetails(int userId)
        {
            F9070ReportListingData form9070ReportListingData = new F9070ReportListingData();
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            string[] tableName = new string[] { form9070ReportListingData.ReportHeader.TableName, form9070ReportListingData.F9070GetReportListing.TableName };
            Utility.LoadDataSet(form9070ReportListingData, "f9070_pclst_ReportListing", ht, tableName);
            return form9070ReportListingData;
        }
    }
}
