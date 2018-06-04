using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
namespace D820002
{
    public class F820002Controller : Controller
    {
        public new F820002WorkItem WorkItem
        {
            get { return base.WorkItem as F820002WorkItem; }
        }
    }
}
