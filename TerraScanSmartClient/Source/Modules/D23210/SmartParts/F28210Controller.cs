
namespace D23210
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F28210Controller : Controller
    {

        /// <summary>
        /// From the form F28210 workitem
        /// </summary>
        public new F28210WorkItem WorkItem
        {
            get { return base.WorkItem as F28210WorkItem; }
        }
    }
}
