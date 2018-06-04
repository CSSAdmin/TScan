
namespace D30080
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F35080Controller : Controller
    {
        /// <summary>
        /// From the form F29610 workitem
        /// </summary>
        public new F35080WorkItem WorkItem
        {
            get { return base.WorkItem as F35080WorkItem; }
        }
    }
}
