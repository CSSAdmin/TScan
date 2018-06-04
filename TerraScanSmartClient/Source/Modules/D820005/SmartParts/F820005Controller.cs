using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
namespace D820005
{
    public class F820005Controller : Controller
    {
        public new F820005WorkItem WorkItem
        {
            get { return base.WorkItem as F820005WorkItem; }
        }
    }
}
