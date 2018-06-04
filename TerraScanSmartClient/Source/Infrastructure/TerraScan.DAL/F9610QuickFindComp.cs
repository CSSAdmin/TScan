// -------------------------------------------------------------------------------------------
// <copyright file="F9610QuickFindComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F9610QuickFindComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 3/6/08             Malliga             Created
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
    /// F9610QuickFindComp Class File.
    /// </summary>
    
    public static class F9610QuickFindComp
    {
        
        /// <summary>
        /// F9610s the quick find.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="keyword">The keyword.</param>
        /// <returns></returns>
        public static F9610QuickFind F9610QuickFind(int form,string keyword)
        {
            F9610QuickFind quickfinditems = new F9610QuickFind();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", form);
            ht.Add("@KeyWord", keyword);
            string[] tableName = new string[] { quickfinditems.F9610_GetQuickFindItem.TableName, quickfinditems.F9610_GetQuickFindRows.TableName };
            Utility.LoadDataSet(quickfinditems, "f9610_pcget_QuickFind", ht, tableName);
            return quickfinditems;
        }
    }
}
