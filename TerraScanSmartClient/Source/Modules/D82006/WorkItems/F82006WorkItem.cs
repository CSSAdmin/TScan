//--------------------------------------------------------------------------------------------
// <copyright file="F82006WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F82006WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 25 Nov 06        Sadha Shivudu M    Created
//*********************************************************************************/
namespace D82006
{
    #region namespace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    #endregion namespace

    /// <summary>
    /// F82006 WorkItem
    /// </summary>
    public class F82006WorkItem : WorkItem
    {
        #region CRUD Methods.

        #region Get Contractor and Employee List

        /// <summary>
        /// F82006_s the get contractor list.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <returns>contractManagementData</returns>
        public F82006ContractManagementData F82006_GetContractorList(int contractorId)
        {
               return WSHelper.F82006_GetContractorList(contractorId);
        }

        #endregion Get Contractor and Employee List

        #region Save Contractor and Employee List

        /// <summary>
        /// F82006_s the save contractor list.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <param name="contractorXml">The contractor XML.</param>
        /// <param name="contractorEmployeeXml">The contractor employee XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>errorId</returns>
        public int F82006_SaveContractorList(int? contractorId, string contractorXml, string contractorEmployeeXml, int userId)
        {
            return WSHelper.F82006_SaveContractorList(contractorId, contractorXml, contractorEmployeeXml, userId);
        }

        #endregion Save Contractor and Employee List

        #region Delete Contractor and Employee List

        /// <summary>
        /// F82006_s the delete contractor list.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <param name="userId">The user id.</param>
        public void F82006_DeleteContractorList(int contractorId, int userId)
        {
            WSHelper.F82006_DeleteContractorList(contractorId, userId);
        }

        /// <summary>
        /// F82006_s the delete contractor employee.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="userId">The user id.</param>
        public void F82006_DeleteContractorEmployee(int contractorId, int employeeId, int userId)
        {
            WSHelper.F82006_DeleteContractorEmployee(contractorId, employeeId, userId);
        }

        #endregion Delete Contractor and Employee List

        #endregion CRUD Methods.

        #region Base Methods

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

        #endregion Base Methods.
    }
}
