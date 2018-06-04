// -------------------------------------------------------------------------------------------
// <copyright file="F84723WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F84723</summary>
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
    /// F84723WorkItem Class file
    /// </summary>
    public class F84723WorkItem : WorkItem
    {
        #region F84723 Water Hydrant Properties

        #region Get Water Hydrant Properties

        /// <summary>
        /// To Load Water Hydrant Properties
        /// </summary>
        /// <param name="hydrantId">The hydrantId.</param>
        /// <returns>Typed DataSet Containing the Water Hydrant Properties Details.</returns>
        public F84723WaterHydrantPropertiesData F84723_GetWaterHydrantProperties(int hydrantId)
        {
            return WSHelper.F84723_GetWaterHydrantProperties(hydrantId);
        }

        #endregion Get Water Hydrant Properties

        #region Check Main Valve ID

        /// <summary>
        /// To Check the Main Valve ID
        /// </summary>
        /// <param name="mainValveId">The main valve id.</param>
        /// <returns>
        /// The Integer Value containing whether Main Valve Id exists are not
        /// </returns>
        public int F84723_CheckMainValveId(int mainValveId)
        {
            return WSHelper.F84723_CheckMainValveId(mainValveId);
        }

        #endregion Check Main Valve ID

        #region Save Water Hydrant Properties

        /// <summary>
        /// To Save Water Hydrant Properties.
        /// </summary>
        /// <param name="hydrantId">The hydrant id.</param>
        /// <param name="waterHydrantPropties">The XML String containing the Water Hydrant Properties Details.</param>
        /// <returns>The integer valu containing the hydrantId</returns>
        public int F84723_SaveWaterHydrantProperties(int hydrantId, string waterHydrantPropties, int userId)
        {
            return WSHelper.F84723_SaveWaterHydrantProperties(hydrantId, waterHydrantPropties, userId);
        }

        #endregion Save Water Hydrant Properties

        #region Delete Water Hydrant Properties

        /// <summary>
        /// To Delete Water Hydrant Properties.
        /// </summary>
        /// <param name="hydrantId">hydrantId</param>
        public void F84723_DeleteWaterHydrantProperties(int hydrantId, int userId)
        {
            WSHelper.F84723_DeleteWaterHydrantProperties(hydrantId, userId);
        }

        #endregion Delete Water Hydrant Properties

        #endregion F84723 Water Hydrant Properties

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
