

namespace D30075
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F35075Controller : Controller
    {
        /// <summary>
        /// From the form F29610 workitem
        /// </summary>
        public new F35075WorkItem WorkItem
        {
            get { return base.WorkItem as F35075WorkItem; }
        }
    }
}
