// -------------------------------------------------------------------------------------------
// <copyright file="F9040SnapShotUtilityComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F9040SnapShotUtilityComp</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
// 
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    #endregion NameSpaces

    /// <summary>
    /// F9040SnapShotUtilityComp 
    /// </summary>
    public static class F9040SnapShotUtilityComp
    {
        #region SnapShotUtility

        #region F9040_ListBatchButtonSnapShots

        /// <summary>
        /// To List the F1440 Batch Button SnapShots for Current form slice.
        /// </summary>
        /// <param name="formsSliceNo">The forms slice no.</param>
        /// <returns>Typed DataSet containg the list of F1440 Batch Button SnapShots for Current form slice</returns>
        public static F9040SnapShotUtilityData F9040_ListBatchButtonSnapShots(int formsSliceNo)
        {
            F9040SnapShotUtilityData snapShotUtilityData = new F9040SnapShotUtilityData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", formsSliceNo);
            Utility.LoadDataSet(snapShotUtilityData.ListSnapShot, "f1440_pclst_SnapShot", ht);
            return snapShotUtilityData;
        }

        #endregion F9040_ListBatchButtonSnapShots

        #region F9040_SaveBatchButtonSnapShots

        /// <summary>
        /// To save the F1440 Batch Button SnapShots for Current form slice.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotDetails">The snap shot details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>returns the snapshot id</returns>
        public static int F9040_SaveBatchButtonSnapShots(int snapShotId, string snapShotDetails, int userId)
        { 
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotXML", snapShotDetails);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f1440_pcins_SnapShot", ht);
        }

        #endregion F9040_SaveBatchButtonSnapShots

        #region ListSnapShots

        /// <summary>
        /// Lists the SnapShots for the form.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>F9040SnapShotUtilityData Dataset</returns>
        public static F9040SnapShotUtilityData F9040_ListSnapShots(int formId)
        {
            F9040SnapShotUtilityData snapShotUtilityData = new F9040SnapShotUtilityData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", formId);
            string[] optionalParameter = new string[] { snapShotUtilityData.ListSnapShot.TableName, snapShotUtilityData.DefaultIncludeRows.TableName };
            Utility.LoadDataSet(snapShotUtilityData, "f9040_pclst_SnapShot", ht, optionalParameter);
            return snapShotUtilityData;
        }

        #endregion ListSnapShot

        #region ListSnapShotResult      

        #endregion ListSnapShotResult

        #region SaveSnapShot

        /// <summary>
        /// F9040_s the save snap shot.
        /// </summary>
        /// <param name="snapShotId">The snap shot id.</param>
        /// <param name="snapShotXML">The snap shot XML.</param>
        /// <param name="snapshotItemsXML">The snapshot items XML.</param>
        /// <param name="filterXML">Filter items XML</param>
        /// <param name="pinType">Pinning Type</param>
        /// <param name="userId">userId</param>
        /// <param name="parentSnapShotID">Parent SnapShot ID</param>
        /// <returns>the saved snapshotid</returns>
        public static int F9040_SaveSnapShot(int snapShotId, string snapShotXML, string snapshotItemsXML, string filterXML, string pinType, int userId, int parentSnapShotID)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotXML", snapShotXML);
            ht.Add("@SnapshotItemsXML", snapshotItemsXML);
            ht.Add("@FilterXML", filterXML);
            ht.Add("@SnapshotType", pinType);
            ht.Add("@UserID", userId);
            ht.Add("@ParentSnapShotID", parentSnapShotID);
            return Utility.FetchSPExecuteKeyId("f9040_pcins_SnapShot", ht);            
        }

        #endregion SaveSnapShot

        #region DeleteSnapShot

        /// <summary>
        /// To Delete F040 Snapshot
        /// </summary>
        /// <param name="snapshotId">The snapshotId</param>
        /// <param name="userId">userId</param>
        public static void F9040_DeleteSnapShot(int snapshotId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SnapshotID", snapshotId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f9040_pcdel_SnapShot", ht);
        }

        #endregion DeleteSnapShot

        #endregion SnapShotUtility
    }
}
