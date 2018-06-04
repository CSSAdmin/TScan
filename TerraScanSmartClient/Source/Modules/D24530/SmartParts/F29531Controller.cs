
namespace D24530
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F29531Controller : Controller
    {
        /// <summary>
        /// From the form F29531 workitem
        /// </summary>
        public new F29531WorkItem WorkItem
        {
            get { return base.WorkItem as F29531WorkItem; }
        }
    }
}
