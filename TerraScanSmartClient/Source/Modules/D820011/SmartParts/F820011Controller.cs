using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
namespace D820011
{
    public class F820011Controller : Controller
    {
        public new F820011WorkItem WorkItem
        {
            get { return base.WorkItem as F820011WorkItem; }
        }
    }
}
