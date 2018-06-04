// -------------------------------------------------------------------------------------------
// <copyright file="F2010StateCodeSelectionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F2010-StateCode Selection</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 11/12/2007       KUPPUSAMY.B         Created 
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
    /// F2010StateCodeSelectionComp class file
    /// </summary>
    public static class F2010StateCodeSelectionComp
    {
       #region List F2010_StateCodeSelection

        /// <summary>
        /// F2010_s the list state code selection.
        /// </summary>
        /// <returns></returns>
        public static F2010StateCodeSelectionData F2010_ListStateCodeSelection()
        {
            F2010StateCodeSelectionData stateCodeSelectionData = new F2010StateCodeSelectionData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(stateCodeSelectionData.F2010_ListStateCode, "F2010_pclst_StateCodeSelection", ht);
            return stateCodeSelectionData;
        }
       #endregion  List F2010_StateCodeSelection
    }
}
