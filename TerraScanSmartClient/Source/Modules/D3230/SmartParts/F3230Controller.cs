namespace D3230
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F3230Controller : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new F3230WorkItem WorkItem
        {
            get { return base.WorkItem as F3230WorkItem; }
        }
    }
}
