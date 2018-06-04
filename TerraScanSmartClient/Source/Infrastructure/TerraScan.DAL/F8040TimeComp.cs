// -------------------------------------------------------------------------------------------
// <copyright file="F8040TimeComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Excise Tax Rates</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16 Oct 06		Thilak raj         Created
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
    /// Class contains method for 8040 Time slice
    /// </summary>
    public static class F8040TimeComp
    {
       #region List
        /// <summary>
        /// F8040_s the list time information.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns> List of time details of 8040</returns>
        public static F8040TimeData F8040_ListTimeInformation(int formId, int keyId)
        {
            F8040TimeData timeDataSet = new F8040TimeData();
            Hashtable ht = new Hashtable();
            ht.Add("@FormID", formId);
            ht.Add("@KeyID", keyId);
            Utility.LoadDataSet(timeDataSet.ListTime, "f8040_pclst_FSTime", ht);
            return timeDataSet;
        }

        /// <summary>
        /// F8040_s the list time information.
        /// </summary>
        /// <param name="isactive">The active value</param>
        /// <returns>ListTimeDataTable</returns>
        public static F8040TimeData F8040_ListTimeResourceInformation(int isactive)
        {
            F8040TimeData timeResourceDataSet = new F8040TimeData();
            Hashtable ht = new Hashtable();
            ht.Add("@IsActive", isactive);
            Utility.LoadDataSet(timeResourceDataSet.ListTimeResource, "f8040_pclst_FSTimeResource", ht);
            return timeResourceDataSet;
        }
        #endregion List

        #region Save
        /// <summary>
        /// F8040_s the save time.
        /// </summary>
        /// <param name="timeDetails">The time details.</param>
        /// <param name="userId">userId</param>
        public static void F8040_SaveTime(string timeDetails, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FSTimeItems", timeDetails);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8040_pcins_FSTime", ht);
        }
        #endregion Save

        #region Update
        /// <summary>
        /// F8040_s the update time.
        /// </summary>
        /// <param name="timeDetails">The time details.</param>
        /// <param name="userId">userId</param>
        public static void F8040_UpdateTime(string timeDetails, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FSTime", timeDetails);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8040_pcupd_FSTime", ht);
        }
        #endregion Update

        #region Delete
        /// <summary>
        /// F8040_s the delete time.
        /// </summary>
        /// <param name="timeId">The time id.</param>
        /// <param name="userId">userId</param>
        public static void F8040_DeleteTime(int timeId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TRID", timeId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f8040_pcdel_FSTime", ht);
        }
        #endregion Delete

        #region Check eventID

        /// <summary>
        /// Checks the event id.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>Integer value</returns>
        public static int F8040_CheckEventId(int formId, int keyId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FormID", formId);
            ht.Add("@KeyID", keyId);
            return Utility.FetchSPExecuteKeyId("f8040_pcchk_EventID", ht);
        }

        #endregion
    }
}
