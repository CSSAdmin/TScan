// -------------------------------------------------------------------------------------------
// <copyright file="F84722WaterValveLocationComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access GDoc Water Valve Location Methods</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 19/12/2006       VijayaKumar.M       Added
// 
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
    /// F84722WaterValveLocationComp Class file
    /// </summary>
    public static class F84722WaterValveLocationComp
    {
        #region F84722 Water Valve Location

        #region Get Water Valve Location

        /// <summary>
        /// To Load F84722 Water valve Location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="formId">The form id.</param>
        /// <returns>
        /// Typed DataSet Containing All the Water Valve Loaction Details
        /// </returns>
        public static F84722WaterValveLocationData F84722_GetWaterValveLocation(int keyId, int formId)
        {
            F84722WaterValveLocationData waterValveLocationData = new F84722WaterValveLocationData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValveID", keyId);
            ht.Add("@FormID", formId);
            Utility.LoadDataSet(waterValveLocationData.ListWaterValveLocation, "f84722_pcget_FS_WaterValveLocation", ht);
            return waterValveLocationData;
        }

        #endregion Get Water Valve Location

        #region Save Water Valve Location

        /// <summary>
        /// To Save F84722 Water valve Location.
        /// </summary>
        /// <param name="keyId">The key id.</param>
        /// <param name="waterValveLocation">The water valve location.</param>
        /// <param name="formId">The form id.</param>
        ///<param name="userId">userId</param>
        /// <returns>The integer value containing key id</returns>
        public static int F84722_SaveWaterValveLocation(int keyId, string waterValveLocation, int formId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ValveID", keyId);
            ht.Add("@WaterValveLoc", waterValveLocation);
            ht.Add("@FormID", formId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f84722_pcupd_FS_WaterValveLocation", ht);
        }

        #endregion Save Water Valve Location

        #endregion F84722 Water Valve Location
    }
}
