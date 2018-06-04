using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
namespace D820009
{
    public class F820009Controller : Controller
    {
        public new F820009WorkItem WorkItem
        {
            get { return base.WorkItem as F820009WorkItem; }
        }
    }
}
