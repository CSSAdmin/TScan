// -------------------------------------------------------------------------------------------------
// <copyright file="F39133WorkItem.cs" company="Congruent">
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



namespace D34133
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities; 

    public class F39133WorkItem : WorkItem 
    {
        #region F39133 Land Code Values

        #region F39133_ListLandCodeValues

        /// <summary>
        /// To List Land Code Values details.
        /// </summary>
        /// <returns>Returns typed dataset containing the entire land code values deatils </returns>
        public F39133LandCodeValueData F39133_ListLandCodeValues()
        {
            return WSHelper.F39133_ListLandCodeValues();
        }

        #endregion F391033_ListLandCodeValues

        #region F39133_ListIndividualLandCodeValuesItems

        /// <summary>
        /// To List Individual Land Code Values Items.
        /// </summary>
        /// <returns>Returns Typed Dataset containing following datatable:
        /// GetAppRollYear -- containing the application roll year
        /// ListNeighborhoodType -- containing the Neighborhood Type
        /// ListLandCode -- containing the LandCode
        /// ListUnitType -- containing the Unit type
        /// </returns>
        public F39133LandCodeValueData F39133_ListIndividualLandCodeValuesItems()
        {
            return WSHelper.F39133_ListIndividualLandCodeValuesItems();
        }

        #endregion F39133_ListIndividualLandCodeValuesItems

        #region F39133_ListNeighborhood
        /// <summary>
        /// F36033_s the type of the list neighborhood.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>ListNeighborhoodType -- containing the Neighborhood Type</returns>
        public F39133LandCodeValueData F39133_ListNeighborhoodType(int rollYear)
        {
            return WSHelper.F39133_ListNeighborhoodType(rollYear);
        }
        #endregion F39133_ListNeighborhood

        #region F39133_DeleteLandCodeValue

        /// <summary>
        /// To Delete land code value.
        /// </summary>
        /// <param name="luvId">The luv id.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns>Integer value</returns>
        public int F39133_DeleteLandCodevalue(int luvId, int userId)
        {
            return WSHelper.F39133_DeleteLandCodevalue(luvId, userId);
        }

        #endregion F39133_DeleteLandCodeValue

        #region F39133_SaveLandCodeValue

        /// <summary>
        /// To save land code value.
        /// </summary>
        /// <param name="landUnqiueId">The land unqiue id.</param>
        /// <param name="landValueItems">The land value items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>saved key id</returns>
        public int F39133_SaveLandCodeValue(int? landUnqiueId, string landValueItems, int userId)
        {
            return WSHelper.F39133_SaveLandCodeValue(landUnqiueId, landValueItems, userId);
        }

        #endregion F39133_SaveLandCodeValue

        #region F39133_CalculateNonCropValues
        /// <summary>
        /// To CalculateNonCropValues.
        /// </summary>
        /// <param name="RollYear">The RollYear.</param>
        /// <param name="CropRate">The CropRate.</param>
        /// <param name="NonCropRate">The NonCropRate.</param>
        /// <returns>saved key id</returns>
        public F39133LandCodeValueData F39133_CalculateNonCropValue(int rollYear, decimal? CropRate, decimal? NonCropRate)
        {
            return WSHelper.F39133_CalculateNonCropValue(rollYear, CropRate, NonCropRate);    
        }

        #endregion

        #endregion F36033 Land Code Values


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
