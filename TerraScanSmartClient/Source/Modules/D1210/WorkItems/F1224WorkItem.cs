//--------------------------------------------------------------------------------------------
// <copyright file="F1224WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Methods for the F1224 WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 19-10-2006       Krishna Abburi      Created
//*********************************************************************************/

namespace D1210
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    public class F1224WorkItem : WorkItem
    {
        #region Public Methods

        #region List AccontNames

        /// <summary>
        /// F1224 the account names.
        /// </summary>
        /// <returns>the datatable contains the Account Names</returns>
        public F1224CheckPrintQueueData.ListAccountNamesDataTable F1224_AccountNames()
        {
            return WSHelper.F1224_AccountNames();
        }

        #endregion

        /// <summary>
        /// F1224_s the get check number.
        /// </summary>
        /// <param name="registerID">The register ID.</param>
        /// <returns>Check Number </returns>
        public F1224CheckPrintQueueData.CheckNumberTableDataTable F1224_GetCheckNumber(int registerId)
        {
            return WSHelper.F1224_GetCheckNumber(registerId);
        }

        /// <summary>
        /// F1224_s the list un printed checks.
        /// </summary>
        /// <param name="registerID">The register ID.</param>
        /// <returns>UnPrinted checks</returns>
        public F1224CheckPrintQueueData.ListUnPrintedChecksTableDataTable F1224_ListunPrintedChecks(int registerId)
        {
            return WSHelper.F1224_ListUnPrintedChecks(registerId);
        }

        /// <summary>
        /// F1224_s the create checks.
        /// </summary>
        /// <param name="registerID">The register ID.</param>
        /// <param name="userID">The user ID.</param>
        /// <param name="startingCheckNumber">The starting check number.</param>
        /// <param name="checkItems">The check items.</param>
        /// <returns>printed Check Numbers</returns>
        public F1224CheckPrintQueueData.ListCreateChecksTableDataTable F1224_CreateChecks(int registerId, int userId, Int64 startingCheckNumber, string checkItems)
        {
            return WSHelper.F1224_CreateChecks(registerId, userId, startingCheckNumber, checkItems);
        }

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
