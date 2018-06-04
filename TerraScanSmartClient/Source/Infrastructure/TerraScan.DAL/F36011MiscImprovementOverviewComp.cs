// -------------------------------------------------------------------------------------------
// <copyright file="F36011MiscImprovementOverviewComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36011MiscImprovementOverviewComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 28/06/07         M.Vijayakumar       Created
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
    /// F36011MiscImprovementOverviewComp class file 
    /// </summary>
    public static class F36011MiscImprovementOverviewComp
    {
        #region F36011 Misc Improvement Overview

        #region List Depr Table

        /// <summary>
        /// To List the Depr Table details
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>
        /// Typed dataset containing the Depr Table details
        /// </returns>
        public static F36011MiscImprovementOverviewData F36011_ListDeprTable(int valueSliceId)
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            Utility.LoadDataSet(miscImprovementOverviewData.ListDeprTable, "f36011_pclst_DeprTable", ht);
            return miscImprovementOverviewData;
        }

        #endregion List Depr Table

        #region  List Misc Code

        /// <summary>
        ///To List Misc Code Details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Typed dataset containing the Misc Code Details</returns>
        public static F36011MiscImprovementOverviewData F36011_ListMiscCode(int valueSliceId)
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            string[] optionalParameter = new string[] { miscImprovementOverviewData.ListMICodeNew.TableName, miscImprovementOverviewData.ListValidValueSliceID.TableName };
            Utility.LoadDataSet(miscImprovementOverviewData, "f36011_pclst_MICode", ht, optionalParameter);
            return miscImprovementOverviewData;
        }

        #endregion List Misc Code

        #region List MICatalog Code

        /// <summary>
        /// To List Catalog Code Details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Typed Dataset containg the MICatalog Code Details</returns>
        public static F36011MiscImprovementOverviewData F36011_ListCatalogCode(int valueSliceId)
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            Utility.LoadDataSet(miscImprovementOverviewData.ListMICodeComboboxDatatable, "f36011_pclst_MICatalogCode", ht);
            return miscImprovementOverviewData;
        }

        #endregion List MICatalog Code

        #region List Misc Improvements

        /// <summary>
        /// To List Misc Improvements details.
        /// </summary>
        /// <param name="miscId">The misc id.</param>
        /// <returns>Typed dataset containing the Misc Improvements details</returns>
        public static F36011MiscImprovementOverviewData F36011_ListMiscImprovements(int miscId)
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            Hashtable ht = new Hashtable();
            ht.Add("@MID", miscId);
            Utility.LoadDataSet(miscImprovementOverviewData.ListMiscImprovementsNew, "f36011_pcget_MiscImprovements", ht);
            return miscImprovementOverviewData;
        }

        #endregion List Misc Improvements

        #region Delete MICode

        /// <summary>
        /// To Delete MID in Misc Improvements OverView.
        /// </summary>
        /// <param name="miscId">The misc id.</param>
        /// <param name="userId">userId</param>
        public static void F36011_DeleteMICode(int miscId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@MID", miscId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f36011_pcdel_MI_Catalog", ht);
        }

        #endregion Delete MICode

        #region Save Misc Improvements

        /// <summary>
        /// To Save the Misc Improvements Overview
        /// </summary>
        /// <param name="miscmId">mid</param>
        /// <param name="miscItems">xml string containing the Misc Improvents Overview Details</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer value containing the key id</returns>
        public static int F36011_SaveMiscImprovement(int miscmId, string miscItems, int userId)
        {
            Hashtable ht = new Hashtable();

            if (miscmId > 0)
            {
                ht.Add("@MID", miscmId);
            }

            ht.Add("@MiscItems", miscItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f36011_pcins_MiscImprovements", ht);
        }

        #endregion Save Misc Improvements

        #region List Qualit Comm

        /// <summary>
        /// F36011_s the list quality comm.
        /// </summary>
        /// <returns>Typed dataset containing the Quality Comm list table</returns>
        public static F36011MiscImprovementOverviewData F36011_ListQualityComm()
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(miscImprovementOverviewData.ListQualityComm, "f36011_pclst_QualityComm", ht);
            return miscImprovementOverviewData;
        }

        #endregion List Qualit Comm

        #region List Qualit Res

        /// <summary>
        /// F36011_s the list quality res.
        /// </summary>
        /// <returns>Typed dataset containing the Quality Res table</returns>
        public static F36011MiscImprovementOverviewData F36011_ListQualityRes()
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(miscImprovementOverviewData.ListQualityRes, "f36011_pclst_QualityRes", ht);
            return miscImprovementOverviewData;
        }

        #endregion List Qualit Comm

        #region List Condition

        /// <summary>
        /// F36011_s the list Condition
        /// </summary>
        /// <returns>Typed dataset containing the Condition table</returns>
        public static F36011MiscImprovementOverviewData F36011_ListCondition()
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(miscImprovementOverviewData.ListCondition, "f36011_pclst_Condition", ht);
            return miscImprovementOverviewData;
        }

        #endregion List Condition

        #region List DeprFuncCategory

        /// <summary>
        /// F36011_s the list Depr FuncCategory
        /// </summary>
        /// <returns>Typed dataset containing the Depr FuncCategory table</returns>
        public static F36011MiscImprovementOverviewData F36011_ListDeprFuncCategory()
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(miscImprovementOverviewData.ListDeprFuncCategory, "f36011_pclst_DeprFuncCategory", ht);
            return miscImprovementOverviewData;
        }

        #endregion List DeprFuncCategory

        #region List MiscCatalogChoice

        /// <summary>
        /// F36012_s the list misc catalog choice.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="fieldNum">The field num.</param>
        /// <returns>Typed dataset containing the MiscCatalogChoice table</returns>
        public static F36011MiscImprovementOverviewData F36012_ListMiscCatalogChoice(int miscCodeId, int fieldNum)
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            Hashtable ht = new Hashtable();
            ht.Add("@MICodeID", miscCodeId);
            ht.Add("@FieldNum", fieldNum);
            Utility.LoadDataSet(miscImprovementOverviewData.ListMiscCatalogChoice, "f36012_pclst_MiscCatalogChoice", ht);
            return miscImprovementOverviewData;
        }

        #endregion List MiscCatalogChoice

        #region recalc MiscImprovement
        /// <summary>
        /// To List the recalcMiscDepr Table details
        /// </summary>
        /// <param name="withPrimary">The withPrimary.</param>
        /// /// <param name="yearIn">yearIn</param>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="condition">condition</param>
        /// <param name="economicLife">economicLife</param>
        /// <param name="effectiveAge">effectiveAge</param>
        /// <param name="physDeprPerc">physDeprPerc</param>
        /// <param name="funcDeprPerc">funcDeprPerc</param>
        /// <param name="BaseCost">BaseCost</param>
        /// <param name="physDepr">physDepr</param>
        /// <param name="funcDepr">funcDepr</param>
        /// <param name="valueSliceId">valueSliceId</param>
        /// <param name="miscCodeId">miscCodeId</param>
        /// <returns>
        /// Typed dataset containing the reCalcMiscDepr Table details
        /// </returns>
        public static F36011MiscImprovementOverviewData F36011_RecalcMiscImprovement(bool withprimary, int? yearIn, string condition, int? economicLife, int? effectiveAge, decimal? physDeprPerc, decimal? funcDeprPerc, decimal? BaseCost, decimal? physDepr, decimal? funcDepr, int valueSliceId, int miscCodeId)
        {
            F36011MiscImprovementOverviewData miscImprovementOverviewData = new F36011MiscImprovementOverviewData();
            Hashtable ht = new Hashtable();
            ht.Add("@WithPrimary", withprimary);
            ht.Add("@YearIn", yearIn);
            ht.Add("@Condition", condition);
            ht.Add("@EconomicLife", economicLife);
            ht.Add("@EffectiveAge", effectiveAge);
            ht.Add("@PhysDeprPercent", physDeprPerc);
            ht.Add("@FuncDeprPercent", funcDeprPerc);
            ht.Add("@BaseCost", BaseCost);
            ht.Add("@PhysDepr", physDepr);
            ht.Add("@FuncDepr", funcDepr);
            ht.Add("@ValuesliceID", valueSliceId);
            ht.Add("@MICodeID", miscCodeId);
            Utility.LoadDataSet(miscImprovementOverviewData.RecalcMiscImprovement, "f36011_pcget_RecalcMiscImprovement", ht);
            return miscImprovementOverviewData;
        }

        #endregion recalc MiscImprovement

        #endregion F36011 Misc Improvement Overview

        # region Copy or Move Misc Improvements.
        /// <summary>
        /// To get the copy or move object Details.
        /// </summary>
        /// <param name="parcelId"> Parcel Id</param>
        /// <returns>Typed dataset containing Misc Improvement Details</returns>
        public static F3602CopyMoveMiscImprovement GetObjectDetails(int parcelId)
        {
            F3602CopyMoveMiscImprovement miscImprovementData = new F3602CopyMoveMiscImprovement();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            Utility.LoadDataSet(miscImprovementData.GetObjectDetailsTable, "f3206_pclst_ObjectsList", ht);
            return miscImprovementData;
        }

        /// <summary>
        /// To get the object type Details.
        /// </summary>
        /// <param name="parcelId"> Parcel Id</param>
        /// <returns>Typed dataset containing Misc Improvement Details</returns>
        public static F3602CopyMoveMiscImprovement GetObjectTypesList()
        {
            F3602CopyMoveMiscImprovement miscImprovementData = new F3602CopyMoveMiscImprovement();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(miscImprovementData.ObjectTypeTable, "f3206_pclst_ObjectTypes", ht);
            return miscImprovementData;
        }

        /// <summary>
        /// To get the object values slices list.
        /// </summary>
        /// <param name="parcelId"> Parcel Id</param>
        /// <returns>Typed dataset containing Misc Improvement Details</returns>
        public static F3602CopyMoveMiscImprovement GetValueSlicesList(int parcelId, int objectId)
        {
            F3602CopyMoveMiscImprovement miscImprovementData = new F3602CopyMoveMiscImprovement();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@ObjectID", objectId);
            Utility.LoadDataSet(miscImprovementData.GetValueSliceDetails, "f3206_pclst_ValueslicesList", ht);
            return miscImprovementData;
        }

        /// <summary>
        /// To get the misc improvement list.
        /// </summary>
        /// <param name="parcelId"> Parcel Id</param>
        /// <returns>Typed dataset containing Misc Improvement Details</returns>
        public static F3602CopyMoveMiscImprovement GetMiscImprovementsList(int valueSliceID)
        {
            F3602CopyMoveMiscImprovement miscImprovementData = new F3602CopyMoveMiscImprovement();
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceID);
            Utility.LoadDataSet(miscImprovementData.GetMiscImprovementDetails, "f3206_pclst_MiscImpsList", ht);
            return miscImprovementData;
        }

        /// <summary>
        /// To enable the copy or move miscimprovement details.
        /// </summary>
        /// <param name="parcelId"> Parcel Id</param>
        /// <returns>Typed dataset containing Misc Improvement Details</returns>
        public static F3602CopyMoveMiscImprovement F3602_ProcessMiscImprovements(string copyMove, int parcelId, bool isNewObject, int existingObjectId, int newObjectTypeId, bool isNewValueslice, int existingValueslice, string miscImprovements, int userId)
        {
            F3602CopyMoveMiscImprovement miscImprovementData = new F3602CopyMoveMiscImprovement();
            Hashtable ht = new Hashtable();
            ht.Add("@CopyOrMove", copyMove);
            ht.Add("@ParcelID", parcelId);
            ht.Add("@IsCreateNewObject", isNewObject);
            ht.Add("@ExistingObjectID", existingObjectId);
            ht.Add("@NewObjectTypeID", newObjectTypeId);
            ht.Add("@IsCreateNewValueSlice", isNewValueslice);
            ht.Add("@ExistingValueSliceID", existingValueslice);
            ht.Add("@MiscImprovements", miscImprovements);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(miscImprovementData._F3602CopyMoveMiscImprovement, "f3206_pcexe_MoveOrCopyMiscImps", ht);
            return miscImprovementData;
        }

        #endregion Copy or Move Misc Improvements.
    }
}
