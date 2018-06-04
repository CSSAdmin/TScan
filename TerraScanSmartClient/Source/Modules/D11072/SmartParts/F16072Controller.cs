namespace D11072
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class F16072Controller : Controller
    {

        public new F16072WorkItem WorkItem
        {
            get { return base.WorkItem as F16072WorkItem; }
        }
    }
}
