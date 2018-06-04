// -------------------------------------------------------------------------------------------
// <copyright file="F32012CatalogComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Check Detail</summary>
// Release history
// **********************************************************************************
// Date              Author            Description
// ----------       ---------          ---------------------------------------------------------
// 20110124      D.LathaMaheswari      Created
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System.Collections;
    using TerraScan.DataLayer;
    using TerraScan.BusinessEntities;
    using System.Data;

    public static class F3201SketchLinkComp
    {
        #region Load Sketch Link

        /// <summary>
        /// F3201_s the get sketch link data.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Sketch Link data</returns>
        public static F3201SketchLinkData F3201_GetSketchLinkData(int parcelId, int userId)
        {
            F3201SketchLinkData getSketchData = new F3201SketchLinkData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@UserID", userId);
            string[] tableName = new string[] { getSketchData.TerragonData.TableName, getSketchData.PolygonData.TableName, getSketchData.LinkedData.TableName, getSketchData.HeaderDetails.TableName};
            Utility.LoadDataSet(getSketchData, "f3201_pcget_SketchLinks", ht, tableName);
            return getSketchData;
        }

        #endregion Load Sketch Link

        #region Save Catalog

        /// <summary>
        /// F3201_s the save sketch link data.
        /// </summary>
        /// <param name="linkXML">The link XML.</param>
        /// <param name="parcelId">The Parcel Id</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Error Message</returns>
        public static string F3201_SaveSketchLinkData(string linkXML, int parcelId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            ht.Add("@LinksXML", linkXML);
            ht.Add("@ParcelID", parcelId);
            return Utility.FetchSingleOuputParameter("f3201_pcupd_SketchLinks", ht, "@Message");
        }

        #endregion Save Catalog
    }
}
