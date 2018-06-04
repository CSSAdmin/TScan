// ------------------------------------------------------------------------------------------------------------
// <copyright file="F1513FundSelectionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F1513FundSelectionComp.cs methods</summary>
// Release history
//*************************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ------------------------------------------------------------------------
// 
// 
// ------------------------------------------------------------------------------------------------------------

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
    /// Class file for F1513FundSelectionComp
    /// </summary>
    public static class F1513FundSelectionComp
    {
        #region F1513 Fund Selection

        #region F1513_GetFundSelection

        /// <summary>
        /// To Get the Fund Selection details
        /// </summary>
        /// <param name="fund">The Fund</param>
        /// <param name="description">The Description</param>
        /// <returns>Typed Dataset Containing the Fund Selection details</returns>
        public static F1513FundSelectionData F1513_GetFundSelection(string fund, string description)
        {
            F1513FundSelectionData fundSelectionData = new F1513FundSelectionData();
            Hashtable ht = new Hashtable();
            ////if (fund != -999)
            ////{
                ht.Add("@Fund", fund);
            ////}

            ht.Add("@Description", description);
            Utility.LoadDataSet(fundSelectionData.GetFundSelection, "f1513_pclst_FundSelection", ht);
            return fundSelectionData;
        }

        /// <summary>
        /// F1513_CentralFundItemValidation
        /// </summary>
        /// <param name="fundId"></param>
        /// <param name="rollYear"></param>
        /// <returns></returns>
        public static int F1513_CentralFundItemValidation(int fundId, int rollYear)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FundID", fundId);
            ht.Add("@Rollyear", rollYear);
            return Utility.FetchSPExecuteKeyId("f1513_pcget_CentralFundItemValidation", ht);
        }

        #endregion F1513_GetFundSelection

        #endregion F1513 Fund Selection
    }
}
