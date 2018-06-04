
namespace D30085
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F35085Controller : Controller
    {

        /// <summary>
        /// From the form F35085 workitem
        /// </summary>
        public new F35085WorkItem WorkItem
        {
            get { return base.WorkItem as F35085WorkItem; }
        }
    }
}
