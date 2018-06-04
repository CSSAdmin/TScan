//--------------------------------------------------------------------------------------------
// <copyright file="F84121WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F84121WorkItem.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 15 Dec 06        JYOTHI              Created
//*********************************************************************************/
namespace D84100
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
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F84121WorkItem
    /// </summary>
    public class F84121WorkItem : WorkItem
    {
        #region F84121 Sanitary Manhole Properties

        #region Get Sanitary Manhole Properties

        /// <summary>
        ///  To Load F84121 Sanitary Manhole properties.
        /// </summary>
        /// <param name="manholeId">The manhole ID.</param>
        /// <returns>Typed DataSet Containing All the Sanitary Manhole properties Details</returns>
        public F84121SanitaryManholePropertiesData F84121_GetSanitaryManholeProperties(int manholeId)
        {
            return WSHelper.F84121_GetSanitaryManholeProperties(manholeId);
        }

        #endregion

        #region Save Sanitary Manhole Properties

        /// <summary>
        /// To Save F84121 Sanitary Manhole properties.
        /// </summary>
        /// <param name="manholeId">The manhole ID.</param>
        /// <param name="sanitaryManholeProperties">The XML string Containing All values in Sanitary Manhole properties.</param>
        /// <returns>The integer value containing manhole id</returns>
        public int F84121_SaveSanitaryManholeProperties(int manholeId, string sanitaryManholeProperties, int userId)
        {
            return WSHelper.F84121_SaveSanitaryManholeProperties(manholeId, sanitaryManholeProperties, userId);
        }

        #endregion

        #region Delete Sanitary Manhole Properties

        /// <summary>
        /// To Delete F84121 Sanitary Manhole properties
        /// </summary>
        /// <param name="manholeId">The Manhole Id</param>
        public void F84121_DeleteSanitaryManholeProperties(int manholeId, int userId)
        {
            WSHelper.F84121_DeleteSanitaryManholeProperties(manholeId, userId);
        }

        #endregion

        #endregion

        #region F8000 GDoc Commons

        #region Get GDocBusiness

        /// <summary>
        /// To Load GDoc Business ComboBoxs.
        /// </summary>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public GDocCommonData F8000_GetGDocBusiness()
        {
            return WSHelper.F8000_GetGDocBusiness();
        }

        #endregion

        #region Get GDocDiameter

        /// <summary>
        /// To Load GDoc Diameter ComboBoxs.
        /// </summary>
        /// <param name="featureClassId">The FeatureClassId </param>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public GDocCommonData F8000_GetGDocDiameter(int featureClassId)
        {
            return WSHelper.F8000_GetGDocDiameter(featureClassId);
        }

        #endregion

        #region Get GDocPropertyReference

        /// <summary>
        /// To Load GDoc PropertyReference ComboBoxs.
        /// </summary>
        /// <param name="featureClassId">The FeatureClassId </param>
        /// <param name="refField">The Ref Field</param>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public GDocCommonData F8000_GetGDocPropertyReference(int featureClassId, string refField)
        {
            return WSHelper.F8000_GetGDocPropertyReference(featureClassId, refField);
        }

        #endregion

        #region Get GDocStreet

        /// <summary>
        /// To Load GDoc Street ComboBoxs.
        /// </summary>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public GDocCommonData F8000_GetGDocStreet()
        {
            return WSHelper.wListStreets();
        }

        #endregion

        #region Get GDocUser

        /// <summary>
        /// To Load GDoc User ComboBoxs.
        /// </summary>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public GDocCommonData F8000_GetGDocUser()
        {
            return WSHelper.F8000_GetGDocUser();
        }

        #endregion

        #endregion

        /// <summary>
        /// Gets the FormDetails
        /// </summary>
        /// <param name="form">Form</param>
        /// <param name="userId">The user id.</param>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.GetFormDetailsDataTable GetFormDetails(int form, int userId)
        {
            return WSHelper.GetFormDetails(form, userId).GetFormDetails;
        }

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
    }
}
