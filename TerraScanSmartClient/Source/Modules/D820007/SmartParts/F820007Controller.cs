using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
namespace D820007
{
    public class F820007Controller : Controller
    {
        public new F820007WorkItem WorkItem
        {
            get { return base.WorkItem as F820007WorkItem; }
        }
    }
}
