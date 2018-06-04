// -------------------------------------------------------------------------------------------
// <copyright file="F49912LegalComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F49912LegalComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
//                  Kuppusamy.B              Created
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;
    using System.Data;

    /// <summary>
    /// F49912LegalComp
    /// </summary>
    public class F49912LegalComp
    {
        /// <summary>
        /// F49912_s the list legal field.
        /// </summary>
        /// <param name="InstID">The inst ID.</param>
        /// <returns></returns>
        public static F49912LegalData F49912_ListLegalField(int InstID)            
        {
            Hashtable ht = new Hashtable();
            F49912LegalData legalData = new F49912LegalData();
            ht.Add("@InstID", InstID);

            string[] tableNames = new string[] 
            { 
                legalData.SubDivisionTable.TableName,
                legalData.NEDetailsTable.TableName,
                legalData.NWDetailsTable.TableName,
                legalData.SWDetailsTable.TableName,
                legalData.SEDetailsTable.TableName,
                legalData.CommentsDetailsTable.TableName                
            };

            Utility.LoadDataSet(legalData, "f49912_pcget_LegalDetails", ht, tableNames);
            return legalData;


        }

        #region Insert

        /// <summary>
        /// F49912_s the insert legal field details.
        /// </summary>
        /// <param name="instid">The instid.</param>
        /// <param name="legalItems">The legal items.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isCopy">The is copy.</param>
        /// <returns>The saved record status.</returns>
        public static int F49912_InsertLegalFieldDetails(int instid, string legalItems, int userId, int isCopy)
        {
            Hashtable ht = new Hashtable();
            if (instid == 0)
            {
                ht.Add("@InstID", DBNull.Value);
            }
            else
            {
                ht.Add("@InstID", instid);
            }

            ht.Add("@LegalItems", legalItems);
            ht.Add("@UserID", userId);
            ht.Add("@IsCopy", isCopy);

            int legalDetailsID;
            legalDetailsID = Utility.FetchSPExecuteKeyId("f49912_pcins_LegalValues", ht);
            return legalDetailsID;
        }


        /// <summary>
        /// F49912_s the list sub division combo.
        /// </summary>
        /// <returns></returns>
        public static F49912LegalData F49912_ListSubDivisionCombo()
        {
            Hashtable ht = new Hashtable();
            F49912LegalData legalData = new F49912LegalData();            
            string[] tableNames = new string[] 
            { 
                legalData.F49912SubDivisionComboTable.TableName              
            };

            Utility.LoadDataSet(legalData, "f49912_pclst_LegalSubDivision", ht, tableNames);
            return legalData;
        }

        #endregion Insert
    }
}
