namespace D9030
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F9612 controller
    /// </summary>
    public class F9612Controller : Controller 
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F9612WorkItem WorkItem
        {
            get { return base.WorkItem as F9612WorkItem; }
        }
    }
}
