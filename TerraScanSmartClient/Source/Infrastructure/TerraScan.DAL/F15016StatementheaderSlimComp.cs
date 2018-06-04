// -------------------------------------------------------------------------------------------
// <copyright file="F15016StatementheaderSlimComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F15016StatementheaderSlimComp.cs methods
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
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;
    using System.Collections;

    /// <summary>
    /// F15016StatementheaderSlimComp class file
    /// </summary>
    public static class F15016StatementheaderSlimComp
    {
        /// <summary>
        /// F15016_GetstatementHeaderSlimDetails
        /// </summary>
        /// <param name="statementId">statementId</param>
        /// <returns>Typed dataset</returns>
        public static F15016StatementHeaderData F15016_GetstatementHeaderSlimDetails(int statementId)
        {
            F15016StatementHeaderData form15016StatementHeaderData = new F15016StatementHeaderData();
            Hashtable ht = new Hashtable();
            ht.Add("@StatementID", statementId);
            Utility.LoadDataSet(form15016StatementHeaderData.f15016StatementHeaderSlim, "f15016_pcget_StatementHeaderSlim", ht);
            return form15016StatementHeaderData;
        }
    }
}
