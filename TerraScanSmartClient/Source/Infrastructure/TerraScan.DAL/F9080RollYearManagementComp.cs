// -------------------------------------------------------------------------------------------
// <copyright file="F9080RollYearManagementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access attachment related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------


namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.DataLayer;
    using System.Data;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F9080RollYearManagementComp
    /// </summary>
    public static class F9080RollYearManagementComp
    {
        /// <summary>
        /// F9080_s the get RollYear Management Comp.
        /// </summary>
        /// <param name="Roll Year">Roll Year.</param>
        /// <param name="User ID">The User id.</param>
        /// <returns>Returns Get RollYear DataSet</returns>
        public static F9080RollYearManagementData F9080_GetRollYearManagement(short rollYear, int userId)
        {
            F9080RollYearManagementData rollYearDataset = new F9080RollYearManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@RollYear", rollYear);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(rollYearDataset.GetRollYearManagement, "f9080_pcget_RollYearManagement", ht);
            return rollYearDataset;
        }

        /// <summary>
        /// F9080_s the list Roll Year Management.
        /// </summary>
        /// <param name="UserId">The User Id.</param>
       /// <returns>Returns Steps Dataset</returns>
       public static F9080RollYearManagementData F9080_ListRollYearManagement(int userId)
        {
            F9080RollYearManagementData stepsDataSet = new F9080RollYearManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(stepsDataSet.ListRollYearManagement, "f9080_pclst_RollYearManagement", ht);
            return stepsDataSet;
        }


       /// <summary>
       /// Execute the Roll year Step
       /// </summary>
      /// <param name="RollOverId">The RollOver id.</param>
       /// <param name="formId">The User id.</param>
       public static string F9080_ExecRollYearStep(short rollOverId, int userId)
       {
           //int primaryOutput;
           Hashtable ht = new Hashtable();
           ht.Add("@RollOverID", rollOverId);
           ht.Add("@UserID", userId);
           //primaryOutput = Utility.FetchSPExecuteKeyId("f9080_pcexe_RollYearStep", ht);
           //return primaryOutput;
           return Utility.FetchSingleOuputParameter("f9080_pcexe_RollYearStep", ht, "@Result");
       }
        
    }
}
