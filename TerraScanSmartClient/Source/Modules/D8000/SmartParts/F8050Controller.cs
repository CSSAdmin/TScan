//--------------------------------------------------------------------------------------------
// <copyright file="F8050Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9050 Gdoc Comments Controllers.
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
    /// class for F8050Controller
    /// </summary>
    public class F8050Controller : Controller
    {
        /// <summary>
        /// From the form F8050 workitem
        /// </summary>
        public new F8050WorkItem WorkItem
        {
            get { return base.WorkItem as F8050WorkItem; }
        }
    }
}
