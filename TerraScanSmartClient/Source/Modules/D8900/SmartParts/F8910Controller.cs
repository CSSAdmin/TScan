//--------------------------------------------------------------------------------------------
// <copyright file="F8910Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8910Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09/10/2001   	M.Vijayakumar      Created
//                  
//*********************************************************************************/

namespace D8900
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    ///  Class for F8910Controller
    /// </summary>
    public class F8910Controller : Controller
    {
        /// <summary>
        /// From the form F8910 workitem
        /// </summary>
        public new F8910WorkItem WorkItem
        {
            get { return base.WorkItem as F8910WorkItem; }
        }
    }
}
