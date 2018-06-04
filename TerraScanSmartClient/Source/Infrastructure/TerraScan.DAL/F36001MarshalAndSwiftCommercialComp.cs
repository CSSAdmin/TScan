// -------------------------------------------------------------------------------------------
// <copyright file="F36001MarshalAndSwiftCommercialComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F27006ParcelOwnershipComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 26/07/07         V.Karthikeyan      Created
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
    /// F36001MarshalAndSwiftCommercialComp class file
    /// </summary>
    public static class F36001MarshalAndSwiftCommercialComp
    {
        #region  F36001 Marshal And Swift Commercial

        #region Get Marshal And Swift Commercial

        /// <summary>
        /// To get marshal and swift commercial details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Typed Dataset containing the Marshal And Swift Commercial details</returns>
        public static F36001MarshalAndSwiftCommercialData F36001_GetMarshalAndSwiftCommercial(int valueSliceId)
        {
            F36001MarshalAndSwiftCommercialData marshalAndSwiftCommercialData = new F36001MarshalAndSwiftCommercialData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            string[] optionalParameter = new string[] { marshalAndSwiftCommercialData.GetMarshallSwiftCommercial.TableName, marshalAndSwiftCommercialData.GetEstimate.TableName, marshalAndSwiftCommercialData.GetOccupancy.TableName, marshalAndSwiftCommercialData.GetComponent.TableName, marshalAndSwiftCommercialData.ListDeprTable.TableName, marshalAndSwiftCommercialData.ListDeprValueDataTable.TableName };
            Utility.LoadDataSet(marshalAndSwiftCommercialData, "f36001_pcget_MS_Commercial_HTC", ht, optionalParameter);
            return marshalAndSwiftCommercialData;
        }

        #endregion Get Marshal And Swift Commercial

        #region Save Marshal And Swift Commercial

        /// <summary>
        /// To save marshal and swift commercial.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="estimateDetails">The estimate details.</param>
        /// <param name="occupancyDetails">The occupancy details.</param>
        /// <param name="componentDetails">The component details.</param>
        /// <param name="depreciationXml">The depreciation XML.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>integer Value</returns>
        /// ///
        public static int F36001_SaveMarshalAndSwiftCommercial(int valueSliceId, string estimateDetails, string occupancyDetails, string componentDetails, string depreciationXml, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            ht.Add("@EstimateDetails", estimateDetails);
            ht.Add("@OccupancyDetails", occupancyDetails);
            ht.Add("@ComponentDetails", componentDetails);
            ht.Add("@DepreciationItems", depreciationXml);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f36001_pcins_MS_Commercial_HTC", ht);
        }

        #endregion Save Marshal And Swift Commercial

        #endregion F36001 Marshal And Swift Commercial
    }
}
