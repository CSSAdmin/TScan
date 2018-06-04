

namespace TerraScan.UI
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.ObjectBuilder;
    using System.Data;
    /// <summary>
    /// controller
    /// </summary>
    public class NavigationPanelSmartPartController : Controller 
    {
        /// <summary>
        /// property to get the workitem
        /// </summary>
        public new MainWorkItem WorkItem
        {
            get { return base.WorkItem as MainWorkItem; }
        }

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <value>The menu.</value>
        public DataSet Menu
        {
            get { return (DataSet)WorkItem.State["Menu"]; }
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static DataSet GetFormPermissions(int userId, int applicationId)
        {
            return MainWorkItem.GetFormPermissions(userId, applicationId);
        }

        
    }
}
