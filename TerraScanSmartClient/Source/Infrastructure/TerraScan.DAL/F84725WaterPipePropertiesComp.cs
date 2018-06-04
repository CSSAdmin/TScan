// -------------------------------------------------------------------------------------------
// <copyright file="F84725WaterPipePropertiesComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F84725 Water Pipe Properties Methods </summary>
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
    /// F84725WaterPipePropertiesComp Class File
    /// </summary>
    public static class F84725WaterPipePropertiesComp
    {
        #region F84725 Water Pipe Properties 

        #region Get Water Pipe Properties

        /// <summary>
        /// To Load Water Pipe Properties
        /// </summary>
        /// <param name="pipeId">The Pipe Id</param>
        /// <returns>Typed DataSet Containing the Water Pipe Properties details</returns>
        public static F84725WaterPipePropertiesData F84725_GetWaterPipeProperties(int pipeId)
        {
            F84725WaterPipePropertiesData waterPipePropertiesData = new F84725WaterPipePropertiesData();
            Hashtable ht = new Hashtable();
            ht.Add("@PipeID", pipeId);
            Utility.LoadDataSet(waterPipePropertiesData.GetWaterPipePropertiesDataTable, "f84725_pcget_FS_WaterPipeProperty", ht);
            return waterPipePropertiesData;
        }

        #endregion Get Water Pipe Properties

        #region Save Water Pipe Properties

        /// <summary>
        /// To Save water pipe properties.
        /// </summary>
        /// <param name="pipeId">The pipe id.</param>
        /// <param name="waterPipeProperties">The XML String Containing the Water Pipe Properties details.</param>
        ///<param name="userId">userId</param>
        /// <returns>the integer value containing the pipeid</returns>
        public static int F84725_SaveWaterPipeProperties(int pipeId, string waterPipeProperties, int userId)
        {            
            Hashtable ht = new Hashtable();
            ht.Add("@PipeID", pipeId);
            ht.Add("@WaterPipeProp", waterPipeProperties);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f84725_pcins_FS_WaterPipeProperty", ht);              
        }

        #endregion Save Water Pipe Properties

        #region Delete Water Pipe Properties        

         /// <summary>
        /// To Delete water pipe properties.
        /// </summary>
        /// <param name="pipeId">the pipe Id</param>
        ///<param name="userId">userId</param>
        public static void F84725_DeleteWaterPipeProperties(int pipeId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@PipeID", pipeId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f84725_pcdel_FS_WaterPipeProperty", ht);
        }

        #endregion Delete Water Pipe Properties      

        #endregion F84725 Water Pipe Properties
    }
}
