// ---------------------------------------------------------------------------------------------------------------
// <copyright file="F36032WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F36032 LandCodes</summary>
// Release history
//*****************************************************************************************************************
// Date			    Author		       Description
// ----------		---------		   ----------------------------------------------------------------------------
// 14/09/2007       Shiva              Created
// 
// ----------------------------------------------------------------------------------------------------------------
namespace D36030
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;

    /// <summary>
    /// F36032 WorkItem Class file
    /// </summary>
    public class F36032WorkItem : WorkItem
    {
        #region F36032 Land Codes

        #region F36032_ListLandItems

        /// <summary>
        /// F36032_s the list land items.
        /// </summary>
        /// <param name="rollYear">The roll year.</param>
        /// <returns>The landCodeDataSet.</returns>
        public F36032LandCodesData F36032_ListLandItems(int? rollYear)
        {
            return WSHelper.F36032_ListLandItems(rollYear);
        }

        #endregion F36032_ListLandItems

        #region F36032_ListLandCodeDetails

        /// <summary>
        /// F36032_s the list land code details.
        /// </summary>
        /// <returns>the landCodesDataSet</returns>
        public F36032LandCodesData F36032_ListLandCodeDetails()
        {
            return WSHelper.F36032_ListLandCodeDetails();
        }

        #endregion F36032_ListLandCodeDetails        

        #region F36032_DeleteLandCode

        /// <summary>
        /// F36032_s the delete land code.
        /// </summary>
        /// <param name="landCodeID">The land code ID.</param>
        /// <param name="userID">The user ID.</param>
        /// <returns>Integer Value</returns>
        public int F36032_DeleteLandCode(int landCodeId, int userId)
        {
            return WSHelper.F36032_DeleteLandCode(landCodeId, userId);
        }

        #endregion F36032_DeleteLandCode

        #region F36032_SaveLandCodeDetails

        /// <summary>
        /// To save the land code deatils
        /// </summary>
        /// <param name="landCodeId">The land code id.</param>
        /// <param name="landItems">The land items.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// integer value containing the save land Code Id
        /// </returns>
        public int F36032_SaveLandCodeDetails(int? landCodeId, string landItems, int userId)
        {
            return WSHelper.F36032_SaveLandCodeDetails(landCodeId, landItems, userId);
        }

        #endregion F36032_SaveLandCodeDetails

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

        #endregion F36032 Land Codes

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
