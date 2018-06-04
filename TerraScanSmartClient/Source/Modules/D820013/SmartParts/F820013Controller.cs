using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
namespace D820013
{
    public class F820013Controller : Controller
    {
        public new F820013WorkItem WorkItem
        {
            get { return base.WorkItem as F820013WorkItem; }
        }
    }
}
