// ------------------------------------------------------------------------------------------------------------
// <copyright file="F1060SudpendedPaymentSelectionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F1060SudpendedPaymentSelectionComp.cs methods</summary>
// Release history
//*************************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ------------------------------------------------------------------------
// 
// 
// ------------------------------------------------------------------------------------------------------------

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
    /// F1060SudpendedPaymentSelectionComp class file
    /// </summary>
    public static class F1060SudpendedPaymentSelectionComp
    {
        #region F1060 Suspended Payment Selection

        #region F1060 List Suspended Payment

        /// <summary>
        /// F1060_s the list suspended payment.
        /// </summary>
        /// <param name="lastName">Name of the last.</param>
        /// <param name="firstName">Name of the first.</param>
        /// <param name="receiptDate">The receipt date.</param>
        /// <returns>Typed DataSet containing the Suspended Payment Details.</returns>
        //public static F1060SudpendedPaymentSelectionData F1060_ListSuspendedPayment(string lastName, string firstName, string receiptDate)
        //{
        //    F1060SudpendedPaymentSelectionData suspendedPaymentSelectionData = new F1060SudpendedPaymentSelectionData();
        //    Hashtable ht = new Hashtable();
        //    ht.Add("@LastName", lastName);
        //    ht.Add("@FirstName", firstName);
        //    ht.Add("@ReceiptDate", receiptDate);
        //    Utility.FillDataSet(suspendedPaymentSelectionData.ListSuspendedPayment, "f1060_pclst_SuspendedPayment", ht);
        //    return suspendedPaymentSelectionData;
        //}
        public static F1060SudpendedPaymentSelectionData F1060_ListSuspendedPayment(string searchDetail)
        {
            F1060SudpendedPaymentSelectionData suspendedPaymentSelectionData = new F1060SudpendedPaymentSelectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@PaymentItems", searchDetail);
            Utility.LoadDataSet(suspendedPaymentSelectionData.ListSuspendedPayment, "f1060_pclst_SuspendedPayment", ht);
            return suspendedPaymentSelectionData;
        }
        #endregion F1060 List Suspended Payment

        #endregion F1060 Sudpended Payment Selection
    }
}
