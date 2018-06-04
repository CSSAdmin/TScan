using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
namespace D820004
{
    public class F820004Controller : Controller
    {
        public new F820004WorkItem WorkItem
        {
            get { return base.WorkItem as F820004WorkItem; }
        }
    }
}
