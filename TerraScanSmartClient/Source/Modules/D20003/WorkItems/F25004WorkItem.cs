// -------------------------------------------------------------------------------------------
// <copyright file="F25004WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F25004 Funcationality</summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 04/05/2007       M.Vijayakumar      Created
// 
// -------------------------------------------------------------------------------------------

namespace D20003
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

    /// <summary>
    /// F25004WorkItem Class File
    /// </summary>
    public class F25004WorkItem : WorkItem
    {
        #region F25003 Situs Management

        #region List Situs Management Details

        /// <summary>
        /// To List Situs Mangement Details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="situsId">The situs id.</param>
        /// <returns>Typed Dataset containing the Situs Mangement Details</returns>
        public F25003SitusManagementData F25003_ListSitusMangement(int parcelId, int situsId)
        {
            return WSHelper.F25003_ListSitusMangement(parcelId, situsId);
        }

        #endregion List Situs Management Details

        #region List Street Details

        /// <summary>
        /// To List Street Details.
        /// </summary>
        /// <returns>Typed Dataset containing the Street Details</returns>
        public F25003SitusManagementData F25003_ListStreet()
        {
            return WSHelper.F25003_ListStreet();
        }

        #endregion List Street Details

        #region List Unit Type Details

        /// <summary>
        /// To list Unit Type Details.
        /// </summary>
        /// <returns>Typed DataSet containing the Unit Type Details</returns>
        public F25003SitusManagementData F25003_ListUnitType()
        {
            return WSHelper.F25003_ListUnitType();
        }

        #endregion List Unit Type Details

        #region Save Situs Management

        /// <summary>
        /// To Save List Mangement Details.
        /// </summary>
        /// <param name="situsId">The situs id.</param>
        /// <param name="situsItems">The situs items.</param>
        /// <returns>Intger value containing the new SitusID</returns>
        public int F25003_SaveListMangement(int situsId, string situsItems,bool isFuturePush, int userId)
        {
            return WSHelper.F25003_SaveListMangement(situsId, situsItems,isFuturePush,userId);
        }

        #endregion Save Situs Management

        #endregion F25003 Situs Management

        #region List Master Street List

        /// <summary>
        /// To List Master Street List.
        /// </summary>
        /// <param name="streetName">Name of the street.</param>
        /// <param name="city">The city.</param>
        /// <returns>Typed DataSet Containing the Master Street List details.</returns>
        public F25011StreetListManagementData F25011_ListMasterStreetList(int streetID,string streetName, string city)
        {
            return WSHelper.F25011_ListMasterStreetList(streetID,streetName, city);
        }

        #endregion List Master Street List

        #region WorkItem Common Methods

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

        #endregion WorkItem Common Methods
    }
}
