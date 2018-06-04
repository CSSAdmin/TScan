using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
namespace D820016
{
    public class F820016Controller : Controller
    {
        public new F820016WorkItem WorkItem
        {
            get { return base.WorkItem as F820016WorkItem; }
        }
    }
}
