//--------------------------------------------------------------------------------------------
// <copyright file="AdditionalOperationCountEntity.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//*********************************************************************************/

namespace TerraScan.SmartParts
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// AdditionalOperationCountEntity
    /// </summary>
    public class AdditionalOperationCountEntity
    {
        /// <summary>
        /// Attachments Count
        /// </summary>
        public int AttachmentCount;

        /// <summary>
        /// Comments Count
        /// </summary>
        public int CommentCount;

        /// <summary>
        /// high Priority for comments
        /// </summary>
        public bool HighPriority;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AdditionalOperationCountEntity"/> class.
        /// </summary>
        /// <param name="attachmentCount">The attachment count.</param>
        /// <param name="commentCount">The comment count.</param>
        /// <param name="highPriority">if set to <c>true</c> [high priority].</param>
        public AdditionalOperationCountEntity(int attachmentCount, int commentCount, bool highPriority)
        {
            this.AttachmentCount = attachmentCount;
            this.CommentCount = commentCount;
            this.HighPriority = highPriority;
        }
    }
}
