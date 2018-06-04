// -------------------------------------------------------------------------------------------
// <copyright file="F29600SeniorExemptComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F29600SeniorExemptComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
//17 Sep 2007       Ramya.D              Created
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;
    using System.Data;

    /// <summary>
    ///  Data Access Layer which talks to the DB directly for F29600
    /// </summary>
    public static class F29600SeniorExemptComp
    {
        /// <summary>
        /// F29600_s the get senior exemption details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The senior excemt dataset.</returns>
        public static F29600SeniorExemptData F29600_GetSeniorExemptionDetails(int eventId,int userId)
        {
            F29600SeniorExemptData seniorExemptionData = new F29600SeniorExemptData();
            Hashtable ht = new Hashtable();
            DataSet ds = new DataSet(); 
            ht.Add("@EventID", eventId);
            ht.Add("@UserID", userId);
            string[] tableNames = new string[] { seniorExemptionData.f29600ListSeniorExemptionDataTable.TableName, seniorExemptionData.f29600GetSeniorExemptionMode.TableName, seniorExemptionData.f29600_pcget_SeniorExemptionRollYear.TableName, seniorExemptionData.f29600GetSeniorExemptionOwnerComboDataTable.TableName };
            Utility.LoadDataSet(seniorExemptionData, "f29600_pcget_SeniorExemption", ht, tableNames);
            Utility.LoadDataSet(ds, "f29600_pcget_SeniorExemption", ht);
            return seniorExemptionData;
        }

        /// <summary>
        /// F29600_GetSeniorExemptionCode
        /// </summary>
        /// <param name="effectiveDate">eventId</param>
        /// <returns>seniorExemptionData</returns>
        public static F29600SeniorExemptData F29600_GetSeniorExemptionCode(string effectiveDate)
        {
            F29600SeniorExemptData seniorExemptionData = new F29600SeniorExemptData();
            Hashtable ht = new Hashtable();
            ht.Add("@EffectiveDate", effectiveDate);
            Utility.LoadDataSet(seniorExemptionData.f29600ListExemptionCodeDataTable, "f29600_pclst_ExemptionCode", ht);
            return seniorExemptionData;
        }

        /// <summary>
        /// F29600_saveSeniorExemptionDetails
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <param name="seniorExemptItems">seniorExemptItems</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public static int F29600_saveSeniorExemptionDetails(int eventId, string seniorExemptItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@SeniorExemption", seniorExemptItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f29600_pcins_SeniorExemption", ht);
        }
    }
}
