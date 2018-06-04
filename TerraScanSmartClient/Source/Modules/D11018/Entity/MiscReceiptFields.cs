//--------------------------------------------------------------------------------------------
// <copyright file="MiscReceiptFields.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains members for the MiscReceiptFields.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 27 Feb 07       Ranjani             created
//*********************************************************************************/
namespace D11018
{
    using System;

    /// <summary>
    /// members for receipt and items communication
    /// </summary>
    public class MiscReceiptFields
    {
        /// <summary>
        /// miscTemplateId
        /// </summary>
        private int miscTemplateId = -999;

        /// <summary>
        /// ownerId
        /// </summary>
        private int ownerId;   

        /// <summary>
        /// defaultComment
        /// </summary>
        private string defaultComment = string.Empty;  

        /// <summary>
        /// receiptItems
        /// </summary>
        private string receiptItems;    

        /// <summary>
        /// highPriority
        /// </summary>
        private bool highPriority;

        /// <summary>
        /// Gets or sets the owner id.
        /// </summary>
        /// <value>The owner id.</value>
        public int OwnerId
        {
            get { return this.ownerId; }
            set { this.ownerId = value; }
        }

        /// <summary>
        /// Gets or sets the default comment.
        /// </summary>
        /// <value>The default comment.</value>
        public string DefaultComment
        {
            get { return this.defaultComment; }
            set { this.defaultComment = value; }
        }

        /// <summary>
        /// Gets or sets the receipt items.
        /// </summary>
        /// <value>The receipt items.</value>
        public string ReceiptItems
        {
            get { return this.receiptItems; }
            set { this.receiptItems = value; }
        }

        /// <summary>
        /// Gets or sets the misc template id.
        /// </summary>
        /// <value>The misc template id.</value>
        public int MiscTemplateId
        {
            get { return this.miscTemplateId; }
            set { this.miscTemplateId = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [high priority].
        /// </summary>
        /// <value><c>true</c> if [high priority]; otherwise, <c>false</c>.</value>
        public bool HighPriority
        {
            get { return this.highPriority; }
            set { this.highPriority = value; }
        }
    }
}
