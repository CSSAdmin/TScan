// -------------------------------------------------------------------------------------------
// <copyright file="F36000MarshalAndSwiftComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F36000MarshalAndSwiftComp.cs </summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
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
    /// F36000MarshalAndSwiftComp class file
    /// </summary>
    public static class F36000MarshalAndSwiftComp
    {
        #region F36000 Marshal & Swift

        #region Get House Type Collection

        /// <summary>
        /// To Get Marshal and swift House Type collection.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Typed DataSet containing Marshal and swift House Type collection details.</returns>
        public static F36000MarshalAndSwiftData F36000_GetHouseTypeCollection(int valueSliceId)
        {
            F36000MarshalAndSwiftData marshalAndSwiftData = new F36000MarshalAndSwiftData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            string[] tableName = new string[] { marshalAndSwiftData.ListHouseTypeCollectionDatatable.TableName, marshalAndSwiftData.ListEstimateIDDataTable.TableName, marshalAndSwiftData.ListDeprTable.TableName, marshalAndSwiftData.ListDeprValueDataTable.TableName };
            Utility.LoadDataSet(marshalAndSwiftData, "f36000_pcget_HouseTypeCollection", ht, tableName);
            return marshalAndSwiftData;
        }

        #endregion Get House Type Collection

        #region Depreciation Percentage

        /// <summary>
        /// F36000_s the get depr percentage.
        /// </summary>
        /// <param name="age">The age.</param>
        /// <param name="objectCondition">The object condition.</param>
        /// <param name="deprTableId">The depr table id.</param>
        /// <returns>string</returns>
        public static string F36000_GetDeprPercentage(int age, decimal objectCondition, int deprTableId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@Age", age);
            ht.Add("@ObjectCondition", objectCondition);
            ht.Add("@DeprTableID", deprTableId);
            return Utility.FetchSPExecuteKeyString("f36000_pcget_DepreciationPercent", ht);
        }

        #endregion Depreciation Percentage

        #region Depr Table Name

        /// <summary>
        /// F36000_s the name of the get depr table.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="propertyQuality">The property quality.</param>
        /// <returns>int</returns>
        public static int F36000_GetDeprTableNameId(int valueSliceId, int propertyQuality)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            ht.Add("@Quality", propertyQuality);
            return Utility.FetchSPExecuteKeyId("f36000_pcget_DepreciationTableName", ht);
        }

        #endregion Depr Table Name

        #region Depr Save

        /// <summary>
        /// F36000_s the save depreciation details.
        /// </summary>
        /// <param name="depreciationXml">The depreciation XML.</param>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public static int F36000_SaveDepreciationDetails(string depreciationXml, int valueSliceId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            ht.Add("@DepreciationItems", depreciationXml);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f36000_pcins_Depreciation", ht);
        }  

        #endregion Depr Save

        #endregion F36000 Marshal & Swift
    }
}
