namespace D3200
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
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;

    public class F3201WorkItem : WorkItem
    {

        #region WorkItemEvents

        /// <summary>
        /// Called when [run started].
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Called when [activated].
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        #endregion WorkItemEvents

        /// <summary>
        /// F3201_s the get sketch link data.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Sketch Data Link</returns>
        public F3201SketchLinkData F3201_GetSketchLinkData(int parcelId, int userId)
        {
            return WSHelper.F3201_GetSketchLinkData(parcelId, userId);
        }

        /// <summary>
        /// F3201_s the save sketch link data.
        /// </summary>
        /// <param name="linkXML">The link XML.</param>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Error Message</returns>
        public string F3201_SaveSketchLinkData(string linkXML, int parcelId, int userId)
        {
            return WSHelper.F3201_SaveSketchLinkData(linkXML, parcelId, userId);
        }
    }
}
