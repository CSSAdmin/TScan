//--------------------------------------------------------------------------------------------
// <copyright file="F1557Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1557Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 28/09/2016   Priyadharshini.R       Created              
//*********************************************************************************/

namespace D11001
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// class for the controller F1557
    /// </summary>
    public class F1557Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value>The work item.</value>
        public new F1557WorkItem WorkItem
        {
            get { return base.WorkItem as F1557WorkItem; }
        }
    }
}
