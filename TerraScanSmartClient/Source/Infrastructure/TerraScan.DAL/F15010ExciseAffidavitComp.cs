// -------------------------------------------------------------------------------------------
// <copyright file="F15010ExciseAffidavitComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access GDoc methods to Load Common ComboBoxs </summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 26/1/2007        VijayaKumar.M       Added
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
    /// ExciseAffidavitComp
    /// </summary>
    public static class F15010ExciseAffidavitComp
    {
        #region F15010 Excise Affidavit

        /// <summary>
        /// Gets the excise affidavit details.
        /// </summary>
        /// <returns>Return ExciseTaxAffidavitData Dataset</returns>
        public static F15010ExciseAffidavitData F15010_GetExciseIndividualType()
        {
            F15010ExciseAffidavitData exciseIndividualType = new F15010ExciseAffidavitData();
            Hashtable ht = new Hashtable();
            string[] tableName = new string[] { exciseIndividualType.ExciseIndividualType.TableName, exciseIndividualType.ExciseDeedType.TableName, exciseIndividualType.ExciseSource.TableName ,exciseIndividualType.ConfiguredRollYear.TableName};
            Utility.LoadDataSet(exciseIndividualType, "f15010_pclst_ExciseIndividualType", ht, tableName);
            return exciseIndividualType;
        }

        /// <summary>
        /// To Delete Affidavits Data.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="userId">The user id.</param>
        public static void F15010_DeleteAffidavitDetails(int statementId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f15010_pcdel_ExciseTaxAffidavitStatment", ht);
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
        public static F15010ExciseAffidavitData F15010_GetExciseTaxAffidavitCalulateAmountDue(DateTime saleDate, DateTime paymentDate, int exciseRateId, int taxCode, double taxableSaleAmount)
        {
            F15010ExciseAffidavitData exciseTaxAffidavitAmountDueData = new F15010ExciseAffidavitData();
            Hashtable ht = new Hashtable();
            ht.Add("@SaleDate", saleDate);
            ht.Add("@PaymentDate", paymentDate);
            ht.Add("@ExciseRateID", exciseRateId);
            ht.Add("@TaxCode", taxCode);
            ht.Add("@TaxableSaleAmount", taxableSaleAmount);
            Utility.LoadDataSet(exciseTaxAffidavitAmountDueData.CalAmountDue, "f15010_pcget_ExciseTaxAffidavitAmountDue", ht);
            return exciseTaxAffidavitAmountDueData;
        }

        /// <summary>
        /// Gets the ExciseTaxAffidavit based on the  statmentid.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>The dataset containing the comments.</returns>
        public static F15010ExciseAffidavitData F15010_GetExciseTaxAffidavitDetails(int statementId)
        {
            F15010ExciseAffidavitData exciseTaxAffiDavitData = new F15010ExciseAffidavitData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            ////DataSet ds = new DataSet();
            string[] tableName = new string[] { exciseTaxAffiDavitData.General.TableName, exciseTaxAffiDavitData.PartiesHeader.TableName, exciseTaxAffiDavitData.ParcelHeader.TableName, exciseTaxAffiDavitData.Affidavit.TableName, exciseTaxAffiDavitData.AmountDue.TableName, exciseTaxAffiDavitData.Suppliment.TableName, exciseTaxAffiDavitData.MobileHome.TableName, exciseTaxAffiDavitData.DorSubmitTable.TableName    };
            Utility.LoadDataSet(exciseTaxAffiDavitData, "f15010_pcget_ExciseTaxAffidavitStatmentDetails", ht, tableName);
            return exciseTaxAffiDavitData;
        }

        /// <summary>
        /// Gets the affidavit statement id.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="orderField">The order field.</param>
        /// <param name="orderBy">The order by.</param>
        /// <returns>Returns dataset for list of statementID</returns>
        public static F15010ExciseAffidavitData F15010_GetAffidavitStatementId(int formId, string orderField, string orderBy)
        {
            F15010ExciseAffidavitData affidavitStatementIdData = new F15010ExciseAffidavitData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", formId);
            ht.Add("@OrderField", orderField);
            ht.Add("@Orderby", orderBy);
            Utility.LoadDataSet(affidavitStatementIdData.ListAffidavitStatementId, "f15010_pcget_ExciseTaxAffidavitStatementIds", ht);
            return affidavitStatementIdData;
        }

        /// <summary>
        /// Gets the affidavit statement id.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="partiesAddress">The parties address.</param>
        /// <param name="parcelDetails">The parcel details.</param>
        /// <param name="exciseAffidavitDetails">The excise affidavit details.</param>
        /// <param name="mobileHomeDetails">The mobile home details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>returns AffiDavit Details</returns>
        public static int F15010_SaveAffiDavitDetails(int statementId, string partiesAddress, string parcelDetails, string exciseAffidavitDetails, string mobileHomeDetails, int userId)
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
            ht.Add("@MobileHomeDetails", mobileHomeDetails);
            ht.Add("@ExciseAffidavitDetails", exciseAffidavitDetails);
            ht.Add("@UserID", userId);

            //return Utility.FetchSPOutput("f15010_pcins_ExciseTaxAffidavitDetails", ht);
            return Utility.FetchSPExecuteKeyId("f15010_pcins_ExciseTaxAffidavitDetails", ht);
        }

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public static F15010ExciseAffidavitData F15010_GetOwnerDetails(int ownerId)
        {
            F15010ExciseAffidavitData ownerDetail = new F15010ExciseAffidavitData();
            Hashtable ht = new Hashtable();
            ht.Add("@OwnerID", ownerId);
            Utility.LoadDataSet(ownerDetail.ListPartiesOwnerDetail, "f9101_pcget_MasterName", ht);
            return ownerDetail;
        }

        /// <summary>
        /// Get the owner Status.
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns></returns>
        public static F15010ExciseAffidavitData F15010_GetOwnerStatus(int ownerId)
        {
            F15010ExciseAffidavitData ownerStatus = new F15010ExciseAffidavitData();
            Hashtable ht = new Hashtable();
            ht.Add("@Ownerid", ownerId);
            string[] tableName = new string[] { ownerStatus.OwnerStatusLow.TableName, ownerStatus.OwnerStatusHigh.TableName };
            Utility.LoadDataSet(ownerStatus, "f15010_GetOwnerStatus", ht,tableName);
            return ownerStatus;
        }

        /// <summary>
        /// Gets the district selection.
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <returns>Returns Dataset foe District Selection</returns>
        public static F15010ExciseAffidavitData F15010_GetDistrictSelection(int exciseRateId)
        {
            F15010ExciseAffidavitData districtSelection = new F15010ExciseAffidavitData();
            Hashtable ht = new Hashtable();
            ht.Add("@ExciseRateID", exciseRateId);
            Utility.LoadDataSet(districtSelection.ListAffidavitDistrictSelection, "f15013_pcget_ExciseTaxDistrict", ht);
            return districtSelection;
        }

        /// <summary>
        /// F15010_s the get parcel detail.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="parcelNumber">The parcel number.</param>
        /// <returns>Returns Dataset foe parcel Selection</returns>
        public static F15010ExciseAffidavitData F15010_GetParcelDetail(int? parcelId, string parcelNumber)
        {
            F15010ExciseAffidavitData districtSelection = new F15010ExciseAffidavitData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@ParcelNumber", parcelNumber);
            string[] tableName = new string[] { districtSelection.ListParcelDetailTable.TableName, districtSelection.PartiesOwnerDetail.TableName, districtSelection.ExciseUseCode.TableName };
            Utility.LoadDataSet(districtSelection, "f15010_pcget_ParcelInfoForExcise", ht, tableName);
            return districtSelection;
        }

        /// <summary>
        /// F15010_s the list excise WAC.
        /// </summary>
        /// <returns></returns>
        public static F15010ExciseAffidavitData F15010_ListExciseWAC()
        {
            F15010ExciseAffidavitData exciseWAC = new F15010ExciseAffidavitData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(exciseWAC.ListExciseWAC, "f15010_pclst_ExciseWAC", ht);
            return exciseWAC;
        }

        /// <summary>
        /// F5010_s the list excise individual.
        /// </summary>
        /// <param name="ExciseIndividualElements">The excise individual elements.</param>
        /// <returns></returns>
        public static F15010ExciseAffidavitData F15010_ListExciseIndividual(string ExciseIndividualElements)
        {
            F15010ExciseAffidavitData exciseIndividualElements = new F15010ExciseAffidavitData();
            Hashtable ht = new Hashtable();
            ht.Add("@ExciseIndividualElements", ExciseIndividualElements);
            Utility.LoadDataSet(exciseIndividualElements.ListExciseIndividual, "f15010_pclst_ExciseIndividual", ht);
            return exciseIndividualElements;
        }

        /// <summary>
        /// F1510_s the list open space field.
        /// </summary>
        /// <param name="parcelIds">The parcel ids.</param>
        /// <returns></returns>
        public static F15010ExciseAffidavitData F15010_ListOpenSpaceField(string parcelIds)
        {
            F15010ExciseAffidavitData openSpaceFieldData = new F15010ExciseAffidavitData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelList", parcelIds);
            Utility.LoadDataSet(openSpaceFieldData.OpenSpaceData, "f15010_pcget_OpenSpaceData", ht);
            return openSpaceFieldData;
        }

        #endregion F15010 Excise Affidavit
    }
}
