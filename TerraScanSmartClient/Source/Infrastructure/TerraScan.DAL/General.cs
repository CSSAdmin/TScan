// -------------------------------------------------------------------------------------------
// <copyright file="General.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access comment related information</summary>
// Release history
// VERSION	DESCRIPTION
// -------------------------------------------------------------------------------------------

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;

    /// <summary>
    /// Main Class For General Components
    /// </summary>
    public static class General
    {
        #region MenuItems

        #region GetMenuItems

        /// <summary>
        /// get the menuItems depends  on the userid.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="applicationId">applicationID.</param>
        /// <returns>MenuItems DataSet</returns>
        public static DataSet GetMenuItems(int userId, int applicationId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@UserId", userId);
            ht.Add("@ApplicationID", applicationId);
            return DataProxy.FetchDataSet("f9002_pcget_Menu", ht);
        }
        
        #endregion 

        #region GetFormItems

        /// <summary>
        /// Gets the form items.
        /// </summary>
        /// <returns>returns dataset</returns>
        public static DataSet GetFormItems()
        {
            return DataProxy.FetchDataSet("f9002_pclst_Form");
        }

        #endregion

        #region GetFormTitle

        /// <summary>
        /// Gets the form title.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>String with Title</returns>
        public static string GetFormTitle(int formId)
        {
            object tempvalue = null;
            Hashtable ht = new Hashtable();
            ht.Add("@FormID", formId);

            tempvalue = DataProxy.FetchSpObject("f9001_pcget_TitleCaption", ht);

            if (tempvalue != null)
            {
                return tempvalue.ToString();
            }

            return string.Empty;
        }

        #endregion

        #endregion

        #region UserPermissions

        #region GetFormPermissions

        /// <summary>
        /// get the Form Permissions depends  on the userid.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="applicationId">applicationID.</param>
        /// <returns>MenuItems DataSet</returns>
        public static DataSet GetFormPermissions(int userId, int applicationId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@UserId", userId);
            ht.Add("@ApplicationID", applicationId);
            return DataProxy.FetchDataSet("f9002_pcget_FormPermission", ht);
        }

        #endregion
        #endregion 

        #region Get Working Day

        /// <summary>
        /// get next working day - depends on clode time.
        /// </summary>
        /// <returns>return today or next working day</returns>
        public static DateTime F9001_GetNextWorkingDay()
        {
            object tempvalue = null;
            Hashtable ht = new Hashtable();            
            tempvalue = Utility.FetchSpObject("f9001_pcget_GetWorkingDay", ht);

            if (tempvalue != null)
            {
                return Convert.ToDateTime(tempvalue);
            }

            return DateTime.Now;
        }

        #endregion
    }
}
