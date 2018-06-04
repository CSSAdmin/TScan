//--------------------------------------------------------------------------------------------
// <copyright file="F8901Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8901Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10/10/2001   	M.Vijayakumar      Created
//                  
//*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace D8900
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Class for F8901Controller
    /// </summary>
    public class F8901Controller : Controller
    {
        /// <summary>
        /// From the form F8901 workitem
        /// </summary>
        public new F8901WorkItem WorkItem
        {
            get { return base.WorkItem as F8901WorkItem; }
        }
    }
}
