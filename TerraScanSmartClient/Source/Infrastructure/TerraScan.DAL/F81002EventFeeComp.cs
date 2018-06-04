// -------------------------------------------------------------------------------------------
// <copyright file="F81002EventFeeComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F81001EventFeeCatalogComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 06/11/07        Jaya Prakash .k     Created
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
    /// F81002EventFeeComp class file 
    /// </summary>    
    public static class F81002EventFeeComp
    {
        #region EventFee

        /// <summary>
        /// Get the Event Fee data
        /// </summary>
        /// <param name="eventId">EventId</param>
        /// <param name="form">Form</param>
        /// <returns>DataSet</returns>
        public static F81002EventFeeData F81002_GetEventFee(int eventId, int form)
        {
            F81002EventFeeData getEventFeeData = new F81002EventFeeData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@Form", form);
            Utility.LoadDataSet(getEventFeeData.GetFeeEvent, "f81002_pcget_FeeEvent", ht);
            return getEventFeeData;
        }

        /// <summary>
        /// To Save the Event Fee
        /// </summary>
        /// <param name="eventId">mid</param>
        /// <param name="feeItems">xml string containing the Event Fee</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer value containing the key id</returns>
        public static int F81002_SaveEventFee(int eventId, string feeItems, int userId)
        {
            Hashtable ht = new Hashtable();

            if (eventId > 0)
            {
                ht.Add("@EventId", eventId);
            }

            ht.Add("@FeeItems", feeItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f81002_pcins_FeeEvent", ht);
        }

        /// <summary>
        /// Delete the Event Fee data
        /// </summary>
        /// <param name="eventId">EventID</param>
        /// <param name="userId">UserID</param>
        public static void F81002_DeleteEventFee(int eventId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FeeID", eventId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f81002_pcdel_FeeEvent", ht);
        }

        #endregion EventFee
    }    
}
