// -------------------------------------------------------------------------------------------
// <copyright file="F84123SanitaryPipePropertiesComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F84123SanitaryPipePropertiesComp</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
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
    /// F84123SanitarypipePropertiesComp Class File
    /// </summary>
   public static class F84123SanitaryPipePropertiesComp
    {
        #region F84123 Sanitary Pipe Properties

        #region Get Sanitary Pipe Properties

        /// <summary>
        ///  To Load F84123 Sanitary pipe properties.
        /// </summary>
        /// <param name="pipeId">The Pipe ID.</param>
        /// <returns>Typed DataSet Containing All the Sanitary Pipe properties Details</returns>
        public static F84123SanitaryPipePropertiesData F84123_GetSanitaryPipeProperties(int pipeId)
        {
            F84123SanitaryPipePropertiesData sanitaryPipePropertiesData = new F84123SanitaryPipePropertiesData();
            Hashtable ht = new Hashtable();
            ht.Add("@PipeID", pipeId);
            Utility.LoadDataSet(sanitaryPipePropertiesData.GetSanitaryPipeProperties, "f84123_pcget_FS_SanitaryPipeProperty", ht);
            return sanitaryPipePropertiesData;
        }

        #endregion Get Sanitary Pipe Properties

        #region Save Sanitary Pipe Properties

        /// <summary>
        /// To Save F84123 Sanitary Pipe properties.
        /// </summary>
        /// <param name="pipeId">The Pipe ID.</param>
        /// <param name="sanitayPipeProperties">The XML string Containing All values in Sanitary Pipe properties.</param>
        ///<param name="userId">userId</param>
        /// <returns>The integer value containing pipe id</returns>
        public static int F84123_SaveSanitaryPipeProperties(int pipeId, string sanitayPipeProperties, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@PipeID", pipeId);
            ht.Add("@SanitaryPipe", sanitayPipeProperties);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f84123_pcins_FS_SanitaryPipeProperty", ht);
        }

        #endregion Save Sanitary pipe Properties

        #region Delete Sanitary Pipe Properties

        /// <summary>
        /// To Delete F84123 Sanitary Pipe properties
        /// </summary>
        /// <param name="pipeId">The Pipe Id</param>
        ///<param name="userId">userId</param>
        public static void F84123_DeleteSanitaryPipeProperties(int pipeId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@PipeID", pipeId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f84123_pcdel_FS_SanitaryPipeProperty", ht);
        }

        #endregion Delete Sanitary Pipe Properties

        #endregion F84123 Sanitary Pipe Properties
    }
}
