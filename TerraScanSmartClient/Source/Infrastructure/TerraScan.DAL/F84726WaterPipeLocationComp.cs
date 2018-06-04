// -------------------------------------------------------------------------------------------
// <copyright file="F84726WaterPipeLocationComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F84726 Water Pipe Location Methods</summary>
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
    /// F84726WaterPipeLocationComp Class file
    /// </summary>
    public static class F84726WaterPipeLocationComp
    {
        #region F84726 Water Pipe Location

        #region Get Water Pipe Location

        /// <summary>
        /// To Load Water Pipe Location.
        /// </summary>
        /// <param name="pipeId">The Pipe Id.</param>
        /// <returns>Typed Dataset Containg the Water Pipe Location Details.</returns>
        public static F84726WaterPipeLocationData F84726_GetWaterPipeLocation(int pipeId)
        {
            F84726WaterPipeLocationData waterPipeLocationData = new F84726WaterPipeLocationData();
            Hashtable ht = new Hashtable();
            ht.Add("@PipeID", pipeId);
            Utility.LoadDataSet(waterPipeLocationData.GetWaterPipeLocationDataTable, "f84726_pcget_FS_WaterPipeLocation", ht);
            return waterPipeLocationData;
        }

        #endregion Get Water Pipe Location

        #region Save Water Pipe Location

        /// <summary>
        /// To Save Water Pipe Location.
        /// </summary>
        /// <param name="pipeId">The Pipe Id.</param>
        /// <param name="waterPipeLocation">The Xml String containing the Water Pipe Location details</param>
        ///<param name="userId">userId</param>
        /// <returns>The Integer value containing pipe Id value</returns>
        public static int F84726_SaveWaterPipeLocation(int pipeId, string waterPipeLocation, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@PipeID", pipeId);
            ht.Add("@WaterPipeLoc", waterPipeLocation);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f84726_pcupd_FS_WaterPipeLocation", ht);
        }

        #endregion Save Water Pipe Location
       
        #endregion F84726 Water Pipe Location
    }
}
