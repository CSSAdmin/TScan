//----------------------------------------------------------------------------------
// <copyright file="F8904WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F8904WorkItem.
// </summary>
//----------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			        Author		       Description
// ----------		    ---------		   -----------------------------------------
// 18 Oct 06            VINOTH             Created
//*********************************************************************************/

namespace D8900
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    #endregion

    /// <summary>
    /// F8904WorkItem Class
    /// </summary>
    public class F8904WorkItem : WorkItem
    {
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
        /// Gets the Event Grid Details
        /// </summary>
        /// <param name="workId">workId</param>
        /// <returns>GetEventGridDataTable Datatable</returns>
        public F8904EventGridData.GetEventGridDataTable GetEventGridDetails(int workId)
        {
            return WSHelper.F8904_GetGridDetails(workId).GetEventGrid;
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
