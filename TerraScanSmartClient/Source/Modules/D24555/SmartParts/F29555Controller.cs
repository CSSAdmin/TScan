

namespace D24555
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

   public class F29555Controller :Controller
   {
       /// <summary>
       /// Gets the work item.
       /// </summary>
       /// <value>The work item.</value>
       public new F29555WorkItem WorkItem
       {
           get { return base.WorkItem as F29555WorkItem; }
       }
   }
    
}
