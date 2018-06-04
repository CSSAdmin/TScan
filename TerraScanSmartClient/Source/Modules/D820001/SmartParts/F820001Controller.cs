using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
namespace D820001
{
    public class F820001Controller : Controller
    {
        public new F820001WorkItem WorkItem
        {
            get { return base.WorkItem as F820001WorkItem; }
        }
    }
}