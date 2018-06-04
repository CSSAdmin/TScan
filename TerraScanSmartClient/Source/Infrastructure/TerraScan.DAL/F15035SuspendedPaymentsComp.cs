// -------------------------------------------------------------------------------------------
// <copyright file="F15035SuspendedPaymentsComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F15035SuspendedPaymentsComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 18/05/07         Sadashivudu        Created
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
    /// F15035SuspendedPaymentsComp class file
    /// </summary>
    public static class F15035SuspendedPaymentsComp
    {
        #region F15035 Suspended Payments

        /// <summary>
        /// F15035s the suspended payments.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <returns>typed dataset</returns>
        public static F15035SuspendedPaymentsData F15035SuspendedPayments(int receiptId)
        {
            F15035SuspendedPaymentsData suspendedPaymentsData = new F15035SuspendedPaymentsData();
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptID", receiptId);
            string[] optionalParameter = new string[] { suspendedPaymentsData.GetSuspendedPayment.TableName,suspendedPaymentsData.ListReceiptPaymentItem.TableName  };
            Utility.LoadDataSet(suspendedPaymentsData, "f15035_pcget_SuspendedPayment", ht, optionalParameter);
            return suspendedPaymentsData;
        }

        /// <summary>
        /// F15035_s the delete suspended payment.
        /// </summary>
        /// <param name="receiptId">The receipt ID.</param>
        /// <param name="userId">userId</param>
        public static void F15035_DeleteSuspendedPayment(int receiptId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ReceiptID", receiptId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f15035_pcdel_SuspendedPayment", ht);
        }

        /// <summary>
        /// F15035_s the check suspended accounts.
        /// </summary>
        /// <returns>status id</returns>
        public static int F15035_CheckSuspendedAccounts()
        {
            Hashtable ht = new Hashtable();
            return Utility.FetchSPExecuteKeyId("f1405_pcchk_SuspendedAccounts", ht);
        }

        #endregion F15035 Suspended Payments
    }
}
