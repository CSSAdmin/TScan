//***********************************************************************************************/
// <summary>
//	This file contains methods for the F25051Controller.
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
    /// F25051Controller Class file
    /// </summary>
    public class F25051Controller : Controller
    {
        /// <summary>
        /// From the form F39100 workitem
        /// </summary>
        public new F25051WorkItem WorkItem
        {
            get { return base.WorkItem as F25051WorkItem; }
        }
    }
}
