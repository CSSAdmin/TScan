// -------------------------------------------------------------------------------------------
// <copyright file="F84721WaterValvePropertiesComp.cs" company="Congruent">
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
    /// F84721WaterValvePropertiesComp Class File
    /// </summary>
    public static class F84721WaterValvePropertiesComp
    {
        #region F84721 Water Valve Properties 

        #region Get Water Valve Properties

        /// <summary>
        ///  To Load F84721 Water valve properties.
        /// </summary>
        /// <param name="valveId">The valve ID.</param>
        /// <returns>Typed DataSet Containing All the Water valve properties Details</returns>
        public static F84721WaterValvePropertiesData F84721_GetWaterValveProperties(int valveId)
        {
            F84721WaterValvePropertiesData waterValvePropertiesData = new F84721WaterValvePropertiesData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValveID", valveId);
            Utility.LoadDataSet(waterValvePropertiesData.GetWaterValveProperties, "f84721_pcget_FS_WaterValveProperty", ht);
            return waterValvePropertiesData;
        }

        #endregion Get Water Valve Properties

        #region Save Water Valve Properties

        /// <summary>
        /// To Save F84721 Water valve properties.
        /// </summary>
        /// <param name="valveId">The valve ID.</param>
        /// <param name="waterValveProperties">The XML string Containing All values in Water valve properties.</param>
        ///<param name="userId">userId</param>
        /// <returns>The integer value containing valve id</returns>
        public static int F84721_SaveWaterValveProperties(int valveId, string waterValveProperties, int userId)
        {            
            Hashtable ht = new Hashtable();
            ht.Add("@ValveID", valveId);
            ht.Add("@WaterValveProp", waterValveProperties);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f84721_pcins_FS_WaterValveProperty", ht); 
        }

        #endregion Save Water Valve Properties

        #region Delete Water Valve Properties

        /// <summary>
        /// To Delete F84721 Water valve properties
        /// </summary>
        /// <param name="valveId">The ValveId</param>
        ///<param name="userId">userId</param>
        public static void F84721_DeleteWaterValveProperties(int valveId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ValveID", valveId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f84721_pcdel_FS_WaterValveProperty", ht);
        }

        #endregion Delete Water Valve Properties

        #endregion F84721 Water Valve Properties
    }
}
