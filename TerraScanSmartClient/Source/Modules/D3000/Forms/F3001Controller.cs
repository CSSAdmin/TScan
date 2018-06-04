//--------------------------------------------------------------------------------------------
// <copyright file="F1401Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1031WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 21 Aug 07        karthikeyan V            Created
//*********************************************************************************/

namespace D3000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F3001Controller : Controller
    {
        /// <summary>
        ///  Gets the current work item where the controller lives.
        /// </summary>     
        public new F3001WorkItem WorkItem
        {
            get { return base.WorkItem as F3001WorkItem; }
        }
    }
}
