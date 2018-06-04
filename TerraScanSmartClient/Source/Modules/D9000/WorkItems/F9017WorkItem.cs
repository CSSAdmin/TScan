//--------------------------------------------------------------------------------------------
// <copyright file="F9017WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the GetSqlQueryResult.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 13 Sep 06        VINOTHBABU         Created
//*********************************************************************************/

namespace D9000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.Helper;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F9016WorkItem class
    /// </summary>
    public class F9017WorkItem : WorkItem
    {
        #region GetFormDetails

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

        #endregion

        #region ListUserNames

        /// <summary>
        /// List UserNames
        /// </summary>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.ListUsersDataTable ListUserNames()
        {
            return WSHelper.ListUserNames().ListUsers;
        }

        #endregion

        #region Get SliceForm Details

        /// <summary>
        /// List UserNames
        /// </summary>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.GetFormManagementDetailsDataTable F9002_GetFormManagementDetails(int form, int userId)
        {
            return WSHelper.F9002_GetFormManagementDetails(form, userId).GetFormManagementDetails;
        }

        #endregion

        #region OnRunStarted

        /// <summary>
        /// Fires the <see cref="RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="Run"/>
        /// method is called on the <see cref="WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }
        
        #endregion

        #region OnActivated

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
