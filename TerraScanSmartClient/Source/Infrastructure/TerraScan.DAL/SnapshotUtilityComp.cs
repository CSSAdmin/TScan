// -------------------------------------------------------------------------------------------
// <copyright file="SnapshotUtilityComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access attachment related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.DataLayer;
    using System.Data;
    using System.Collections;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// SnapshotUtilityComp class
    /// </summary>
    public static class SnapshotUtilityComp
    {
        /// <summary>
        /// Gets the snapshot utility list.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>return dataset</returns>
        public static SnapshotUtilityData GetSnapshotUtilityList(int formId)
        {
            SnapshotUtilityData snapshotUtilityData = new SnapshotUtilityData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", formId);
            Utility.LoadDataSet(snapshotUtilityData.ListSnapshot, "f9051_pclst_Snapshot", ht);
            return snapshotUtilityData;
            ////return DataProxy.FetchDataSet("f9051_pclst_Snapshot", ht);
        }

        /// <summary>
        /// Deletes the snapshot utility.
        /// </summary>
        /// <param name="snapshotId">The snapshot ID.</param>
        /// <param name="userId">userId</param>
        public static void DeleteSnapshotUtility(int snapshotId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SnapshotID", snapshotId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f9051_pcdel_Snapshot", ht);
        }

        /// <summary>
        /// Inserts the query utility.
        /// </summary>
        /// <param name="snapshotId">The snapshot id.</param>
        /// <param name="snapshotName">Name of the snapshot.</param>
        /// <param name="snapshotFormId">The snapshot form id.</param>
        /// <param name="description">The description.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="overrideValue">The override value.</param>
        /// <param name="keyIds">The key ids.</param>
        /// <returns>return integer</returns>
        public static int InsertSnapshotUtility(int snapshotId, string snapshotName, int snapshotFormId, string description, int recordCount, int userId, int overrideValue, string keyIds)
        {
            Hashtable ht = new Hashtable();
            if (snapshotId == 0)
            {
                ht.Add("@SnapshotID", DBNull.Value);
            }
            else
            {
                ht.Add("@SnapshotID", snapshotId);
            }

            ht.Add("@SnapShotName", snapshotName);
            ht.Add("@Form", snapshotFormId);
            ht.Add("@SnapshotNote", description);
            ht.Add("@RecordCount", recordCount);
            ht.Add("@UserID", userId);            
            ht.Add("@IsOverride", overrideValue);
            ht.Add("@KeyIDs", keyIds);
            ////return DataProxy.FetchSPOutput("f9051_pcins_Snapshot", ht); 
            return Utility.FetchSPOutput("f9051_pcins_Snapshot", ht); 
        }
    }
}
