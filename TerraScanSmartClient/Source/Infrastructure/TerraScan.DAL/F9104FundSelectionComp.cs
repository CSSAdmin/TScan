// ------------------------------------------------------------------------------------------------------------
// <copyright file="F9104FundSelectionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F9104FundSelectionComp.cs methods</summary>
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
    /// Class file for F9104FundSelectionComp
    /// </summary>
    public static class F9104FundSelectionComp
    {
        #region F9104 Fund Selection 

        #region F9104_GetFundSelection

        /// <summary>
        /// To Get the Fund Selection details
        /// </summary>
        /// <param name="fund">The Fund</param>
        /// <param name="description">The Description</param>
        /// <returns>Typed Dataset Containing the Fund Selection details</returns>
        public static F9104FundSelectionData F9104_GetFundSelection(int fund, string description)
        {
            F9104FundSelectionData fundSelectionData = new F9104FundSelectionData();
            Hashtable ht = new Hashtable();
            if (fund != -999)
            {
                ht.Add("@Fund", fund);
            }
           
            ht.Add("@Description", description);
            Utility.LoadDataSet(fundSelectionData.GetFundSelection, "f9104_pclst_FundSelection", ht);
            return fundSelectionData;
        }

        #endregion F9104_GetFundSelection

        #endregion F9104 Fund Selection
    }
}
