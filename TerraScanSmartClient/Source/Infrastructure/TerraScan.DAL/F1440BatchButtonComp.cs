// -------------------------------------------------------------------------------------------
// <copyright file="F1440BatchButtonComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F1440BatchButtonComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 13/12/07         M.Vijayakumar       Created
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
    /// F1440BatchButtonComp class file
    /// </summary>
    public static class F1440BatchButtonComp
    {
        #region F1440 Batch Button SmartPart

        #region F1440_SaveRecieptinSnapShotBatchButtonControl

        /// <summary>
        /// F1440_SaveRecieptinSnapShotBatchButtonControl        
        /// To Insert or Update the newly created Receipt id  to the particular snapshot id.
        /// </summary>
        /// <param name="snapshotId">the snapshotId</param>
        /// <param name="receiptId">the receiptId</param>
        /// <param name="userId">the userId</param>
        /// <returns>returns the no of items count in snapshot</returns>
        public static int F1440_SaveRecieptinSnapShotBatchButtonControl(int snapshotId, int? receiptId, int userId)
        {
            Hashtable ht = new Hashtable();

            ht.Add("@SnapshotID", snapshotId);
            if (receiptId != null || receiptId > 0)
            {
                ht.Add("@ReceiptID", receiptId);
            }

            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f1440_pcins_BatchButton", ht);
        }

        #endregion F1440_SaveRecieptinSnapShotBatchButtonControl

        #endregion F1440 Batch Button SmartPart       
    }
}
