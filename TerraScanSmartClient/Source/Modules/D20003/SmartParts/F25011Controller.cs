//--------------------------------------------------------------------------------------------
// <copyright file="F25011Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F25011Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 04/05/2007       M.Vijayakumar      Created
//                  
//*********************************************************************************/

namespace D20003
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F25011Controller class file
    /// </summary>
    public class F25011Controller : Controller
    {
        /// <summary>
        /// From the form F25011 workitem
        /// </summary>
        public new F25011WorkItem WorkItem
        {
            get { return base.WorkItem as F25011WorkItem; }
        }
    }
}
