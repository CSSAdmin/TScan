namespace D30080
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F35081Controller : Controller
    {

        public new F35081WorkItem WorkItem
        {
            get { return base.WorkItem as F35081WorkItem; }
        }
    }
}
