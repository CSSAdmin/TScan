// <summary>
//	This file contains methods for the F27081Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//
//*********************************************************************************/
namespace D22081
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F27081 Controller
    /// </summary>
    public class F27081Controller : Controller 
    {
        /// <summary>
        /// From the form F27081 workitem
        /// </summary>
        public new F27081WorkItem WorkItem
        {
            get { return base.WorkItem as F27081WorkItem; }
        }

    }
}
