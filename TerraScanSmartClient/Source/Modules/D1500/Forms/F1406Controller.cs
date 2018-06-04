

namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

   public class F1406Controller:Controller
    {
        /// <summary>
        /// From the form F1503 workitem
        /// </summary>
        public new F1406WorkItem WorkItem
        {
            get { return base.WorkItem as F1406WorkItem; }
        }
    }
}
