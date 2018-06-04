//--------------------------------------------------------------------------------------------
// <copyright file="F1404Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1404Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 9/10/2009    	R.Malliga           Created
//                  
//*********************************************************************************/

namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F1404Controller : Controller
    {
        /// <summary>
        /// From the form F1402 workitem
        /// </summary>
        public new F1404WorkItem WorkItem
        {
            get { return base.WorkItem as F1404WorkItem; }
        }
    }
}
