// ------------------------------------------------------------------------------------------------------------
// <copyright file="F1515SubFundSelectionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F1515SubFundSelectionComp.cs methods</summary>
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
    /// Class file for F1515SubFundSelectionComp
    /// </summary>
    public static class F1515SubFundSelectionComp
    {
        #region F1515 Sub Fund Selection

        #region F1515_GetSubFundSelection

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
        public static F1515SubFundSelectionData F1515_GetSubFundSelection(string subFund, string description, int rollYear, int iscash)
        {
            F1515SubFundSelectionData subFundSelectionData = new F1515SubFundSelectionData();
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

            Utility.LoadDataSet(subFundSelectionData.GetSubFundSelection, "f1515_pclst_SubFundSelection", ht);
            return subFundSelectionData;
        }

        #endregion F1515_GetSubFundSelection

        #endregion F1515 Sub Fund Selection
    }
}
