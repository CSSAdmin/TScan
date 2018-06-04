
namespace D23510
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F28510Controller : Controller
    {

        /// <summary>
        /// From the form F28510 workitem
        /// </summary>
        public new F28510WorkItem WorkItem
        {
            get { return base.WorkItem as F28510WorkItem; }
        }
    }
}
