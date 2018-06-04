// -------------------------------------------------------------------------------------------
// <copyright file="F9008ReportDetailsComp.cs" company="Congruent">
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
    /// Main class for ReportDetails Component
    /// </summary>
    public static class F9008ReportDetailsComp
    {
        /// <summary>
        /// Get F9008report details.
        /// </summary>
        /// <param name="userId">The userID.</param>        
        /// <returns>F9008ReportDetailsData</returns>
        public static F9008ReportDetailsData F9008GetReportDetails(int userId)
        {            
            F9008ReportDetailsData form9008ReportDetailsData = new F9008ReportDetailsData();
            Hashtable ht = new Hashtable();            
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(form9008ReportDetailsData, "f9007_pclst_ReportPrinter", ht, new string[] { form9008ReportDetailsData.F9008GetReportDetails.TableName });
            return form9008ReportDetailsData;
        }

        #region SaveReportDetails

        /// <summary>
        /// To Save Report Details.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="printerConf">The printer conf.</param>
        public static void F9008_SaveReportDetails(int userId, string printerConf)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            ht.Add("@PrinterConf", printerConf);
            Utility.ImplementProcedure("f9008_pcupd_ReportPrinter", ht);
        }

        #endregion SaveReportDetails
    }
}
