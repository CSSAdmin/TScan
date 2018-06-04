// -------------------------------------------------------------------------------------------
// <copyright file="F3501NeighborhoodParcelLocks.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F3501NeighborhoodParcelLocks</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 23-june-2007     Ramya.D             Created 
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
    /// F3501NeighborhoodParcelLocks
    /// </summary>
    public static class F3501NeighborhoodParcelLocks
    {
         #region ListNeighborhood Parcel locks  
       
        /// <summary>
        /// Get the Neighborhood Parcel locks
        /// </summary>
        /// <param name="nbhdId">nbhdID</param>
        /// <returns>Typed dataset</returns>
        public static F3501NeighborhoodParcelLocksData ListNeighborhoodParcelLocks(int nbhdId)
        {
            F3501NeighborhoodParcelLocksData neighborhoodParcelLocksData  = new F3501NeighborhoodParcelLocksData();
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDID", nbhdId);
            Utility.LoadDataSet(neighborhoodParcelLocksData.f35100NeighborhoodParcelLocks, "f35100_pcget_NeighborhoodLocks", ht);
            return neighborhoodParcelLocksData;
        }

        /// <summary>
        /// F3501_UpdateParcelLockingDetails 
        /// </summary>
        /// <param name="keyId">keyId</param>
        /// <param name="valueLock">valueLock</param>
        /// <param name="adminLock">adminLock</param>
        /// <param name="lockAppraisal">lockAppraisal</param>
        /// <param name="primaryId">primaryID</param>
        /// <param name="userId">userId</param>
        /// <returns>Typed dataset</returns>
        public static F3501NeighborhoodParcelLocksData UpdateParcelLockingDetails(int keyId, int valueLock, int adminLock, int lockAppraisal, int primaryId, int userId)
        {
            F3501NeighborhoodParcelLocksData neighborhoodParcelLocksData = new F3501NeighborhoodParcelLocksData();
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDID", keyId);
            ht.Add("@LockAppraisalBy", lockAppraisal);
            ht.Add("@LockAssessmentBy", valueLock);
            ht.Add("@LockAdminBy", adminLock);
            ht.Add("@PrimaryKeyID", primaryId);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(neighborhoodParcelLocksData.f35100_pcupd_NeighborhoodLocks, "f35100_pcupd_NeighborhoodLocks", ht);
            return neighborhoodParcelLocksData;
        }

        #endregion ListNeighborhood Parcel locks 
    }
}
