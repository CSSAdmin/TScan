// -------------------------------------------------------------------------------------------
// <copyright file="F27006ParcelOwnershipComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F27006ParcelOwnershipComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 28/03/07         M.Vijayakumar       Created
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
    /// F27006ParcelOwnershipComp Class file
    /// </summary>
    public static class F27006ParcelOwnershipComp
    {
        #region F27006 Parcel Ownership

        #region List Parcel Ownership

        /// <summary>
        /// To List the Parcel Ownership Details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>Typed Dataset Containing the Parcel Ownership Details</returns>
        public static F27006ParcelOwnershipData F27006_ListParcelOwnership(int parcelId)
        {
            F27006ParcelOwnershipData parcelOwnershipData = new F27006ParcelOwnershipData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            string[] optionalParameter = new string[] { parcelOwnershipData.ListParcelOwnershipDataTable.TableName, parcelOwnershipData.ListOwnerValidID.TableName, parcelOwnershipData.ListSeparateStmtDataTable.TableName };
            Utility.LoadDataSet(parcelOwnershipData, "f27006_pclst_ParcelOwnership", ht, optionalParameter);
            return parcelOwnershipData;
        }

        #endregion List Parcel Ownership

        #region List All Owner Details

        /// <summary>
        /// To List All Owners Details
        /// </summary>
        /// <param name="firstName">The First Name.</param>
        /// <param name="lastName">The Last Name.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="city">The city.</param>
        /// <returns>Typed Dataset Containg the All Owners Details</returns>
        public static F27006ParcelOwnershipData F27006_ListALLOwnerDetails(string firstName, string lastName, string address1, string address2, string city)
        {
            F27006ParcelOwnershipData parcelOwnershipData = new F27006ParcelOwnershipData();
            Hashtable ht = new Hashtable();
            ht.Add("@FirstName", firstName);
            ht.Add("@LastName", lastName);
            ht.Add("@Address1", address1);
            ht.Add("@Address2", address2);
            ht.Add("@City", city);
            Utility.LoadDataSet(parcelOwnershipData.ListAllOwnersDetailDataTable, "f27006_pclst_OwnerSearch", ht);
            return parcelOwnershipData;
        }

        #endregion List All Owner Details

        #region Check Ownership Details

        /// <summary>
        /// To Check Given Ownership Details is valid.
        /// </summary>
        /// <param name="ownershipDetails">The ownership details.</param>
        /// <returns>returns an integer Value whather given details are correct or not</returns>
        public static int F27006_CheckOwnershipDetails(string ownershipDetails)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CheckOwnership", ownershipDetails);
            return Utility.FetchSPExecuteKeyId("f27006_pcchk_Ownership", ht);
        }

        #endregion Check Ownership Details

        #region Save Parcel Ownership

        /// <summary>
        /// To Save Parcel Ownership Details.
        /// </summary>
        /// <param name="parcelOwnership">The parcel ownership.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="userId">userId</param>
        public static void F27006_SaveParcelOwnership(string parcelOwnership, int parcelId, int userId, bool isfuturePush)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@ParcelOwnership", parcelOwnership);
            ht.Add("@UserID", userId);
            ht.Add("@IsFuturePush", isfuturePush);
            Utility.ImplementProcedure("f27006_pcins_ParcelOwnership", ht);
        }

        #endregion Save Parcel Ownership

        #region F27006 list MOwnerType Selection


        /// <summary>
        /// F9075_s the list template.
        /// </summary>
        /// <returns>listtemplateData</returns>
        public static F27006ParcelOwnershipData ListMOwnerType()
        {
            F27006ParcelOwnershipData getMOwnerTypeData = new F27006ParcelOwnershipData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(getMOwnerTypeData.listMOwnerTypeDataTable, "f27006_pclst_MOwnerType", ht);
            return getMOwnerTypeData;
        }

        #endregion

        #endregion F27006 Parcel Ownership
    }
}
