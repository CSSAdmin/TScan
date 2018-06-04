// -------------------------------------------------------------------------------------------
// <copyright file="F36010MiscImprovementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36010MiscImprovementComp.cs.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 30/06/07                             Created
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
    /// F36010MiscImprovementComp Class file
    /// </summary>
    public static class F36010MiscImprovementComp
    {
        #region Misc Improvement Catalog

        /// <summary>
        /// Get the Misc Improvement data
        /// </summary>
        /// <param name="miscCodeId">miscCodeID</param>
        /// <returns>1</returns>
        public static F36010MiscImprovementCatalog F36010_GetMiscImprovementCatalog(int miscCodeId)
        {
            F36010MiscImprovementCatalog getMiscImprovementData = new F36010MiscImprovementCatalog();
            Hashtable ht = new Hashtable();
            ht.Add("@MICodeID", miscCodeId);
            string[] tableName = new string[] { getMiscImprovementData.GetMICatalog.TableName, getMiscImprovementData.GetMiscCatalogChoice.TableName };
            Utility.LoadDataSet(getMiscImprovementData, "f36010_pcget_MI_Catalog", ht, tableName);
            return getMiscImprovementData;
        }

        /// <summary>
        /// F36010_s the save misc improvement catalog.
        /// </summary>
        /// <param name="miscCodeId">The misc code id.</param>
        /// <param name="miscCatalogItems">The misc catalog items.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="miscCatalogChoiceItems">The misc catalog choice items.</param>
        /// <returns>The save record status.</returns>
        public static int F36010_SaveMiscImprovementCatalog(int miscCodeId, string miscCatalogItems, int userId, string miscCatalogChoiceItems)
        {
            Hashtable ht = new Hashtable();
            if (miscCodeId != -99)
            {
                ht.Add("@MICodeID", miscCodeId);
            }

            ht.Add("@Misc_CatalogItems", miscCatalogItems);
            ht.Add("@UserID", userId);
            ht.Add("@MiscCatalogChoiceItems", miscCatalogChoiceItems);

            return Utility.FetchSPExecuteKeyId("f36010_pcins_MI_Catalog", ht);
        }

        /// <summary>
        /// Delete the Misc Improvement data
        /// </summary>
        /// <param name="miscCodeId">miscCodeID</param>
        /// <param name="userId">userId</param>
        public static void F36010_DeleteMiscImprovementCatalog(int miscCodeId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@MICodeID", miscCodeId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f36010_pcdel_MI_Catalog", ht);
        }

        /// <summary>
        /// Check the Misc Improvement Catalog
        /// </summary>
        /// <param name="miscCodeId">miscCodeID</param>
        /// <param name="miscCode">miscCode</param>
        /// <param name="rollYear">rollYear</param>
        /// <returns>Inget value</returns>
        public static int F36010_CheckMiscImprovementCatalog(int miscCodeId, string miscCode, int rollYear)
        {
            Hashtable ht = new Hashtable();
            if (miscCodeId != -99)
            {
                ht.Add("@MICodeID", miscCodeId);
            }
            
            ht.Add("@MICode", miscCode);
            ht.Add("@RollYear", rollYear);
            return Utility.FetchSPExecuteKeyId("f36010_pcchk_MI_Catalog", ht);
        }
        
        /// <summary>
        /// F36010_s the list depr table.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>miscImprovementData</returns>
        public static F36010MiscImprovementCatalog F36010_ListDeprTable(int rollYear)
        {
            F36010MiscImprovementCatalog miscImprovementData = new F36010MiscImprovementCatalog();
            Hashtable ht = new Hashtable();
            ht.Add("@RollYear", rollYear);
            Utility.LoadDataSet(miscImprovementData.ListDeprTable, "f36010_pclst_DeprTable", ht);
            return miscImprovementData;
        }

        #endregion Misc Improvement Catalog
    }
}
