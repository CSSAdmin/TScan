// -------------------------------------------------------------------------------------------
// <copyright file="F9043SnapShotOperationsComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F1440BatchButtonComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		            Description
// ----------		---------		        --------------------------------------------------
// 06 March 13         A.Purushotham           Create
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

    public static class F9044SnapshotOperationsComp
    {
        public static F9044SnapshotOperations GetSnapshotDetails(int FormNum, int UserId)
        {
            F9044SnapshotOperations snapshotDetails = new F9044SnapshotOperations();
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", FormNum);
            ht.Add("@UserID", UserId);
            string[] optionalParameter = new string[] { snapshotDetails.f9044_pcget_SnapshotOperations.TableName, snapshotDetails.f9044_pcget_Operations.TableName };
            Utility.LoadDataSet(snapshotDetails, "f9044_pcget_SnapshotOperations", ht, optionalParameter);
            // snapshotDetails.f9043_pcget_Operations = snapshotDetails.
            return snapshotDetails;
        }

        public static F9044SnapshotOperations GetSnapshotOperationCount(int OperationId, int LOSnapshotId, int ROSnapshotId, int UserId)
        {
            F9044SnapshotOperations snapshotCountDetails = new F9044SnapshotOperations();
            Hashtable ht = new Hashtable();
            ht.Add("@OperationID", OperationId);
            ht.Add("@LOSnapshotID", LOSnapshotId);
            ht.Add("@ROSnapshotID", ROSnapshotId);
            ht.Add("@UserID", UserId);
            Utility.LoadDataSet(snapshotCountDetails.f9044_pcget_SnapshotOperationCount, "f9044_pcget_SnapshotOperationCount", ht);
            return snapshotCountDetails;
        }
        public static void insertSnapshotDetails(int OperationId, int LOSnapshotId, int ROSnapshotId, int RecordCount, string NewSnapshotName, int UserId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@OperationID", OperationId);
            ht.Add("@LOSnapshotID", LOSnapshotId);
            ht.Add("@ROSnapshotID", ROSnapshotId);
            ht.Add("@RecordCount", RecordCount);
            ht.Add("@NewSnapshotName", NewSnapshotName);
            ht.Add("@UserID", UserId);
            Utility.ImplementProcedure("f9044_pcins_SnapshotOperations", ht);

        }
    }


}
