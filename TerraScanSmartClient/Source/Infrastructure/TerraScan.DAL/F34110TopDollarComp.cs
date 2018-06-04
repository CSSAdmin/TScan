// -------------------------------------------------------------------------------------------
// <copyright file="F34110TopDollarComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F34110TopDollar.cs methods
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
    ///  Data Access Layer which talks to the DB directly for F34110
    /// </summary>
    public static class F34110TopDollarComp
    {
        /// <summary>
        /// F34110_ the get TopDollarDetails.
        /// </summary>
        /// <param name="TopDollarID">TopDollarId.</param>
        /// <returns>The TopDollar dataset.</returns>
        public static F34110TopDollarData F34110_GetTopDollarDetails(int TopDollarID)
        {
            F34110TopDollarData TopDollarData = new F34110TopDollarData();
            Hashtable ht = new Hashtable();
            ht.Add("@TopDollarID", TopDollarID);
            //string[] tableNames = new string[] { TopDollarData.TopDollarDataTable.TableName };
            Utility.LoadDataSet(TopDollarData.TopDollarDataTable, "f39110_pcget_TopDollar", ht);
            return TopDollarData;
        }

        /// <summary>
        /// F34110_SaveTopDollar
        /// </summary>
        /// <param name="TopDollarID">TopDollarID</param>
        /// <param name="TopDollarDetails">TopDollarDetails</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public static int F34110_SaveTopDollarDetails(int? TopDollarID, string TopDollarDetails, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TopDollarID", TopDollarID);
            ht.Add("@TopDollarDetails", TopDollarDetails);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f39110_pcins_TopDollar", ht);
        }

        /// <summary>
        /// F39110s the delete TopDollar Values.
        /// </summary>
        /// <param name="TopDollarID">The TopDollarID.</param>
        /// <param name="userId">The user id.</param>
        public static void F34110_DeleteTopDollarDetails(int TopDollarID, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TopDollarID", TopDollarID);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f39110_pcdel_TopDollar", ht);
        }
       
        ///<summary>
        /// used to Calculate Non Crop Dollar
        /// </summary>
        /// <param name="CropTopDollar">Crop Top DOllar</param>
        /// <param name="County Factor">County Factor</param>
        public static F34110TopDollarData F34110_CropTopDollar(decimal CropDollar, decimal CountyFactor)
        {
            F34110TopDollarData TopDollarData = new F34110TopDollarData();
            Hashtable ht = new Hashtable();
            ht.Add("@CropTopDollar", CropDollar);
            ht.Add("@CountyFactor", CountyFactor);
            Utility.LoadDataSet(TopDollarData.NonCropDollarDataTable, "f39110_pcget_NonCropDollar", ht);
            return TopDollarData;
         
        }
    }
}
