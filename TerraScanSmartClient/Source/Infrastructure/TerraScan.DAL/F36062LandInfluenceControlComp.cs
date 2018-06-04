// -------------------------------------------------------------------------------------------
// <copyright file="F3606LandInfluenceControlComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F36062LandInfluenceControlComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
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

    public static class F36062LandInfluenceControlComp
    {
        #region F36062lANDINFLUENCE CONTROL

        /// <summary>
        /// Used to Get the landInfluence Control Items Details.
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <returns>
        /// Typed Dataset containing the Land Control Items Details
        /// </returns>
        public static F36062LandInfluenceData F36062_LandInfluenceItems(int nbhdId)
        {
            F36062LandInfluenceData LandControlData = new F36062LandInfluenceData();
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDID", nbhdId);
            string[] tableName = new string[] { LandControlData.ListLandInfluenceTable.TableName, LandControlData.GetLandDescriptionTitle.TableName, LandControlData.TypeComboboxTable.TableName };
            Utility.LoadDataSet(LandControlData, "f36062_pclst_InfluenceType", ht, tableName);
            return LandControlData;
        }

        #region F36062_SaveInfluenceControl

        /// <summary>
        /// Used to save the Land Control Items Details .
        /// </summary>
        /// <param name="nbhdId">The NBHD id.</param>
        /// <param name="InfluenceItems">The Land control items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>saved key id</returns>
        public static int F36062_SaveInfluenceControl(int? nbhdId, string InfluenceItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDID", nbhdId);
            ht.Add("@InfluenceItems", InfluenceItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f36062_pcins_InfluenceType", ht);
        }

        #endregion F36062_SaveInfluenceControl




        #endregion F36062lANDINFLUENCE CONTROL
    }
}
