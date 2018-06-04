// -------------------------------------------------------------------------------------------
// <copyright file="F35075StateAssessedOwnerComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F35075StateAssessedOwnerComp.cs methods
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
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;

    /// <summary>
    /// Data Access Layer which talks to the DB directly for F35075
    /// </summary>
    public class F35075StateAssessedOwnerComp
    {
        /// <summary>
        /// F35075_s the get state assessed owner details.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <returns></returns>
        public static F35075StateAssessedData F35075_GetStateAssessedOwnerDetails(int stateId)
        {
            F35075StateAssessedData stateAssessedOwnerData = new F35075StateAssessedData();
            Hashtable ht = new Hashtable();
            ht.Add("@StateID", stateId);
            string[] tableName = new string[] { stateAssessedOwnerData.GetStateAssessedOwner.TableName, stateAssessedOwnerData.ListStateAssessedDetails.TableName, stateAssessedOwnerData.GetStateAssessedRecordCount.TableName };
            Utility.LoadDataSet(stateAssessedOwnerData, "f35075_pcget_StateAssessed", ht, tableName);
            return stateAssessedOwnerData;
        }
        
        /// <summary>
        /// F35075_s the save state assessed owner.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <param name="assessedItems">The assessed items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F35075_SaveStateAssessedOwner(int? stateId, string assessedItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@StateID", stateId);
            ht.Add("@AssessedItems", assessedItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f35075_pcins_StateAssessed", ht);
        }
        
        /// <summary>
        /// F35076_s the save state assessed grid.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <param name="codeItems">The code items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F35076_SaveStateAssessedGrid(int? stateId, string codeItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@StateID", stateId);
            ht.Add("@CodeItems", codeItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f35076_pcins_StateAssessedDetails", ht);
        }


        /// <summary>
        /// F35075_s the delete state assessed.
        /// </summary>
        /// <param name="stateId">The state id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F35075_DeleteStateAssessed(int stateId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@StateID", stateId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f35075_pcdel_StateAssessed", ht);
        }

        /// <summary>
        /// F35076_s the delete state assessed details.
        /// </summary>
        /// <param name="stateIemId">The state iem id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F35076_DeleteStateAssessedDetails(int stateIemId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@StateItemID", stateIemId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f35076_pcdel_StateAssessedDeatils", ht);
        }
        

    }
}
