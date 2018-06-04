// ------------------------------------------------------------------------------------------------------------
// <copyright file="F1503GenericManagementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access F1503GenericManagementComp.cs methods</summary>
// Release history
//*************************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ------------------------------------------------------------------------
// 
// 
// ------------------------------------------------------------------------------------------------------------

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
    /// Class file for F1503GenericManagementComp
    /// </summary>
    public static class F1503GenericManagementComp
    {
        #region F1503 Generic Management Comp

        #region GetGenericElementMgmt

        /// <summary>
        /// To Get the Generic Element Management details
        /// </summary>
        /// <param name="keyValue">The key value(Element ID)</param>
        /// <param name="description">The Description</param>
        /// <param name="formName">The Form Name</param>
        /// <returns>Typed Dataset containing the Element ID and Description Value</returns>
        public static F1503GenericManagementData F1503_GetGenericElementMgmt(string keyValue, string description, string formName)
        {
            F1503GenericManagementData genericElementManagementData = new F1503GenericManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@ElementName", formName);
            ht.Add("@ElementKeyName", keyValue);
            ht.Add("@Description", description);
            Utility.LoadDataSet(genericElementManagementData.GetGenericElementMgmt, "f1503_pclst_ElementDetails", ht);
            return genericElementManagementData;             
        }

        #endregion GetGenericElementMgmt

        #region SaveGenericElementMgmt

        /// <summary>
        /// To Save the Generic Element Management details
        /// </summary>
        /// <param name="functionElemnts">The Xml string containing Element ID and Description Value</param>
        /// <param name="formName">The Form name</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer value containing whether save is compleded or not</returns>
        public static int F1503_SaveGenericElementMgmt(string functionElemnts, string formName, int userId)
        {            
            Hashtable ht = new Hashtable();
            ht.Add("@FunctionElemnts", functionElemnts);
            ht.Add("@IdentifyTable", formName);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecutedReturnValue("f1503_pcins_ElementDetails", ht);       
        }

        #endregion SaveGenericElementMgmt

        #region DeleteGenericElementMgmt

        /// <summary>
        /// To Delete the Generic Element Management details
        /// </summary>
        /// <param name="elementId">The Particular Element ID</param>        
        /// <param name="formName">The Form name</param>
        /// <param name="userId">userId</param>
        public static void F1503_DeleteGenericElementMgmt(string elementId, string formName, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ElementID", elementId);
            ht.Add("@TableName", formName);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f1503_pcdel_ElementDetails", ht);
        }

        #endregion DeleteGenericElementMgmt

        #endregion F1503 Generic Management Comp
    }
}
