
namespace D2000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Class file for F2001Controller
    /// </summary>
    public class F2001Controller : Controller
    {
        /// <summary>
        /// From the form F1513 workitem
        /// </summary>
        public new F2001WorkItem WorkItem
        {
            get { return base.WorkItem as F2001WorkItem; }
        }

    }
}
