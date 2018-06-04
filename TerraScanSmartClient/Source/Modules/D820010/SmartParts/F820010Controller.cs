using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
namespace D820010
{
    public class F820010Controller : Controller
    {
        public new F820010WorkItem WorkItem
        {
            get { return base.WorkItem as F820010WorkItem; }
        }
    }
}
