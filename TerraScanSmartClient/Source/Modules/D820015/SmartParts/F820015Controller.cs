using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
namespace D820015
{
    public class F820015Controller : Controller
    {
        public new F820015WorkItem WorkItem
        {
            get { return base.WorkItem as F820015WorkItem; }
        }
    }
}
