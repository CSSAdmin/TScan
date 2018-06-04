//--------------------------------------------------------------------------------------------
// <copyright file="F15050 Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15050Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09/05/07        A.Sriparameswari       Created                
//*********************************************************************************/
namespace D11050
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F15050Controller ClassFile
    /// </summary>
    public class F15050Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value>The work item</value>
        public new F15050WorkItem WorkItem
        {
            get { return base.WorkItem as F15050WorkItem; }
        }       
    }
}
