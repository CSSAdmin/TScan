using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
namespace D820014
{
    public class F820014Controller : Controller
    {
        public new F820014WorkItem WorkItem
        {
            get { return base.WorkItem as F820014WorkItem; }
        }
    }
}
