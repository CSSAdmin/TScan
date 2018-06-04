// -------------------------------------------------------------------------------------------
// <copyright file="F49920InstrumentSearchEngineComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F49920InstrumentSearchEngineComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 2/2/08             Malliga             Created
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
    /// F49920InstrumentSearchEngine Class File.
    /// </summary>
    public static class F49920InstrumentSearchEngineComp
    {
        /// <summary>
        /// F49920_s the list instrument search.
        /// </summary>
        /// <param name="instrumentcondition">The instrumentcondition.</param>
        /// <returns>DataSet</returns>
        public static F49920InstrumentSearchEngineData F49920_ListInstrumentSearch(string instrumentcondition)
        {
            F49920InstrumentSearchEngineData instrumentSearchEngineDetails = new F49920InstrumentSearchEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@InstrumentCondition", instrumentcondition);
            Utility.LoadDataSet(instrumentSearchEngineDetails.ListInstrument, "f49920_pclst_Instrument", ht);
            return instrumentSearchEngineDetails;
        }

        /// <summary>
        /// F49920_s the list instrument load.
        /// </summary>
        /// <returns>DataSet</returns>
        public static F49920InstrumentSearchEngineData F49920_ListInstrumentLoad()
        {
            F49920InstrumentSearchEngineData listInstrumentLoaditems = new F49920InstrumentSearchEngineData();
            Hashtable ht = new Hashtable();
            string[] tableName = new string[] { listInstrumentLoaditems.ListInstrumentLoadItems.TableName, listInstrumentLoaditems.InstrumentLoadItems.TableName };
            Utility.LoadDataSet(listInstrumentLoaditems, "f49920_pclst_InstrumentLoadItems", ht, tableName);
            return listInstrumentLoaditems;
        }
    }
}
