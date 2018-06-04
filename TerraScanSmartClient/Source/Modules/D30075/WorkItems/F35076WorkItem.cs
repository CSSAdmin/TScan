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

namespace D30075
{
    public class F35076WorkItem : WorkItem
    {
        /// <summary>
        /// F35075_s the get state assessed owner details.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <returns></returns>
        public F35075StateAssessedData F35075_GetStateAssessedOwnerDetails(int stateId)
        {
            return WSHelper.F35075_GetStateAssessedOwnerDetails(stateId);
        }
        
        /// <summary>
        /// F35076_s the save state assessed grid.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <param name="codeItems">The code items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F35076_SaveStateAssessedGrid(int? stateId, string codeItems, int userId)
        {
            return WSHelper.F35076_SaveStateAssessedGrid(stateId, codeItems, userId);
        }

        /// <summary>
        /// F35075_s the delete state assessed.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F35075_DeleteStateAssessed(int stateId, int userId)
        {
            return WSHelper.F35075_DeleteStateAssessed(stateId, userId);
        }

        /// <summary>
        /// F35076_s the delete state assessed details.
        /// </summary>
        /// <param name="stateItemId">The state item id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public int F35076_DeleteStateAssessedDetails(int stateItemId, int userId)
        {
            return WSHelper.F35076_DeleteStateAssessedDetails(stateItemId, userId);
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
