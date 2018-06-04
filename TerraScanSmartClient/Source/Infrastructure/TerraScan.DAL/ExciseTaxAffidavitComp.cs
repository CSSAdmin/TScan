// -------------------------------------------------------------------------------------------
// <copyright file="ExciseTaxAffidavitComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access comment related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------;

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
    /// ExciseTaxAffidavitComp
    /// </summary>
     public static class ExciseTaxAffidavitComp
    {
        #region ExciseTaxAffidavitDetails

        /// <summary>
        /// Gets the excise tax affidavit details.
        /// </summary>
        /// <returns>Return ExciseTaxAffidavitData Dataset</returns>
        public static ExciseIndividualType GetExciseIndividualType()
        {   
            ExciseIndividualType exciseIndividualType = new ExciseIndividualType();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(exciseIndividualType._ExciseIndividualType, "f1105_pclst_ExciseIndividualType", ht);
            return exciseIndividualType;
        }

        /// <summary>
        /// Gets the ExciseTaxAffidavit based on the  statmentid.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>The dataset containing the comments.</returns>
         public static ExciseTaxAffidavitData GetExciseTaxAffidavitDetails(int statementId)
        {
            ExciseTaxAffidavitData exciseTaxAffiDavitData = new ExciseTaxAffidavitData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            ////DataSet ds = new DataSet();
            string[] tableName = new string[] { exciseTaxAffiDavitData.General.TableName, exciseTaxAffiDavitData.PartiesHeader.TableName, exciseTaxAffiDavitData.ParcelHeader.TableName, exciseTaxAffiDavitData.Affidavit.TableName, exciseTaxAffiDavitData.AmountDue.TableName, exciseTaxAffiDavitData.Suppliment.TableName };
            Utility.LoadDataSet(exciseTaxAffiDavitData, "f1105_pcget_ExciseTaxAffidavitStatmentDetails", ht, tableName);
            return exciseTaxAffiDavitData;
        }

        /// <summary>
        /// Excises the tax affidavit calulate amount due.
        /// </summary>
        /// <param name="saleDate">The sale date.</param>
        /// <param name="paymentDate">The payment date.</param>
        /// <param name="exciseRateId">The excise rate ID.</param>
        /// <param name="taxCode">The tax code.</param>
        /// <param name="taxableSaleAmount">The taxable sale amount.</param>
        /// <returns>AmountDue Calcualted Value </returns>
         public static ExciseTaxAffidavitAmountDueData GetExciseTaxAffidavitCalulateAmountDue(DateTime saleDate, DateTime paymentDate, int exciseRateId, int taxCode, double taxableSaleAmount)
         {
             ExciseTaxAffidavitAmountDueData exciseTaxAffidavitAmountDueData = new ExciseTaxAffidavitAmountDueData();
             Hashtable ht = new Hashtable();
             ht.Add("@SaleDate", saleDate);
             ht.Add("@PaymentDate", paymentDate);
             ht.Add("@ExciseRateID", exciseRateId);
             ht.Add("@TaxCode", taxCode);
             ht.Add("@TaxableSaleAmount", taxableSaleAmount);
             Utility.LoadDataSet(exciseTaxAffidavitAmountDueData.AmountDue, "f1105_pcget_ExciseTaxAffidavitAmountDue", ht);
             return exciseTaxAffidavitAmountDueData;
         }

         /// <summary>
         /// Gets the affidavit statement id.
         /// </summary>
         /// <param name="formId">The form id.</param>
         /// <param name="orderField">The order field.</param>
         /// <param name="orderBy">The order by.</param>
         /// <returns>Returns dataset for list of statementID</returns>
         public static ExciseTaxAffidavitData GetAffidavitStatementId(int formId, string orderField, string orderBy)
         {
             ExciseTaxAffidavitData affidavitStatementIdData = new ExciseTaxAffidavitData();
             Hashtable ht = new Hashtable();
             ht.Add("@Form", formId);
             ht.Add("@OrderField", orderField);
             ht.Add("@Orderby", orderBy);
             Utility.LoadDataSet(affidavitStatementIdData.ListAffidavitStatementId, "f1105_pcget_ExciseTaxAffidavitStatementIds", ht);
             return affidavitStatementIdData;
         }

         /// <summary>
         /// Gets the affidavit statement id.
         /// </summary>
         /// <param name="statementId">The statement id.</param>
         /// <param name="partiesAddress">The parties address.</param>
         /// <param name="parcelDetails">The parcel details.</param>
         /// <param name="exciseAffidavitDetails">The excise affidavit details.</param>
         /// <param name="userId">The userId.</param>
         /// <returns>returns AffiDavit Details</returns>
         public static int SaveAffiDavitDetails(int statementId, string partiesAddress, string parcelDetails, string exciseAffidavitDetails, int userId)
         {
             Hashtable ht = new Hashtable();
             if (statementId == 0)
             {
                 ht.Add("@StatementID", DBNull.Value);
             }
             else
             {
                 ht.Add("@StatementID", statementId);
             }
             
             ht.Add("@PartiesAddress", partiesAddress);
             ht.Add("@ParcelDetails", parcelDetails);
             ht.Add("@ExciseAffidavitDetails", exciseAffidavitDetails);
             ht.Add("@UserID", userId);
             ////Utility.ExecuteSP("f1105_pcins_ExciseTaxAffidavitDetails", ht);
             ////return DataProxy.FetchSPOutput("f1105_pcins_ExciseTaxAffidavitDetails", ht);  
             return Utility.FetchSPOutput("f1105_pcins_ExciseTaxAffidavitDetails", ht);
        }

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
         public static PartiesOwnerDetailsData GetOwnerDetails(int ownerId)
         {
             PartiesOwnerDetailsData ownerDetail = new PartiesOwnerDetailsData();
             Hashtable ht = new Hashtable();
             ht.Add("@OwnerID", ownerId);

             Utility.LoadDataSet(ownerDetail.ListPartiesOwnerDetail, "f9101_pcget_MasterName", ht);
             return ownerDetail;
         }

         /// <summary>
         /// Gets the district selection.
         /// </summary>
         /// <param name="exciseRateId">The excise rate id.</param>
         /// <returns>Returns Dataset foe District Selection</returns>
         public static AffidavitDistrictSelectionData GetDistrictSelection(int exciseRateId)
         {
             AffidavitDistrictSelectionData districtSelection = new AffidavitDistrictSelectionData();
             Hashtable ht = new Hashtable();
             ht.Add("@ExciseRateID", exciseRateId);

             Utility.LoadDataSet(districtSelection.ListAffidavitDistrictSelection, "f1101_pcget_ExciseTaxDistrictInfo", ht);
             return districtSelection;
         }

         /// <summary>
         /// Affidavits the data delete.
         /// </summary>
         /// <param name="statementId">The statement id.</param>
         /// <param name="userId">The userId.</param>
         public static void DeleteAffidavitDetails(int statementId, int userId)
         { 
             Hashtable ht = new Hashtable();
             ht.Add("@StatementID", statementId);
             ht.Add("@UserID", userId);
             Utility.ImplementProcedure("f1105_pcdel_ExciseTaxAffidavitStatment", ht);
         }

         /// <summary>
         /// Executes the affdvt query.
         /// </summary>
         /// <param name="formId">The form id.</param>
         /// <param name="whereCondnSql">The where condn SQL.</param>
         /// <param name="orderByCondn">The order by condn.</param>
         /// <returns>Returns ExecuteAffdvtQuery Dataset</returns>
         public static QueryByFormData ExecuteAffdvtQuery(int formId, string whereCondnSql, string orderByCondn)
         {
             QueryByFormData queryByForm = new QueryByFormData();
             Hashtable ht = new Hashtable();
             ht.Add("@FormID", formId);
             ht.Add("@WhereCondnSql", whereCondnSql);
             ht.Add("@OrderByCondn", orderByCondn);
             string[] optionalParameter = new string[] { queryByForm.ListStatementId.TableName, queryByForm.ListRecordCount.TableName };
             Utility.LoadDataSet(queryByForm, "f9050_pcexe_Query", ht, optionalParameter);
             return queryByForm;
         }

        #endregion Comments
    }
}
