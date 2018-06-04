// -------------------------------------------------------------------------------------------
// <copyright file="F96000OwnerManagementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F96000OwnerManagementComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.DataLayer;
    using System.Data;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F96000OwnerManagementclass file
    /// </summary>
    public static class F96000OwnerManagementComp
    {
        #region GetOwnerManagementDetails

        /// <summary>
        /// Gets the F96000_GetOwnerDetails
        /// It Returns two table[OwnerDetails,OwnerList]
        /// </summary>
        /// <param name="ownerId">ownerID</param>
        /// <returns>Type Dataset Returns two table[OwnerDetails,OwnerList]</returns>
        public static F96000OwnerManagementData F96000_GetOwnerManagementDetails(int ownerId)
        {
            F96000OwnerManagementData ownerManagementData = new F96000OwnerManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@OwnerID", ownerId);
            string[] tableName = new string[] 
            { 
                ownerManagementData.F96000GetOwnerDetails.TableName, 
                ownerManagementData.F96000GetStatusList.TableName
            };

            Utility.LoadDataSet(ownerManagementData, "f96000_pcget_OwnerDetails", ht, tableName);
            return ownerManagementData;
        }
        #endregion GetStatusList

        #region ListOwnerStatusType

        /// <summary>
        /// ListOwnerStatusType
        /// </summary>
        /// <returns>Typed dataset</returns>
        public static F96000OwnerManagementData F96000_ListOwnerStatusType()
        {
            F96000OwnerManagementData ownerManagementData = new F96000OwnerManagementData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(ownerManagementData.F96000ListOwnerStatusType, "f96000_pclst_OwnerStatusType", ht);
            return ownerManagementData;
        }


        /// <summary>
        /// F96000_s the country combo details.
        /// </summary>
        /// <returns></returns>
        public static F96000OwnerManagementData F96000_CountryComboDetails()
        {
            F96000OwnerManagementData CountryCombo = new F96000OwnerManagementData();
            Hashtable ht = new Hashtable();
            string[] tableName = new string[] 
            { 
                CountryCombo.f96000_pclst_Country.TableName, 
                CountryCombo.LoadDefaultCountry.TableName
            };
            Utility.LoadDataSet(CountryCombo, "f96000_pclst_Country", ht,tableName);
            return CountryCombo;
        }
        #endregion ListOwnerStatusType

        #region Insert OwnerManagementDetails

       /// <summary>
        /// F96000_InsertOwnerManagementDetails
       /// </summary>
        /// <param name="ownerId">ownerID</param>
        /// <param name="ownerDetails">ownerDetails</param>
        /// <param name="ownerStatus">ownerStatus</param>
        /// <param name="userId">userId</param>
       /// <returns>integere value</returns>
        public static int F96000_InsertOwnerManagementDetails(int ownerId, string ownerDetails, string ownerStatus, int userId)
        {
            ////F96000OwnerManagementData ownerManagementData = new F96000OwnerManagementData();
            Hashtable ht = new Hashtable();
            if (ownerId == 0)
            {
                ht.Add("@OwnerID", DBNull.Value);
            }
            else
            {
                ht.Add("@OwnerID", ownerId);
            }
            
            ht.Add("@OwnerDetails", ownerDetails);
            ht.Add("@OwnerStatus", ownerStatus);
            ht.Add("@UserID", userId);

            int ownermanagementID;
            ownermanagementID = Utility.FetchSPExecuteKeyId("f96000_pcins_OwnerManagement", ht);
            return ownermanagementID;
        }
        #endregion Insert OwnerManagementDetails

        #region DeleteDatas

        /// <summary>
        /// F96000_s the delete data.
        /// </summary>
        /// <param name="statusId">The status id.</param>
        public static void F96000_DeleteData(int statusId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@OwnerStatusID", statusId);
            Utility.ImplementProcedure("f96000_pcdel_OwnerStatus", ht);
        }

        #endregion
    }  
}