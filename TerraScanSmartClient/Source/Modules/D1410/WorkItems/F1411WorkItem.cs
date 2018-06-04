//--------------------------------------------------------------------------------------------
// <copyright file="F1411WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1411 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//
//*********************************************************************************/


namespace D1410
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;


    /// <summary>
    /// F1411 WorkItem
    /// </summary>
    public class F1411WorkItem : WorkItem 
    {
        #region F1411_listParcelStmtSearchDetails
        /// <summary>
        /// F1411_listParcelStmtDetails
        /// </summary>
        /// <param name="searchNumber">searchNumber</param>
        /// <returns>DataSet</returns>
        public F1411ParcelStatementSearchData f1411ParcelStmtSearch(string searchNumber)
        {
            return WSHelper.f1411ParcelStmtSearch(searchNumber);
        }

        #endregion F1411_listParcelStmtSearchDetails

        #region Protected Methods

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

        #endregion
    }
}
