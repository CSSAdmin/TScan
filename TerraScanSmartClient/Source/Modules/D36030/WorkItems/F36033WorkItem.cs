// ---------------------------------------------------------------------------------------------------------------
// <copyright file="F36033WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F36033 Land Code Values</summary>
// Release history
//*****************************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ----------------------------------------------------------------------------
// 14/09/2007       M.Vijayakumar      Created
// 
// ----------------------------------------------------------------------------------------------------------------

namespace D36030
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

    /// <summary>
    /// F36033WorkItem Class file
    /// </summary>
    public class F36033WorkItem : WorkItem
    {
        #region F36033 Land Code Values

        #region F36033_ListLandCodeValues

        /// <summary>
        /// To List Land Code Values details.
        /// </summary>
        /// <returns>Returns typed dataset containing the entire land code values deatils </returns>
        public F36033LandCodesValuesData F36033_ListLandCodeValues()
        {
            return WSHelper.F36033_ListLandCodeValues();
        }

        #endregion F36033_ListLandCodeValues

        #region F36033_ListIndividualLandCodeValuesItems

        /// <summary>
        /// To List Individual Land Code Values Items.
        /// </summary>
        /// <returns>Returns Typed Dataset containing following datatable:
        /// GetAppRollYear -- containing the application roll year
        /// ListNeighborhoodType -- containing the Neighborhood Type
        /// ListLandCode -- containing the LandCode
        /// ListUnitType -- containing the Unit type
        /// </returns>
        public F36033LandCodesValuesData F36033_ListIndividualLandCodeValuesItems()
        {
            return WSHelper.F36033_ListIndividualLandCodeValuesItems();
        }

        #endregion F36033_ListIndividualLandCodeValuesItems

        #region F36033_ListNeighborhood
        /// <summary>
        /// F36033_s the type of the list neighborhood.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>ListNeighborhoodType -- containing the Neighborhood Type</returns>
        public F36033LandCodesValuesData F36033_ListNeighborhoodType(int rollYear)
        {
            return WSHelper.F36033_ListNeighborhoodType(rollYear);
        }
        #endregion F36033_ListNeighborhood

        #region F36033_DeleteLandCodeValue

        /// <summary>
        /// To Delete land code value.
        /// </summary>
        /// <param name="luvId">The luv id.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns>Integer value</returns>
        public int F36033_DeleteLandCodevalue(int luvId, int userId)
        {
            return WSHelper.F36033_DeleteLandCodevalue(luvId, userId);
        }

        #endregion F36033_DeleteLandCodeValue

        #region F36033_SaveLandCodeValue

        /// <summary>
        /// To save land code value.
        /// </summary>
        /// <param name="landUnqiueId">The land unqiue id.</param>
        /// <param name="landValueItems">The land value items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>saved key id</returns>
        public int F36033_SaveLandCodeValue(int? landUnqiueId, string landValueItems, int userId)
        {
            return WSHelper.F36033_SaveLandCodeValue(landUnqiueId, landValueItems, userId);
        }

        #endregion F36033_SaveLandCodeValue

        #region Attachement and Comment

        /// <summary>
        /// Gets the comments count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>GetCommentsCount</returns>
        public CommentsData.GetCommentsCountDataTable GetCommentsCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetCommentsCount(formId, keyId, userId).GetCommentsCount;
        }

        /// <summary>
        /// Gets the attachment count.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>GetAttachmentCount</returns>
        public int GetAttachmentCount(int formId, int keyId, int userId)
        {
            return WSHelper.GetAttachmentCount(formId, keyId, userId);
        }

        #endregion Attachement and Comment

        #endregion F36033 Land Code Values

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

        #endregion Work Item Methods
    }
}
