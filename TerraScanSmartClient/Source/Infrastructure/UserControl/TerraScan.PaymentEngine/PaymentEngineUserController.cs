

namespace TerraScan.PaymentEngine
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;

    public class PaymentEngineUserController : Controller
    {
        /// <summary>
        /// Gets the work item.
        /// </summary>
        /// <value>The work item.</value>
        public new PaymentWorkItem WorkItem
        {
            get { return new PaymentWorkItem(); }
        }
    }
}
