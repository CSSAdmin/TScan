

namespace D24636
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

   public class F29636Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F29636WorkItem WorkItem
        {
            get { return base.WorkItem as F29636WorkItem; }
        }
    }
}
