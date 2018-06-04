
namespace D20000
{

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
   
    public class F25006Controller : Controller
    {
        public new F25006WorkItem WorkItem
        {
            get { return base.WorkItem as F25006WorkItem; }
        }
    }
}
