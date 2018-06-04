using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
namespace D820012
{
    public class F820012Controller : Controller
    {
        public new F820012WorkItem WorkItem
        {
            get { return base.WorkItem as F820012WorkItem; }
        }
    }
}
