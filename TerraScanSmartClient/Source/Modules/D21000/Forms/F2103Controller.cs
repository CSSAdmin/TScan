// <summary>
//	This file contains methods for the F2103Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//
//*********************************************************************************/

namespace D21000
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F2103Controller : Controller
    {
        /// <summary>
        /// From the form F2101 workitem
        /// </summary>
        public new F2103WorkItem WorkItem
        {
            get { return base.WorkItem as F2103WorkItem; }
        }
    }
}
