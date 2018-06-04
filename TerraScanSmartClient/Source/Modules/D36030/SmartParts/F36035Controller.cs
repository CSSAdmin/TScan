//--------------------------------------------------------------------------
// <copyright file="F36033Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F36032 FS Land Codes.
// </summary>
//--------------------------------------------------------------------------
// Change History
//**************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------
// 14/09/2007       Kuppusamy.B              Created
//                  
//**************************************************************************
namespace D36030
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F36035Controller : Controller
    {
        /// <summary>
        /// From the form F36035 workitem
        /// </summary>
        public new F36035WorkItem WorkItem
        {
            get { return base.WorkItem as F36035WorkItem; }
        }
    }
}
    