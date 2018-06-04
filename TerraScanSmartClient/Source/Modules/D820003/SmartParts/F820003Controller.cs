using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
namespace D820003
{
    public class F820003Controller : Controller
    {
        public new F820003WorkItem WorkItem
        {
            get { return base.WorkItem as F820003WorkItem; }
        }
    }
}
