// -------------------------------------------------------------------------------------------
// <copyright file="MaterialsFooterComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Materials Footer</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 25 Oct 06		JAYANTHI	            Created

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// Data Access Layer which talks to the DB directly for F8042 Time Footer
    /// </summary>
    public static class MaterialsFooterComp
    {
        /// <summary>
        /// Gets the Materails Footer Details as Typed Dataset from DB
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <param name="formId">form Id</param>
        /// <returns>Typed Dataset</returns>
        public static MaterialsFooterData F8046_GetMaterialsFooterDetails(int eventId, int formId)
        {
            MaterialsFooterData materialsFooterData = new MaterialsFooterData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@FormID", formId);
            Utility.LoadDataSet(materialsFooterData.GetMaterialFooter, "f8046_pcget_MaterialFooter", ht);
            return materialsFooterData;
        }
    }
}
