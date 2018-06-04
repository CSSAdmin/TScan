// <summary>
//	This file contains methods for the F39135Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//
//*********************************************************************************/



namespace D34135
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.CompositeUI;  

    public class F39135Controller : Controller 
    {
        /// <summary>
        /// From the form F39100 workitem
        /// </summary>
        public new F39135WorkItem WorkItem
        {
            get { return base.WorkItem as F39135WorkItem; }
        }

    }
}
