// -------------------------------------------------------------------------------------------
// <copyright file="F1025AutoFundTransferComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to get F1025AutoFundTransferComp</summary>
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
    using TerraScan.DataLayer;
    using System.Data;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// AutoFundTransfer class file
    /// </summary>
    public static class F1025AutoFundTransferComp
    {
        #region List RollYear

        /// <summary>
        /// F1025_s the list roll year.
        /// </summary>
        /// <returns>Typed dataset</returns>
        public static F1025AutoFundTransferData F1025_ListRollYear()
        {
            F1025AutoFundTransferData autoFundDetails = new F1025AutoFundTransferData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(autoFundDetails.ListAutoFundRollYear, "f1025_pclst_AutoFundTransferRollYear", ht);
            return autoFundDetails;
        }

        #endregion

        #region List AutoFundTransfer Details

        /// <summary>
        /// F1025_s the list auto fund transfer details.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>Typed  Dataset</returns>
        public static F1025AutoFundTransferData F1025_ListAutoFundTransferDetails(int rollYear)
        {
            F1025AutoFundTransferData autoFundDetails = new F1025AutoFundTransferData();
            Hashtable ht = new Hashtable();
            ht.Add("@RollYear", rollYear);
            string[] tableName = new string[] 
            {
                autoFundDetails.ListAutoFundAccountTransferDetails.TableName
            };

            Utility.LoadDataSet(autoFundDetails, "f1025_pclst_AutoFundTransferAccount", ht, tableName);
            return autoFundDetails;
        }
        #endregion

        #region Delete

        /// <summary>
        /// F1025_s the delete auto fund transfer details.
        /// </summary>
        /// <param name="autoTransferId">The auto transfer ID.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>integer value</returns>
        public static int F1025_DeleteAutoFundTransferDetails(int autoTransferId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@AutoTransferID", autoTransferId);
            ht.Add("@UserID", userId);
            return DataProxy.ExecuteSP("f1025_pcdel_AutoFund", ht);
        }

        #endregion Delete

        #region Update AutoFundTransfer

        /// <summary>
        /// F1025_s the update auto fund transfer details.
        /// </summary>
        /// <param name="autoFundItems">The auto fund items.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>integer value</returns>
        public static int F1025_UpdateAutoFundTransferDetails(string autoFundItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@AutoFundItems", autoFundItems);
            ht.Add("@UserID", userId);
            int autoFundId;
            autoFundId = Utility.FetchSPExecuteKeyId("f1025_pcins_AutoFund", ht);
            return autoFundId;
        }

        #endregion Update AutoFundTransfer

        #region Check RollYear

        /// <summary>
        /// F1025_s the check roll year.
        /// </summary>
        /// <param name="autoFundItems">The auto fund items.</param>
        /// <returns>integer value</returns>
        public static int F1025_CheckRollYear(string autoFundItems)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@AutoFundItems", autoFundItems);
            
            int checkRollYear;
            checkRollYear = Utility.FetchSPExecuteKeyId("f1025_pcchk_AutoFund", ht);
            return checkRollYear;
        }

        #endregion Check RollYear        
    }
}
