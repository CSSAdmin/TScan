namespace D24650
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F29650 Controller
    /// </summary>
    public class F29650Controller : Controller
    {
        /// <summary>
        /// From the form F29650 workitem
        /// </summary>
        public new F29650WorkItem WorkItem
        {
            get { return base.WorkItem as F29650WorkItem; }
        }
    }
}
