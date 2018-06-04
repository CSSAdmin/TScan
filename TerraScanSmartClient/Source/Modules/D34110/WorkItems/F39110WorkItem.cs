// -------------------------------------------------------------------------------------------------
// <copyright file="F34110WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//***********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//  
//***********************************************************************************************/


namespace D34110
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// workItem for F34110
    /// </summary>
    public class F39110WorkItem : WorkItem
    {
        #region F34110_GetTopDollarDetails

        /// <summary>
        /// F34110_GetTopDollarDetails
        /// </summary>
        /// <param name="TopDollarId">TopDollarId</param>
        /// <returns>DataSet</returns>
        public F34110TopDollarData F34110_GetTopDollarDetails(int TopDollarID)
        {
            return WSHelper.F34110_GetTopDollarDetails(TopDollarID);
        }

        #endregion

        #region F34110_SaveTopDollarDetails

        /// <summary>
        /// F34110_SaveTopDollarDistrict
        /// </summary>
        /// <param name="TopDollarID">TopDollarID</param>
        /// <param name="TopDollarDetails">TopDollarDetails</param>
        /// <returns>int</returns>
        public int F34110_SaveTopDollarDetails(int? TopDollarID, string TopDollarDetails, int userId)
        {
            return WSHelper.F34110_saveTopDollarDetails(TopDollarID, TopDollarDetails, userId);
        }

        #endregion

        #region F34110_DeleteTopDollarDetails

        ///<summary>
        /// F34100_Delete Agland Details
        /// </summary>
        /// <Param name="AglandID">AglandID</Param>
        /// <Param name="AglandID">UserID</Param>
        public void F34110_DeleteTopDollarDetails(int TopDollarID, int UserId)
        {
            WSHelper.F34110_DeleteTopDollarDetails(TopDollarID, UserId);
        }



        #endregion

        #region F34110 Calculate non Crop Dollar

        /// <summary>
        /// F34110_Calculate non crop Dollar
        /// </summary>
        /// <param name="cropDollar">cropDollar</param>
        /// <param name="countyFacytor">countyFacytor</param>
        /// <returns>DataSet</returns>
        public F34110TopDollarData F34110_CropTopDollar(decimal CropDollar, decimal CountyFactor)
        {
            return WSHelper.F34110_CropTopDollar(CropDollar,CountyFactor);
        }

        #endregion F34110 Calculate non Crop Dollar

        #region Work Item Methods

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
