// <summary>
//	This file contains methods for the F2102Controller.
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

    public class F2102Controller : Controller
    {
        /// <summary>
        /// From the form F2101 workitem
        /// </summary>
        public new F2102WorkItem WorkItem
        {
            get { return base.WorkItem as F2102WorkItem; }
        }
    }
}
