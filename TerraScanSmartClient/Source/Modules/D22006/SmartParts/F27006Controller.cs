//--------------------------------------------------------------------------------------------
// <copyright file="F27006Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F27006Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 28/03/07         M.Vijayakumar       Created                
//*********************************************************************************/

namespace D22006
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F27006Controller Class file
    /// </summary>
    public class F27006Controller : Controller
    {
        /// <summary>
        /// From the form F27006 workitem
        /// </summary>
        public new F27006WorkItem WorkItem
        {
            get { return base.WorkItem as F27006WorkItem; }
        }
    }
}
