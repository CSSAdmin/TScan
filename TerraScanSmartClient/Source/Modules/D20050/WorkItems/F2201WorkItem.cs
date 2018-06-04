
namespace D20050
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

    public class F2201WorkItem : WorkItem
    {
        /// <summary>
        /// F2201_s the get personal property description.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        public  F2201CentrallyAssessedSearchData F2201_GetPersonalPropertyDescription(string code)
        {
            return WSHelper.F2201_GetPersonalPropertyDescription(code);
        }

        /// <summary>
        /// F2201_s the get personal property search.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        public  F2201CentrallyAssessedSearchData F2201_GetPersonalPropertySearch(string code, string description)
        {
            return WSHelper.F2201_GetPersonalPropertySearch(code, description);
        }
    }
}
