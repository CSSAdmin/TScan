

namespace D1203
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F1203Controller : Controller
    {
        public new F1203WorkItem WorkItem
        {
            get { return base.WorkItem as F1203WorkItem; }
        }

    }
}
