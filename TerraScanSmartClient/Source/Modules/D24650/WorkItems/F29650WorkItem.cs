namespace D24650
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

    public class F29650WorkItem : WorkItem
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

        #region F24650 Exemption

        #region Get Exemption Type

        /// <summary>
        /// Get Exemption Type
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>DataSet contains Exemption Types</returns>
        public F29650ExemptionData GetExemptionType(int eventId)
        {
            return WSHelper.GetExemptionType(eventId);
        }

        #endregion Get Exemption Type

        #region Get Exemption

        /// <summary>
        /// Get Exemption Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <returns>DataSet contains Exemption Details</returns>
        public F29650ExemptionData GetExemptionDetails(int eventId)
        {
            return WSHelper.GetExemptionDetails(eventId);
        }

        #endregion Get Exemption

        #region Get Exemption Loss

        /// <summary>
        /// Get Exemption Loss
        /// </summary>
        /// <param name="lossValue">Loss</param>
        /// <param name="maxValue">Maximum</param>
        /// <returns>Decimal</returns>
        public decimal GetExemptionLoss(decimal lossValue, decimal maxValue)
        {
            return WSHelper.GetExemptionLoss(lossValue, maxValue);
        }

        #endregion Get Exemption Loss

        #region Save Exemption

        /// <summary>
        /// Save Exemption Deatils
        /// </summary>
        /// <param name="exemptionElements">Exemption Details</param>
        /// <param name="userId">User ID</param>
        public void SaveExemptionDetails(string exemptionElements, int userId)
        {
            WSHelper.SaveExemptionDetails(exemptionElements, userId);
        }

        #endregion Save exemption

        #region Delete Exemption

        /// <summary>
        /// Delete Exemption Details
        /// </summary>
        /// <param name="eventId">Event ID</param>
        /// <param name="exemptionEventId">Exemption Event ID</param>
        /// <param name="userId">User ID</param>
        public void DeleteExemptionDetails(int eventId, int exemptionEventId, int userId)
        {
            WSHelper.DeleteExemptionDetails(eventId, exemptionEventId, userId);
        }

        #endregion Delete Exemption

        #endregion F24650 Exemption

    }
}
