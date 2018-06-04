//--------------------------------------------------------------------------------------------
// <copyright file="F1403Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1403Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09 Oct 09        R.Malliga            Created
//*********************************************************************************/

namespace D2000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F1403Controller : Controller
    {

        /// <summary>
        ///  Gets the current work item where the controller lives.
        /// </summary>     
        public new F1403WorkItem WorkItem
        {
            get { return base.WorkItem as F1403WorkItem; }
        }
    }
}
