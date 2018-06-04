
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

  
    public class F3200WorkItem:WorkItem 
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
        /// F3001_s the get object detail.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <returns>DataSet</returns>
        public DataSet F3200_SaveSketchData(int objectId, string sketchData, int userId)
        {
            return WSHelper.F3200_SaveSketchData(objectId, sketchData, userId); 
        }
    }
}
