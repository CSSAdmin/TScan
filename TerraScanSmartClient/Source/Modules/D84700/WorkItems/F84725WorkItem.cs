// -------------------------------------------------------------------------------------------
// <copyright file="F84725WorkItem.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F84725</summary>
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
    /// F84725WorkItem class file
    /// </summary>
    public class F84725WorkItem : WorkItem
    {
        #region F84725 Water Pipe Properties

        #region Get Water Pipe Properties

        /// <summary>
        /// To Load Water Pipe Properties
        /// </summary>
        /// <param name="pipeId">The Pipe Id</param>
        /// <returns>Typed DataSet Containing the Water Pipe Properties details</returns>
        public F84725WaterPipePropertiesData F84725_GetWaterPipeProperties(int pipeId)
        {
            return WSHelper.F84725_GetWaterPipeProperties(pipeId);
        }

        #endregion Get Water Pipe Properties

        #region Save Water Pipe Properties

        /// <summary>
        /// To Save water pipe properties.
        /// </summary>
        /// <param name="pipeId">The pipe id.</param>
        /// <param name="waterPipeProperties">The XML String Containing the Water Pipe Properties details.</param>
        /// <returns>the integer value containing the pipeid</returns>
        public int F84725_SaveWaterPipeProperties(int pipeId, string waterPipeProperties, int userId)
        {
            return WSHelper.F84725_SaveWaterPipeProperties(pipeId, waterPipeProperties, userId);
        }

        #endregion Save Water Pipe Properties

        #region Delete Water Pipe Properties

        /// <summary>
        /// To Delete water pipe properties.
        /// </summary>
        /// <param name="pipeId">the pipe Id</param>
        public void F84725_DeleteWaterPipeProperties(int pipeId, int userId)
        {
            WSHelper.F84725_DeleteWaterPipeProperties(pipeId, userId);
        }

        #endregion Delete Water Pipe Properties

        #endregion F84725 Water Pipe Properties

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
