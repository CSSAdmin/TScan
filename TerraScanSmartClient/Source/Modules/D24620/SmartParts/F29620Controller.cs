
namespace D24620
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F29620Controller : Controller
    {
        /// <summary>
        /// From the form F29610 workitem
        /// </summary>
        public new F29620WorkItem WorkItem
        {
            get { return base.WorkItem as F29620WorkItem; }
        }
    }
}
