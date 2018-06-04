
namespace D91000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    public class F9105WorkItem :WorkItem
    {


        public F96000OwnerManagementData F96000_CountryComboDetails()
        {
            return WSHelper.F96000_CountryComboDetails();
        }

        /// <summary>
        /// F9105_s the name of the execute copy.
        /// </summary>
        /// <param name="copyData">The copy data.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F9105_ExecuteCopyName(string copyData, int userId)
        {
            return WSHelper.F9105_ExecuteCopyName(copyData, userId);
        }
        #region SliceEvents
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
        #endregion SliceEvents

    }
}
