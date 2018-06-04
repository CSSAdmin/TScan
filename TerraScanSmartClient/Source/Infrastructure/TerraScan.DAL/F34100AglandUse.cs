// -------------------------------------------------------------------------------------------
// <copyright file="F34100AglandUse.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F34100AglandUse.cs methods
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
    public static class F34100AglandUse
    {
        /// <summary>
        /// F34100_ the get AglandUseDetails.
        /// </summary>
        /// <param name="AglandID">The TIF id.</param>
        /// <returns>The TIFDistrict dataset.</returns>
        public static F34100AglandUseData F34100_GetAglandDetails(int AglandID)
        {
            F34100AglandUseData AglandData = new F34100AglandUseData();
            Hashtable ht = new Hashtable();
            //DataSet ds = new DataSet();
            ht.Add("@AglandID", AglandID);
            string[] tableNames = new string[] { AglandData.AglandUseDataTable.TableName, AglandData.AglandTypeDataTable.TableName,AglandData.AglandMethodDataTable.TableName};
            Utility.LoadDataSet(AglandData, "f39100_pcget_AglandUse", ht, tableNames);
            return AglandData;
        }

        /// <summary>
        /// F34100_SaveAglandUse
        /// </summary>
        /// <param name="AglandID">AglandID</param>
        /// <param name="AglandIDDetails">AglandIDDetails</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public static int F34100_SaveAglandDetails(int? AglandID, string AglandDetails, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@AglandID", AglandID);
            ht.Add("@AglandDetails", AglandDetails);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f39100_pcins_AglandUse", ht);
        }

        /// <summary>
        /// F39100s the delete Agland Values.
        /// </summary>
        /// <param name="AglandId">The Agland id.</param>
        /// <param name="userId">The user id.</param>
        public static void F34100_DeleteAglandDetails(int AglandID, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@AglandID", AglandID);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f39100_pcdel_AglandUse", ht);
        }



    }
}
