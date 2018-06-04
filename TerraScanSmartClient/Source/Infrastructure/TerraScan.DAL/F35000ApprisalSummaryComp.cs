// -------------------------------------------------------------------------------------------
// <copyright file="F35000ApprisalSummaryComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access Apprisal Summary related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------;

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;
    using System.Collections;

    /// <summary>
    /// F35000 Apprisal Summary Comp
    /// </summary>
    public static class F35000ApprisalSummaryComp
    {
        #region Insert/Update Value Slice

        /// <summary>
        /// F35000_s the update value slice.
        /// </summary>
        /// <param name="valueSliceId">The value slice ID.</param>
        /// <param name="valueSliceHeaderItems">The value slice header items.</param>
        /// <param name="userId">user id</param>
        /// <returns>Primary Key Id or Error Id.</returns>
        public static int F35000_InsertOrUpdateValueSlice(int? valueSliceId, string valueSliceHeaderItems, int userId)
        {
            int errorId;
            Hashtable ht = new Hashtable();
            ht.Add("@ValueSliceID", valueSliceId);
            ht.Add("@ValueSliceHeaderItems", valueSliceHeaderItems);
            ht.Add("@UserID", userId);
            errorId = Utility.FetchSPExecuteKeyId("f35000_pcins_ValueSlice", ht);
            return errorId;
        }

        #endregion Insert/Update Value Slice

        #region Get Appraisal Summary Details

        /// <summary>
        /// F1410_s the get owner receipting.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>Returns appraisalSummary DataSet</returns>
        public static F35000AppraisalSummaryData F35000_GetAppraisalSummaryObjects(int parcelId)
        {
            F35000AppraisalSummaryData appraisalSummaryDataSet = new F35000AppraisalSummaryData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            string[] tableName = new string[] { appraisalSummaryDataSet.GetObjectSummaryTable.TableName, appraisalSummaryDataSet.GetSliceSummaryTable.TableName, appraisalSummaryDataSet.GetParcelValidTable.TableName };
            Utility.LoadDataSet(appraisalSummaryDataSet, "f35000_pcget_AppraisalSummary", ht, tableName);
            return appraisalSummaryDataSet;
        }

        #endregion Get Appraisal Summary Details


        #region Object Total Value
        /// <summary>
        /// F35000_s the object total.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns></returns>
        public static F35000AppraisalSummaryData F35000_ObjectTotal(int parcelId)
        {
            
            F35000AppraisalSummaryData appraisalValue = new F35000AppraisalSummaryData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            Utility.LoadDataSet(appraisalValue.f35000ObjectTotal, "f35000_pcget_ObjectTotal", ht);
          return appraisalValue ;
            
        } 
        #endregion

        #region Insert Object

        /// <summary>
        /// F35000_s the insert object.
        /// </summary>
        /// <param name="parcelId">The parcel ID.</param>
        /// <param name="objectTypeId">The object type ID.</param>
        /// <param name="description">The description.</param>
        /// <param name="userId">userId</param>
        /// <returns>Primary Key Id if Success else Error Id</returns>        
        public static int F35000_InsertObject(int parcelId, Int16 objectTypeId, string description, int userId)
        {
            int errorId;
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@ObjectTypeID", objectTypeId);
            ht.Add("@Description", description);
            ht.Add("@UserID", userId);
            errorId = Utility.FetchSPExecuteKeyId("f35000_pcins_Object", ht);
            return errorId;
        }

        #endregion

        #region List Object Slice Types

        /// <summary>
        /// F35000_s the list object slice types.
        /// </summary>
        /// <returns>DataSet Contains the List Object and Slice Types</returns>
        public static F35000AppraisalSummaryData F35000_ListObjectSliceTypes(int? parcelId)
        {
            F35000AppraisalSummaryData appraisalSummaryDataSet = new F35000AppraisalSummaryData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
             string[] tableName = new string[] { appraisalSummaryDataSet.ListObjectTypes.TableName, appraisalSummaryDataSet.ListSliceTypes.TableName };
             Utility.LoadDataSet(appraisalSummaryDataSet, "f35000_pclst_Object_Slice_Types", ht, tableName);
            //Utility.FillDataSet(appraisalSummaryDataSet.ListObjectTypes, "f35000_pclst_Object_Slice_Types", ht);
            return appraisalSummaryDataSet;
        }

        #endregion

        #region List Slice Types
        /// <summary>
        /// F35000_s the list slice types.
        /// </summary>
        /// <returns>DataSet Contains the List  Slice Types</returns>
        public static F35000AppraisalSummaryData F35000_ListSliceTypes(int objectId)
        {
            F35000AppraisalSummaryData appraisalSummaryDataSet = new F35000AppraisalSummaryData();
            Hashtable ht = new Hashtable();
            ht.Add("@ObjectID", objectId);
            Utility.LoadDataSet(appraisalSummaryDataSet.ListSliceTypes, "f35000_pclst_Slice_Types", ht);
            return appraisalSummaryDataSet;
        }
        #endregion

        #region CheckUser

        /// <summary>
        /// F35000_s the check appraisal summary user.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <param name="objectId">The object id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The appraisal summary dataset.</returns>
        public static F35000AppraisalSummaryData F35000_CheckAppraisalSummaryUser(int valueSliceId, int objectId, int userId)
        {
            F35000AppraisalSummaryData appraisalSummaryDataSet = new F35000AppraisalSummaryData();
            Hashtable ht = new Hashtable();
            if (valueSliceId > 0)
            {
                ht.Add("@ValueSliceID", valueSliceId);
            }
            if (objectId > 0)
            {
                ht.Add("@ObjectID", objectId);
            }
            ht.Add("@UserID", userId);
            string[] tableName = new string[] {appraisalSummaryDataSet.f35000_checkAppraisalUserTable.TableName};
            Utility.LoadDataSet(appraisalSummaryDataSet, "f35000_pcchk_AppraisalSummary", ht, tableName);
            return appraisalSummaryDataSet;
        }
        #endregion CheckUser

        #region Save Appraisal

        /// <summary>
        /// F35000_s the save appraisal.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="propertiesXML">The properties XML.</param>
        /// <param name="userId">The user id.</param>
        public static void F35000_SaveAppraisal(int parcelId, string propertiesXML, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@PropertiesXML", propertiesXML);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f35000_pcupd_AppraisalSummaryProperties", ht);
        }

        #endregion Save Appreaisal

    }
}
