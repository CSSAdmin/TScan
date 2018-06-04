//--------------------------------------------------------------------------------------------
// <copyright file="F9002Controller.cs" company="Congruent">
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

namespace D9002
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F9002Controller class file
    /// </summary>
    public class F9002Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F9002WorkItem WorkItem
        {
            get { return base.WorkItem as F9002WorkItem; }
        }
    }
}
