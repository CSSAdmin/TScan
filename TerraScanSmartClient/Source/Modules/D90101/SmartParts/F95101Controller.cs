//--------------------------------------------------------------------------------------------
// <copyright file="F95101Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F95101Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                  H.Vinayagamurthy       Created// 
//                  
//*********************************************************************************/

namespace D90101
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F95101Controller Class file
    /// </summary>
    public class F95101Controller : Controller
    {
        /// <summary>
        /// From the form F84725 workitem
        /// </summary>
        public new F95101WorkItem WorkItem
        {
            get { return base.WorkItem as F95101WorkItem; }
        }
    }
}
