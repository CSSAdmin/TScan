//--------------------------------------------------------------------------------------------
// <copyright file="RecordNavigationEntity.cs" company="Congruent">
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
    /// RecordNavigationEntity class file
    /// </summary>
    public class RecordNavigationEntity
    {
        /// <summary>
        /// RecordNavigationFlag
        /// </summary>
        public bool RecordNavigationFlag;

        /// <summary>
        /// RecordIndex
        /// </summary>
        public int RecordIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:RecordNavigationEntity"/> class.
        /// </summary>
        /// <param name="recordNavigationFlag">if set to <c>true</c> [record navigation flag].</param>
        /// <param name="recordIndex">Index of the record.</param>
        public RecordNavigationEntity(bool recordNavigationFlag, int recordIndex)
        {
            this.RecordNavigationFlag = recordNavigationFlag;
            this.RecordIndex = recordIndex;
        }
    }
}
