
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
    /// F84123WorkItem
    /// </summary>
    public class F84123WorkItem : WorkItem 
    {

        #region F84123 Sanitary Pipe Properties

        #region Get Sanitary Pipe Properties

        /// <summary>
        ///  To Load F84123 Sanitary Pipe Properties
        /// </summary>
        /// <param name="pipeId">The pipe ID.</param>
        /// <returns>Typed DataSet Containing All the Sanitary Pipe Properties Details</returns>
        public F84123SanitaryPipePropertiesData F84123_GetSanitaryPipeProperties(int pipeId)
        {
            return WSHelper.F84123_GetSanitaryPipeProperties(pipeId);
        }

        #endregion Get Sanitary Pipe Properties

        #region Save Sanitary Pipe Properties

        /// <summary>
        /// To Save F84123 Sanitary Pipe Properties.
        /// </summary>
        /// <param name="pipeId">The pipe ID.</param>
        /// <param name="sanitaryPipeProperties">The XML string Containing All values in Sanitary Pipe Properties.</param>
        /// <returns>The integer value containing pipe id</returns>
        public int F84123_SaveSanitaryPipeProperties(int pipeId, string sanitaryPipeProperties, int userId)
        {
            return WSHelper.F84123_SaveSanitaryPipeProperties(pipeId, sanitaryPipeProperties, userId);
        }

        #endregion Save Sanitary Pipe Properties

        #region Delete Sanitary Pipe Properties

        /// <summary>
        /// To Delete F84123 Sanitary Pipe Properties.
        /// </summary>
        /// <param name="pipeId">The Pipe Id</param>
        public void F84123_DeleteSanitaryPipeProperties(int pipeId, int userId)
        {
            WSHelper.F84123_DeleteSanitaryPipeProperties(pipeId, userId);
        }

        #endregion Delete Sanitary Pipe Properties

        #endregion F84123 Sanitary Pipe Properties

        #region GetGDocEventHeader

        /// <summary>
        /// Gets the GDoc event header.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns>Typed dataset containing the Event,Event date,Work Order and Is complete. </returns>
        public GDocEventHeaderData GetGDocEventHeader(int eventId)
        {
            return WSHelper.GetGDocEventHeader(eventId);
        }

        #endregion GetGDocEventHeader

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
