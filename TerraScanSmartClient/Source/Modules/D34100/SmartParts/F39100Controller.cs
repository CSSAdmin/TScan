// <summary>
//	This file contains methods for the F34100Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//
//*********************************************************************************/

namespace D34100
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F39100 Controller
    /// </summary>
    public class F39100Controller : Controller
    {
        /// <summary>
        /// From the form F39100 workitem
        /// </summary>
        public new F39100WorkItem WorkItem
        {
            get { return base.WorkItem as F39100WorkItem; }
        }

    }
}
