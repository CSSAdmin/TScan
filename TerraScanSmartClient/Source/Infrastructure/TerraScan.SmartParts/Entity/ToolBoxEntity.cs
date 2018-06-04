//--------------------------------------------------------------------------------------------
// <copyright file="ToolBoxEntity.cs" company="Congruent">
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
// 10/10/2006       Ranjani
//*********************************************************************************/
namespace TerraScan.SmartParts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// ToolBoxEntity
    /// </summary>
    public class ToolBoxEntity
    {
        /// <summary>
        /// Current Form Id
        /// </summary>
        public int CurrentFormId;

        /// <summary>
        /// Where Condition
        /// </summary>
        public string WhereCondition;

        /// <summary>
        /// User Defined Where Condition
        /// </summary>
        public string UserDefinedWhereCondition;

        /// <summary>
        /// SnapshotId XmlString
        /// </summary>
        public string SnapshotIdXmlString;

        /// <summary>
        /// Snapshot Name
        /// </summary>
        public string SnapshotName;

        /// <summary>
        /// Snapshot Description
        /// </summary>
        public string SnapshotDescription;

        /// <summary>
        /// Snapshot Count
        /// </summary>
        public int SnapshotCount;

        /// <summary>
        /// key id(probably query or snapshot id)
        /// </summary>
        public int KeyId;

        /// <summary>
        /// CalledFormStatus - yes - contain information to process
        /// </summary>
        public bool CalledFormStatus;

        /// <summary>
        /// variable Holds the ParentWorkItem
        /// </summary>
        public WorkItem ParentWorkItem;          
    }
}
