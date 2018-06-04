// -------------------------------------------------------------------------------------------
// <copyright file="TimeFooterComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Stoppage Event Details</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 17 Oct 06		JAYANTHI	            Created

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
    public static class TimeFooterComp
    {
        /// <summary>
        /// Gets the Time Footer Details as Typed Dataset from DB
        /// </summary>
        /// <param name="eventId">eventId</param>
        /// <param name="formId">form Id</param>
        /// <returns>Typed Dataset</returns>
        public static TimeFooterData F8042_GetTimeFooterDetails(int eventId, int formId)
        {
            TimeFooterData timeFooterData = new TimeFooterData();            
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@FormID", formId);
            Utility.LoadDataSet(timeFooterData.GetTimeFooter, "f8042_pcget_TimeFooter", ht);
            return timeFooterData;
        }
    }
}
