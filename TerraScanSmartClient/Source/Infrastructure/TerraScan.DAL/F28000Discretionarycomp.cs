// -------------------------------------------------------------------------------------------
// <copyright file="F28000Discretionarycomp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access attachment related information</summary>
// Release history
// VERSION	DESCRIPTION
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
    
    public static class F28000Discretionarycomp
    {
        #region Get Discretionary Details

        /// <summary>
        /// Discretionary Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>Discretionary Details</returns>
        public static F28000DiscretionaryData F28000_GetDiscretionaryDetails(int eventId)
        {
            F28000DiscretionaryData discretionaryData = new F28000DiscretionaryData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            string[] tableName = new string[] { discretionaryData.Discretionary.TableName, discretionaryData.DiscretionaryDetails.TableName, discretionaryData.StateList.TableName };
            Utility.LoadDataSet(discretionaryData, "f23000_pcget_DiscretionaryDetails", ht, tableName);
            return discretionaryData;
        }

        #endregion Get Discretionary Details

        #region Get Class 
        
        /// <summary>
        /// Class Details
        /// </summary>
        /// <param name="stateId">State ID</param>
        /// <param name="eventId">Event ID</param>
        /// <returns>Class Details</returns>
        public static F28000DiscretionaryData F28000_GetClass(int stateId, int eventId)
        {
            F28000DiscretionaryData discretionaryData = new F28000DiscretionaryData();
            Hashtable ht = new Hashtable();
            ht.Add("@State", stateId);
            ht.Add("@EventID", eventId);
            Utility.LoadDataSet(discretionaryData.ClassDetail, "f23000_pcget_Class", ht);
            return discretionaryData;
        }

        #endregion Get Class

        #region Get Exemption Amount

        /// <summary>
        /// Exemption Amount
        /// </summary>
        /// <param name="rollYear">roll Year</param>
        /// <param name="exemptionYear">Exemption Year</param>
        /// <param name="subjectAmount">Subject Amount</param>
        /// <returns>Exemption Amount</returns>
        public static F28000DiscretionaryData F28000_GetExemptionAmount(int rollYear, int exemptionYear, decimal subjectAmount)
        {
            F28000DiscretionaryData discretionaryData = new F28000DiscretionaryData();
            Hashtable ht = new Hashtable();
            ht.Add("@Rollyear", rollYear);
            ht.Add("@ExemptionYear", exemptionYear);
            ht.Add("@SubjectAmount", subjectAmount);
            Utility.LoadDataSet(discretionaryData.ExemptionAmount, "f23000_pcget_ExemptionAmount", ht);
            return discretionaryData;
        }

        #endregion Get Exemption Amount

        #region Save Discretionary Details

        /// <summary>
        /// Save Discretionary Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="discretionaryId">discretionary ID</param>
        /// <param name="discretionaryItems">XML string</param>
        /// <param name="userId">User ID</param>
        /// <returns>Confirmation Value</returns>
        public static int F28000_SaveDiscretionaryDetail(int eventId, int? discretionaryId, string discretionaryItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@DiscretionaryID", discretionaryId);
            ht.Add("@DiscretionaryItems", discretionaryItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f23000_pcins_Discretionarydetails", ht);
        }

        #endregion Save Discretionary Details

        #region Delete Discretionary Details
        
        /// <summary>
        /// Delete Discretionary Details
        /// </summary>
        /// <param name="discretionaryId">discretionary ID</param>
        /// <param name="discretionaryItems">XML String</param>
        /// <param name="userId">USer ID</param>
        public static void F28000_DeletediscretionaryDetails(int? discretionaryId, string discretionaryItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@DiscretionaryID", discretionaryId);
            ht.Add("@DiscretionaryItems", discretionaryItems);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f23000_pcdel_discretionaryDetails", ht);
        }

        #endregion Delete Discretionary Details
    }
}
