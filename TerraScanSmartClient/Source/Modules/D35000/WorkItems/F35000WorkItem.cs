//--------------------------------------------------------------------------------------------
// <copyright file="F35000WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Appraisal Summary.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 02 April 07   	Shiva M     	    Created
// 05 April 07      Vinoth              Web Methods
//**********************************************************************************************/

namespace D35000
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F35000 Apprisal Summary WorkItem
    /// </summary>
    public class F35000WorkItem : WorkItem
    {
        #region GetForm Detials

        /// <summary>
        /// Gets the form details.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>FormDetails DataSet</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

        #endregion

        #region Insert/Update Value Slice

        /// <summary>
        /// F35000_s the update value slice.
        /// </summary>
        /// <param name="valueSliceID">The value slice ID.</param>
        /// <param name="valueSliceHeaderItems">The value slice header items.</param>
        /// <returns>Primary Key Id or Error Id.</returns>
        public int F35000_InsertOrUpdateValueSlice(int? valueSliceID, string valueSliceHeaderItems,int userId)
        {
            return WSHelper.F35000_InsertOrUpdateValueSlice(valueSliceID, valueSliceHeaderItems, userId);
        }

        #endregion

        #region GetAppraisalSummaryObjects

        /// <summary>
        /// F35000_s the get appraisal summary objects.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>F35000AppraisalSummary DataSet</returns>
        public F35000AppraisalSummaryData F35000_GetAppraisalSummaryObjects(int parcelId)
        {
            return WSHelper.F35000_GetAppraisalSummaryObjects(parcelId);
        }

        #endregion GetAppraisalSummaryObjects

        #region F35000_CheckAppraisalSummaryUser

        /// <summary>
        /// F35000_s the get appraisal summary objects.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns>F35000AppraisalSummary DataSet</returns>
        public F35000AppraisalSummaryData F35000_CheckAppraisalSummaryUser(int valueSliceId,int objectId,int userId)
        {
            return WSHelper.F35000_CheckAppraisalSummaryUser(valueSliceId, objectId, userId);
        }

        #endregion F35000_CheckAppraisalSummaryUser

        #region Insert Object

        /// <summary>
        /// F35000_s the insert object.
        /// </summary>
        /// <param name="parcelID">The parcel ID.</param>
        /// <param name="objectTypeID">The object type ID.</param>
        /// <param name="description">The description.</param>
        /// <returns>Primary Key Id if Success else Error Id</returns>
        public int F35000_InsertObject(int parcelID, Int16 objectTypeID, string description,int userID)
        {
            return WSHelper.F35000_InsertObject(parcelID, objectTypeID, description, userID);
        }

        #endregion

        #region SaveAppraisal
        /// <summary>
        /// F35000_s the SaveAppraisal object.
        /// </summary>
        /// <param name="parcelID">The parcel ID.</param>
        /// <param name="ProperitiesXML">The ProperitiesXML.</param>
        public void F35000_SaveAppraisal(int parcelId, string propertiesXML, int userId)
        {
            WSHelper.F35000_SaveAppraisal(parcelId, propertiesXML, userId);      
        }

        #endregion SaveAppraisal

        #region List Object Slice Types

        /// <summary>
        /// F35000_s the list object slice types.
        /// </summary>
        /// <returns>DataSet Contains the List Object and Slice Types</returns>
        public F35000AppraisalSummaryData F35000_ListObjectSliceTypes(int parcelId)
        {
            return WSHelper.F35000_ListObjectSliceTypes(parcelId);
        }

        /// <summary>
        /// F35000_s the object total.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <returns></returns>
        public F35000AppraisalSummaryData F35000_ObjectTotal(int parcelId)
        {
            return WSHelper.F35000_ObjectTotal(parcelId);
        }
        #endregion

        #region List Slice Types

        ///<summary>
        ///F3500_s list Slice Types
        /// </summary> 
        /// <returns>DataSet contains List Slice Types based on objectID0</returns>
        public F35000AppraisalSummaryData F35000_ListSliceTypes(int objectId)
        {
            return WSHelper.F35000_ListSliceTypes(objectId);   
        }
        #endregion

        #region To Get Configuration Value

        /// <summary>
        /// Gets the config Roll Year.
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>GetConfigDetails</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        #endregion To Get Configuration Value

        #region Protected Methods

        /// <summary>
        /// Called when [run started].
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Called when [activated].
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        #endregion
    }
}
