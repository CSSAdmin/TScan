// -------------------------------------------------------------------------------------------
// <copyright file="F11018MiscReceiptComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to insert Receipts</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 27 feb 06		Ranjani	            Created
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
    /// Main class for Misc Receipt Component
    /// </summary>
    public static class F11018MiscReceiptComp
    {
        #region 15018 Get receipt

        /// <summary>
        /// Gets the Misc Receipt details based on the receiptId
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>
        /// The typed dataset containing the receipt information of the receiptId.
        /// </returns>
        public static F11018MiscReceiptData F15018_GetMiscReceipt(int receiptId)
        {
            F11018MiscReceiptData miscReceiptData = new F11018MiscReceiptData();
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptID", receiptId);
            Utility.LoadDataSet(miscReceiptData, "f15018_pcget_MiscReceipt", ht, new string[] { miscReceiptData.GetMiscReceipt.TableName, miscReceiptData.ListReceiptItem.TableName });
            return miscReceiptData;
        }

        #endregion 15018 Get receipt

        #region 1021 Get Misc Template

        /// <summary>
        /// gets the Misc Receipt template
        /// </summary>
        /// <param name="miscTemplateId">The misc template ID.</param>
        /// <returns>The typed dataset containing the receipt Template information of the miscTemplateID</returns>
        public static F11018MiscReceiptData F1021_GetMiscReceiptTemplate(int miscTemplateId)
        {
            F11018MiscReceiptData miscReceiptData = new F11018MiscReceiptData();
            Hashtable ht = new Hashtable();
            ht.Add("@MiscTemplateID", miscTemplateId);
            Utility.LoadDataSet(miscReceiptData.ListReceiptItem, "f1022_pclst_MiscReceiptTemplateItem", ht);
            return miscReceiptData;
        }

        #endregion 1021 Get Misc Template

        #region 1021 Insert Misc Template

        /// <summary>
        /// saves the Misc Receipt template
        /// </summary>
        /// <param name="miscTemplateDetails">The misc template details.</param>
        /// <param name="templateItems">The template items.</param>
        /// <param name="userId">userId</param>
        /// <returns>
        /// new created templated id - return templatedid if succeed else return negative value
        /// </returns>
        public static int F1021_SaveMiscReceiptTemplate(string miscTemplateDetails, string templateItems, int userId)
        {           
            Hashtable ht = new Hashtable();
            ht.Add("@MiscTemplateDetails", miscTemplateDetails);
            ht.Add("@TemplateItems", templateItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f1021_pcins_MiscReceiptTemplate", ht);            
        }

        #endregion 1021 Insert Misc Template

        #region 1022 List Misc Template

        /// <summary>
        /// List the Misc Receipt template
        /// </summary>
        /// <returns>
        /// The typed dataset containing the Misc Receipt Template
        /// </returns>
        public static F11018MiscReceiptData F1022_ListMiscReceiptTemplate()
        {
            F11018MiscReceiptData miscReceiptData = new F11018MiscReceiptData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(miscReceiptData.ListMiscReceiptTemplate, "f1022_pclst_MiscReceiptTemplate", ht);
            return miscReceiptData;
        }

        #endregion 1022 List Misc Template

        #region 1022 Delete Misc Template

        /// <summary>
        /// Deletes the Misc Receipt Template based on the miscTemplateID
        /// </summary>
        /// <param name="miscTemplateId">The misc template ID.</param>
        /// <param name="userId">userId</param>
        public static void F1022_DeleteMiscReceiptTemplate(int miscTemplateId, int userId)
        {           
            Hashtable ht = new Hashtable();
            ht.Add("@MiscTemplateID", miscTemplateId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f1022_pcdel_MiscReceiptTemplate", ht);            
        }

        #endregion 1022 Delete Misc Template

        /// <summary>
        /// Save district details
        /// </summary>
        /// <param name="levyOption">Levy OptionID</param>
        /// <param name="districtId">District ID</param>
        /// <param name="amountValue">Amount</param>
        /// <param name="userId">UserID</param>
        /// <returns>F11018MiscReceipt dataset</returns>
        public static F11018MiscReceiptData F1024_SaveDistrictDetails(int levyOption, int districtId, decimal amountValue, int userId,bool IsReplace,string SubFundXML)
        {
            F11018MiscReceiptData miscReceiptData = new F11018MiscReceiptData();
            Hashtable ht = new Hashtable();
            ht.Add("@LevyOptionID", levyOption);
            ht.Add("@DistrictID", districtId);
            ht.Add("@Amount", amountValue);
            ht.Add("@UserID", userId);
            ht.Add("@IsReplace", IsReplace);
            ht.Add("@SubFundsXML", SubFundXML);
            //Utility.FillDataSet(miscReceiptData.DistrictDistributionItems, "f1024_pclst_DistrictDistributionItems", ht);
            Utility.LoadDataSet(miscReceiptData.ListReceiptItem, "f1024_pclst_DistrictDistributionItems", ht);
            return miscReceiptData;
        }

        /// <summary>
        /// List account details
        /// </summary>
        /// <param name="filterValue">Filter Value</param>
        /// <returns>Account details</returns>
        public static F11018MiscReceiptData F15018_ListAccountDetails(string filterValue, int? rollYear, int? formNo)
        {
            F11018MiscReceiptData miscReceiptData = new F11018MiscReceiptData();
            Hashtable ht = new Hashtable();
            ht.Add("@Filter", filterValue);
            ht.Add("@RollYear", rollYear);
            ht.Add("@Form", formNo);
            Utility.LoadDataSet(miscReceiptData.AccountListing, "f1345_pclst_AccountListing", ht);
            return miscReceiptData;
        }
    }
}
