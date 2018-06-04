// -------------------------------------------------------------------------------------------------
// <copyright file="F2001WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------


namespace D2000
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

    public class F2001WorkItem : WorkItem
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
        /// Gets the details of F25000 Parceldetails
        /// </summary>
        /// <param name="eventId">Parcel id</param>
        /// <returns>Typed Dataset</returns>
        //public F25000ParcelHeaderData F25000_GetParcelDetails(int parcelId)
        //{
        //    return WSHelper.F25000_GetParcelDetails(parcelId);
        //}
        public F2001ParcelLockingData F2001_getParcelLockingDetails(int keyId)
        {
            return WSHelper.F2001_getParcelLockingDetails(keyId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        public F2001ParcelLockingData F2001_getParcelLockingUsername(int userId)
        {
            return WSHelper.F2001_getParcelLockingUsername(userId);
        }

        /// <summary>
        /// F2001_UpdateParcelLockingDetails
        /// </summary>
        /// <param name="keyId">keyId</param>
        /// <param name="lockvalue">lockvalue</param>
        /// <param name="Adminlock">Adminlock</param>
        /// <param name="Appraisallock">Appraisallock</param>
        /// <returns></returns>
        public int F2001_UpdateParcelLockingDetails(int keyId, int lockvalue, int adminlock, int appraisallock, int userId)
        {
            return WSHelper.F2001_UpdateParcelLockingDetails(keyId, lockvalue, adminlock, appraisallock, userId);
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

        #endregion WorkItems Methods
    }
}
