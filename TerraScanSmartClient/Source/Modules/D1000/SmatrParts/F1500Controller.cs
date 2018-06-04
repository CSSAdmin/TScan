
namespace D1000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;


    /// <summary>
    /// 
    /// </summary>
   public class F1500Controller :Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
       public new F1500WorkItem WorkItem
       {
           get { return base.WorkItem as F1500WorkItem; }
       }
    }
}
