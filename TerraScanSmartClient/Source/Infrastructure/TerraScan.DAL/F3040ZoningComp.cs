// ------------------------------------------------------------------------------------------------------------
// <copyright file="F3040ZoningComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F3040ZoningComp.cs methods</summary>
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
    /// F3040ZoningComp class file 
    /// </summary>
    public static class F3040ZoningComp
    {
        #region F3040 Zoning

        #region F3040 Get Zoning

        /// <summary>
        /// Used to Get the Zoning Details
        /// </summary>
        /// <returns>Gets Typed DataSet containing the Zoning Details.</returns>
        public static F3040ZoningData F3040_GetZoningDetails()
        {
            F3040ZoningData zoningDetailsData = new F3040ZoningData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(zoningDetailsData.ListZoning, "f35100_pclst_Zoning", ht);
            return zoningDetailsData;
        }

        #endregion F3040 Get Zoning

        #region F3040 Save Zoning

        /// <summary>
        /// Used to Save the Zoning Details
        /// </summary>
        /// <param name="zoningDetails">The zoning details.</param>
        /// <param name="userId">userId</param>
        /// <returns>Typed DataSet containing the Zoning Details to Save.</returns>
        public static int F3040_SaveZoningDetails(string zoningDetails, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@Zoning", zoningDetails);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecutedReturnValue("f3040_pcins_Zoning", ht);
        }

        #endregion F3040 Save Zoning

        #endregion F3040 Zoning
    }
}
