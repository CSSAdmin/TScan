// -------------------------------------------------------------------------------------------
// <copyright file="F2004ParcelCopy.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update GDoc Comment</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
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
    /// Data Access Layer which talks to the DB directly for F2004
    /// </summary>
    public static class F2004ParcelCopy
    {
        /// <summary>
        /// GetParcelTypeDetails
        /// </summary>
        /// <param name="parcelId">parcelId</param>
        /// <returns>typed dataset</returns>
        public static F2004ParcelCopyData GetParcelTypeDetails(int parcelId)
        {
            F2004ParcelCopyData parcelType = new F2004ParcelCopyData();
            Hashtable ht = new Hashtable();
            if (parcelId > 0)
            {
                ht.Add("@ParcelID", parcelId);
            }

            Utility.LoadDataSet(parcelType.f2004ListParcelType, "f2004_pclst_ParcelType", ht);
            return parcelType;
        }

        /// <summary>
        /// Gets the parcel attachment details.
        /// </summary>
        /// <param name="oldParcelID">The old parcel ID.</param>
        /// <param name="newParcelID">The new parcel ID.</param>
        /// <param name="userID">The user ID.</param>
        /// <param name="moduleID">The module ID.</param>
        /// <returns>The parcel compy dataset.</returns>
        public static F2004ParcelCopyData GetParcelAttachmentDetails(int oldParcelID, int newParcelID, int userID, int moduleID)
        {
            F2004ParcelCopyData parcelType = new F2004ParcelCopyData();
            Hashtable ht = new Hashtable();
            ht.Add("@OldParcelID", oldParcelID);
            ht.Add("@NewParcelID", newParcelID);
            ht.Add("@UserID", userID);
            ht.Add("@ModuleID", moduleID);
            Utility.LoadDataSet(parcelType.getParcelAttachmentTable, "f2000_pcget_ParcelAttachment", ht);
            return parcelType;
        }

        /// <summary>
        /// CreateNewParcelCopy
        /// </summary>
        /// <param name="parcelId">parcelID</param>
        /// <param name="parcelTypeId">parcelTypeID</param>
        /// <param name="copyAllObjects">copyAllObjects</param>
        /// <param name="copyAllSlices">copyAllSlices</param>
        /// <param name="copyAttachments">copyAttachments</param>
        /// <param name="copyComments">copyComments</param>
        /// <param name="parcelElements">parcelElements</param>
        /// <param name="userId">userId</param>
        /// <returns>integer value</returns>
        public static int CreateNewParcelCopy(int parcelId, int parcelTypeId, int copyAllObjects, int copyAllSlices, int copyAttachments, int copyComments, string parcelElements, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@ParcelTypeID", parcelTypeId);
            ht.Add("@CopyAllObjects", copyAllObjects);
            ht.Add("@CopyAllSlices", copyAllSlices);
            ht.Add("@CopyAttachments", copyAttachments);
            ht.Add("@CopyComments", copyComments);
            ht.Add("@ParcelElements", parcelElements);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f2000_pcexe_CreateNewParcel", ht);
        }
    }
}
