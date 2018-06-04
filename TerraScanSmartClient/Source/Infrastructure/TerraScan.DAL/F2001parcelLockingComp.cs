// -------------------------------------------------------------------------------------------
// <copyright file="F2001parcelLockingComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F2001parcelLockingComp.cs methods
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
    public static class F2001parcelLockingComp
    {
        /// <summary>
        /// F2001_getParcelLockingDetails
        /// </summary>
        /// <param name="keyId">keyId</param>
        /// <returns>Typed dataset</returns>
        public static F2001ParcelLockingData F2001_getParcelLockingDetails(int keyId)
        {
            F2001ParcelLockingData parcelLockingData = new F2001ParcelLockingData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", keyId);
            Utility.LoadDataSet(parcelLockingData.f2001ParcelLock, "f2001_pcget_ParcelLock", ht);
            return parcelLockingData;
        }

        /// <summary>
        /// F2001_getParcelLockingUsername
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>Typed dataset</returns>
        public static F2001ParcelLockingData F2001_getParcelLockingUsername(int userId)
        {
            F2001ParcelLockingData parcelLockingData = new F2001ParcelLockingData();
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(parcelLockingData.f2001getUserName, "f2001_pcget_UserName", ht);
            return parcelLockingData;
        }

        /// <summary>
        /// F2001_UpdateParcelLockingDetails
        /// </summary>
        /// <param name="keyId">keyId</param>
        /// <param name="valueLock">valueLock</param>
        /// <param name="adminLock">adminLock</param>
        /// <param name="lockAppraisal">lockAppraisal</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer value</returns>
        public static int F2001_UpdateParcelLockingDetails(int keyId, int valueLock, int adminLock, int lockAppraisal, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", keyId);
            ht.Add("@LockValueBy", valueLock);
            ht.Add("@LockAdminBy", adminLock);
            ht.Add("@LockAppraisalBy", lockAppraisal);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecutedReturnValue("f2001_pcupd_ParcelLock", ht);
        }

        /// <summary>
        /// F2001_GetValidUserName
        /// </summary>
        /// <param name="keyId">keyId</param>
        /// <param name="userId">userId</param>
        /// <param name="formNo">formNo</param>
        /// <returns>Integer Value</returns>
        public static int F2001_GetValidUserName(int keyId, int userId, string formNo)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID ", keyId);
            ht.Add("@UserID", userId);
            ht.Add("@Form", formNo);
            return Utility.FetchSPExecuteKeyId("f2001_pcchk_UserID", ht);
        }
    }
}
