// -------------------------------------------------------------------------------------------
// <copyright file="F82006ContractManagementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F82006 Contract Management</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 26/11/2008       Sadha Shivudu M       Added
// 
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F82006ContractManagementComp class file.
    /// </summary>
    public static class F82006ContractManagementComp
    {
        #region F82006 Contractor Management

        #region Get Contractor and Employee List

        /// <summary>
        /// F82006_s the get contractor list.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <returns>contractManagementData</returns>
        public static F82006ContractManagementData F82006_GetContractorList(int contractorId)
        {
            F82006ContractManagementData contractManagementData = new F82006ContractManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@ContractorID", contractorId);
            string[] tableName = new string[] { contractManagementData.GetContractorList.TableName, contractManagementData.ListContractorEmployee.TableName };
            Utility.LoadDataSet(contractManagementData, "f82006_pcget_ContractorList", ht, tableName);
            return contractManagementData;
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
        public static int F82006_SaveContractorList(int? contractorId, string contractorXml, string contractorEmployeeXml, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ContractorID", contractorId);
            ht.Add("@ContractorXML", contractorXml);
            ht.Add("@ContractorEmployeeXML", contractorEmployeeXml);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f82006_pcins_ContractorList", ht);
        }

        #endregion Save Contractor and Employee List

        #region Delete Contractor and Employee List

        /// <summary>
        /// F82006_s the delete contractor list.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <param name="userId">The user id.</param>
        public static void F82006_DeleteContractorList(int contractorId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ContractorID", contractorId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f82006_pcdel_ContractorList", ht);
        }

        /// <summary>
        /// F82006_s the delete contractor employee.
        /// </summary>
        /// <param name="contractorId">The contractor id.</param>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="userId">The user id.</param>
        public static void F82006_DeleteContractorEmployee(int contractorId, int employeeId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ContractorID", contractorId);
            ht.Add("@EmployeeID", employeeId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f82006_pcdel_ContractorEmployee", ht);
        }

        #endregion Delete Contractor and Employee List

        #endregion F82006 Contractor Management
    }
}
