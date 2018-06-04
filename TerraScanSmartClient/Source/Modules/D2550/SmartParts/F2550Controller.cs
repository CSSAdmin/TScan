//--------------------------------------------------------------------------------------------
// <copyright file="F2550Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F2550Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20 Aug 06        JYOTHI              Created
//*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;

namespace D2550
{
    public class F2550Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F2550WorkItem WorkItem
        {
            get { return base.WorkItem as F2550WorkItem; }
        }
    }
}
