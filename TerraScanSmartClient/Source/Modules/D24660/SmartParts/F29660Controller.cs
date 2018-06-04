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


namespace D24660
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.CompositeUI;  

    public class F29660Controller : Controller 
    {
        /// <summary>
        /// From the form F29660 workitem
        /// </summary>
       
        public new F29660WorkItem WorkItem
        {
            get { return base.WorkItem as F29660WorkItem; }
        }


    }
}
