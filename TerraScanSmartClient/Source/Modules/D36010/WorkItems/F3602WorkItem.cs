// -------------------------------------------------------------------------------------------------
// <copyright file="F2005WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------

namespace D36010
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    public class F3602WorkItem:WorkItem
    {
        # region Copy or Move Misc Improvements.

        /// <summary>
        /// Gets the sandwich and its slice information.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>FormMasterData</returns>
        public FormMasterData GetSandwichAndItsSliceInformation(int form, int keyId, int userId)
        {
            return WSHelper.GetSandwichAndItsSliceInformation(form, keyId, userId);
        }

        /// <summary>
        /// To Get the Object details
        /// </summary>
        /// <param name="valueSliceId">ValuSliceID</param>
        /// <returns>Typed dataset containing the Depr Table details</returns>
        public F3602CopyMoveMiscImprovement GetObjectDetails(int parcelId)
        {
            return WSHelper.GetObjectDetails(parcelId);
        }

        /// <summary>
        /// To List the Object Types.
        /// </summary>
        public F3602CopyMoveMiscImprovement GetObjectTypesList()
        {
            return WSHelper.GetObjectTypesList();
        }

        /// <summary>
        /// To List the Object Types.
        /// </summary>
        public F3602CopyMoveMiscImprovement GetValueSlicesList(int parcelId,int objectId)
        {
            return WSHelper.GetValueSlicesList(parcelId, objectId);
        }

        /// <summary>
        /// To List the Object Types.
        /// </summary>
        public F3602CopyMoveMiscImprovement GetMiscImprovementsList(int valueSliceId)
        {
            return WSHelper.GetMiscImprovementsList(valueSliceId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <param name="parcelId"></param>
        /// <param name="isNewObject"></param>
        /// <param name="existingObjectId"></param>
        /// <param name="newObjectTypeId"></param>
        /// <param name="isNewValueslice"></param>
        /// <param name="existingValueslice"></param>
        /// <param name="miscImprovements"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public F3602CopyMoveMiscImprovement F3602_ProcessMiscImprovements(string copyMove, int parcelId, bool isNewObject, int existingObjectId, int newObjectTypeId, bool isNewValueslice, int existingValueslice, string miscImprovements, int userId)
        {
            return WSHelper.F3602_ProcessMiscImprovements(copyMove, parcelId, isNewObject, existingObjectId, newObjectTypeId, isNewValueslice, existingValueslice, miscImprovements, userId);
        }

        #endregion

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
