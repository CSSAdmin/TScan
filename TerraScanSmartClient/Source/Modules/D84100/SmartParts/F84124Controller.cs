namespace D84100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F84124Controller
    /// </summary>
    public class F84124Controller : Controller 
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F84124WorkItem WorkItem
        {
            get
            {
                return base.WorkItem as F84124WorkItem;
            }
        }
    }
}
