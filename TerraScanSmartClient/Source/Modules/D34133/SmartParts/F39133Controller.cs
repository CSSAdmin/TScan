// <summary>
//	This file contains methods for the F39133Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//
//*********************************************************************************/


namespace D34133
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F39133Controller : Controller 
    {
        /// <summary>
        /// From the form F39100 workitem
        /// </summary>
        public new F39133WorkItem WorkItem
        {
            get { return base.WorkItem as F39133WorkItem; }
        }
    }
}
