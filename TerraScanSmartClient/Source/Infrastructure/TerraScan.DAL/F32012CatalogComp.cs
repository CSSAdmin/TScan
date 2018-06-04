// -------------------------------------------------------------------------------------------
// <copyright file="F32012CatalogComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Check Detail</summary>
// Release history
// **********************************************************************************
// Date              Author            Description
// ----------       ---------          ---------------------------------------------------------
// 06 OCT 09      D.LathaMaheswari      Created
// -------------------------------------------------------------------------------------------
namespace TerraScan.Dal
{
    using System.Collections;
    using TerraScan.DataLayer;
    using TerraScan.BusinessEntities;
    using System.Data;

    /// <summary>
    /// F32012 Catalog comp
    /// </summary>
    public static class F32012CatalogComp
    {
        #region Load Catalog

        /// <summary>
        /// F32012_s the get catalog data.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Catalog Data</returns>
        public static F32012CatalogData F32012_GetCatalogData(int valueSliceId)
        {
            F32012CatalogData getSketchData = new F32012CatalogData();
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            string[] tableName = new string[] { getSketchData.ConfigurationValue.TableName, getSketchData.ParcelXML.TableName, getSketchData.HtcXML.TableName, getSketchData.CatalogXML.TableName, getSketchData.ConfigXML.TableName };
            Utility.LoadDataSet(getSketchData, "f32012_pcget_SketchDataXML", ht, tableName);
            return getSketchData;
        }

        #endregion Load Catalog

        #region Save Catalog

        /// <summary>
        /// F32012_s the save catalog.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="catalogData">The catalog data.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Conirmation value for Save</returns>
        public static DataSet F32012_SaveCatalog(int objectId, string catalogData, int userId)
        {
            Hashtable ht = new Hashtable();
            if (objectId != -99)
            {
                ht.Add("@ValueSliceID", objectId);
            }

            ht.Add("@TerragonXML", catalogData);
            ht.Add("@UserID", userId);
            DataSet sketchDataSet = new DataSet();
            DataTable resultTable = new DataTable();
            sketchDataSet.Tables.Add(resultTable);
            Utility.FetchSPOuputParameters(sketchDataSet.Tables[0], "f32012_pcins_SketchCatalog", ht);
            return sketchDataSet;
            //return Utility.FetchSPExecuteKeyId("f32012_pcexe_SketchCatalog", ht);
        }

        #endregion Save Catalog
    }
}
