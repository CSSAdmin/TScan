namespace D35060
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

    public class F35060WorkItem : WorkItem
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

        #region F35060 Schedule Item Code

        #region Get Schedule Item Code

        /// <summary>
        /// Gets the schedule item codes.
        /// </summary>
        /// <returns></returns>
        public F35060ScheduleItemCodeData GetScheduleItemCodes()
        {
            return WSHelper.GetScheduleItemCodes();
        }

        #endregion Get Schedule Item Code

        #region Save Schedule Item Code

        /// <summary>
        /// Saves the schedule item codes.
        /// </summary>
        /// <param name="scheduleCodeElements">The schedule code elements.</param>
        /// <param name="userId">The user id.</param>
        public void SaveScheduleItemCodes(string scheduleCodeElements, int userId)
        {
            WSHelper.SaveScheduleItemCodes(scheduleCodeElements, userId);
        }

        #endregion Save Schedule Item Code

        #region Delete Schedule Item Code

        /// <summary>
        /// Deletes the schedule item codes.
        /// </summary>
        /// <param name="itemCodeId">The item code id.</param>
        /// <param name="userId">The user id.</param>
        public void DeleteScheduleItemCodes(string itemCodeId, int userId)
        {
            //WSHelper.DeleteScheduleItemCodes(itemCodeId, userId);
        }

        #endregion Delete Schedule Item Code

        #endregion F35060 Schedule Item Code

    }
}
