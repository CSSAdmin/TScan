
namespace D25055
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


    /// <summary>
    /// F35055WorkItem
    /// </summary>
    public class F35055WorkItem : WorkItem
    {
        /// <summary>
        /// F35055_s the get PP line items details.
        /// </summary>
        /// <param name="scheduleID">The schedule ID.</param>
        /// <returns>returns F35055PPLineItemData</returns>
        public F35055PPLineItemData F35055_GetPPLineItemsDetails(int scheduleId)
        {
            return WSHelper.F35055_GetPPLineItemsDetails(scheduleId);
        }

        /// <summary>
        /// F35055_s the get value calculation.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="pipeDeprTableId">The pipe depr table id.</param>
        /// <param name="originalValue">The original value.</param>
        /// <param name="trend">The trend.</param>
        /// <param name="year">The year.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public F35055PPLineItemData F35055_GetValueCalculation(int scheduleId, int pipeDeprTableId, Int64 originalValue, int trend, Int16 year, Int16 rollYear)
        {
            return WSHelper.F35055_GetValueCalculation(scheduleId, pipeDeprTableId, originalValue, trend, year, rollYear);
        }

        /// <summary>
        /// F35055_s the save schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F35055_SaveScheduleLineItem(int scheduleId, string scheduleItems, int userId)
        {
            return WSHelper.F35055_SaveScheduleLineItem(scheduleId, scheduleItems, userId);
        }

        /// <summary>
        /// F35055_s the update schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItems">The schedule items.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public int F35055_UpdateScheduleLineItem(int scheduleId, string scheduleItems, int userId, Int16 rollYear)
        {
            return WSHelper.F35055_UpdateScheduleLineItem(scheduleId, scheduleItems, userId, rollYear);
        }

        /// <summary>
        /// F35055_s the delete schedule line item.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <param name="scheduleItemIds">The schedule item ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F35055_DeleteScheduleLineItem(int scheduleId, string scheduleItemIds, int userId)
        {
            return WSHelper.F35055_DeleteScheduleLineItem(scheduleId, scheduleItemIds, userId);
        }

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
    }
}
