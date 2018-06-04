// -------------------------------------------------------------------------------------------
// <copyright file="F36011WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F36011</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 2/11/2007        Malliga       ///Created
// 
// -------------------------------------------------------------------------------------------

namespace D36040
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using System.Windows.Forms;
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    public class F36041WorkItem : WorkItem
    {
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

        #region F36041CropDetails

        #region To Get Crop Details
        /// <summary>
        /// Gets the F36041_CropDetails        
        /// </summary>
        /// <param name="valueSliceID">valueSliceID</param>
        /// <returns>Type Dataset Returns CropDetails</returns>
        public F36041CropData F36041_ListCropDetails(int valueSliceId)
        {
            return WSHelper.F36041_CropDetails(valueSliceId);
        }

        #endregion 

        #region To Get CropCode Details
        /// <summary>
        /// Gets the F36041_CropCodeDetails        
        /// </summary>
        /// <param name="valueSliceID">valueSliceID</param>
        /// <returns>Type Dataset Returns CropCodeDetails</returns>
        public F36041CropData F36041_ListCropCodeDetails(int valueSliceId)
        {
            return WSHelper.F36041_CropCodeDetails(valueSliceId);
        }
        #endregion

        #region Save Crop Details
        /// <summary>
        /// Save Crop Details
        /// </summary>
        /// <param name="ValueSliceId">ValueSliceId</param>
        /// <param name="cropItems">cropItems</param>
        /// <param name="userId">UserID</param>
        /// <returns>integer</returns>
        public int F36041_SaveCropCodeDetails(int valueSliceId, string cropItems, int userId)
        {
            return WSHelper.F36041_SaveCropCodeDetails(valueSliceId, cropItems, userId);
        }
        #endregion

        #region F36041_DeleteCrop

        /// <summary>
        /// F36041_s the delete crop.
        /// </summary>
        /// <param name="cropId">The crop id.</param>
        /// <param name="userId">The user id.</param>
        public void F36041_DeleteCrop(int cropId, int userId)
        {
            WSHelper.F36041_DeleteCrop(cropId, userId);
        }

        #endregion F36041_DeleteCrop

        #region F36041_DeleteCropIds

        /// <summary>
        /// F36041_s the delete crop.
        /// </summary>
        /// <param name="cropId">The crop id.</param>
        /// <param name="userId">The user id.</param>
        public void F36041_DeleteCropIds(string cropIds, int userId)
        {
            WSHelper.F36041_DeleteCropIds(cropIds, userId);
        }

        #endregion F36041_DeleteCrop


        #endregion F36041CropDetails
    }
}
