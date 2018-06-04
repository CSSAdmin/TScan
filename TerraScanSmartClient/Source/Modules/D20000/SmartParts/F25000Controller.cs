

namespace D20000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F27006Controller Class file
    /// </summary>
    public class F25000Controller : Controller
    {
        /// <summary>
        /// From the form F25000 workitem
        /// </summary>
        public new F25000WorkItem WorkItem
        {
            get { return base.WorkItem as F25000WorkItem; }
        }
    }
}
