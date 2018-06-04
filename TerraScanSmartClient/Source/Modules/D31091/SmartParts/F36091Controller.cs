
namespace D31091
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F36091Controller : Controller
    {

        /// <summary>
        /// From the form F36091 workitem
        /// </summary>
        public new F36091WorkItem WorkItem
        {
            get { return base.WorkItem as F36091WorkItem; }
        }
    }
}
