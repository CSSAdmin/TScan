

namespace D1203
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using System.Windows.Forms;
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    public class F1203WorkItem : WorkItem
    {
        /// <summary>
        /// Fires the <see cref="RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="Run"/>
        /// method is called on the <see cref="WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Fires the <see cref="Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        /// <summary>
        /// F1203s the load due date management.
        /// </summary>
        /// <returns></returns>
        public F1203DueDateManagementData F1203LoadDueDateManagement()
        {

            return WSHelper.F1203LoadDueDateManagement();
        }

        /// <summary>
        /// F1203_s the save due date management.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="dueDateXML">The due date XML.</param>
        public void F1203_SaveDueDateManagement(int userId, string dueDateXML)
        {
            WSHelper.F1203_SaveDueDateManagement(userId, dueDateXML);
        }
    }
}
