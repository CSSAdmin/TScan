// -------------------------------------------------------------------------------------------
// <copyright file="F1557ManagePayment.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F1557ManagePayment.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		         Description
// ----------		---------		     -------------------------------------------------------
//  28-09-2016      R.Priyadharshini      Created
// -------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerraScan.DataLayer;
using System.Collections;
using TerraScan.BusinessEntities;


namespace TerraScan.Dal
{
    public class F1557ManagePaymentComp
    {
        #region Get Payment Management

        /// <summary>
        /// Get the Payment Management.
        /// </summary>
        /// <returns>The dataset containing the Get Payment Management</returns>
        public static F1557PayamentManagementData GetPaymentManagement(int ReceiptID)
        {
            F1557PayamentManagementData ManageDetails = new F1557PayamentManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptID", ReceiptID);
            string[] tableNames = new string[] { ManageDetails.PaymentManagementTable.TableName, ManageDetails.PaymentManageGrid.TableName };
            Utility.LoadDataSet(ManageDetails, "f1557_pcget_PaymentManagement", ht, tableNames);
            return ManageDetails;
        }
        #endregion

        /// <summary>
        /// F1557 the insert receipt payment.
        /// </summary>
        /// <param name="receiptPayment">The receipt payment.</param>
        /// <param name="userId">userId</param>
        public static void F1557_InsertPayment(string receiptPayment, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptPayListing", receiptPayment);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f1557_pcins_PaymentManagement", ht);
        }

        /// <summary>
        /// F1557 the Update receipt payment.
        /// </summary>
        /// <param name="receiptPayment">The receipt payment.</param>
        /// <param name="userId">userId</param>
        public static void F1557_UpdatePayment(string receiptPayment, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptPayListing", receiptPayment);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f1557_pcupd_PaymentManagement", ht);
        }
        #region F1557_DeletePaymentIds

        /// <summary>
        /// F1557_s the delete Payment.
        /// </summary>
        /// <param name="PaymentIDs">The PaymentIDs id.</param>
        /// <param name="userId">The user id.</param>
        public static void F1557_DeletePaymentIds(string PaymentIDs, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@PaymentIDs", PaymentIDs);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f1557_pcdel_PaymentManagement", ht);
        }
        #endregion F1557_DeletePaymentIds

    }
}
