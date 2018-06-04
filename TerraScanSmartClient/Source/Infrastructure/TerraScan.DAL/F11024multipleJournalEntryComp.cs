// -------------------------------------------------------------------------------------------
// <copyright file="F11020RealPropertyComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to insert Receipts</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 12 jan 15		Purushotham	            Created
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
    /// F11024multipleJournalEntryComp
    /// </summary>
    public static class F11024multipleJournalEntryComp
    {

        /// <summary>
        /// F11024_s the get multiple journal template details.
        /// </summary>
        /// <param name="jetTemplateID">The jet template ID.</param>
        /// <returns></returns>
        public static F11024MultiplejournalEntryData F11024_GetMultipleJournalTemplateDetails(int jetTemplateID)
        {
            F11024MultiplejournalEntryData tempObject = new F11024MultiplejournalEntryData();
            Hashtable ht = new Hashtable();
            ht.Add("@JETemplateID", jetTemplateID);
            Utility.LoadDataSet(tempObject.f1124_JournalEntryTemplateItem, "f1124_pclst_JournalEntryTemplateItem", ht);
            return tempObject;
        }

        /// <summary>
        /// F11024_s the save multiple journal template.
        /// </summary>
        /// <param name="transferDate">The transfer date.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="description">The description.</param>
        /// <param name="journalTemplateDetails">The journal template details.</param>
        public static void F11024_SaveMultipleJournalTemplate(string transferDate,int userId, string description, string journalTemplateDetails )
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TransferDate", transferDate);
            ht.Add("@UserID", userId);
            ht.Add("@Description", description);
            ht.Add("@JournalEntries", journalTemplateDetails);
            DataProxy.ExecuteSP("f11024_pcins_JournalEntryBulkReceipt", ht);
        }

        /// <summary>
        /// F11024_s the search template details.
        /// </summary>
        /// <returns></returns>
        public static F11024MultiplejournalEntryData F11024_SearchTemplateDetails()
        {
            F11024MultiplejournalEntryData tempObject = new F11024MultiplejournalEntryData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(tempObject.f1124_SelectJournalEntryTemplate, "f1124_pclst_JournalEntryTemplate", ht);
            return tempObject;
        }
    }
}
