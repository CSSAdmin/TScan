// -------------------------------------------------------------------------------------------
// <copyright file="F25008ParcelMiscellaneousComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F25008ParcelMiscellaneousComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 15/04/07         H.Vinayagamurthy       Created
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
    /// F25008ParcelMiscellaneousComp class file
    /// </summary>
    public static class F25008ParcelMiscellaneousComp
    {
        #region F25008 Parcel MiscellaneousComp

        #region Get Parcel Miscellaneous

        /// <summary>
        /// Get the ParcelMiscellaneous Data
        /// </summary>
        /// <param name="parcelId">parcelID</param>
        /// <returns>Parcel Miscellaneous</returns>
        public static F25008ParcelMiscellaneousData F25008_ParcelMiscellaneousData(int parcelId)
        {
            F25008ParcelMiscellaneousData parcelMiscellaneousData = new F25008ParcelMiscellaneousData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            Utility.LoadDataSet(parcelMiscellaneousData.GetParcelMiscellaneous, "f25008_pcget_ParcelMiscellaneous", ht);
            return parcelMiscellaneousData;
        }

        #endregion Get Parcel Miscellaneous

        #region Get Parcel Miscellaneous Configuration

        /// <summary>
        /// Get the ParcelMiscellaneous Configuration Data
        /// </summary>
        /// <returns>Parcel Miscellaneous Config</returns>
        public static F25008ParcelMiscellaneousData F25008_ParcelMiscellaneousConfigData()
        {
            F25008ParcelMiscellaneousData parcelMiscellaneousConfigData = new F25008ParcelMiscellaneousData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(parcelMiscellaneousConfigData.GetParcelMiscellaneousConfiguration, "f25008_pcget_ParcelMiscellaneousConfig", ht);
            return parcelMiscellaneousConfigData;
        }

        #endregion Get Parcel Miscellaneous Configuration

        #region Save Parcel Miscellaneous

        /// <summary>
        /// Save the ParcelMiscellaneous Records
        /// </summary>
        /// <param name="parcelId">parcelID</param>
        /// <param name="miscellaneous">miscellaneous</param>
        /// <param name="userId">userId</param>
        public static void F25008_SaveParcelMiscellaneous(int parcelId, string miscellaneous, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@Miscellaneous", miscellaneous);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f25008_pcins_ParcelMiscellaneous", ht);
        }

        #endregion Save Parcel Miscellaneous

        #endregion F25008 Parcel MiscellaneousComp
    }
}