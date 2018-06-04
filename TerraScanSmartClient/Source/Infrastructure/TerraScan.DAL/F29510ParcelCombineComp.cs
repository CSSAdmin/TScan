// -------------------------------------------------------------------------------------------
// <copyright file="F29510ParcelCombineComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Check Detail</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 19 Sep 07		D.LathaMaheswari	Created
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
    /// F29500ParcelSplitComp
    /// </summary>
    public static class F29510ParcelCombineComp
    {
        #region F29510_GetBaseParcelValue
        /// <summary>
        /// F29510 the base parcel value.
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>F29510BaseParcelCombineData</returns>
        public static F29510ParcelCombineData F29510_GetBaseParcelValue(int eventId)
        {
            F29510ParcelCombineData parcelCombineDataSet = new F29510ParcelCombineData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            Utility.LoadDataSet(parcelCombineDataSet.f29510ListParcel, "f29510_pclst_Parcel", ht);
            return parcelCombineDataSet;
        }
        #endregion F29510_GetBaseParcelValue

        #region F29510_GetCombineParcelDetails
        /// <summary>
        /// F29510 combine parcel value.
        /// </summary>
        /// <param name="parcelId">Parcel ID</param>
        /// <returns>F29510ParcelCombineData</returns>
        public static DataSet F29510_GetCombineParcelDetails(int parcelId)
        {
            DataSet parcelCombineParcelDataSet = new DataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            Utility.LoadDataSet(parcelCombineParcelDataSet, "f29510_pcget_CombineParcel", ht);
            return parcelCombineParcelDataSet;
        }
        #endregion F29510_GetCombineParcelDetails

        #region F29510_SaveCombineParcelDetails

        /// <summary>
        /// F29510 Save Combine Parcel Details
        /// </summary>
        /// <param name="combineId">CombineID</param>
        /// <param name="parcelNumber">ParcelNumber</param>
        /// <param name="combineItems">CombineItems</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        public static int F29510_SaveCombineParcelDetails(int? combineId, string parcelNumber, string combineItems, int userId,bool IsAttachment,bool IsComment,bool IsPermit,bool IsAssociation,bool IsNewConstruction)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CombineID", combineId);
            ht.Add("@ParcelNumber", parcelNumber);
            ht.Add("@CombineItems", combineItems);
            ht.Add("@UserID", userId);
            ht.Add("@IsAttachment", IsAttachment);
            ht.Add("@IsComment", IsComment);
            ht.Add("@IsPermit", IsPermit);
            ht.Add("@IsAssociation", IsAssociation);
            ht.Add("@IsNewConstruction", IsNewConstruction);
            return Utility.FetchSPExecuteKeyId("f29510_pcins_CombineDetails", ht);
        }

        #endregion F29510_SaveCombineParcelDetails

        #region F29510_CreateCombinedParcel

        /// <summary>
        /// Create Combined Parcel Details
        /// </summary>
        /// <param name="combineId">CombineID</param>
        /// <param name="eventId">EventID</param>
        /// <param name="parcelNumber">ParcelNumber</param>
        /// <param name="userId">UserID</param>
        /// <returns>F29510ParcelCombineData</returns>
        public static F29510ParcelCombineData F29510_CreateCombinedParcel(int combineId, string eventId, string parcelNumber, int userId, bool IsAttachment, bool IsComment, bool IsPermit, bool IsAssociation, bool IsNewConstruction)
        {
            F29510ParcelCombineData listOutput = new F29510ParcelCombineData();
            listOutput.OutputParams.AddOutputParamsRow("@PrimaryKeyID", string.Empty, "int", 8);
            listOutput.OutputParams.AddOutputParamsRow("@Results", string.Empty, "string", 3000);

            Hashtable ht = new Hashtable();
            ht.Add("@CombineID", combineId);
            ht.Add("@EventID", eventId);
            ht.Add("@ParcelNumber", parcelNumber);
            ht.Add("@UserID", userId);
            ht.Add("@IsAttachment", IsAttachment);
            ht.Add("@IsComment", IsComment);
            ht.Add("@IsPermit", IsPermit);
            ht.Add("@IsAssociation", IsAssociation);
            ht.Add("@IsNewConstruction", IsNewConstruction);
            //return Utility.FetchSPExecuteKeyId("f29510_pcexe_CreateParcel", ht);
            Utility.SPParameters("f29510_pcexe_CreateParcel", listOutput.OutputParams, ht, listOutput.OutputValues);
            return listOutput; 
        }

        #endregion F29510_CreateCombinedParcel
    }
}
