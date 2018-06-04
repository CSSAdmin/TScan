// -------------------------------------------------------------------------------------------
// <copyright file="F25009LegalManagementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F25009LegalManagementComp</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// May 08 2007      Karthikeyan.B      Added
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
    /// F25009LegalManagementComp Class File
    /// </summary>
    public static class F25009LegalManagementComp
    {
        #region Get Legal Management

        /// <summary>
        ///  To Load F25009 Legal Management.
        /// </summary>
        /// <param name="parcelId">The Parcel ID.</param>
        /// <param name="userId">userId</param>
        /// <returns>Typed DataSet Containing All the Legal Management Details</returns>
        public static F25009LegalManagementData F25009_GetLegalManagement(int parcelId, int userId)
        {
            F25009LegalManagementData legalManagementData = new F25009LegalManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(legalManagementData.GetLegalManagement, "f25009_pcget_Legal", ht);
            return legalManagementData;
        }

        #endregion

        #region Save Legal Management

        /// <summary>
        /// To Save F25009 Legal Management.
        /// </summary>
        /// <param name="legalId">The Legal ID.</param>
        /// <param name="legalItems">The XML string Containing All the Legal Items.</param>
        /// <param name="userId">userId</param>
        /// <returns>The integer value containing Parcel Id</returns>
        public static int F25009_SaveLegalManagement(int legalId, string legalItems,bool isFuturePush, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@LegalID", legalId);
            ht.Add("@LegalItems", legalItems);
            ht.Add("@UserID", userId);
            ht.Add("@IsFuturePush", isFuturePush);
            return Utility.FetchSPExecuteKeyId("f25009_pcins_Legal", ht);
        }

        #endregion

        #region List Sub-Division

        /// <summary>
        ///  To Load Sub Division.
        /// </summary>        
        /// <returns>Typed DataSet Containing All the Subdivisions</returns>
        public static F25009LegalManagementData F25009_ListSubdivision()
        {
            F25009LegalManagementData legalManagementData = new F25009LegalManagementData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(legalManagementData.ListSubdivision, "f25009_pclst_Subdivision", ht);
            return legalManagementData;
        }

        #endregion
    }
}
