// -------------------------------------------------------------------------------------------
// <copyright file="SupportFormCallComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access attachment related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.DataLayer;
    using System.Data;
    using System.Collections;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// SupportFormCallComp Class
    /// </summary>
    public static class SupportFormCallComp
    {
        #region GetFormDetails

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>SupportFormData Dataset</returns>
        public static SupportFormData GetFormDetails(int form, int userId)
        {
            SupportFormData supportForm = new SupportFormData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", form);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(supportForm.GetFormDetails, "f9016_pcget_SupportFormCall", ht);
            return supportForm;
        }

        #endregion

        #region ListUserNames

        /// <summary>
        /// List UserNames
        /// </summary>
        /// <returns>SupportFormData Dataset</returns>
        public static SupportFormData ListUserNames()
        {
            SupportFormData supportForm = new SupportFormData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(supportForm.ListUsers, "f9016_pclst_UserNames", ht);
            return supportForm;
        }

        #endregion

        #region GetFormManagement

        /// <summary>
        /// Gets the form details.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Typed dataset</returns>
        public static SupportFormData F9002_GetFormManagementDetails(int form, int userId)
        {
            SupportFormData supportForm = new SupportFormData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", form);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(supportForm.GetFormManagementDetails, "f9002_pcget_FormManagement", ht);
            return supportForm;
        }

        #endregion GetFormManagement

        #region FormCallTranslator
        
        /// <summary>
        /// Gets the translated form details.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyValue">The key value.</param>
        /// <returns>The support form dataset.</returns>
        public static SupportFormData GetTranslatedFormDetails(int formNo, string keyValue)
        {
            SupportFormData formCallTranslator = new SupportFormData();
            Hashtable htt = new Hashtable();

            htt.Add("@Form", formNo);
            htt.Add("@Param1In", keyValue);
            htt.Add("@Param1Out", 0);
            htt.Add("@Param2", null);
            htt.Add("@Param3", null);
            htt.Add("@Param4", null);
            Utility.FetchSPExecuteOutputValue(formCallTranslator.FormCallTranslate, "f9016_pcget_FormCallTranslate", htt);
            return formCallTranslator;
        }

        #endregion FormCallTranslator
    }
}
