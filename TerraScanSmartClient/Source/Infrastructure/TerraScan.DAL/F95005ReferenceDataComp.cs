// -------------------------------------------------------------------------------------------
// <copyright file="F95005ReferenceDataComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F95005ReferenceDataComp.cs </summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 14/06/2007       VijayaKumar.M       Added
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
    /// F95005ReferenceDataComp Class file
    /// </summary>
    public static class F95005ReferenceDataComp
    {
        #region F95005 Reference Data

        #region List Refereence Data

        /// <summary>
        /// To List the Reference Data Details
        /// </summary>
        /// <param name="masterFormNo">masterFormNo</param>
        /// <returns>Typed DataSet containg the Reference Data Details</returns>
        public static DataSet F95005_ListReferenceData(int masterFormNo)
        { 
            DataSet referenceData = new DataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@FormNumber", masterFormNo);
            Utility.LoadDataSet(referenceData, "f95005_pclst_FS_ReferenceData", ht);
            return referenceData;
        }

        #endregion List Refereence Data

        #region Save Refereence Data

        /// <summary>
        /// To Save the Reference Data Details
        /// </summary>
        /// <param name="referenceData">Xml String containing the Reference Data Details</param>
        /// <param name="deletedData">Xml string containing the deleted data in Reference Data Details.</param>
        /// <param name="tableName">Tabel Name of the Reference Data</param>
        /// <param name="keyColumn">Key Column Name of the Reference Data Table</param>
        /// <param name="userId">userId</param>
        /// <returns>
        /// Integer Value containing Whther save is performed or Not
        /// if Saved return = 0
        /// else Unsaved return = -1
        /// </returns>
        public static int F95005_SaveReferenceData(string referenceData, string deletedData, string tableName, string keyColumn, int userId)
        {
            Hashtable ht = new Hashtable();

            if (!string.IsNullOrEmpty(referenceData))
            {
                ht.Add("@ReferenceData", referenceData);
            }

            ////when deleted data in Reference Data Details exists
            if (!string.IsNullOrEmpty(deletedData))
            {
                ht.Add("@DeletedData", deletedData);
            }

            ht.Add("@TableName", tableName);
            ht.Add("@KeyColumn", keyColumn);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f95005_pcins_FS_ReferenceData", ht);
        }

        #endregion Refereence Data

        #endregion F95005 Reference Data
    }
}
