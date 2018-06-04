
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

    public class F35080WorkItem : WorkItem
    {


        /// <summary>
        /// F35080_s the central assessed owner details.
        /// </summary>
        /// <param name="centralId">The central id.</param>
        /// <returns></returns>
        public  F35080CentralAssessedOwner F35080_CentralAssessedOwnerDetails(int centralId)
        {
            return WSHelper.F35080_CentralAssessedOwnerDetails(centralId);            
         }

        /// <summary>
        /// F35080_s the property class combo.
        /// </summary>
        /// <returns></returns>
        public  F35080CentralAssessedOwner F35080_PropertyClassCombo()
        {

            return WSHelper.F35080_PropertyClassCombo();
        }

        /// <summary>
        /// F35080_s the delete owner details.
        /// </summary>
        /// <param name="centralId">The central id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public  void F35080_DeleteOwnerDetails(int centralId, int userId)
        {

             WSHelper.F35080_DeleteOwnerDetails(centralId, userId);
        }

        /// <summary>
        /// F35080_s the insert owner central details.
        /// </summary>
        /// <param name="centralId">The central id.</param>
        /// <param name="centralXML">The central XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public  int F35080_InsertOwnerCentralDetails(int? centralId, string centralXML, int userId)
        {

            return WSHelper.F35080_InsertOwnerCentralDetails(centralId, centralXML, userId);
        }


        /// <summary>
        /// F35080_s the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns></returns>
        public F35080CentralAssessedOwner F35080_OwnerDetails(int ownerId)
        {
            return WSHelper.F35080_OwnerDetails(ownerId);
        }

        /// <summary>
        /// Gets the owner details.
        /// </summary>
        /// <param name="ownerId">The owner id.</param>
        /// <returns>Returns PartiesOwnerDetails Dataset</returns>
        public PartiesOwnerDetailsData GetOwnerDetails(int ownerId)
        {
            return WSHelper.GetOwnerDetails(ownerId);
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
