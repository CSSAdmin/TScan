//----------------------------------------------------------------------------------
// <copyright file="F9600WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Search String.
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------
// 13 Nov 06        VINOTH             Created
//*********************************************************************************/

namespace D9600
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.Helper;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F9600WorkItem Class
    /// </summary>
    public class F9600WorkItem : WorkItem
    {
        /////// <summary>
        /////// F9600_s the list search result.
        /////// </summary>
        /////// <param name="searchvalue">The searchvalue.</param>
        /////// <param name="appId">The app id.</param>
        /////// <returns>DataSet</returns>
        ////public F9600SearchData F9600_ListSearchResult(string searchvalue, int appId)
        ////{
        ////   return WSHelper.F9600_ListSearchResult(searchvalue, appId);
        ////}

        /// <summary>
        /// F9600_s the list sort result.
        /// </summary>
        /// <param name="searchValue">The search value.</param>
        /// <param name="appId">The app id.</param>
        /// <param name="searchOrder">if set to <c>true</c> [search order].</param>
        /// <param name="groupOrder">if set to <c>true</c> [group order].</param>
        /// <returns>DataSet</returns>
        public F9600SearchData.ListSearchTableDataTable F9600_ListSortResult(string searchValue, int appId, bool searchOrder, bool groupOrder)
        {
            return WSHelper.F9600_ListSortResult(searchValue, appId, searchOrder, groupOrder).ListSearchTable;
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
