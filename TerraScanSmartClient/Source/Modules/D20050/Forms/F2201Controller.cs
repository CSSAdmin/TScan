
namespace D20050
{

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    public class F2201Controller:Controller
    {
        public new F2201WorkItem WorkItem
        {
            get { return base.WorkItem as F2201WorkItem; }
        }
    }
}
