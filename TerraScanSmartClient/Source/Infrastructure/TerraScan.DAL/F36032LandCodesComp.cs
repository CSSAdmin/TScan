// -------------------------------------------------------------------------------------------
// <copyright file="F36032LandCodesComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36032LandCodesComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 14/09/07         Shiva             Created
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
    /// F36032LandCodesComp Class File.
    /// </summary>
    public static class F36032LandCodesComp
    {
        #region F36032LandCodesComp

        #region F36032_ListLandItems

        /// <summary>
        /// F36032_s the list land items.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>The landCodeDataSet.</returns>
        public static F36032LandCodesData F36032_ListLandItems(int? rollYear)
        {
            F36032LandCodesData landCodesDataSet = new F36032LandCodesData();
            Hashtable ht = new Hashtable();
            ht.Add("@RollYear", rollYear);
            string[] tableName = new string[] { landCodesDataSet.GetConfigRollYear.TableName, landCodesDataSet.ListLandType1.TableName, landCodesDataSet.ListLandType2.TableName, landCodesDataSet.ListLandType3.TableName, landCodesDataSet.ListLandCode.TableName };
            Utility.LoadDataSet(landCodesDataSet, "f36032_pclst_LoadingLandItems", ht, tableName);
            return landCodesDataSet;
        }

        #endregion F36032_ListLandItems

        #region F36032_ListLandCodeDetails

        /// <summary>
        /// F36032_s the list land code details.
        /// </summary>
        /// <returns>the landCodesDataSet</returns>
        public static F36032LandCodesData F36032_ListLandCodeDetails()
        {
            F36032LandCodesData landCodesDataSet = new F36032LandCodesData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(landCodesDataSet.ListLandCodeDetails, "f36032_pclst_LandCodeDetails", ht);
            return landCodesDataSet;
        }

        #endregion F36032_ListLandCodeDetails       

        #region F36032_DeleteLandCode

        /// <summary>
        /// F36032_s the delete land code.
        /// </summary>
        /// <param name="landCodeId">The land code ID.</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer value</returns>
        public static int F36032_DeleteLandCode(int landCodeId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@LandCodeID", landCodeId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f36032_pcdel_LandCode", ht);
        }

        #endregion F36032_DeleteLandCode

        #region F36032_SaveLandCodeDetails

        /// <summary>
        /// To save the land code deatils
        /// </summary>
        /// <param name="landCodeId">landCodeId</param>
        /// <param name="landItems">landItems</param>
        /// <param name="userId">userId</param>
        /// <returns>integer value containing the save land Code Id</returns>
        public static int F36032_SaveLandCodeDetails(int? landCodeId, string landItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@LandCodeID", landCodeId);
            ht.Add("@LandItems", landItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f36032_pcins_LandCode", ht);
        }

        #endregion F36032_SaveLandCodeDetails

        #endregion F36032LandCodesComp
    }
}
