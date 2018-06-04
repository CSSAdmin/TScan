//--------------------------------------------------------------------------------------------
// <copyright file="F9066Controller.cs" company="Congruent">
//  Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F9066Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 23 Nov 07        D.LathaMaheswari   Created
//                  
//*********************************************************************************/

namespace D9065
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F9066Controller
    /// </summary>
    public class F9066Controller : Controller
    {
        /// <summary>
        ///  From the form F9066 workitem
        /// </summary>
        public new F9066WorkItem WorkItem
        {
            get { return base.WorkItem as F9066WorkItem; }
        }
    }
}
