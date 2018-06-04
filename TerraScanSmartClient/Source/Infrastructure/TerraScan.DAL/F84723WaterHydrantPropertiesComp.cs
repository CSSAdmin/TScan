// -------------------------------------------------------------------------------------------
// <copyright file="F84723WaterHydrantPropertiesComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access GDoc methods to Load Common ComboBoxs </summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 19/12/2006       VijayaKumar.M       Added
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
    /// F84723WaterHydrantPropertiesComp Class file
    /// </summary>
    public static class F84723WaterHydrantPropertiesComp
    {
        #region F84723 Water Hydrant Properties

        #region Get Water Hydrant Properties

        /// <summary>
        /// To Load Water Hydrant Properties
        /// </summary>
        /// <param name="hydrantId">The hydrantId.</param>
        /// <returns>Typed DataSet Containing the Water Hydrant Properties Details.</returns>
        public static F84723WaterHydrantPropertiesData F84723_GetWaterHydrantProperties(int hydrantId)
        {
            F84723WaterHydrantPropertiesData waterHydrantPropertiesData = new F84723WaterHydrantPropertiesData();
            Hashtable ht = new Hashtable();
            ht.Add("@HydrantID", hydrantId);
            Utility.LoadDataSet(waterHydrantPropertiesData.GetWaterHydrantPropertiesDataTable, "f84723_pcget_FS_WaterHydrantProperty", ht);
            return waterHydrantPropertiesData;
        }

        #endregion Get Water Hydrant Properties

        #region Check Main Valve ID

        /// <summary>
        /// To Check the Main Valve ID
        /// </summary>
        /// <param name="mainValveId">The main valve id.</param>
        /// <returns>
        /// The Integer Value containing whether Main Valve Id exists are not
        /// </returns>
        public static int F84723_CheckMainValveId(int mainValveId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@MainValveID", mainValveId);            
            return Utility.FetchSPExecuteKeyId("f84723_pcchk_FS_WaterHydrantProperty", ht);
        }

        #endregion Check Main Valve ID

        #region Save Water Hydrant Properties

        /// <summary>
        /// To Save Water Hydrant Properties.
        /// </summary>
        /// <param name="hydrantId">The hydrant id.</param>
        /// <param name="waterHydrantPropties">The XML String containing the Water Hydrant Properties Details.</param>
        ///<param name="userId">userId</param>
        /// <returns>The integer valu containing the hydrantId</returns>
        public static int F84723_SaveWaterHydrantProperties(int hydrantId, string waterHydrantPropties, int userId)
        {            
            Hashtable ht = new Hashtable();
            ht.Add("@HydrantID", hydrantId);
            ht.Add("@WaterHydrantProp", waterHydrantPropties);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f84723_pcins_FS_WaterHydrantProperty", ht);
        }

        #endregion Save Water Hydrant Properties

        #region Delete Water Hydrant Properties

        /// <summary>
        /// To Delete Water Hydrant Properties.
        /// </summary>
        /// <param name="hydrantId">hydrantId</param>
        ///<param name="userId">userId</param>
        public static void F84723_DeleteWaterHydrantProperties(int hydrantId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@HydrantID", hydrantId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f84723_pcdel_FS_WaterHydrantProperty", ht);
        }

        #endregion Delete Water Hydrant Properties      

        #endregion F84723 Water Hydrant Properties
    }
}
