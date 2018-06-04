// -------------------------------------------------------------------------------------------------
// <copyright file="F15105WorkItem.cs" company="Congruent">
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
    /// F15100WorkItem class
    /// </summary>
    public class F15105WorkItem :WorkItem
    {
        #region F15100WorkItem

        /// <summary>
        /// F15105 workitem
        /// </summary>
        /// <param name="receiptId">ReceiptId</param>
        /// <returns>GetReceiptHeaderDetails</returns>
        public F15100ReceiptHeaderData GetReceiptHeaderDetails(int receiptId)
        {
            return WSHelper.GetReceiptHeaderDetails(receiptId);
        }

        /// <summary>
        /// F15105 workitem
        /// </summary>
        /// <param name="receiptId">ReceiptId</param>
        /// <returns>GetReceiptHeaderDetails</returns>
        public F15100ReceiptHeaderData GetReceiptListDetails(int receiptId)
        {
            return WSHelper.GetReceiptListDetails(receiptId);
        }

        #region Save Receipt Header

        /// <summary>
        /// To Save the Receipt Header Receipt Number.
        /// </summary>
        /// <param name="receiptId">The receipt id.</param>
        /// <param name="receiptNumber">The receipt number.</param>
        /// <param name="userId">The user id.</param>
        public void F15100_SaveReceiptHeaderreceiptNumber(int receiptId, string receiptNumber, int userId)
        {
            WSHelper.F15100_SaveReceiptHeaderreceiptNumber(receiptId, receiptNumber, userId);
        }

        #endregion Save Receipt Header

        #endregion F15105WorkItem  
    }
}
