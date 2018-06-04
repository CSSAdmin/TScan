
// ---------------------------------------------------------------------------------------------------------------
// <copyright file="F36040WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F36040 Permanent Crop</summary>
// Release history
//*****************************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ----------------------------------------------------------------------------
// 26/10/2007       Shiva               Created
// 
// ----------------------------------------------------------------------------------------------------------------

namespace D36040
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F36040 WorkItem Class file
    /// </summary>
    public class F36040WorkItem : WorkItem
    {
        /// <summary>
        /// F36040_s the type of the list neighborhood.
        /// </summary>
        /// <returns>permanentCropDataSet</returns>
        public F36040PermanentCropData F36040_ListNeighborhoodType()
        {
            return WSHelper.F36040_ListNeighborhoodType();
        }

        /// <summary>
        /// F36040_s the type of the list neighborhood.
        /// </summary>
        /// <returns>permanentCropDataSet</returns>
        public F36040PermanentCropData F36040_ListCropCatalog()
        {
            return WSHelper.F36040_ListCropCatalog();
        }

        /// <summary>
        /// F36040_s the delete crop catalog.
        /// </summary>
        /// <param name="cropVId">The crop V id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>status of the transaction.</returns>
        public int F36040_DeleteCropCatalog(int cropVId, int userId)
        {
            return WSHelper.F36040_DeleteCropCatalog(cropVId, userId);
        }

        /// <summary>
        /// F36040_s the save crop catalog.
        /// </summary>
        /// <param name="cropUnqiueId">The crop unqiue id.</param>
        /// <param name="cropCatalogItems">The crop catalog items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>status of the transaction.</returns>
        public int F36040_SaveCropCatalog(int? cropUnqiueId, string cropCatalogItems, int userId)
        {
            return WSHelper.F36040_SaveCropCatalog(cropUnqiueId, cropCatalogItems, userId);
        }

        /// <summary>
        /// F36040_s the type of the list crop neighborhood.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public F36040PermanentCropData F36040_ListCropNeighborhoodType(int rollYear)
        {
            return WSHelper.F36040_ListCropNeighborhoodType(rollYear);
        }

        #region Protected Methods.
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

        #endregion
    }
}
