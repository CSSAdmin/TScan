//--------------------------------------------------------------------------------------------
// <copyright file="StatusBarEntity.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the StatusBarEntity.
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
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// StatusBarEntity
    /// </summary>
    public class StatusBarEntity
    {
        /// <summary>
        /// Where Condition
        /// </summary>
        public string WhereCondition;

        /// <summary>
        /// User Defined Where Condition
        /// </summary>
        public string UserDefinedWhereCondition;        

        /// <summary>
        /// optionalInputParameter
        /// </summary>
        public object[] OptionalInputParameter;

        /// <summary>
        /// called form instance
        /// </summary>
        public Form CalledForm;

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
