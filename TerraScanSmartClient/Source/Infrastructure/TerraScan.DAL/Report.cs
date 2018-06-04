// -------------------------------------------------------------------------------------------
// <copyright file="Report.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access report related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;

    /// <summary>
    /// Main Class for the Reports Component
    /// </summary>
  public static class Report
    {
        /// <summary>
        /// Gets the Reports details
        /// </summary>
        /// <param name="reportId">The report id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>DataSet With Details Path Of report</returns>
      public static DataSet GetReportDetails(int reportId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@Report", reportId);
            ht.Add("@UserID", userId);
            return DataProxy.FetchDataSet("f9007_pcget_ReportDetails", ht);
        }

        /// <summary>
        /// Gets the auto print status.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Returns true/false</returns>
      public static int GetAutoPrintStatus(int formId, int userId)
      {
          Hashtable ht = new Hashtable();
          ht.Add("@FormID", formId);
          ht.Add("@UserID", userId);
          ////return DataProxy.FetchSPOutput("f9007_pcchk_AutoPrint", ht);
          return Utility.FetchSPOutput("f9007_pcchk_AutoPrint", ht);
      }

      /// <summary>
      /// Saves the auto print.
      /// </summary>
      /// <param name="formId">The form id.</param>
      /// <param name="userId">The user id.</param>
      /// <param name="autoPrint">if set to <c>true</c> [auto print].</param>
      public static void SaveAutoPrint(int formId, int userId, bool autoPrint)
      {
          Hashtable ht = new Hashtable();
          ht.Add("@FormID", formId);
          ht.Add("@UserID", userId);
          ht.Add("@IsAutoPtint", autoPrint);
          DataProxy.ExecuteSP("f9007_pcins_AutoPrint", ht);
      }
    }
}
