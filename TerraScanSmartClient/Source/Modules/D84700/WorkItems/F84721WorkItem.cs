// -------------------------------------------------------------------------------------------
// <copyright file="F84721WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F84721</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
// 
// -------------------------------------------------------------------------------------------

namespace D84700
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
    /// F84721WorkItem Class file
    /// </summary>
    public class F84721WorkItem : WorkItem
    {
        #region F84721 Water Valve Properties

        #region Get Water Valve Properties

        /// <summary>
        ///  To Load F84721 Water valve properties.
        /// </summary>
        /// <param name="valveId">The valve ID.</param>
        /// <returns>Typed DataSet Containing All the Water valve properties Details</returns>
        public F84721WaterValvePropertiesData F84721_GetWaterValveProperties(int valveId)
        {
            return WSHelper.F84721_GetWaterValveProperties(valveId);
        }

        #endregion Get Water Valve Properties

        #region Save Water Valve Properties

        /// <summary>
        /// To Save F84721 Water valve properties.
        /// </summary>
        /// <param name="valveId">The valve ID.</param>
        /// <param name="waterValveProperties">The XML string Containing All values in Water valve properties.</param>
        /// <returns>The integer value containing valve id</returns>
        public int F84721_SaveWaterValveProperties(int valveId, string waterValveProperties, int userId)
        {
            return WSHelper.F84721_SaveWaterValveProperties(valveId, waterValveProperties, userId);
        }

        #endregion Save Water Valve Properties

        #region Delete Water Valve Properties

        /// <summary>
        /// To Delete F84721 Water valve properties
        /// </summary>
        /// <param name="valveId">The ValveId</param>
        public void F84721_DeleteWaterValveProperties(int valveId, int userId)
        {
            WSHelper.F84721_DeleteWaterValveProperties(valveId, userId);
        }

        #endregion Delete Water Valve Properties

        #endregion F84721 Water Valve Properties

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

        #endregion Get GDocBusiness

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

        #endregion Get GDocDiameter

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

        #endregion Get GDocPropertyReference

        #region Get GDocStreet

        /// <summary>
        /// To Load GDoc Street ComboBoxs.
        /// </summary>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public GDocCommonData wListStreets()
        {
            return WSHelper.wListStreets();
        }

        #endregion Get GDocStreet

        #region Get GDocUser

        /// <summary>
        /// To Load GDoc User ComboBoxs.
        /// </summary>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public GDocCommonData F8000_GetGDocUser()
        {
            return WSHelper.F8000_GetGDocUser();
        }

        #endregion Get GDocUser

        #endregion F8000 GDoc Commons

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
