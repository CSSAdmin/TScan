// -------------------------------------------------------------------------------------------
// <copyright file="F35001ValueSliceHeaderComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access ValueSlice Header related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------;

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;
    using System.Collections;

    /// <summary>
    /// F35001 ValueSlice Header Comp
    /// </summary>
    public static class F35001ValueSliceHeaderComp
    {
        #region Get Value Slice Header

        /// <summary>
        /// F35001_s the get value slice header.
        /// </summary>
        /// <param name="valueSliceId">The value slice ID.</param>
        /// <returns>the DataSet with the Header and Adjustment Values.</returns>
        public static F35001ValueSliceHeaderData F35001_GetValueSliceHeader(int valueSliceId)
        {
            F35001ValueSliceHeaderData valueSliceHeaderData = new F35001ValueSliceHeaderData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            string[] tableName = new string[] { valueSliceHeaderData.GetValueSliceHeader.TableName };
            Utility.LoadDataSet(valueSliceHeaderData, "f35001_pcget_ValueSliceHeader", ht, tableName);
            return valueSliceHeaderData;
        }

        #endregion

        #region Get Adjustment Slice Value

        /// <summary>
        /// F35001_s the get adjustment slice value.
        /// </summary>
        /// <param name="valueSliceId">The value slice ID.</param>
        /// <param name="type">The type.</param>
        /// <param name="isvalue">The is value.</param>
        /// <param name="adjustmentValue">The adjustment value.</param>
        /// <returns>Object Contains the Adjustment Value.</returns>
        public static string F35001_GetAdjustmentSliceValue(int valueSliceId, byte type, bool isvalue, decimal adjustmentValue)
        {
            string adjustmentSliceValue;
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            ht.Add("@Type", type);
            ht.Add("@IsValue", isvalue);
            ht.Add("@AdjustmentValue", adjustmentValue);
            adjustmentSliceValue = Utility.FetchSPExecuteKeyString("f35002_pcget_AdjustmentSliceValue", ht);
            return adjustmentSliceValue;
        }

        #endregion

        #region List Adjustment Types

        /// <summary>
        /// F35002_s the type of the list adjustment.
        /// </summary>
        /// <param name="masterFromNo">The master from no.</param>
        /// <returns>Adjustment Types dataTable</returns>
        public static F35001ValueSliceHeaderData.ListAdjustmentTypeDataTable F35002_ListAdjustmentType(int? masterFromNo)
        {
            F35001ValueSliceHeaderData valueSliceHeaderData = new F35001ValueSliceHeaderData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", masterFromNo);
            Utility.LoadDataSet(valueSliceHeaderData.ListAdjustmentType, "f35002_pclst_AdjustmentType", ht);
            return valueSliceHeaderData.ListAdjustmentType;
        }

        #endregion

        #region Delete Value Slice

        /// <summary>
        /// F35001_s the delete value slice.
        /// </summary>
        /// <param name="valueSliceId">The value slice ID.</param>
        /// <param name="userId">userId</param>
        public static void F35001_DeleteValueSlice(int valueSliceId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f35001_pcdel_ValueSlice", ht);
        }

        #endregion
    }
}
