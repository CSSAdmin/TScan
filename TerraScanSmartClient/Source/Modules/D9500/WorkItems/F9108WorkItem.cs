// -------------------------------------------------------------------------------------------------
// <copyright file="F9108WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------

namespace D9500
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;
    
    /// <summary>
    /// F9108WorkItem  class
    /// </summary> 
    public class F9108WorkItem : WorkItem
    {
        /// <summary>
        /// Gets the district selection data.
        /// </summary>
        /// <param name="districtID">The district ID.</param>
        /// <param name="district">The district.</param>
        /// <param name="description">The description.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>typed dataset</returns>
        ////public static DistrictSelectionData GetDistrictSelectionData(int districtId, int district, string description, int rollYear)
        ////{
        ////    return WSHelper.GetDistrictSelectionData(districtId, district, description, rollYear);
        ////}

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
