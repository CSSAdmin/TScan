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
    public class ShellFormController :Controller 
    {
        /// <summary>
        /// Property to access workitem
        /// </summary>
        public new MainWorkItem WorkItem
        {
            get { return base.WorkItem as MainWorkItem; }
        }

        /// <summary>
        /// Method to get menuitems
        /// </summary>
        /// <param name="userId">The userid</param>
        /// <returns>Dataset contining menu items</returns>
        public DataSet GetMenuItems(int userId, int applicationId)
        {
            return WorkItem.GetMenuItems(userId, applicationId);
        }

        /// <summary>
        /// Method to get form permissions
        /// </summary>
        /// <param name="userId">The userid</param>
        /// <returns>Dataset contining form permission information</returns>
        public static DataSet GetFormPermissions(int userId, int applicationId)
        {
            return MainWorkItem.GetFormPermissions(userId, applicationId);
        }

        /// <summary>
        /// method to make the form active
        /// </summary>
        /// <param name="formName">The form to be made active</param>
        /// <param name="active">int value representing the form being active</param>
        public void SetActiveForms(int form, int active)
        {
            WorkItem.SetActiveForms(form, active);
        }

        /// <summary>
        /// method to get the Form Items
        /// </summary>
        public DataSet GetFormItems
        {
            get { return WorkItem.GetFormItems; }
        }
    }
}
