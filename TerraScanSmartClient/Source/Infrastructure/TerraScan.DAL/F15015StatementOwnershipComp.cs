// -------------------------------------------------------------------------------------------
// <copyright file="F15015StatementOwnershipComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F15015StatementOwnershipComp.cs methods
// </summary>
// Release history
// ********************************************************************************************
// Date               Author            Description
// ----------        ---------       -------------------------------------------------------
// 09/04/07         M.Vijayakumar       Created
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    /// <summary>
    /// F15015StatementOwnershipComp Class file
    /// </summary>
    public static class F15015StatementOwnershipComp
    {
        #region F15015 Statement Ownership

        #region List Statement Ownership

        /// <summary>
        /// To List Statement Ownership Details.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <returns>Typed Dataset Containing the Statement Ownership Details</returns>
        public static F15015StatementOwnershipData F15015_ListStatementOwnership(int statementId)
        {
            F15015StatementOwnershipData statementOwnershipData = new F15015StatementOwnershipData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            string[] optionalParameter = new string[] { statementOwnershipData.ListStatementOwnershipDataTable.TableName, statementOwnershipData.ListOwnerValidID.TableName };
            Utility.LoadDataSet(statementOwnershipData, "f15015_pclst_StatementOwnership", ht, optionalParameter);
            return statementOwnershipData;
        }

        #endregion List Statement Ownership
        #region List All Owner Details

        /// <summary>
        /// To List All Owners Details
        /// </summary>
        /// <param name="firstName">The First Name.</param>
        /// <param name="lastName">The Last Name.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="city">The city.</param>
        /// <returns>Typed Dataset Containg the All Owners Details</returns>
        public static F15015StatementOwnershipData F15015_ListALLOwnerDetails(string firstName, string lastName, string address1, string address2, string city)
        {
            F15015StatementOwnershipData statementOwnershipData = new F15015StatementOwnershipData();
            Hashtable ht = new Hashtable();
            ht.Add("@FirstName", firstName);
            ht.Add("@LastName", lastName);
            ht.Add("@Address1", address1);
            ht.Add("@Address2", address2);
            ht.Add("@City", city);
            Utility.LoadDataSet(statementOwnershipData.ListAllOwnersDetailDataTable, "f27006_pclst_OwnerSearch", ht);
            return statementOwnershipData;
        }

        #endregion List All Owner Details

        #region F15015 list MOwnerType Selection

        /// <summary>
        /// Lists the type of the M owner.
        /// </summary>
        /// <returns>MOwner List.</returns>
        public static F15015StatementOwnershipData F15015_ListMOwnerType()
        {
            F15015StatementOwnershipData getMOwnerTypeData = new F15015StatementOwnershipData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(getMOwnerTypeData.listMOwnerTypeDataTable, "f27006_pclst_MOwnerType", ht);
            return getMOwnerTypeData;
        }

        #endregion 

        #region Save Statement Ownership

        /// <summary>
        /// F15015_s the save statement ownership.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="statementOwner">The statement owner.</param>
        /// <param name="userId">The user id.</param>
        public static void F15015_SaveStatementOwnership(int statementId, string statementOwner, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            ht.Add("@StatementOwner", statementOwner);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f15015_pcins_StatementOwnership", ht);
        }

        #endregion Save Statement Ownership

        #endregion F15015 Statement Ownership
    }
}
