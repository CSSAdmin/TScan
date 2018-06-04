
namespace D1500
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
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    public class F1406WorkItem :WorkItem
    {
        #region basic

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

        #endregion basic

        /// <summary>
        /// F1406_s the central assessed grid details.
        /// </summary>
        /// <param name="centralSearchXML">The central search XML.</param>
        /// <returns></returns>
        public  F1406CentralAssessedSearchData F1406_CentralAssessedGridDetails(string centralSearchXML)
        {
            return WSHelper.F1406_CentralAssessedGridDetails(centralSearchXML);
        }

        /// <summary>
        /// F1406_s the load propert class combo.
        /// </summary>
        /// <returns></returns>
        public  F1406CentralAssessedSearchData F1406_LoadPropertClassCombo()
        {
            return WSHelper.F1406_LoadPropertClassCombo();
        }

        public F1404ScheduleSelectionData F1404_GetScheduleType(int? scheduleId)
        {
            return WSHelper.F1404_GetScheduleType(scheduleId);
        }

    }
}
