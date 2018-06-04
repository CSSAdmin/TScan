// -------------------------------------------------------------------------------------------
// <copyright file="F25055WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// </summary>
// Release history
// ********************************************************************************************
// Date               Author            Description
// ----------        ---------     -------------------------------------------------------
// 
// -------------------------------------------------------------------------------------------
namespace D25055
{
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F25055WorkItem
    /// </summary>
    public class F25055WorkItem : WorkItem
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

        /// <summary>
        /// Gets the property header details.
        /// </summary>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns>Personal property details</returns>
        public F25055PropertyHeaderData GetPropertyHeaderDetails(int scheduleId)
        {
            return WSHelper.GetPropertyHeaderDetails(scheduleId);
        }
    }
}
