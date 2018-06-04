using System;
using System.Collections.Generic;
using System.Text;

namespace D8000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F8001Controller : Controller
    {
        /// <summary>
        /// Gets the current work item where the controller lives.
        /// </summary>
        /// <value></value>
        public new F8001WorkItem WorkItem
        {
            get { return base.WorkItem as F8001WorkItem; }
        }
    }


}
