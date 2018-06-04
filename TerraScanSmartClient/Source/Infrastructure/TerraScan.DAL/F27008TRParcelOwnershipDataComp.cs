// -------------------------------------------------------------------------------------------
// <copyright file="F27008TRParcelOwnershipComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F27006ParcelOwnershipComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
//                   Manojkumar       Created
// 20111209          Manoj Kumar      removed the Parameter IsFuture Push  for F27008.
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
    /// F27008TRParcelOwnershipComp Class file
    /// </summary>
    public static class F27008TRParcelOwnershipDataComp
    {
        #region List Parcel Ownership

        /// <summary>
        /// To List the Parcel Ownership Details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>Typed Dataset Containing the Parcel Ownership Details</returns>
        public static F27008TRParcelOwnershipData F27008_ListParcelOwnership(int parcelId)
        {
            F27008TRParcelOwnershipData parcelOwnershipData = new F27008TRParcelOwnershipData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            string[] optionalParameter = new string[] { parcelOwnershipData.ListParcelOwnershipDatatable.TableName, parcelOwnershipData.ListOwnerValidID.TableName, parcelOwnershipData.ListOwnersDatatable.TableName  };
            Utility.LoadDataSet(parcelOwnershipData, "f27008_pclst_ParcelOwnership", ht, optionalParameter);
            return parcelOwnershipData;
        }

        #endregion List Parcel Ownership

        #region Save Parcel Ownership

        /// <summary>
        /// To Save Parcel Ownership Details.
        /// </summary>
        /// <param name="parcelOwnership">The parcel ownership.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="userId">userId</param>
        public static void F27008_SaveParcelOwnership(string parcelOwnership, int parcelId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@ParcelOwnership", parcelOwnership);
            ht.Add("@UserID", userId);
            //ht.Add("@IsFuturePush", isfuturePush); 
            Utility.ImplementProcedure("f27008_pcupd_ParcelOwnership", ht);
        }

        #endregion Save Parcel Ownership

        #region GetOwnerDetails
        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public static F27008TRParcelOwnershipData F27008_GetOwnerDetails(int extraownerId,int userId)
        {
            F27008TRParcelOwnershipData listOwnersDatatable = new F27008TRParcelOwnershipData();
            Hashtable ht = new Hashtable();
            ht.Add("@OwnerID", extraownerId);
            ht.Add("@UserID", userId);
            string[] optionalParameter = new string[] { listOwnersDatatable.ListOwnersDatatable.TableName };
            Utility.LoadDataSet(listOwnersDatatable, "f27008_pcget_Owner", ht, optionalParameter);
            return listOwnersDatatable;
        }
        #endregion GetOwnerDetails

    }
}
