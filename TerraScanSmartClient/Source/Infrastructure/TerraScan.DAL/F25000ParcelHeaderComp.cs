// -------------------------------------------------------------------------------------------
// <copyright file="F25000ParcelHeaderComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F25000ParcelHeaderComp.cs methods
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
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    /// <summary>
    /// Data Access Layer which talks to the DB directly for F25000
    /// </summary>
   public static class F25000ParcelHeaderComp
   {
       /// <summary>
       /// F25000_GetParcelDetails
       /// </summary>
       /// <param name="parcelId">parcelId</param>
       /// <returns>Typed dataset</returns>
       public static F25000ParcelHeaderData F25000_GetParcelDetails(int parcelId)
       {
           F25000ParcelHeaderData parcelHeaderData = new F25000ParcelHeaderData();
           Hashtable ht = new Hashtable();
           ht.Add("@ParcelID", parcelId);
           Utility.LoadDataSet(parcelHeaderData.f25000ParcelHeader, "f25000_pcget_ParcelHeader", ht);
           return parcelHeaderData;
       }

       /// <summary>
       /// UpdateParcelHeaderDetails
       /// </summary>
       /// <param name="parcelId">parcelID</param>
       /// <param name="parcelDetails">parcelDetails</param>
       /// <param name="userId">userId</param>
       /// <param name="rollYear">The roll year.</param>
       /// <returns>Integer value</returns>
       public static int UpdateParcelHeaderDetails(int parcelId, string parcelDetails,bool isCopyHeader, int userId)
       {
           Hashtable ht = new Hashtable();
           ht.Add("@ParcelID", parcelId);
           ht.Add("@ParcelItems", parcelDetails);
           ht.Add("@IsCopyHeader", isCopyHeader);
           ht.Add("@UserID", userId);
           return Utility.FetchSPExecuteKeyId("f25000_pcupd_ParcelDetails", ht);
       }

       #region F25000 list Primary Improvement Selection


       /// <summary>
       /// F9075_s the list template.
       /// </summary>
       /// <returns>listtemplateData</returns>
       public static F25000ParcelHeaderData ListPrimaryImprovement()
       {
           F25000ParcelHeaderData getPrimaryImprovementData = new F25000ParcelHeaderData();
           Hashtable ht = new Hashtable();
           Utility.LoadDataSet(getPrimaryImprovementData.f25000ListParcelImprovement, "f25000_pclst_ParcelImprovement", ht);
           return getPrimaryImprovementData;
       }

       #endregion 

       #region F25000 list Primary Land Type Selection


       /// <summary>
       /// F9075_s the list template.
       /// </summary>
       /// <returns>listtemplateData</returns>
       public static F25000ParcelHeaderData ListPrimaryLandType()
       {
           F25000ParcelHeaderData getPrimaryLandTypeData = new F25000ParcelHeaderData();
           Hashtable ht = new Hashtable();
           Utility.LoadDataSet(getPrimaryLandTypeData.f25000ListParcelLandTypes, "f25000_pclst_ParcelLandTypes", ht);
           return getPrimaryLandTypeData;
       }

       #endregion 
    }
}
