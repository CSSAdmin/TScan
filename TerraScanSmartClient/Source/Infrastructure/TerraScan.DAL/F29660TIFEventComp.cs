// -------------------------------------------------------------------------------------------
// <copyright file="F29660TifEventComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F29660TIFEventComp.cs methods
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
    using TerraScan.DataLayer; 
    using TerraScan.Dal;
    using System.Data ;

    public static class F29660TIFEventComp
    {

        /// <summary>
        /// F29660_ the get TIF District details.
        /// </summary>
        /// <param name="EventId">The Event id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The TIFEvent dataset.</returns>
        public static F29660TIFEventData F29660_GetTIFEventDetails(int EventId, int userId)
        {
            F29660TIFEventData TIFEventData = new F29660TIFEventData();
            Hashtable ht = new Hashtable();
            DataSet ds = new DataSet();
            ht.Add("@EventID", EventId);
            ht.Add("@User", userId);
            string[] tableNames = new string[] { TIFEventData.F29660TIFEventDataTable.TableName,TIFEventData.DistrictComboboxDataTable.TableName };
            Utility.LoadDataSet(TIFEventData, "f29660_pcget_TIF", ht, tableNames);
            //Utility.FillDataSet(ds, "f29660_pcget_TIF", ht);
            return TIFEventData;
            //Utility.FillDataSet(TIFEventData.F29660TIFEventDataTable , "f29660_pcget_TIF", ht);
            //return TIFEventData;
        }


        /// <summary>
        /// F29660_SaveTIFEvent
        /// </summary>
        /// <param name="EventId">EventID</param>
        /// <param name="TIFId">TIFID</param>
        /// <param name="BaseValue">BaseValue</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public static int F29660_SaveTIFEventDetails(int? EventId,int TIFId, decimal BaseValue, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", EventId);
            ht.Add("@TIFID", TIFId);
            ht.Add("@BaseValue", BaseValue);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f29660_pcins_TIF", ht);
        }



    }
}
