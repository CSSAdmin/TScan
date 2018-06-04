// -------------------------------------------------------------------------------------------
// <copyright file="F2551EditStatementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F2551EditStatementComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 27 Sep 2011         Manoj Kumar. P             Created
//---------------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.BusinessEntities;
    using System.Collections;
    using TerraScan.DataLayer;

    #endregion Namespace

    /// <summary>
    /// F2552StatementSelectionComp Class
    /// </summary>
    public static class F2552StatementSelectionComp
    {
        #region ListEditStatementSelectionDetails

        /// <summary>
        /// F2552_s the list StatementSelection details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="TypeID">TheType id.</param>
        /// <returns>The  Statement  selection dataset.</returns>
        public static F2552StatementSelectionData F2552_ListStatementSelectionDetails(int parcelId, int typeId, int userId)
        {
            F2552StatementSelectionData StatementSelectionData = new F2552StatementSelectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@TypeID", typeId);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(StatementSelectionData.StatementDataTable, "f2552_pclst_StatementSelection", ht);
            return StatementSelectionData;
        }
         #endregion
    }
}
