//--------------------------------------------------------------------------------------------
// <copyright file="F1555Controller.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1555Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 04/05/2007       S. Pradeep         created                  
//*********************************************************************************/

namespace D11001
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// class for the controller F1555
    /// </summary>
    public class F1555Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value>The work item.</value>
        public new F1555WorkItem WorkItem
        {
            get { return base.WorkItem as F1555WorkItem; }
        }
    }
}
