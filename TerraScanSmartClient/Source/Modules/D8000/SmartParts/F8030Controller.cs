//--------------------------------------------------------------------------------------------
// <copyright file="F8030Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8030Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//                  
//*********************************************************************************/

namespace D8000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// class for F8002Controller
    /// </summary>
    public class F8030Controller : Controller
    {
        /// <summary>
        /// From the form F8030 workitem
        /// </summary>
        public new F8030WorkItem WorkItem
        {
            get { return base.WorkItem as F8030WorkItem; }
        }
    }
}
