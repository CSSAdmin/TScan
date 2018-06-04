// -------------------------------------------------------------------------------------------
// <copyright file="F27007ParcelheaderSlimComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F27007ParcelheaderSlimComp</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
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
    /// Data Access Layer which talks to the DB directly for F27007
    /// </summary>
   public static class F27007ParcelheaderSlimComp
   {
       /// <summary>
       /// F27007_GetParcelHeaderSlimDetails Class file
       /// </summary>
       /// <param name="parcelId">parcelId</param>
       /// <returns>typed dataset</returns>
       public static F27007ParcelHeaderSlimData F27007_GetParcelHeaderSlimDetails(int parcelId)
       {
           F27007ParcelHeaderSlimData form27007parcelHeaderSlimData = new F27007ParcelHeaderSlimData();
           Hashtable ht = new Hashtable();
           ht.Add("@ParcelID", parcelId);
           Utility.LoadDataSet(form27007parcelHeaderSlimData.f27007ParcelHeaderSlim, "f25000_pcget_ParcelHeader", ht);
           return form27007parcelHeaderSlimData;
       }
    }
}
