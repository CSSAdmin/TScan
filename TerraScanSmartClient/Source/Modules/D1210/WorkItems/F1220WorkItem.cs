//--------------------------------------------------------------------------------------------
// <copyright file="F1220WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1213 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 09-10-2006       Shiva              Created
//*********************************************************************************/

namespace D1210
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F1220 WorkItem
    /// </summary>
    public class F1220WorkItem : WorkItem
    {
        #region Public Methods

        #region List AccontNames

        /// <summary>
        /// F1220_s the account names.
        /// </summary>
        /// <returns>the datatable contains the Account Names</returns>
        public F1220AccountRegisterData.ListAccountNamesDataTable F1220_AccountNames()
        {
            return WSHelper.F1220_AccountNames();
        }

        #endregion

        #region Get ReconciledDetails

        /// <summary>
        /// F1220_s the get reconciled details.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <returns>datatable holds the Reconciled Details</returns>
        public F1220AccountRegisterData.ReconciledDetailsDataTable F1220_GetReconciledDetails(int registerId)
        {
            return WSHelper.F1220_GetReconciledDetails(registerId);
        }

        #endregion

        #region List AccountRegister

        /// <summary>
        /// F1220_s the list account register.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="beginningDate">The beginning date.</param>
        /// <returns>datatable contains the Account Register Details</returns>
        public F1220AccountRegisterData.ListAccountRegisterDataTable F1220_ListAccountRegister(int registerId, DateTime beginningDate)
        {
            return WSHelper.F1220_ListAccountRegister(registerId, beginningDate);
        }

        #endregion

        #region GetAccountRegisterDetails

        /// <summary>
        /// F1220_s the get account register details.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="beginningDate">The beginning date.</param>
        /// <returns>accountRegister DataSet</returns>
        public F1220AccountRegisterData F1220_GetAccountRegisterDetails(int registerId, DateTime beginningDate)
        {
            return WSHelper.F1220_GetAccountRegisterDetails(registerId, beginningDate);
        }

        #endregion

        #endregion

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
