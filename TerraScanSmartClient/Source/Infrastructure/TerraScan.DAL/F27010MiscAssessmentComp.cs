// -------------------------------------------------------------------------------------------
// <copyright file="F27010MiscAssessmentComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F27000MiscAssessmentComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 28 Mar 2008      D.LathaMaheswari     Created
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
    /// F27010MiscAssessmentComp Class file
    /// </summary>
    public static class F27010MiscAssessmentComp
    {
        #region F27010 Misc Assessment

        #region GetRollYear
        /// <summary>
        /// F27010s the get roll year.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>Integer</returns>
        public static int F27010GetRollYear(int parcelId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            return Utility.FetchSPExecuteKeyId("f27010_pcget_ParcelRollYear", ht);
        }
        #endregion GetRollYear

        #region Get Assessment Type
        /// <summary>
        /// F27010s the type of the get assessment.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>DataSet</returns>
        public static F27010MiscAssessmentData F27010GetAssessmentType(int rollYear)
        {

            F27010MiscAssessmentData assessmentSelection = new F27010MiscAssessmentData();
            Hashtable ht = new Hashtable();
            ht.Add("@RollYear", rollYear);
            Utility.LoadDataSet(assessmentSelection.AssessmentTypeTable, "f27010_pclst_MADistrictType", ht);
            return assessmentSelection;
        }
        #endregion Get Assessment Type

        #region GetDistrict
        /// <summary>
        /// F27010s the get district.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="madTypeId">The mad type id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>DataSet</returns>
        public static F27010MiscAssessmentData F27010GetDistrict(int parcelId, int madTypeId, int rollYear)
        {

            F27010MiscAssessmentData districtSelection = new F27010MiscAssessmentData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@MADTypeID", madTypeId);
            ht.Add("@RollYear", rollYear);
            Utility.LoadDataSet(districtSelection.DistrictTable, "f27010_pclst_MADistrict", ht);
            return districtSelection;
        }
        #endregion GetDistrict

        #region Check Default District
        /// <summary>
        /// F27010s the check default district.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="madTypeId">The mad type id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>DataSet</returns>
        public static int F27010CheckDefaultDistrict(int parcelId, int madTypeId, int rollYear)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@MADTypeID", madTypeId);
            ht.Add("@RollYear", rollYear);
            return Utility.FetchSPExecuteKeyId("f27010_pcget_DefaultDistrict", ht);
        }
        #endregion Check Default District

        #region Get ToolTip Message
        /// <summary>
        /// F27010s the get message.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="madTypeId">The mad type id.</param>
        /// <param name="madDistrictId">The mad district id.</param>
        /// <returns></returns>
        public static F27010MiscAssessmentData F27010GetMessage(int parcelId, int madTypeId, int madDistrictId)
        {

            F27010MiscAssessmentData messageData = new F27010MiscAssessmentData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@MADTypeID", madTypeId);
            ht.Add("@MADistrictID", madDistrictId);
            Utility.LoadDataSet(messageData.GetMessageTable, "f27010_pcget_Message", ht);
            return messageData;
        }
        #endregion Get ToolTip Message

        #region GetMiscAssessment (MADType1)
        /// <summary>
        /// F27010s the get misc data.
        /// </summary>
        /// <param name="madDistrictId">The mad district id.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns></returns>
        public static F27010MiscAssessmentData F27010GetMiscData(int madDistrictId, int parcelId)
        {

            F27010MiscAssessmentData miscData = new F27010MiscAssessmentData();
            Hashtable ht = new Hashtable();
            ht.Add("@MADistrictID", madDistrictId);
            ht.Add("@ParcelID", parcelId);
            Utility.LoadDataSet(miscData.ListMiscAssessmentTable, "f27010_pclst_MAType1", ht);
            return miscData;
        }
        #endregion GetMiscAssessment (MADType1)

        #region GetMiscAssessment (Other MADType)
        
        /// <summary>
        /// F27010s the get other misc data.
        /// </summary>
        /// <param name="madDistrictId">The mad district id.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="procedureName">Name of the procedure.</param>
        /// <returns>The misc assessment dataset.</returns>
        public static F27010MiscAssessmentData F27010GetOtherMiscData(int madDistrictId, int parcelId, string procedureName)
        {
            F27010MiscAssessmentData miscData = new F27010MiscAssessmentData();
            Hashtable ht = new Hashtable();
            ht.Add("@MADistrictID", madDistrictId);
            ht.Add("@ParcelID", parcelId);
            Utility.LoadDataSet(miscData.ListMiscAssessmentTable, procedureName, ht);
            return miscData;
        }

        #endregion GetMiscAssessment (Other MADType)

        #region GetDefaultMiscData

        /// <summary>
        /// F27010s the get default misc data.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="madTypeId">The mad type id.</param>
        /// <returns>DataSet</returns>
        public static F27010MiscAssessmentData F27010GetDefaultMiscData(int parcelId, int madTypeId)
        {
            F27010MiscAssessmentData miscData = new F27010MiscAssessmentData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@MATypeID", madTypeId);
            string[] tableName = new string[] { miscData.AssessmentTypeTable.TableName, miscData.DistrictTable.TableName, miscData.ListMiscAssessmentTable.TableName, miscData.GetMessageTable.TableName };
            Utility.LoadDataSet(miscData, "f27010_pcget_MAssessment", ht, tableName);
            return miscData;
        }
        #endregion GetDefaultMiscData

        #region SaveMiscAssessment
        /// <summary>
        /// F27010_s the save misc assessment.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="miscType">Type of the misc.</param>
        /// <param name="madItem">The mad item.</param>
        /// <param name="miscItems">The misc items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Integer</returns>
        public static int F27010_SaveMiscAssessment(int parcelId, string miscType, string madItem, string miscItems, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@MATypeItem", miscType);
            ht.Add("@MADistrictItems", madItem);
            ht.Add("@AssessmentItems", miscItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f27010_pcins_MAssessment", ht);
        }
        #endregion SaveMiscAssessment

        #endregion F27010 Misc Assessment
    }
}
