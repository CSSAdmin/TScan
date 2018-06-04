// -------------------------------------------------------------------------------------------
// <copyright file="F15004AgencyManagmentComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access General Ledger Configuration related information</summary>
// Release history
// // Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 28 Dec 06		Krishna	Abburi     Created
// -------------------------------------------------------------------------------------------
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.DataLayer;
    using System.Data;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// GF15004AgencyManagmentComp class file
    /// </summary>
    public static class F15004AgencyManagmentComp
    {
        #region Get AgencyDetails

        /// <summary>
        /// F15004_s the get agency details.
        /// </summary>
        /// <param name="agencyId">The agency ID.</param>
        /// <returns>agencyManagementData</returns>
        public static F15004AgencyManagementData F15004_GetAgencyDetails(int agencyId)
        {
            F15004AgencyManagementData agencyManagementData = new F15004AgencyManagementData();
            string[] tableName = new string[] { agencyManagementData.GetAgencyDetail.TableName, agencyManagementData.ListDisbursementHistory.TableName };
            Hashtable ht = new Hashtable();
            if (agencyId != -1)
            {
                ht.Add("@AgencyID", agencyId);
            }
            else
            {
                ht.Add("@AgencyID", DBNull.Value);
            }

            Utility.LoadDataSet(agencyManagementData, "f15004_pcget_AgencyDetail", ht, tableName);
            return agencyManagementData;
        }

        #endregion

        #region Check for Agency Dupilcate Record

        /// <summary>
        /// F15004_s the check duplicate agency.
        /// </summary>
        /// <param name="agencyId">The agency ID.</param>
        /// <param name="agencyName">Name of the agency.</param>
        /// <returns>errorId</returns>
        public static int F15004_CheckDuplicateAgency(int agencyId, string agencyName)
        {
            int errorId;
            Hashtable ht = new Hashtable();
            ht.Add("@AgencyID", agencyId);
            ht.Add("@AgencyName", agencyName);
            errorId = Utility.FetchSPExecuteKeyId("f15004_pcchk_Agency", ht);
            return errorId;
        }

        #endregion

        #region Create and  Edit the Agency Details

        /// <summary>
        /// F15004_s the create or edit agency details.
        /// </summary>
        /// <param name="agencyId">The agency ID.</param>
        /// <param name="agencyItems">The agency items.</param>
        /// <param name="userId">userId</param>
        /// <returns>primaryKeyID</returns>
        public static int F15004_CreateOrEditAgencyDetails(int agencyId, string agencyItems, int userId)
        {
            Hashtable ht = new Hashtable();
            if (agencyId != -1)
            {
                ht.Add("@AgencyID", agencyId);
            }
            else
            {
                ht.Add("@AgencyID", DBNull.Value);
            }

            ht.Add("@AgencyItems ", agencyItems);
            ht.Add("@UserID", userId);

            int primaryKeyID;

            primaryKeyID = Utility.FetchSPExecuteKeyId("f15004_pcins_Agency", ht);
            return primaryKeyID;
        }

        #endregion
    }
}
