// ------------------------------------------------------------------------------------------------------------
// <copyright file="F9103SubFundSelectionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F9103SubFundSelectionComp.cs methods</summary>
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
    /// Class file for F9103SubFundSelectionComp
    /// </summary>
    public static class F9103SubFundSelectionComp
    {
        #region F9103 Sub Fund Selection

        #region F9103_GetSubFundSelection

        /// <summary>
        /// To Get the Sub Fund Selection Details
        /// </summary>
        /// <param name="subFund">The Sub fund</param>
        /// <param name="description">The Description</param>
        /// <param name="rollYear">The Roll year</param>
        /// <param name="iscash">The iscash.</param>
        /// <returns>
        /// Typed Dataset containing the Sub Fund Selection Details
        /// </returns>
        public static F9103SubFundSelectionData F9103_GetSubFundSelection(string subFund, string description, int rollYear, int iscash)
        {
            F9103SubFundSelectionData subFundSelectionData = new F9103SubFundSelectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@SubFund", subFund);
            ht.Add("@Description", description);
            if (rollYear != 999)
            {
                ht.Add("@RollYear", rollYear);
            }

            if (iscash != 999)
            {
                ht.Add("@IsCash", iscash);
            }

            Utility.LoadDataSet(subFundSelectionData.GetSubFundSelection, "f9103_pclst_SubFundSelection", ht);
            return subFundSelectionData;
        }

        #endregion F9103_GetSubFundSelection

        #endregion F9103 Sub Fund Selection
    }
}
