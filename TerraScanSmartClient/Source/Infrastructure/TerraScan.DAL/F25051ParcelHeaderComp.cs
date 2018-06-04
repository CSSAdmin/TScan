// -------------------------------------------------------------------------------------------
// <copyright file="F25051ParcelHeaderComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F25051ParcelHeaderComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;
    
    /// <summary>
    /// Data Access Layer which talks to the DB directly for F25000
    /// </summary>
   public static  class F25051ParcelHeaderComp
    {
        /// <summary>
        /// F25051_GetParcelDetails
        /// </summary>
        /// <param name="parcelId">parcelId</param>
        /// <returns>Typed dataset</returns>
        public static F25051ParcelHeaderData F25051_GetParcelDetails(int parcelId)
        {
            F25051ParcelHeaderData parcelHeaderData = new F25051ParcelHeaderData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            Utility.LoadDataSet(parcelHeaderData.f25051ParcelHeader, "f25051_pcget_ParcelHeader", ht);
            return parcelHeaderData;
        }


        /// <summary>
        /// UpdateParcelHeaderDetails
        /// </summary>
        /// <param name="parcelId">parcelID</param>
        /// <param name="parcelDetails">parcelDetails</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer value</returns>
        public static int F25051ParcelHeaderDetails(int parcelId, string parcelDetails, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@ParcelItems", parcelDetails);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f25051_pcupd_ParcelDetails", ht);
        }

        #region F25051 list ParcelClass Type


        /// <summary>
        /// F9075_s the list template.
        /// </summary>
        /// <returns>listtemplateData</returns>
        public static F25051ParcelHeaderData F25051ParcelClassTypes()
        {
            F25051ParcelHeaderData getParcelClassTypeData = new F25051ParcelHeaderData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(getParcelClassTypeData.F25051ParcelClassTypes, "f25051_pclst_ParcelClassTypes", ht);
            return getParcelClassTypeData;
        }

        #endregion

        #region F25000 list Primary Land Type Selection


        /// <summary>
        /// F9075_s the list template.
        /// </summary>
        /// <returns>listtemplateData</returns>
        public static F25051ParcelHeaderData F25051OwnerOccupied()
        {
            F25051ParcelHeaderData getOwnerOccupiedData = new F25051ParcelHeaderData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(getOwnerOccupiedData.F25051OwnerOccupied, "f25051_pclst_OwnerOccupied", ht);
            return getOwnerOccupiedData;
        }

        #endregion 

    }
}
