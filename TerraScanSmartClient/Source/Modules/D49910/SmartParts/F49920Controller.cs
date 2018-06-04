//--------------------------------------------------------------------------
// <copyright file="F49920Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F49920 Instrument Search Engine.
// </summary>
//--------------------------------------------------------------------------
// Change History
//**************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------
// 02/11/2007       Malliga            Created
//                  
//**************************************************************************

namespace D49910
{

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

   public class F49920Controller : Controller
    {
        /// <summary>
        /// From the form F49920 workitem
        /// </summary>
        public new F49920WorkItem WorkItem
        {
            get { return base.WorkItem as F49920WorkItem; }
        }
    }
}
