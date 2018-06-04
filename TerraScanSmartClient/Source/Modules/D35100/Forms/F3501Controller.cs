

namespace D35100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Class file for F3501Controller
    /// </summary>
    public class F3501Controller : Controller
    {
        /// <summary>
        /// From the form F1513 workitem
        /// </summary>
        public new F3501WorkItem WorkItem
        {
            get { return base.WorkItem as F3501WorkItem; }
        }
    }
}
