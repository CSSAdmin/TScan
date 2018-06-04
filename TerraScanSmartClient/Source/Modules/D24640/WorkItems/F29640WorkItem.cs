namespace D24640
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
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    public class F29640WorkItem : WorkItem
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

        #region Get Frozen Value

        /// <summary>
        /// Gets the frozen value.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>DataSet contains Frozen Details</returns>
        public F29640FrozenValueData GetFrozenValue(int eventId)
        {
            return WSHelper.GetFrozenValue(eventId);
        }

        #endregion Get Frozen Value

        #region Save Frozen Value

        /// <summary>
        /// Saves the frozen details.
        /// </summary>
        /// <param name="frozenElements">The frozen elements.</param>
        /// <param name="userId">The user id.</param>
        public void SaveFrozenDetails(string frozenElements, int userId)
        {
            WSHelper.SaveFrozenDetails(frozenElements, userId);
        }

        #endregion Save Frozen Value

        #region Delete Frozen Value

        /// <summary>
        /// Deletes the frozen details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="frozenId">The frozen id.</param>
        /// <param name="userId">The user id.</param>
        public void DeleteFrozenDetails(int eventId, int frozenId, int userId)
        {
            WSHelper.DeleteFrozenDetails(eventId, frozenId, userId);
        }

        #endregion Delete Frozen Value

    }
}
