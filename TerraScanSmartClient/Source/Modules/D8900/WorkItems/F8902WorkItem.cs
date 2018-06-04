//--------------------------------------------------------------------------------------------
// <copyright file="F8902WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8902WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16 Oct 06        VINOTH              Created
//*********************************************************************************/

namespace D8900
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F8902WorkItem Class
    /// </summary>
    public class F8902WorkItem : WorkItem
    {
        /// <summary>
        /// Get Header Information
        /// </summary>
        /// <param name="workId">workId</param>
        /// <returns>GetWorkOrderHeaderDataTable</returns>
        public F8902HeaderData.GetWorkOrderHeaderDataTable GetHeader(int workId)
        {
            return WSHelper.F8902_GetHeader(workId).GetWorkOrderHeader;
        }

        /// <summary>
        /// Save Header Information
        /// </summary>
        /// <param name="headerInformation">headerInformation</param>
        public void SaveHeader(string headerInformation, int userID)
        {
            WSHelper.F8902_SaveHeader(headerInformation,userID);
        }

        /// <summary>
        /// Delete Header Information
        /// </summary>
        /// <param name="workId">workId</param>
        public void DeleteHeader(int workId,int userID)
        {
            WSHelper.F8902_DeleteHeader(workId,userID);
        }

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">UserName</param>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
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
