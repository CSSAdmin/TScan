// -------------------------------------------------------------------------------------------
// <copyright file="F29650ExemptionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F35100NeighborhoodComp.cs methods
// </summary>
// Release history
// ********************************************************************************************
// Date               Author            Description
// ----------        ---------     -------------------------------------------------------
// 
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    /// <summary>
    /// F29650 ExemptionComp
    /// </summary>
    public static class F29650ExemptionComp
    {
        #region List Exemption Type

        /// <summary>
        /// Gets the type of the exemption.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>DataSet contains Exemption Type</returns>
        public static F29650ExemptionData GetExemptionType(int eventId)
        {
            F29650ExemptionData exemptionDetails = new F29650ExemptionData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            Utility.LoadDataSet(exemptionDetails.ListExemptionType, "f29650_pclst_ExemptionType", ht);
            return exemptionDetails;
        }

        #endregion List Exemptio Type

        #region Get Exemption Details

        /// <summary>
        /// Gets the exemption details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>DataSet contains Exemption Details</returns>
        public static F29650ExemptionData GetExemptionDetails(int eventId)
        {
            F29650ExemptionData exemptionDetails = new F29650ExemptionData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            Utility.LoadDataSet(exemptionDetails.GetExemption, "f29650_pcget_Exemption", ht);
            return exemptionDetails;
        }

        #endregion Exemption Details

        #region Get Exemption Loss

        /// <summary>
        /// Gets the exemption loss.
        /// </summary>
        /// <param name="lossValue">The loss value.</param>
        /// <param name="maxValue">The max value.</param>
        /// <returns>Returns Loss value</returns>
        public static decimal GetExemptionLoss(decimal lossValue, decimal maxValue)
        {
            object tempvalue = null;
            Hashtable ht = new Hashtable();
            ht.Add("@Loss", lossValue);
            ht.Add("@Maximum", maxValue);

            tempvalue = DataProxy.FetchSpObject("f29650_pcget_FrozenLoss", ht);

            if (tempvalue != null)
            {
                return (decimal)tempvalue;
            }

            return 0;
        }

        #endregion Get Exemption Loss

        #region Save Frozen Value
        
        /// <summary>
        /// Saves the exemption details.
        /// </summary>
        /// <param name="exemptionElements">The exemption elements.</param>
        /// <param name="userId">The user id.</param>
        public static void SaveExemptionDetails(string exemptionElements, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ExemptionItems", exemptionElements);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f29650_pcins_Exemption", ht);
        }

        #endregion Save Frozen Value

        #region Delete Frozen Value

        /// <summary>
        /// Deletes the exemption details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="exemptionEventId">The exemption event id.</param>
        /// <param name="userId">The user id.</param>
        public static void DeleteExemptionDetails(int eventId, int exemptionEventId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@ExemptionEventID", exemptionEventId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f29650_pcdel_Exemption", ht);
        }

        #endregion Delete Frozen Value
    }
}
