// -------------------------------------------------------------------------------------------
// <copyright file="F15050FeeManagementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access GDoc methods to Load Common ComboBoxs </summary>
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
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    /// <summary>
    /// F15050FeeManagementComp
    /// </summary>
    public static class F15050FeeManagementComp
    {
        /// <summary>
        /// F15050_ComboData
        /// </summary>
        /// <returns>Typed dataset</returns>
        public static F15050FeeManagementData F15050_ComboData()
        {
            F15050FeeManagementData form15050feeManagement = new F15050FeeManagementData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(form15050feeManagement.f15050_pclst_Fee, "f15050_pclst_Fee", ht);
            return form15050feeManagement;
        }

        /// <summary>
        /// F15050_getDatas
        /// </summary>
        /// <param name="feeId">feeId</param>
        /// <returns>Typed dataset</returns>
        public static F15050FeeManagementData F15050_getDatas(int feeId)
        {
            F15050FeeManagementData form15050FeeManagements = new F15050FeeManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@FeeID", feeId);
            string[] tableName = new string[] { form15050FeeManagements.GetFeeDetails.TableName, form15050FeeManagements.GetISValidKey.TableName };
            Utility.LoadDataSet(form15050FeeManagements, "f15050_pcget_Fee", ht, tableName);
            return form15050FeeManagements;
        }

        /// <summary>
        /// F15050_s the save fee management.
        /// </summary>
        /// <param name="feeId">The fee id.</param>
        /// <param name="description">The description.</param>
        /// <param name="amount">The amount.</param>
        /// <param name="accountId">The account id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="feeTypeId">The fee type id.</param>
        /// <returns>The status.</returns>
        public static int F15050_SaveFeeManagement(int feeId, string description, decimal amount, int accountId, int userId, byte feeTypeId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FeeID", feeId);
            ht.Add("@Description", description);
            ht.Add("@Amount", amount);
            ht.Add("@AccountID", accountId);
            ht.Add("@UserID", userId);

            if (feeTypeId > 0)
            {
                ht.Add("@FeeTypeID", feeTypeId);
            }
            else
            {
                ht.Add("@FeeTypeID", null);
            }

            return Utility.FetchSPExecuteKeyId("f15050_pcins_Fee", ht);
        }

        /// <summary>
        /// F15050_ApplyFees
        /// </summary>
        /// <param name="feeXML">feeXML</param>
        /// <param name="amount">amount</param>
        /// <param name="description">description</param>
        /// <param name="accountId">accountId</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public static int F15050_ApplyFees(string feeXML, decimal amount, string description, int accountId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FeeXML", feeXML);
            ht.Add("@Amount", amount);
            ht.Add("@Description", description);
            ht.Add("@AccountID", accountId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f15050_pcins_ApplyFeeItems", ht);
        }

        /// <summary>
        /// F15050_s the list fee types.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The fee mgmt dataset</returns>
        public static F15050FeeManagementData F15050_ListFeeTypes(int userId)
        {
            F15050FeeManagementData form15050feeManagement = new F15050FeeManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(form15050feeManagement.ListFeeTypes, "f15050_pclst_FeeType", ht);
            return form15050feeManagement;
        }

        /// <summary>
        /// F15050_s the remove template.
        /// </summary>
        /// <param name="feeId">The fee id.</param>
        /// <param name="userId">The user id.</param>
        public static void F15050_RemoveTemplate(int feeId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FeeID", feeId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f15050_pcdel_Fee", ht);
        }
    }
}
