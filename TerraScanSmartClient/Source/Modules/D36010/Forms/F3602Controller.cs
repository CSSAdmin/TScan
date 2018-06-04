
namespace D36010
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Class file for F3602Controller
    /// </summary>
    public class F3602Controller:Controller
    {
        /// <summary>
        /// From the form F3602Workitem
        /// </summary>
        public new F3602WorkItem WorkItem
        {
            get { return base.WorkItem as F3602WorkItem; }
        }
    }
}
