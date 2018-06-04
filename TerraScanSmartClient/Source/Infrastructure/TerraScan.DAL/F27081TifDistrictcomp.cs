// -------------------------------------------------------------------------------------------
// <copyright file="F27081TifDistrictComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F227081TifDistrictComp.cs methods
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
    using System.Linq;
    using System.Text;
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.Dal;
    using TerraScan.DataLayer;
    using System.Data;

    /// <summary>
    ///  Data Access Layer which talks to the DB directly for F27081
    /// </summary>
    public static class F27081TifDistrictcomp
    {

        /// <summary>
        /// F27081_ the get TIF District details.
        /// </summary>
        /// <param name="TIFId">The TIF id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The TIFDistrict dataset.</returns>
        public static F27081TIFDistrictData F27081_GetTIFDistrictDetails(int TIFIdDistId)
        {
            F27081TIFDistrictData TIFDistrictData = new F27081TIFDistrictData();
            Hashtable ht = new Hashtable();
            //DataSet ds = new DataSet();
            ht.Add("@TIFDistrictID", TIFIdDistId);
           // ht.Add("@UserID", userId);
            string[] tableNames = new string[] { TIFDistrictData.F27081TIFDistrictDataTable.TableName, TIFDistrictData.F27081_TIFGridDistrictDataTable.TableName };
            Utility.LoadDataSet(TIFDistrictData, "f22081_pcget_TIFDistrict", ht,tableNames);
            return TIFDistrictData;
        }



        /// <summary>
        /// F27081_SaveTIFDistrict
        /// </summary>
        /// <param name="TIFId">TIFID</param>
        /// <param name="TIFDetails">TIFDetails</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public static int F27081_SaveTIFDistrictDetails(int? TIFIdDistId, string TIFDetails, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TIFDistrictID", TIFIdDistId);
            ht.Add("@TIFDistrictItems", TIFDetails);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f22081_pcins_TIFDistrict", ht);
        }

        /// <summary>
        /// F27081_s the delete TIF district details.
        /// </summary>
        /// <param name="TIFIdDistId">The TIF id dist id.</param>
        /// <param name="userId">The user id.</param>
        public static string F27081_DeleteTIFDistrictDetails(int TIFIdDistId, int userId, bool IsReadyToDelete)
         {
            Hashtable ht = new Hashtable();
            ht.Add("@TIFDistrictID", TIFIdDistId);
            ht.Add("@UserID", userId);
            ht.Add("@IsReadyToDelete", IsReadyToDelete);
            return Utility.FetchSingleOuputParameter("f22081_pcdel_TIFDistrict", ht, "@Message");
         }
        
        /// <summary>
        /// F27081_s the get TIF combo box details.
        /// </summary>
        /// <returns></returns>
        public static F27081TIFDistrictData F27081_GetTIFComboBoxDetails(int RollYear)
        {
            F27081TIFDistrictData TIFDistrictData = new F27081TIFDistrictData();
            Hashtable ht = new Hashtable();
            ht.Add("@RollYear", RollYear);
            Utility.LoadDataSet(TIFDistrictData.F27081TIFSubfundComboboxDataTable, "f22081_pclst_TIFSubFund", ht);
            return TIFDistrictData;
        }

    }
}
