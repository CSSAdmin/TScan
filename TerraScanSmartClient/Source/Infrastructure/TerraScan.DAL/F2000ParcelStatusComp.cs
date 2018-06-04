// -------------------------------------------------------------------------------------------
// <copyright file="F2000ParcelStatusComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F2000ParcelStatusComp</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 08-May-07        Sam K               Created 
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
    /// Main Class for ParcelStatus Component
    /// </summary>
    public static class F2000ParcelStatusComp
    {
        #region ListParcelStatus        
       
        /// <summary>
        /// Get the Parcel Status DataTable
        /// </summary>
        /// <param name="parcelId">ParcelID</param>
        /// <returns>Typed dataset</returns>
        public static F2000ParcelStatusData F2000_ListParcelStatus(int parcelId)
        {
            F2000ParcelStatusData parcelStatusData = new F2000ParcelStatusData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            Utility.LoadDataSet(parcelStatusData.ListParcelStatusDataTable, "f2000_pclst_ParcelStatus", ht);
            return parcelStatusData;
        }
        #endregion

        /// <summary>
        /// Lists the record lock status.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyId">The key id.</param>
        /// <returns>The record lock status.</returns>
        public static string ListRecordLockStatus(int formNo, int keyId)
        { 
            Hashtable ht = new Hashtable();
            ht.Add("@FormNo", formNo);
            ht.Add("@KeyID", keyId);
            return Utility.FetchSPExecuteKeyString("f2001_pcget_RecordLockStatus", ht);
        }

        #region Update ParcelStatus
        /// <summary>
        /// Update the ParcelDetails 
        /// </summary>
        /// <param name="parcelId">parcelId</param>
        /// <param name="description">description</param>
        /// <param name="parcelType">parcelType</param>
        /// <param name="isexempt">isExempt</param>
        /// <param name="isownerPrimary">isOwnerPrimary</param>
        /// <param name="userId">userId</param>
        /// <returns>Update and return the same ParcelID if its Sucess</returns>
        public static int F2000_UpdateParcelStatus(int parcelId, string description, string parcelType, int isexempt, int isownerPrimary, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@Description", description);
            ht.Add("@ParcelType", parcelType);
            ht.Add("@IsExempt", isexempt);
            ht.Add("@IsOwnerPrimary", isownerPrimary);
            ht.Add("@UserID", userId);
            int formKeyId;
            formKeyId = Utility.FetchSPExecuteKeyId("f2000_pcupd_ParcelStatus", ht);
            return formKeyId;
        }

        #endregion

        #region Delete Parcel
        /// <summary>
        /// Delete the ParcelID Details
        /// </summary>
        /// <param name="parcelId">ParcelID</param>
        /// <param name="userId">userId</param>
        public static void F2000_DeleteParcelStatus(int parcelId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f2000_pcdel_ParcelStatus", ht);   
        }

        #endregion

        #region ListParcelType

        /// <summary>
        /// GetParcelType
        /// </summary>     
        /// <returns>Typed Dataset</returns>
        public static F2000ParcelStatusData GetParcelType()
        {
            F2000ParcelStatusData parcelType = new F2000ParcelStatusData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(parcelType.f2000ListParcelType, "f2004_pclst_ParcelType", ht);
            return parcelType;
        }

        #endregion
    }
}
