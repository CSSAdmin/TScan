// -------------------------------------------------------------------------------------------------
// <copyright file="F15102WorkItem.cs" company="Congruent">
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
    /// F15002WorkItem class
    /// </summary>
    public class F15102WorkItem : WorkItem
    {
        #region F15102WorkItem

        /// <summary>
        /// F15102 workitem
        /// </summary>
        /// <param name="receiptId">ReceiptId</param>
        /// <returns>GetReceiptHeaderDetails</returns>
        public F15102ReceiptStatementHeaderData GetReceiptStatementHeaderDetails(int receiptId)
        {
            return WSHelper.GetReceiptStatementHeaderDetails(receiptId);
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

        #endregion F15100WorkItem

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
