// <summary>
//	This file contains methods for the F27000Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 14 Apr 07       Sriparameswari             created
//*********************************************************************************/

namespace D22080
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Gets the work item.
    /// </summary>
    /// <value>The work item.</value>
    public class F27080Controller : Controller
    {
        /// <summary>
        /// From the form F36032 workitem
        /// </summary>
        public new F27080WorkItem WorkItem
        {
            get { return base.WorkItem as F27080WorkItem; }
        }
    }
}
