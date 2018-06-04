

namespace D91000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F9105Controller : Controller
    {
       public new F9105WorkItem WorkItem
       {
           get { return base.WorkItem as F9105WorkItem; }
       }
    }
}
