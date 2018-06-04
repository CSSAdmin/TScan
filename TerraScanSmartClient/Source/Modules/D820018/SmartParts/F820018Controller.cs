using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
namespace D820018
{
    public class F820018Controller : Controller
    {
        public new F820018WorkItem WorkItem
        {
            get { return base.WorkItem as F820018WorkItem; }
        }
    }
}
