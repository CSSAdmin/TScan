using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
namespace D820006
{
    public class F820006Controller : Controller
    {
        public new F820006WorkItem WorkItem
        {
            get { return base.WorkItem as F820006WorkItem; }
        }
    }
}
