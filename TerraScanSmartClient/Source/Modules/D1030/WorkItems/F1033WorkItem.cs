// -------------------------------------------------------------------------------------------------
// <copyright file="F1100WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
// -------------------------------------------------------------------------------------------------

namespace D1030
{
    #region NameSpace

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

    #endregion NameSpace

    /// <summary>
    /// F1033 WorkItem
    /// </summary>
    public class F1033WorkItem : WorkItem
    {
        /// <summary>
        /// List PostType
        /// </summary>
        /// <param name="form">form</param>
        /// <returns>List the PostType DataTable</returns>
        public F1033SpecialDistrictSelectionData.ListPostTypeDataTable ListPostType(int? form)
        {
            return WSHelper.F1033_ListPostTypes(form);   
        }

        /// <summary>
        /// List Special Districts
        /// </summary>
        /// <param name="district">district</param>
        /// <param name="rollYear">rollYear</param>
        /// <param name="description">description</param>
        /// <param name="postTypeID">postTypeID</param>
        /// <returns>List the Specila District DataTable</returns>
        public F1033SpecialDistrictSelectionData.ListSpecialDistrictDataTable ListSpecialDistrict(int? district, int? rollYear, string description, int? postTypeID)
        {
            return WSHelper.F1033_ListSpecialDistricts(district, rollYear, description, postTypeID);      
        }

        /// <summary>
        /// Called when [run started].
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Called when [activated].
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }
    }
}
