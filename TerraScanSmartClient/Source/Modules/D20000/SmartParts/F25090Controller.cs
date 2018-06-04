//***********************************************************************************************/
// <summary>
//	This file contains methods for the F25090Controller.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//
//*********************************************************************************/




namespace D20000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;


    /// <summary>
    /// F25090 Controller
    /// </summary>
    public class F25090Controller : Controller
    {
        /// <summary>
        /// From the form F25090 workitem
        /// </summary>
        public new F25090WorkItem WorkItem
        {
            get { return base.WorkItem as F25090WorkItem; }
        }
    }
}
