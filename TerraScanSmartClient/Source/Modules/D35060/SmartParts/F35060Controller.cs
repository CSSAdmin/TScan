namespace D35060
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using System.Linq;
    using Microsoft.Practices.CompositeUI;

    public class F35060Controller : Controller
    {
        /// <summary>
        /// From the form F35060 workitem
        /// </summary>
        public new F35060WorkItem WorkItem
        {
            get { return base.WorkItem as F35060WorkItem; }
        }
    }
}
