
namespace D84100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F84123Controller
    /// </summary>
    public class F84123Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F84123WorkItem WorkItem
        {
            get
            { 
                return base.WorkItem as F84123WorkItem;
            }
        }
    }
}
