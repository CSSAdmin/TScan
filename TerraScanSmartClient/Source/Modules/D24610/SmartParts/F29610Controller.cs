
namespace D24610
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F29610Controller : Controller
    {
        /// <summary>
        /// From the form F29610 workitem
        /// </summary>
        public new F29610WorkItem WorkItem
        {
            get { return base.WorkItem as F29610WorkItem; }
        }
    }
}
