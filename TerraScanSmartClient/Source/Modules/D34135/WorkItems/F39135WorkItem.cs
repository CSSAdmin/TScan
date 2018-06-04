// -------------------------------------------------------------------------------------------------
// <copyright file="F39135WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>// 
// <summary>
// WorkItem
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//***********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//  
//***********************************************************************************************/


namespace D34135
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.SmartParts;
    using TerraScan.Helper;
 
    public class F39135WorkItem : WorkItem 
    {
        #region F36035LandDetails

        /// <summary>
        /// F36035_s the list land details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Type Dataset Returns LandDetails</returns>
        public F39135LandData F39135_LandDetails(int valueSliceId)
        {
            return WSHelper.F39135_LandDetails(valueSliceId);
        }
     
        /// <summary>
        /// F36035_s the list land type details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Type Dataset Returns LandDetails</returns>
        public F39135LandData F39135_Landtypes(int valueSliceId, int rollYear)
        {
            return WSHelper.F39135_Landtypes(valueSliceId,rollYear);
        }
        
        /// <summary>
        /// F36035_s the delete land details.
        /// </summary>
        /// <param name="luid">The luid.</param>
        /// <param name="userId">The user id.</param>
        public void F36035_DeleteLandDetails(int luid, int userId)
        {
            WSHelper.F36035_DeleteLandDetails(luid, userId);
        }

        /// <summary>
        /// Gets the F39135_LandUseDetails        
        /// </summary>
        public  F39135LandData F39135_LandUseTypes(int valueSliceId)
        {
            return WSHelper.F39135_LandUseTypes(valueSliceId);   
        }
      
        /// <summary>
        /// F39135_s the get CalculatedBaseValue.
        /// </summary>
        public F39135LandData F39135_CalculatedBaseValue(string LandCode, int adjustmentTypeID, decimal units, decimal baseCostUnit, decimal adjustment, int? AglandID, int valueSliceId)
        {
            return WSHelper.F39135_CalculatedBaseValue(LandCode, adjustmentTypeID, units, baseCostUnit, adjustment, AglandID, valueSliceId);
        }
        /// <summary>
        /// F36035_s the list land type details.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>Type Dataset Returns LandDetails</returns>
        public F36035LandData F36035_ListLandTypeDetails(int valueSliceId)
        {
            return WSHelper.F36035_ListLandTypeDetails(valueSliceId);
        }

        #endregion

        #region List Influence Types


        /// <summary>
        /// F36035_s the insert land details.
        /// </summary>
        /// <param name="luid">The luid.</param>
        /// <param name="landUnitItems">The land unit items.</param>
        /// <param name="influenceItems">The influence items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>LandDetails</returns>
        public int F39135_InsertLandDetails(int luid, string landUnitItems, string influenceItems, int userId)
        {
            return WSHelper.F39135_InsertLandDetails(luid, landUnitItems, influenceItems, userId);
        }

        /// <summary>
        /// F36035_s the list influence type.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>The influence type dataset</returns>
        public F36035LandData F36035_ListInfluenceType(int valueSliceId)
        {
            return WSHelper.F36035_ListInfluenceType(valueSliceId);
        }

        #endregion List Influence Types


        #region Weighted Rating
        ///<summary>
        /// F39135 Weighted Rating 
        /// </summary>
        public F39135LandData F39135_WeightedRating(string landCode, decimal units, int? landUse, int valueSliceId)
        {
            return WSHelper.F39135_WeightedRating(landCode, units, landUse, valueSliceId);     
        }

         #endregion


        #region GetLandTotals
        ///<summary>
        /// F39135_Land Totals
        /// </summary>
        public F39135LandData F39135_GetLandTotals(int valueSliceId)
        {
            return WSHelper.F39135_GetLandTotals(valueSliceId);  
        }




        #endregion



        ///<summary>
        /// F39135 List Adjustment Type
        /// </summary>
        public F39135LandData F39135_adjustmentTypes()
        {
            return WSHelper.F39135_adjustmentTypes();  
        }

         /// <summary>
        /// F36035_s the get land code.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="landType1">The land type1.</param>
        /// <param name="landType2">The land type2.</param>
        /// <param name="landType3">The land type3.</param>
        /// <param name="valuesliceId">The valueslice id.</param>
        /// <returns>F36035LandData</returns>
        public F36035LandData   F36035_GetLandCode(int rollYear, int landType1, int landType2, int landType3, int valuesliceId, int? AglandID)
        {
            return WSHelper.F36035_GetLandCode(rollYear, landType1, landType2, landType3, valuesliceId, AglandID);
        }

        /// <summary>
        /// F36035_s the get land code base value.
        /// </summary>
        /// <param name="landCode">The land code.</param>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>F36035LandData</returns>
        public F36035LandData F36035_GetLandCodeBaseValue(string landCode, int valueSliceId, int? AglandID)
        {
            return WSHelper.F36035_GetLandCodeBaseValue(landCode, valueSliceId, AglandID);
        }

        #region AttachmentAndComment

        /// <summary>
        /// Gets the Attachments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns> The count of comments.</returns>
        public int GetAttachmentCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetAttachmentCount(formId, keyId, userId);
        }

        /// <summary>
        /// Gets the comments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns> The count of comments.</returns>
        public CommentsData.GetCommentsCountDataTable GetCommentsCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetCommentsCount(formId, keyId, userId).GetCommentsCount;
        }

        #endregion AttachmentAndComment

        #region Work Item Methods

        /// <summary>
        /// Fires the <see cref="RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="Run"/>
        /// method is called on the <see cref="WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Fires the <see cref="Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }
        #endregion
    }
}
