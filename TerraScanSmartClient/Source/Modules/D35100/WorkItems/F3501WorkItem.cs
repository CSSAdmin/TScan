// -------------------------------------------------------------------------------------------------
// <copyright file="F3501WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------


namespace D35100
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F2001WorkItem class file
    /// </summary> 
    public class F3501WorkItem : WorkItem
    {
        #region WorkItems Methods

        /// <summary>
        /// Gets the form details.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>GetFormDetails</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
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

        /// <summary>
        /// ListNeighborhoodParcelLocks
        /// </summary>
        /// <param name="neighborId">neighborId</param>
        /// <returns>F3501NeighborhoodParcelLocksData</returns>
        public F3501NeighborhoodParcelLocksData ListNeighborhoodParcelLocks(int neighborId)
        {
            return WSHelper.ListNeighborhoodParcelLocks(neighborId);
        }

        /// <summary>
        /// F2001_GetValidUserName
        /// </summary>
        /// <param name="keyId">keyId</param>
        /// <param name="userId">userId</param>
        /// <returns>F2001_GetValidUserName</returns>
        public int F2001_GetValidUserName(int keyId, int userId, string formNo)
        {
            return WSHelper.F2001_GetValidUserName(keyId, userId, formNo);
            
        }

        public F3501NeighborhoodParcelLocksData UpdateParcelLockingDetails(int keyId, int valueLock, int adminLock, int lockAppraisal, int primaryId, int userId)
        {
            return WSHelper.UpdateParcelLockingDetails(keyId, valueLock, adminLock, lockAppraisal, primaryId, userId);
        }
        #endregion WorkItems Methods
    }
}
