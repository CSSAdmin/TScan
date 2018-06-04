//--------------------------------------------------------------------------------------------
// <copyright file="F3511WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Fund Selection. 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20121022   	    PALANI              Created
//*********************************************************************************/

namespace D35100
{
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    public class F3511WorkItem : WorkItem
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
        /// Check Duplicate Neighbourhood
        /// </summary>
        /// <param name="nbhdGroupId"></param>
        /// <param name="neighborhoodGroupHeader"></param>
        /// <returns></returns>
        public int DuplicateNeighborhoodHeaderCheck(int nbhdGroupId, string neighborhoodGroupHeader)
        {
            return WSHelper.DuplicateNeighborhoodHeaderCheck(nbhdGroupId, neighborhoodGroupHeader);
        }
           
        /// <summary>
        /// copy of neighbourhood
        /// </summary>
        /// <param name="nbhdGroupId">neibourhood ID</param>
        /// <param name="neighborhoodname">New neighbourhood name</param>
        /// <returns></returns>
        public int CopyNeighbourhood(int nbhdGroupId,string neighborhoodname)
        {
            return WSHelper.F3511_ExeNeighborhoodDetails(nbhdGroupId, neighborhoodname, TerraScan.Common.TerraScanCommon.UserId);
        }

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">userId</param>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }
    }
}
