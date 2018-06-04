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


namespace D3200
{
   public class F32012WorkItem : WorkItem 
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
        /// F32012_s the get catalog data.
        /// </summary>
        /// <param name="valueSliceId">The value slice id.</param>
        /// <returns>catalog Data</returns>
        public F32012CatalogData F32012_GetCatalogData(int valueSliceId)
        {
            return WSHelper.F32012_GetCatalogData(valueSliceId);
        }

        /// <summary>
        /// F32012_s the save catalog.
        /// </summary>
        /// <param name="objectId">The object id.</param>
        /// <param name="catalogData">The catalog data.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Data Set</returns>
        public DataSet F32012_SaveCatalog(int objectId, string catalogData, int userId)
        {
            return WSHelper.F32012_SaveCatalog(objectId, catalogData, userId);
        }
    }
}
