//--------------------------------------------------------------------------------------------
// <copyright file="AdditionalOperationEntity.cs" company="Congruent">
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
    /// AdditionalOperationEntity class file
    /// </summary>
    public class AdditionalOperationEntity
    {
        /// <summary>
        /// AttachmentButtonEnabled
        /// </summary>
        public int AttachmentButtonEnabled;

        /// <summary>
        /// CommentButtonEnabled
        /// </summary>
        public int CommentButtonEnabled;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AdditionalOperationEntity"/> class.
        /// </summary>
        /// <param name="attachmentButtonEnabled">The attachment button enabled.</param>
        /// <param name="commentButtonEnabled">The comment button enabled.</param>
        public AdditionalOperationEntity(int attachmentButtonEnabled, int commentButtonEnabled)
        {
            this.AttachmentButtonEnabled = attachmentButtonEnabled;
            this.CommentButtonEnabled = commentButtonEnabled;
        }
    }
}
