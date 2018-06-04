// -------------------------------------------------------------------------------------------
// <copyright file="F25011StreetListManagementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F25011StreetListManagementComp.cs </summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 04/05/2007       VijayaKumar.M       Added
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
    /// F25011StreetListManagementComp class file
    /// </summary>
    public static class F25011StreetListManagementComp
    {
        #region F25011 Street List Management

        #region Get Street List Management

        /// <summary>
        ///  Get the Master Street Data.
        /// </summary>
        /// <param name="streetId">Street ID</param>
        /// <returns>Typed DataSet Containing the Master Street List record</returns>
        public static F25011StreetListManagementData F25011_GetMasterStreetList(int streetId)
        {
            F25011StreetListManagementData streetManagementData = new F25011StreetListManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@StreetID", streetId);
            Utility.LoadDataSet(streetManagementData.GetStreetListManagement, "f25011_pcget_StreetListManagement", ht);
            return streetManagementData;
        }

        #endregion Get Street List Management
        
        #region List Master Street List
        
        /// <summary>
        /// F25011_s the list master street list.
        /// </summary>
        /// <param name="streetID">The street ID.</param>
        /// <param name="streetName">Name of the street.</param>
        /// <param name="city">The city.</param>
        /// <returns></returns>
        public static F25011StreetListManagementData F25011_ListMasterStreetList(int streetID,string streetName, string city)
        {
            F25011StreetListManagementData streetListManagementData = new F25011StreetListManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@StreetID", streetID);
            ht.Add("@StreetName", streetName);
            ht.Add("@City", city);
            Utility.LoadDataSet(streetListManagementData.ListStreetManagement, "f25011_pclst_StreetListManagement", ht);
            return streetListManagementData;
        }

        #endregion List Master Street List

        #region List Street City Directional Suffix

        /// <summary>
        /// To List Street City Directional Suffix Details.
        /// </summary>
        /// <returns>Typed Dataset conitaining the Street's City, Directional and Suffixs details</returns>
        public static F25011StreetListManagementData F25011_ListStreetCityDirectionalSuffixDetails()
        {
            F25011StreetListManagementData streetListManagementData = new F25011StreetListManagementData();
            Hashtable ht = new Hashtable();
            string[] optionalParameter = new string[] { streetListManagementData.ListCity.TableName, streetListManagementData.ListDirectional.TableName, streetListManagementData.ListSuffix.TableName };
            Utility.LoadDataSet(streetListManagementData, "f25011_pclst_StreetCityDirectionalSuffix", ht, optionalParameter);
            return streetListManagementData;
        }

        #endregion List Street City Directional Suffix

        #region Save Street List Management

        /// <summary>
        /// To Save Street List Management Details.
        /// </summary>
        /// <param name="streetId">The street id.</param>
        /// <param name="streetListMgmt">The street list MGMT.</param>
        /// <param name="userId">userId</param>
        /// <returns>The current Saved streetId</returns>
        public static int F25011_SaveStreetListManagement(int streetId, string streetListMgmt, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@StreetID", streetId);
            ht.Add("@StreetListMgmt", streetListMgmt);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f25011_pcins_StreetListManagement", ht);
        }

        #endregion Save Street List Management

        #region Delete Street List

        /// <summary>
        /// F25011_s the delete street list.
        /// </summary>
        /// <param name="streetId">The street id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Delete Flag</returns>
        public static int F25011_DeleteStreetList(int streetId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@StreetID", streetId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f25011_pcdel_StreetListManagement", ht);
        }
        #endregion Delete Street List

        #endregion F25011 Street List Management
    }
}
