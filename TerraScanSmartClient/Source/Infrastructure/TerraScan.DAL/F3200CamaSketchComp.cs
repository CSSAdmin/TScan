// -------------------------------------------------------------------------------------------
// <copyright file="F3200CamaSketchComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Check Detail</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15 Nov 07		D.LathaMaheswari	Created
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
    /// F3200ParcelSplitComp
    /// </summary>
    public static class F3200CamaSketchComp
    {
        #region F3200_GetSketchData
      
        /// <summary>
        /// Get the Sketch Data
        /// </summary>
        /// <param name="objectId">ObjectID</param>
        /// <returns>String</returns>
        public static F3200CamaSketchData F3200_GetSketchData(int objectId)
        {
            F3200CamaSketchData getSketchData = new F3200CamaSketchData();
            Hashtable ht = new Hashtable();
            ht.Add("@objectid", objectId);
            string[] tableName = new string[] { getSketchData.ConfigurationValue.TableName, getSketchData.ParcelXML.TableName, getSketchData.HtcXML.TableName, getSketchData.CatalogXML.TableName, getSketchData.ConfigXML.TableName };
            //DataSet ds = new DataSet();
            Utility.LoadDataSet(getSketchData, "f3200_pcget_SketchDataXML", ht, tableName);
            //getSketchData.Merge(ds); 
            return getSketchData;
        }
        #endregion F3200_GetSketchData

        #region F3200_GetStyleList
       
        /// <summary>
        /// F3200_GetStyleList
        /// </summary>
        /// <param name="objectId">objectId</param>
        /// <returns>String value</returns>
        public static string F3200_GetStyleList(int objectId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ObjectID", objectId);
            return Utility.FetchSPExecuteKeyId("[f9002_pcchk_FormSmartPart]", ht).ToString();
        }
        #endregion F3200_GetStyleList

        #region F3200_SaveSketch
        /// <summary>
        /// Save the Sketch data
        /// </summary>
        /// <param name="objectId">objectId</param>
        /// <param name="sketchData">sketchData</param>
        /// <param name="userId">userid</param>
        /// <returns>integer</returns>
        public static DataSet F3200_SaveSketchData(int objectId, string sketchData, int userId)
        {
            Hashtable ht = new Hashtable();
            if (objectId != -99)
            {
                ht.Add("@ObjectID", objectId);
            }

            ht.Add("@SketchOutXML", sketchData);
            ht.Add("@UserID", userId);
            DataSet sketchDataSet = new DataSet();
            DataTable resultTable = new DataTable();
            sketchDataSet.Tables.Add(resultTable);
            Utility.FetchSPOuputParameters(sketchDataSet.Tables[0], "f3200_pcins_SketchOutXML", ht);
            return sketchDataSet;
        }
        #endregion F3200_SaveSketch

        /// <summary>
        /// Check the SmartPart
        /// </summary>
        /// <param name="formId">FormNumber</param>
        /// <returns>integer</returns>
        public static int F3200_CheckSmartPart(int formId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@Form", formId);
            return Utility.FetchSPExecuteKeyId("[f9002_pcchk_FormSmartPart]", ht);
        }
    }
}
