//--------------------------------------------------------------------------
// <copyright file="F36041Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36032 Permanent Crop.
// </summary>
//--------------------------------------------------------------------------
// Change History
//**************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------
// 02/11/2007       Malliga            Created
//                  
//**************************************************************************

namespace D36040
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F36041Controller : Controller
    {
        /// <summary>
        /// From the form F36041 workitem
        /// </summary>
        public new F36041WorkItem WorkItem
        {
            get { return base.WorkItem as F36041WorkItem; }
        }
    }
}
