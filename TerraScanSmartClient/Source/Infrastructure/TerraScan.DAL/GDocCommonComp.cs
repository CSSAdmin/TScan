// -------------------------------------------------------------------------------------------
// <copyright file="GDocCommonComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access GDoc methods to Load Common ComboBoxs </summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 19/12/2006       VijayaKumar.M       Added
// 
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
    /// GDocCommonComp Class File
    /// </summary>
    public static class GDocCommonComp
    {
        #region GDoc Common

        #region Get GDocBusiness

        /// <summary>
        /// To Load GDoc Business ComboBoxs.
        /// </summary>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public static GDocCommonData F8000_GetGDocBusiness()
        {
            GDocCommonData gdocCommonData = new GDocCommonData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(gdocCommonData.ListGDocBusiness, "f8000_pclst_GDocBusiness", ht);
            return gdocCommonData;
        }

        #endregion Get GDocBusiness

        #region Get GDocDiameter

        /// <summary>
        /// To Load GDoc Diameter ComboBoxs.
        /// </summary>
        /// <param name="featureClassId">The FeatureClassId </param>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public static GDocCommonData F8000_GetGDocDiameter(int featureClassId)
        {
            GDocCommonData gdocCommonData = new GDocCommonData();
            Hashtable ht = new Hashtable();
            ht.Add("@FeatureClassID", featureClassId);
            Utility.LoadDataSet(gdocCommonData.ListGDocDiameter, "f8000_pclst_GDocDiameter", ht);
            return gdocCommonData;
        }

        #endregion Get GDocDiameter

        #region Get GDocPropertyReference

        /// <summary>
        /// To Load GDoc PropertyReference ComboBoxs.
        /// </summary>
        /// <param name="featureClassId">The FeatureClassId </param>
        /// <param name="refField">The Ref Field</param>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public static GDocCommonData F8000_GetGDocPropertyReference(int featureClassId, string refField)
        {
            GDocCommonData gdocCommonData = new GDocCommonData();
            Hashtable ht = new Hashtable();
            ht.Add("@FeatureClassID", featureClassId);
            ht.Add("@RefField", refField);
            Utility.LoadDataSet(gdocCommonData.ListGDocPropertyReference, "f8000_pclst_GDocPropertyReference", ht);
            return gdocCommonData;
        }

        #endregion Get GDocPropertyReference

        #region Get GDocStreet

        /// <summary>
        /// To Load GDoc Street ComboBoxs.
        /// </summary>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public static GDocCommonData wListStreets()
        {
            GDocCommonData gdocCommonData = new GDocCommonData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(gdocCommonData.ListGDocStreet, "f8000_pclst_GDocStreet", ht);
            return gdocCommonData;
        }

        #endregion Get GDocStreet

        #region Get GDocUser

        /// <summary>
        /// To Load GDoc User ComboBoxs.
        /// </summary>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public static GDocCommonData F8000_GetGDocUser()
        {
            GDocCommonData gdocCommonData = new GDocCommonData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(gdocCommonData.ListGDocUser, "f8000_pclst_GDocUser", ht);
            return gdocCommonData;
        }

        #endregion Get GDocUser

        #endregion GDoc Common
    }
}
