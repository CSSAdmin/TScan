using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
namespace D820008
{
    public class F820008Controller : Controller
    {
        public new F820008WorkItem WorkItem
        {
            get { return base.WorkItem as F820008WorkItem; }
        }
    }
}
