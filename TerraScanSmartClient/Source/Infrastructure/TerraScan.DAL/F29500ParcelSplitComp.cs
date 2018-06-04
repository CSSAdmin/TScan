// -------------------------------------------------------------------------------------------
// <copyright file="F29500ParcelSplitComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Check Detail</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 17 Sep 07		karthikeyan V	            Created
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
    /// F29500ParcelSplitComp
    /// </summary>
    public static class F29500ParcelSplitComp
    {
        /// <summary>
        /// F29500_s the base parcel value.
        /// </summary>
        /// <param name="parcelId">parcelId</param>
        /// <returns>F29500ParcelSplitData</returns>
        public static F29500ParcelSplitData F29500_GetBaseParcelValue(int parcelId)
        {
            F29500ParcelSplitData parcelSplitDataSet = new F29500ParcelSplitData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            string[] tableName = new string[] { parcelSplitDataSet.ListParcelSplitObject.TableName, parcelSplitDataSet.ListParcelSplitValueSlices.TableName, parcelSplitDataSet.ListSplitObject.TableName, parcelSplitDataSet.ListSplitValuseSlice.TableName, parcelSplitDataSet.ListSplitCrop.TableName, parcelSplitDataSet.ListParcelSplitCrop.TableName };
            Utility.LoadDataSet(parcelSplitDataSet, "f29500_pclst_ValueSlices", ht, tableName);
            return parcelSplitDataSet;
        }

        /// <summary>
        /// F29500_s the parcel split load.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>F29500ParcelSplitData</returns>
        public static F29500ParcelSplitData F29500_ParcelSplitLoad(int eventId)
        {
            F29500ParcelSplitData parcelSplitDataSet = new F29500ParcelSplitData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            string[] tableName = new string[] { parcelSplitDataSet.ListParcelSplitObject.TableName, parcelSplitDataSet.ListParcelSplitValueSlices.TableName, parcelSplitDataSet.ListSplitObject.TableName, parcelSplitDataSet.ListSplitValuseSlice.TableName, parcelSplitDataSet.ListSplitCrop.TableName, parcelSplitDataSet.ListParcelSplitCrop.TableName, parcelSplitDataSet.ListSplitHeaderDetail.TableName, parcelSplitDataSet.ListSplitDefinitionHeader.TableName, parcelSplitDataSet.ListObjectSavedValue.TableName, parcelSplitDataSet.ListValueSliceSavedValue.TableName, parcelSplitDataSet.ListCropSavedValue.TableName };
            Utility.LoadDataSet(parcelSplitDataSet, "f29500_pcget_PSHeader", ht, tableName);
            return parcelSplitDataSet;
        }

        /// <summary>
        /// F29500_s the save parcel split.
        /// </summary>
        /// <param name="splitDefinitionXml">The split definition XML.</param>
        /// <param name="splitHeaderXml">The split header XML.</param>
        /// <param name="parcelSplitXml">The parcel split XML.</param>
        /// <param name="parcelObjectXml">The parcel object XML</param>
        /// <param name="cropXml">The Crop XML</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public static int F29500_SaveParcelSplit(string splitDefinitionXml, string splitHeaderXml, string parcelSplitXml, string parcelObjectXml, string cropXml, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelItems", splitDefinitionXml);
            ht.Add("@PSHeaderItems", splitHeaderXml);
            ht.Add("@PSParcelItems", parcelSplitXml);
            ht.Add("@PSParcelObjects", parcelObjectXml);
            ht.Add("@CropItems", cropXml);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f29500_pcins_SplitParcelDetails", ht);
        }

        /// <summary>
        /// F29500_s the create parcel.
        /// </summary>
        /// <param name="splitId">The split id.</param>
        /// <param name="userId">userId</param>
        /// <returns>REsult message</returns>
        public static string F29500_CreateParcel(int splitId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SplitID", splitId);
            ht.Add("@UserID", userId);
            //Utility.ExecuteSP("f29500_pcexe_CreateParcel", ht);
            return Utility.FetchSingleOuputParameter("f29500_pcexe_CreateParcel", ht, "@Results");
        }
    }
}
