// -------------------------------------------------------------------------------------------
// <copyright file="HelpEngineComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Help Engine</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 07 Aug 06		JYOTHI P	            Created
// 07 Aug 06        JYOTHI P                Added ListHelpDocumentForm method
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
    /// Main class for Help Engine Comp
    /// </summary>
    public static class HelpEngineComp
    {
        #region List Help Engine

        /// <summary>
        /// Lists the HelpDocuments 
        /// </summary>
        /// <param name="formFile">The form file.</param>
        /// <returns>returns dataset contains Form details</returns>
        public static HelpEngineData ListHelpDocumentForm(string formFile)
        {
            HelpEngineData helpEngineData = new HelpEngineData();
            Hashtable ht = new Hashtable();
            ht.Add("@FormFile", formFile);
            Utility.LoadDataSet(helpEngineData.ListHelpDocumentForm, "f9018_pclst_HelpDocumentForm", ht);
            return helpEngineData;
        }

        #endregion
    }
}
