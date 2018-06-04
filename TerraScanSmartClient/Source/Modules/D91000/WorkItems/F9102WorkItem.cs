// -------------------------------------------------------------------------------------------------
// <copyright file="F9102WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------

namespace D91000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F9102WorkItem  class
    /// </summary> 
    public class F9102WorkItem : WorkItem
    {

        /// <summary>
        /// To get OwnerStatusDetails
        /// </summary>        
        /// <param name="typeID">The typeID</param>
        /// <param name="keyID">The keyID </param>
        /// <returns>Typed Dataset containing the OwnerStatusDetails</returns>
        public F9102OwnerStatusData F9102_GetOwnerStatusDetails(int typeID, int keyID)
        {
            return WSHelper.F9102_GetOwnerStatusDetails(typeID, keyID);
        }

        #region InsertSnapShotItems

        /// <summary>
        /// F9033_s the insert snap shot items.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="systemSnapShotXML">The system snap shot XML.</param>
        /// <returns></returns>
        public int F9033_InsertSnapShotItems(int? userId, string systemSnapShotXml)
        {
            return WSHelper.F9033_InsertSnapShotItems(userId, systemSnapShotXml);
        }

        #endregion InsertSnapShotItems

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
