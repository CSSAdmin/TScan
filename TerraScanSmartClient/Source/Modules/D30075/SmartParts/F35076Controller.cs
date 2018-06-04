
namespace D30075
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F35076Controller : Controller
    {

        /// <summary>
        /// From the form F29610 workitem
        /// </summary>
        public new F35076WorkItem WorkItem
        {
            get { return base.WorkItem as F35076WorkItem; }
        }
    }
}
