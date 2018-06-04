// -------------------------------------------------------------------------------------------
// <copyright file="F1031SpecialDistrictAssessmentComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F1031 Special District Assessment Details</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20 Nov 06		JYOTHI P	            Created
// 20 Nov 06        JYOTHI P                Added F8044_ListMatetialDetails method
// 20 Nov 06        JYOTHI P                Added F8044_ListMaterialsResource method
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
    /// Main class for Special District Assessment Component
    /// </summary>
    public static class F1031SpecialDistrictAssessmentComp
    {
        #region  Working File Id

        #region List Special District Assessment Details 

        /// <summary>
        /// Lists the Special District Assessment details
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>returns dataset containing specialDistrict Assessment Details</returns>
        public static F1031SpecialDistrictAssessmentData F16031_ListDistrictAssessmentDetails(int workingfileId)
        {
            F1031SpecialDistrictAssessmentData specialDistrictAssessmentData = new F1031SpecialDistrictAssessmentData();
            Hashtable ht = new Hashtable();
            ht.Add("@WorkingFileID", workingfileId);
            string[] tableName = new string[] { specialDistrictAssessmentData.ListDistrictAssessmentProperty.TableName, specialDistrictAssessmentData.ListDistrictAssessmentRates.TableName };
            Utility.LoadDataSet(specialDistrictAssessmentData, "f16031_pcget_SpecialAssessment", ht, tableName);
            return specialDistrictAssessmentData;
        }

        #endregion List Special District Assessment Details

        #region List Special District

        /// <summary>
        /// Lists the Special District details
        /// </summary>
        /// <param name="sadistrictId">The sadistrict id.</param>
        /// <returns>
        /// returns dataset containing specialDistrict Details
        /// </returns>
        public static F1031SpecialDistrictAssessmentData F16031_ListDistrictAssessment(int sadistrictId)
        {
            F1031SpecialDistrictAssessmentData specialDistrictAssessmentData = new F1031SpecialDistrictAssessmentData();
            Hashtable ht = new Hashtable();
            ht.Add("@SADistrictID", sadistrictId);
            string[] tableName = new string[] { specialDistrictAssessmentData.ListSpecialDistrictAssessmentProperty.TableName, specialDistrictAssessmentData.ListDistrictAssessmentRates.TableName };
            Utility.LoadDataSet(specialDistrictAssessmentData, "f16031_pclst_SpecialAssessmentDistrict", ht, tableName);
            return specialDistrictAssessmentData;
        }

        #endregion List Special District

        #region List Special  Assessment ParcelID

        /// <summary>
        /// Lists the Special District Assessment ParcelID
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="rollYear">The rollYear id.</param>
        /// <returns>
        /// returns dataset containing District Assessment ParcelID
        /// </returns>
        public static F1031SpecialDistrictAssessmentData F16031_GetSpecialAssessmentParcel(string parcelNumber, int? parcelId, int? rollYear)
        {
            F1031SpecialDistrictAssessmentData specialDistrictAssessmentData = new F1031SpecialDistrictAssessmentData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelNumber", parcelNumber);
            ht.Add("@ParcelID", parcelId);
            ht.Add("@RollYear", rollYear);
            Utility.LoadDataSet(specialDistrictAssessmentData.GetDistrictAssessmentParcelID, "f16031_pclst_SpecialAssessmentParcel", ht);
            return specialDistrictAssessmentData;
        }

        #endregion List Special  Assessment ParcelID

        #region Delete District Assessment

        /// <summary>
        /// Deletes the District Assessment
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static F1031SpecialDistrictAssessmentData F16031_DeleteDistrictAssessment(int statementId, int userId)
        {
            F1031SpecialDistrictAssessmentData ListInput = new F1031SpecialDistrictAssessmentData();
            ListInput.ListInputVAlue.AddListInputVAlueRow("@IsPass", string.Empty, "bool", 2);
            ListInput.ListInputVAlue.AddListInputVAlueRow("@ErrorMessage", string.Empty, "string", 3000);
            Hashtable ht = new Hashtable();
            ht.Add("@WorkingFileID", statementId);
            ht.Add("@UserID", userId);
            Utility.SPParameters("f16031_pcdel_SpecialAssessment", ListInput.ListInputVAlue, ht, ListInput.ListDeleteOutputValue);
            return ListInput;
        }

        #endregion
        #region Save District Assessment Details

        /// <summary>
        /// Saves the District Assessment Details
        /// </summary>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="districtRates">The district rates.</param>
        /// <param name="isoverride">if set to <c>true</c> [isoverride].</param>
        /// <param name="ownerRide">if set to <c>true</c> [owner ride].</param>
        /// <param name="userId">The userId.</param>
        /// <returns>Key ID</returns>
        public static int F16031_SaveDistrictAssessmentDetails(string districtProperty, string districtRates, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@DistrictProperty", districtProperty);
            ht.Add("@DistrictRates", districtRates);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f16031_pcins_SpecialAssessment", ht);
        }

        #endregion

        #region Check Duplicate Special Assessment
        /// <summary>
        /// F1031_s the check special district statement or owner.
        /// </summary>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="statementFlag">if set to <c>true</c> [statement flag].</param>
        /// <returns>error Id</returns>
        public static F1031SpecialDistrictAssessmentData F16031_CheckSpecialAssessment(string districtProperty)
        {
            F1031SpecialDistrictAssessmentData ListInput = new F1031SpecialDistrictAssessmentData();
            ListInput.ListInputVAlue.AddListInputVAlueRow("@IsPass", string.Empty, "bool", 2);
            ListInput.ListInputVAlue.AddListInputVAlueRow("@ErrorMessage", string.Empty, "string", 3000);
            Hashtable ht = new Hashtable();
            ht.Add("@DistrictProperty", districtProperty);
            Utility.SPParameters("f16031_pcchk_SpecialAssessment", ListInput.ListInputVAlue, ht, ListInput.ListCheckOutPutValue);
            return ListInput; 

        }

        #endregion Check Duplicate Special Assessment


        #region ExeWriterStatements

        ///<summary>
        /// for Execute the Writer Statements
        /// </summary>
        public static void F16031_ExeWriteTaxStatement(int workingFileId, int userId, bool isCancel)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@WorkingFileID", workingFileId);
            ht.Add("@UserID", userId);
            ht.Add("@IsCancel", isCancel);
            DataProxy.ExecuteSP("f16031_pcexe_WriteTaxStatement", ht);
        }

        #endregion ExeWriterStatement

        #endregion  Working File Id


        #region List Special District Assessment Details

        /// <summary>
        /// Lists the Special District Assessment details
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>returns dataset containing specialDistrict Assessment Details</returns>
        public static F1031SpecialDistrictAssessmentData F1031_ListDistrictAssessmentDetails(int statementId)
        {
            F1031SpecialDistrictAssessmentData specialDistrictAssessmentData = new F1031SpecialDistrictAssessmentData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            string[] tableName = new string[] { specialDistrictAssessmentData.ListDistrictAssessmentProperty.TableName, specialDistrictAssessmentData.ListDistrictAssessmentRates.TableName };
            Utility.LoadDataSet(specialDistrictAssessmentData, "f1031_pclst_DistrictAssessmentProperty", ht, tableName);
            return specialDistrictAssessmentData;
        }

        #endregion

        #region List Special District Assessment IDs

        /// <summary>
        /// Lists the Special District Assessment IDs
        /// </summary>
        /// <returns>returns dataset containing District Assessment IDs</returns>
        public static F1031SpecialDistrictAssessmentData F1031_ListDistrictAssessmentIDs()
        {
            F1031SpecialDistrictAssessmentData specialDistrictAssessmentData = new F1031SpecialDistrictAssessmentData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(specialDistrictAssessmentData.ListDistrictAssessmentID, "f1031_pclst_DistrictAssessmentID", ht);
            return specialDistrictAssessmentData;
        }
        
        #endregion

        #region List Special District Assessment ParcelID

        /// <summary>
        /// Lists the Special District Assessment ParcelID
        /// </summary>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>
        /// returns dataset containing District Assessment ParcelID
        /// </returns>
        public static F1031SpecialDistrictAssessmentData F1031_GetDistrictAssessmentParcelID(string parcelNumber, int? parcelId,int? rollYear)
        {
            F1031SpecialDistrictAssessmentData specialDistrictAssessmentData = new F1031SpecialDistrictAssessmentData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelNumber", parcelNumber);
            ht.Add("@ParcelID", parcelId);
            ht.Add("@RollYear",rollYear);
            Utility.LoadDataSet(specialDistrictAssessmentData.GetDistrictAssessmentParcelID, "f1031_pcget_DistrictAssessmentParcelID", ht);
            return specialDistrictAssessmentData;
        }
        
        #endregion

        #region List Special District 

        /// <summary>
        /// Lists the Special District details
        /// </summary>
        /// <param name="sadistrictId">The sadistrict id.</param>
        /// <returns>
        /// returns dataset containing specialDistrict Details
        /// </returns>
        public static F1031SpecialDistrictAssessmentData F1031_ListDistrictAssessment(int sadistrictId)
        {
            F1031SpecialDistrictAssessmentData specialDistrictAssessmentData = new F1031SpecialDistrictAssessmentData();
            Hashtable ht = new Hashtable();
            ht.Add("@SADistrictID", sadistrictId);
            string[] tableName = new string[] { specialDistrictAssessmentData.ListSpecialDistrictAssessmentProperty.TableName, specialDistrictAssessmentData.ListDistrictAssessmentRates.TableName };
            Utility.LoadDataSet(specialDistrictAssessmentData, "f1031_pclst_SpecialDistrictAssessment", ht, tableName);
            return specialDistrictAssessmentData;
        }

        #endregion

        #region Save District Assessment Details

        /// <summary>
        /// Saves the District Assessment Details
        /// </summary>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="districtRates">The district rates.</param>
        /// <param name="isoverride">if set to <c>true</c> [isoverride].</param>
        /// <param name="ownerRide">if set to <c>true</c> [owner ride].</param>
        /// <param name="userId">The userId.</param>
        /// <returns>Key ID</returns>
        public static int F1031_SaveDistrictAssessmentDetails(string districtProperty, string districtRates, bool isoverride, bool ownerRide, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@DistrictProperty", districtProperty);
            ht.Add("@DistrictRates", districtRates);
            ht.Add("@IsOverride", isoverride);
            ht.Add("@OwnerRide", ownerRide);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f1031_pcins_DistrictAssessment", ht);
        }

        #endregion

        #region Delete District Assessment

        /// <summary>
        /// Deletes the District Assessment
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static int F1031_DeleteDistrictAssessment(int statementId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            ht.Add("@UserID", userId);
            return DataProxy.ExecuteSP("f1031_pcdel_DistrictAssessment", ht);
        }

        #endregion

        #region Check Duplicate Statement/Owner

        /// <summary>
        /// F1031_s the check special district statement or owner.
        /// </summary>
        /// <param name="districtProperty">The district property.</param>
        /// <param name="statementFlag">if set to <c>true</c> [statement flag].</param>
        /// <returns>error Id</returns>
        public static int F1031_CheckSpecialDistrictStatementOrOwner(string districtProperty, bool statementFlag)
        {
            int errorId;
            Hashtable ht = new Hashtable();
            ht.Add("@DistrictProperty", districtProperty);

            if (statementFlag)
            {
                errorId = Utility.FetchSPExecuteKeyId("f1031_pcchk_SDStatement", ht);
            }
            else
            {
                errorId = Utility.FetchSPExecuteKeyId("f1031_pcchk_SDOwner", ht);
            }

            return errorId;
        }

        #endregion
    }
}
