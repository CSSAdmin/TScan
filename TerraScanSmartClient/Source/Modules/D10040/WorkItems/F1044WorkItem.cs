// -------------------------------------------------------------------------------------------------
// <copyright file="F1044WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------
namespace D10040
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    public class F1044WorkItem:WorkItem
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
        /// Lists the improve District type.
        /// </summary>
        /// <returns>F25000ParcelHeaderData</returns>
        public F16040ImprovementDistrictDefinition ImprovementDistrictTypelist(string districtIdType)
        {
            return WSHelper.ImprovementDistrictTypelist(districtIdType);
        }
    }
}
