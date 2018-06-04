// -------------------------------------------------------------------------------------------
// <copyright file="F1530CashAcctMgmtComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Institution, cash account and Institution Contact</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 13 Nov 06		Ranjani	            Created
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
    /// Main class for Check Detail Component
    /// </summary>
    public static class F1530CashAcctMgmtComp
    {
        #region Institution

        #region Get Institution

        /// <summary>
        /// Gets the institution list, institution detail, cash account list and institution contact list
        /// </summary>
        /// <param name="institutionId">The institution id.</param>
        /// <returns>F1530CashAccountManagementData with institution Detail</returns>
        public static F1530CashAccountManagementData F1530_GetInstitutionDetail(int institutionId)
        {
            F1530CashAccountManagementData cashAccountManagement = new F1530CashAccountManagementData();
            Hashtable ht = new Hashtable();
            ////check for valid institutionid - if institution is -999, send null as parameter else institutionid for retrieving the record
            if (institutionId != -999)
            {
                ht.Add("@InstitutionID", institutionId);
            }

            Utility.LoadDataSet(cashAccountManagement, "f1530_pcget_CashAccountDetail", ht, new string[] { cashAccountManagement.ListInstitution.TableName, cashAccountManagement.GetInstitution.TableName, cashAccountManagement.ListCashAccount.TableName, cashAccountManagement.ListInstitutionContact.TableName });
            return cashAccountManagement;
        }

        /// <summary>
        /// saves the institution
        /// </summary>
        /// <param name="institutionId">The institution id.</param>
        /// <param name="institutionElements">The institution elements.</param>
        /// <param name="userId">userId</param>
        /// <returns>saved institution id</returns>
        public static int F1530_SaveInstitution(int institutionId, string institutionElements, int userId)
        {
            Hashtable ht = new Hashtable();
            ////check for valid institutionid - if institution is -999, insert record else update
            if (institutionId != -999)
            {
                ht.Add("@InstitutionID", institutionId);
            }

            ht.Add("@InstitutionElements", institutionElements);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f1530_pcins_CashAccountDetail", ht);           
        }

        #endregion

        #endregion

        #region 1531 Cash Account

        #region Get Cash Account

        /// <summary>
        /// Gets the cash account detail
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <returns>
        /// F1530CashAccountManagementData with cash account Detail
        /// </returns>
        public static F1530CashAccountManagementData F1531_GetCashAccountDetail(int registerId)
        {
            F1530CashAccountManagementData cashAccountManagement = new F1530CashAccountManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@RegisterID", registerId);
            Utility.LoadDataSet(cashAccountManagement.GetCashAccount, "f1531_pcget_CashAccountDetail", ht);
            return cashAccountManagement;
        }

        /// <summary>
        /// saves cash account.
        /// </summary>
        /// <param name="registerId">The register id.</param>
        /// <param name="registerItems">The register items.</param>
        /// <param name="userId">userId</param>
        /// <returns>subfund validated result,-1- validation failed else registerId</returns>
        public static int F1531_SaveCashAccount(int registerId, string registerItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@RegisterItems", registerItems);
            ////check for valid registerId - if registerId is -999, insert record else update
            if (registerId != -999)
            {
                ht.Add("@RegisterID", registerId);
            }

            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f1531_pcins_CashAccountDetail", ht);            
        }

        #endregion

        #endregion

        #region 1532 Institution Contact

        #region Get Institution Contact

        /// <summary>
        /// Gets the institution contact detail
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <returns>
        /// F1530CashAccountManagementData with institution contact Detail
        /// </returns>
        public static F1530CashAccountManagementData F1532_GetInstitutionContactDetail(int contactId)
        {
            F1530CashAccountManagementData cashAccountManagement = new F1530CashAccountManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@ContactID", contactId);
            Utility.LoadDataSet(cashAccountManagement.GetInstitutionContact, "f1532_pcget_InstitutionContactDetail", ht);
            return cashAccountManagement;
        }

        /// <summary>
        /// saves the Institution Contact.
        /// </summary>
        /// <param name="contactId">The contact id.</param>
        /// <param name="acctEmelemts">The acct emelemts.</param>
        /// <param name="userId">userId</param>
        /// <returns>saved contact id</returns>
        public static int F1532_SaveInstitutionContact(int contactId, string acctEmelemts, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@AcctEmelemts", acctEmelemts);
            ////check for valid registerId - if registerId is -999, insert record else update
            if (contactId != -999)
            {
                ht.Add("@ContactID", contactId);
            }

            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f1532_pcins_InstitutionContactDetail", ht);
        }

        #endregion

        #endregion       
    }
}
