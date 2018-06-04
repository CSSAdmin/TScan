// -------------------------------------------------------------------------------------------
// <copyright file="F29630.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F29620AglandApplicationCompComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 22/10/08          LathaMaheswari.D    Created
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
    /// F29630BoardOfEqulization Class File.
    /// </summary>
    public class F29630BoardOfEqualizationComp
    {
        /// <summary>
        /// Gets the board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <returns>DataSet</returns>
        public static F29630BoardOfEqualizationData F29630GetBoardOfEqualizationDetails(int boeId)
        {
            F29630BoardOfEqualizationData BoardOfEqualizationDetails = new F29630BoardOfEqualizationData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", boeId);
            string[] tableNames = new string[] { BoardOfEqualizationDetails.GetBOEDetails.TableName, BoardOfEqualizationDetails.GetBOEValues.TableName, BoardOfEqualizationDetails.GetBOEParcelDetails.TableName };
            Utility.LoadDataSet(BoardOfEqualizationDetails, "f29630_pcget_BOE", ht, tableNames);
            return BoardOfEqualizationDetails;
       }

        /// <summary>
        /// Saves the board of equalization details.
        /// </summary>
        /// <param name="boeElements">The boe elements.</param>
        /// <param name="boeValues">The boe values.</param>
        /// <param name="userId">The User Id</param>
        public static void F29630SaveBoardOfEqualizationDetails(string boeElements, string boeValues, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@BOEElements", boeElements);
            ht.Add("@BOEValues", boeValues);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f29630_pcins_BOE", ht);
        }

        /// <summary>
        /// F29630s the delete board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        public static void F29630DeleteBoardOfEqualizationDetails(int boeId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@BOEID", boeId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f29630_pcdel_BoE", ht);
        }

        /// <summary>
        /// F29630s the push board of equalization details.
        /// </summary>
        /// <param name="boeId">The boe id.</param>
        /// <param name="userId">The user id.</param>
        public static void F29630PushBoardOfEqualizationDetails(int boeId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@BOEID", boeId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f29630_pcexe_BoEPushValue", ht);
        }
    }
}
