//--------------------------------------------------------------------------------------------
// <copyright file="F15015Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F15015Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09/04/07         M.Vijayakumar       Created                
//*********************************************************************************/

namespace D11015
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F15015Controller class file
    /// </summary>
    public class F15015Controller : Controller
    {
        /// <summary>
        /// From the form F27006 workitem
        /// </summary>
        public new F15015WorkItem WorkItem
        {
            get { return base.WorkItem as F15015WorkItem; }
        }
    }
}
