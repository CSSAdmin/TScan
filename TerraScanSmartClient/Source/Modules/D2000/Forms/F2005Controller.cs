
namespace D2000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    /// <summary>
    /// Class file for F2005Controller
    /// </summary>
    public class F2005Controller:Controller
    {
        /// <summary>
        /// From the form F2005workitem
        /// </summary>
        public new F2005WorkItem WorkItem
        {
        get { return base.WorkItem as F2005WorkItem;}
        }
    }
}
