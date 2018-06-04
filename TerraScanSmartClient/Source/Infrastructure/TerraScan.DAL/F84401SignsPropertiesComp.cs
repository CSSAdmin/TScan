// -------------------------------------------------------------------------------------------
// <copyright file="F84401SignsPropertiesComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access GDoc methods to Load Common ComboBoxs </summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 24/04/2006       D.LathaMaheswari    Added
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
    /// F84401SignsPropertiesCompClass File
    /// </summary>
    public static class F84401SignsPropertiesComp
    {
        #region F84401 Signs Properties

        #region Get Signs Properties

        /// <summary>
        /// F84401_s the get signs properties.
        /// </summary>
        /// <param name="featureId">The feature id.</param>
        /// <returns>DataSet</returns>
        public static F84401SignsPropertyData F84401_GetSignsProperties(int featureId)
        {
            F84401SignsPropertyData signsPropertiesData = new F84401SignsPropertyData();
            Hashtable ht = new Hashtable();
            ht.Add("@FeatureID", featureId);
            Utility.LoadDataSet(signsPropertiesData.GetSignsProperty, "f84401_pcget_84401", ht);
            return signsPropertiesData;
        }

        #endregion Get Signs Properties

        #region Save Signs Properties

        /// <summary>
        /// F84401_s the save signs properties.
        /// </summary>
        /// <param name="featureId">The feature id.</param>
        /// <param name="signsProperties">The signs properties.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer</returns>
        public static int F84401_SaveSignsProperties(int featureId, string signsProperties, int userId)
        {            
            Hashtable ht = new Hashtable();
            ht.Add("@FeatureID", featureId);
            ht.Add("@84401Items", signsProperties);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f84401_pcins_84401", ht);
        }

        #endregion Save Signs Properties

        #region Delete Signs Properties

        /// <summary>
        /// F84401_s the delete signs properties.
        /// </summary>
        /// <param name="featureId">The feature id.</param>
        /// <param name="userId">The user id.</param>
        public static void F84401_DeleteSignsProperties(int featureId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@FeatureID", featureId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f84401_pcdel_84401", ht);
        }

        #endregion Delete Signs Properties

        #endregion F84401 Signs Properties
    }
}
