//--------------------------------------------------------------------------------------------
// <copyright file="F1402Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F3510Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 14/3/2008    	R.Malliga      Created
//                  
//*********************************************************************************/

namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F1402Controller : Controller
    {
        /// <summary>
        /// From the form F1402 workitem
        /// </summary>
        public new F1402WorkItem WorkItem
        {
            get { return base.WorkItem as F1402WorkItem; }
        }
    }
}
