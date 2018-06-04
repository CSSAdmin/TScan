// -------------------------------------------------------------------------------------------
// <copyright file="F3001ObjectManagementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Affidavit WorkQueue Inspection</summary>
// Release history
//**********************************************************************************
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.DataLayer;
    using System.Data;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// F3001ObjectManagementComp
    /// </summary>
    public static class F3001ObjectManagementComp
    {
        /// <summary>
        /// Gets the object detail.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <returns>F3001ObjectManagementData</returns>
        public static F3001ObjectManagementData F3001_GetObjectDetail(int objectId)
        {
            F3001ObjectManagementData objectManagementDataSet = new F3001ObjectManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@ObjectID", objectId);
            string[] tableNames = new string[] 
            { 
                objectManagementDataSet.ObjectDetailDataTable.TableName, 
                objectManagementDataSet.ClassDetail.TableName
            };
            Utility.LoadDataSet(objectManagementDataSet, "f3001_pcget_ObjectDetail", ht, tableNames);
            return objectManagementDataSet;
        }

        /// <summary>
        /// F3001_s the save object management.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="objectItems">The objectItems.</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public static int F3001_SaveObjectManagement(int objectId, string objectItems, int userId)
        {            
            Hashtable ht = new Hashtable();
            ht.Add("@ObjectID", objectId);
            /*ht.Add("@Description", description);
            ht.Add("@IsValue", valueDetail);
            ht.Add("@IsRoll", rollDetail);
            ht.Add("@StateCode", stateCode);*/
            ht.Add("@ObjectItems", objectItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f3001_pcupd_Object", ht);
        }

        /// <summary>
        /// F3001_s the delete object management.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="userId">UserID</param>
        public static void F3001_DeleteObjectManagement(int objectId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ObjectID", objectId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f3001_pcdel_Object", ht);
        
        }

        /// <summary>
        /// F3001_s the get parcel description.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>string</returns>
        public static string F3001_GetParcelDescription(int parcelId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            return Utility.FetchSPExecuteKeyString("f3001_pcget_ParcelDescription", ht);
        }

        /// <summary>
        /// F3001_s the copy object.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="objectXml">The object XML.</param>
        /// <param name="userId">UserID</param>
        /// <returns>int</returns>
        public static int F3001_CopyObject(int objectId, string objectXml, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ObjectID", objectId);
            ht.Add("@ObjectItems", objectXml);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f3001_pcexe_CreateNewObject", ht);
        }
    }
}
