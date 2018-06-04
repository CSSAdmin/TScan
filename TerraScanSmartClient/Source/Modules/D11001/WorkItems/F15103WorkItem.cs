// -------------------------------------------------------------------------------------------------
// <copyright file="F15103WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------

namespace D11001
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using System.Data;
    using TerraScan.BusinessEntities;

    #endregion NameSpaces

    /// <summary>
    /// F15103WorkItem class
    /// </summary>
    public class F15103WorkItem : WorkItem
    {
        #region F15103WorkItem

        /// <summary>
        /// F15103 workitem
        /// </summary>
        /// <param name="receiptId">ReceiptId</param>
        /// <returns>GetReceiptOwnersDetails</returns>
        public F15103ReceiptOwnersData ListReceiptOwners(int receiptId)
        {
            return WSHelper.ListReceiptOwners(receiptId);
        }

        /// <summary>
        /// F15100 workitem
        /// </summary>
        /// <param name="receiptId">ReceiptId</param>
        /// <returns>GetReceiptHeaderDetails</returns>
        public F15100ReceiptHeaderData GetReceiptHeaderDetails(int receiptId)
        {
            return WSHelper.GetReceiptHeaderDetails(receiptId);
        }

        #endregion F15103WorkItem

        #region Get Form Slice Permission Details
        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">userId</param>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }
        #endregion Get Form Slice Permission Details
    }
}
