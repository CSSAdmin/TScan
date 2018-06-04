// -------------------------------------------------------------------------------------------
// <copyright file="F25003SitusManagementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F25003SitusManagementComp.cs </summary>
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
    /// F25003SitusManagementComp class file
    /// </summary>
    public static class F25003SitusManagementComp
    {
        #region F25003 Situs Management

        #region List Situs Management Details

        /// <summary>
        /// To List Situs Mangement Details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="situsId">The situs id.</param>
        /// <returns>Typed Dataset containing the Situs Mangement Details</returns>
        public static F25003SitusManagementData F25003_ListSitusMangement(int parcelId, int situsId)
        {
            F25003SitusManagementData situsManagementData = new F25003SitusManagementData();
            Hashtable ht = new Hashtable();
            string[] optionalParameter = new string[] { situsManagementData.ListSitusManagement.TableName, situsManagementData.ListParcelValidID.TableName };
            
            if (parcelId != -999)
            {
                ht.Add("@ParcelID", parcelId);
            }

            if (situsId != -999)
            {
                ht.Add("@SitusID", situsId);                
            }

            Utility.LoadDataSet(situsManagementData, "f25003_pclst_Situs", ht, optionalParameter);
            return situsManagementData;
        }

        #endregion List Situs Management Details

        #region List Street Details

        /// <summary>
        /// To List Street Details.
        /// </summary>
        /// <returns>Typed Dataset containing the Street Details</returns>
        public static F25003SitusManagementData F25003_ListStreet()
        {
            F25003SitusManagementData situsManagementData = new F25003SitusManagementData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(situsManagementData.ListStreet, "f25003_pclst_Street", ht);
            return situsManagementData;
        }        

        #endregion List Street Name Details       

        #region List Unit Type Details

        /// <summary>
        /// To list Unit Type Details.
        /// </summary>
        /// <returns>Typed DataSet containing the Unit Type Details</returns>
        public static F25003SitusManagementData F25003_ListUnitType()
        {
            F25003SitusManagementData situsManagementData = new F25003SitusManagementData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(situsManagementData.ListUnitType, "f25003_pclst_Unit", ht);
            return situsManagementData;
        }

        #endregion List Unit Type Details

        #region Save Situs Management

        /// <summary>
        /// To Save List Mangement Details.
        /// </summary>
        /// <param name="situsId">The situs id.</param>
        /// <param name="situsItems">The situs items.</param>
        /// <param name="userId">userId</param>
        /// <returns>Intger value containing the new SitusID</returns>
        public static int F25003_SaveListMangement(int situsId, string situsItems, bool isFuturePush ,int userId)
        {
            Hashtable ht = new Hashtable();

            if (situsId != -999)
            {
                ht.Add("@SitusID", situsId);
            }
            
            ht.Add("@SitusItems", situsItems);
            ht.Add("@IsFuturePush", isFuturePush);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f25003_pcins_Situs", ht);
        }

        #endregion Save Situs Management

        #region Delete Situs Management

        /// <summary>
        /// To Delete the Situs management
        /// </summary>
        /// <param name="situsId">situsId</param>
        /// <param name="userId">userId</param>
        public static void F25003_DeleteSitusManagement(int situsId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SitusID", situsId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f25003_pcdel_Situs", ht);
        }

        #endregion Delete Situs Management

        #endregion F25003 Situs Management
    }
}
