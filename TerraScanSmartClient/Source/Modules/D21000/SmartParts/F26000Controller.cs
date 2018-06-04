namespace D21000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// F26000Controller Class file
    /// </summary>
    public class F26000Controller : Controller
    {
        /// <summary>
        /// From the form F26000 workitem
        /// </summary>
        public new F26000WorkItem WorkItem
        {
            get { return base.WorkItem as F26000WorkItem; }
        }
    }
}