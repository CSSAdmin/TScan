// -------------------------------------------------------------------------------------------
// <copyright file="F84401WorkItem.cs" company="Congruent">
//   Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update F84401</summary>
// Release history
// **********************************************************************************
// Date              Author             Description
// ----------        ---------        ---------------------------------------------------------
// 18/4/2008        D.LathaMaheswari    Created
// -------------------------------------------------------------------------------------------
namespace D84401
{
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.Helper;

    /// <summary>
    /// F84401 WorkItem
    /// </summary>
    public class F84401WorkItem : WorkItem
    {
        #region F84401 Signs Properties

        #region Get Signs Properties

        /// <summary>
        /// F84401_s the get signs properties.
        /// </summary>
        /// <param name="featureId">The feature id.</param>
        /// <returns>DataSet returns Signs  Properties</returns>
        public F84401SignsPropertyData F84401_GetSignsProperties(int featureId)
        {
            return WSHelper.F84401_GetSignsProperties(featureId);
        }

        #endregion Get Signs Properties

        #region Save Signs Properties

        /// <summary>
        /// F84401_s the save signs properties.
        /// </summary>
        /// <param name="featureId">The feature id.</param>
        /// <param name="signsProperties">The signs properties.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>The Sign id.</returns>
        public int F84401_SaveSignsProperties(int featureId, string signsProperties, int userId)
        {
            return WSHelper.F84401_SaveSignsProperties(featureId, signsProperties, userId);
        }

        #endregion Save Signs Properties

        #region Delete Signs Properties

        /// <summary>
        /// F84401_s the delete signs properties.
        /// </summary>
        /// <param name="featureId">The feature id.</param>
        /// <param name="userId">The user id.</param>
        public void F84401_DeleteSignsProperties(int featureId, int userId)
        {
            WSHelper.F84401_DeleteSignsProperties(featureId, userId);
        }

        #endregion Delete Signs Properties

        #endregion F84401 Signs Properties

        #region F8000 GDoc Commons

        #region Get GDocStreet

        /// <summary>
        /// To Load GDoc Street ComboBoxs.
        /// </summary>
        /// <returns>Typed DataSet Containg the details about GDoc User, Diameter, Business, Street and PropertyReference</returns>
        public GDocCommonData ListStreets()
        {
            return WSHelper.wListStreets();
        }

        #endregion Get GDocStreet

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

        #endregion F8000 GDoc Commons

        #region GetConfigValue

        /// <summary>
        /// Gets the Image Patha
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The dataset containing the Config value.</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        #endregion GetConfigValue

        #region WorkItems
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
        #endregion WorkItems
    }
}
