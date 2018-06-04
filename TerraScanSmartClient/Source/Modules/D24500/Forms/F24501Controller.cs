//--------------------------------------------------------------------------------------------
// <copyright file="F25004Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F25004Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16/04/2013       Purushotham.A      Created
//                  
//*********************************************************************************/

namespace D24500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    
    /// <summary>
    /// F24501Controller class file
    /// </summary>
    public class F24501Controller : Controller
    {
        /// <summary>
        /// From the form F24501 workitem
        /// </summary>
        public new F24501WorkItem WorkItem
        {
            get { return base.WorkItem as F24501WorkItem; }
        }
    }
}
