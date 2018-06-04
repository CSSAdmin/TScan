namespace D30080
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

    public class F35081WorkItem : WorkItem
    {


        public F35075StateAssessedData F35075_GetStateAssessedOwnerDetails(int stateId)
        {
            return WSHelper.F35075_GetStateAssessedOwnerDetails(stateId);
        }
        /// <summary>
        /// F35081_s the central assessed grid details.
        /// </summary>
        /// <param name="CentralId">The central id.</param>
        /// <returns></returns>
        public  F35081CentralAssessedGridData F35081_CentralAssessedGridDetails(int CentralId)
        {
            return WSHelper.F35081_CentralAssessedGridDetails(CentralId);
        }

        /// <summary>
        /// F35081_s the central assessed rate details.
        /// </summary>
        /// <param name="subFundId">The sub fund id.</param>
        /// <param name="personalProperty">The personal property.</param>
        /// <param name="realProperty">The real property.</param>
        /// <returns></returns>
        public  F35081CentralAssessedGridData F35081_CentralAssessedRateDetails(int subFundId, decimal personalProperty, decimal realProperty, string description, string centralXMLList)
        {
            return WSHelper.F35081_CentralAssessedRateDetails(subFundId, personalProperty, realProperty,description,centralXMLList);
        }
        /// <summary>
        /// F35081_s the insert owner assessed grid.
        /// </summary>
        /// <param name="centralXMLItems">The central XML items.</param>
        /// <param name="centralId">The central id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public  void F35081_InsertOwnerAssessedGrid(string centralXMLItems, int centralId, int userId)
        {
             WSHelper.F35081_InsertOwnerAssessedGrid(centralXMLItems, centralId, userId);
        }
        /// <summary>
        /// F35081_s the delete owner grid details.
        /// </summary>
        /// <param name="removeXMLItems">The remove XML items.</param>
        /// <param name="centralId">The central id.</param>
        /// <param name="userId">The user id.</param>
        public  void F35081_DeleteOwnerGridDetails(string removeXMLItems, int centralId, int userId)
        {

            WSHelper.F35081_DeleteOwnerGridDetails(removeXMLItems, centralId, userId);
        }

        public F35080CentralAssessedOwner F35080_CentralAssessedOwnerDetails(int centralId)
        {
            return WSHelper.F35080_CentralAssessedOwnerDetails(centralId);
        }

        /// <summary>
        /// F35080_s the delete owner details.
        /// </summary>
        /// <param name="centralId">The central id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public void F35080_DeleteOwnerDetails(int centralId, int userId)
        {

            WSHelper.F35080_DeleteOwnerDetails(centralId, userId);
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
