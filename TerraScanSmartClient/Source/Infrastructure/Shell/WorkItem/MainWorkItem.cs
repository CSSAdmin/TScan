//--------------------------------------------------------------------------------------------
// <copyright file="MainWorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the LoginController.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                              	    Created
// 02 June 06        Suganth/Shiva      Login Contoller for Login Form
// 25 July 06       Thilak Raj          AuthenticationState flag added to check AD
//*********************************************************************************/

namespace TerraScan.UI
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.ObjectBuilder;
    using System.Web.Services.Protocols;
    using System.Data;
    using TerraScan.Helper;
    using TerraScan.Utilities;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Common;
    using System.Configuration;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// Main root Work Item
    /// </summary>
    public class MainWorkItem : WorkItem
    {
        #region Public Methods


        /// <summary>
        /// Used To get the Net_Name for a particular User
        /// </summary>
        /// <param name="userFullName">User FullName</param>
        /// <returns>NetName for a particular fullname</returns>
        public static DataSet GetUserNetName(string userFullName)
        {
            return WSHelper.GetUserNetName(userFullName); 
        }

        /// <summary>
        /// Method to Authorize the User Credential
        /// </summary>
        /// <param name="userName">userName to Validate</param>
        /// <param name="password">passWord to Validate</param>
        /// <param name="domainName">domainName to Validate</param>
        /// <param name="authenticationState">if set to <c>true</c> [authentication state].</param>
        /// <returns>returns the Login Status</returns>
        public static string AuthorizeUser(string userName, string password, string domainName, bool authenticationState)
        {
            Application.DoEvents();
            string domainUserName = string.Empty; 
            if (authenticationState)
            {
               
                /// Get the user name from the domainName
                if((domainName.LastIndexOf("\\") >= 0 ) &&  (domainName.Length - domainName.LastIndexOf("\\") ) >0 ) 
                {
                    domainUserName = domainName.Substring(domainName.LastIndexOf("\\") + 1, (domainName.Length - domainName.LastIndexOf("\\")) - 1);
                }
                else
                {
                    domainUserName = string.Empty;   
            }


                if (!WSHelper.ValidateUser(domainUserName, password))
                {
                    return SharedFunctions.GetResourceString("LoginError");
                }
            }
            //// Implements User With Different domain
           // string netUserName = domainName + @"\" + userName;
           // DataSet userInfoDataset = WSHelper.GetUserInformation(domainName, TerraScanCommon.ApplicationId);
            DataSet userInfoDataset = WSHelper.GetUserInformation(userName, TerraScanCommon.ApplicationId);
            if (userInfoDataset.Tables[0].Rows.Count > 0)
            {
                TerraScanCommon.UserId = Convert.ToInt32(userInfoDataset.Tables[0].Rows[0]["UserId"].ToString());
                TerraScanCommon.UserName = userInfoDataset.Tables[0].Rows[0]["Name_display"].ToString();
                TerraScanCommon.Administrator = Convert.ToBoolean(userInfoDataset.Tables[0].Rows[0]["IsAdministrator"].ToString());
                TerraScanCommon.ApplicationId = Convert.ToInt32(userInfoDataset.Tables[0].Rows[0]["ApplicationId"].ToString());
                     return "success";
            }
            else
            {
                return SharedFunctions.GetResourceString("LoginDenied");
            }
        }

        /// <summary> 
        /// Updates the application status.
        /// </summary>
        /// <param name="checkedOutStatus">if set to <c>true</c> [checked out status].</param>
        /// <param name="onlineStatus">if set to <c>true</c> [online status].</param>
        /// <param name="userId">The user id.</param>
        /// <returns>int</returns>
        public int UpdateApplicationStatus(bool checkedOutStatus, bool onlineStatus, int userId)
        {
            return WSHelper.F9065_UpdateApplicationStatus(checkedOutStatus, onlineStatus, userId);
        }

        /// <summary>
        /// Method to Get the Menu Items for the User
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>DataSet Which Holds the MenuItems for the CurrentUser</returns>
        public DataSet GetMenuItems(int userId, int applicationId)
        {
            DataSet menuDataSet = WSHelper.GetMenuItems(userId, applicationId);
            ////foreach (DataTable table in menuDataSet.Tables)
            ////{
            ////    table.Columns.Add("Active", typeof(int));
            ////    foreach (DataRow row in table.Rows)
            ////    {
            ////        row["Active"] = 0;
            ////    }
            ////}

            State["Menu"] = menuDataSet;
            return menuDataSet;
        }

        /// <summary>
        /// Method to Get the FormPermission for the CurrenUser
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>
        /// DataSet Which Holds the Permission Set for the Current User
        /// </returns>
        public static DataSet GetFormPermissions(int userId, int applicationId)
        {
            return WSHelper.GetFormPermissions(userId, applicationId);
        }

        /// <summary>
        /// method will set the Active Forms in the Mdi
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="active">Active Status</param>
        public void SetActiveForms(int form, int active)
        {
            ////if (string.IsNullOrEmpty(formName))
            ////{
            ////    throw new ArgumentNullException(Properties.Resources.EmptyFormError);
            ////}

            ////if (!string.IsNullOrEmpty(ConfigurationWrapper.CountyName))
            ////{
                ////formName = formName.Replace(ConfigurationWrapper.CountyName, "");
            ////}

            DataSet menuDataSet = (DataSet)this.State["FormItemsDataSet"];
            foreach (DataTable table in menuDataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Form"].ToString()))
                    {
                        if (Int32.Equals(Convert.ToInt32(row["Form"]), form))
                        {
                            row["Active"] = active;
                        }
                    }
                }
            }

            this.State["FormItemsDataSet"] = menuDataSet;
        }

        /// <summary>
        /// Loads the user control.
        /// </summary>
        /// <param name="userControlId">The user control ID.</param>
        /// <param name="formName">Name of the form.</param>
        /// <returns>Smart Part Controls</returns>
        public UserControl LoadUserControl(string userControlId, string formName)
        {
            if (this.Items.Contains(userControlId))
            {
                return (UserControl)this.Items.Get(userControlId);
            }
            else
            {
                string assemblyFullName = System.Reflection.Assembly.GetExecutingAssembly().FullName;
                string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name.ToString();
                System.Reflection.Assembly assembly = System.Reflection.Assembly.Load(assemblyFullName);
                UserControl childForm = new UserControl();
                string tempUserControlID = assemblyName + "." + userControlId;
                childForm = (UserControl)assembly.CreateInstance(tempUserControlID);
                this.Items.Add(childForm, formName + userControlId);
                return childForm;
            }
        }

        /// <summary>
        /// Gets the Year
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The dataset containing the Year.</returns>
        public CommentsData GetConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }

        /// <summary>
        /// Gets the Year
        /// </summary>
        /// <param name="configName">Name of the config.</param>
        /// <returns>The dataset containing the Year.</returns>
        public static CommentsData LoginConfigDetails(string configName)
        {
            return WSHelper.GetConfigDetails(configName);
        }


        /// <summary>
        /// method to get the Form Items
        /// </summary>
        public DataSet GetFormItems
        {
            get
            {
                ////DataSet formItemsDataSet = WSHelper.GetFormItems();
                DataSet formItemsDataSet = (DataSet)this.State["FormItemsDataSet"];
                foreach (DataTable table in formItemsDataSet.Tables)
                {
                    if (!table.Columns.Contains("Active"))
                    {
                        table.Columns.Add("Active", typeof(int));
                        foreach (DataRow row in table.Rows)
                        {
                            row["Active"] = 0;
                        }
                    }
                }

                State["FormItemsDataSet"] = formItemsDataSet;
                return formItemsDataSet;
            }
        }

        #region GetFormTitle

        /// <summary>
        /// Gets the form title.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <returns>String with Title</returns>
        public string GetFormTitle(int formId)
        {
            return WSHelper.GetFormTitle(formId);
        }

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
        /// Gets the translated form details.
        /// </summary>
        /// <param name="formNo">The form no.</param>
        /// <param name="keyValue">The key value.</param>
        /// <returns>SupportFormData Dataset</returns>
        public SupportFormData.FormCallTranslateDataTable GetTranslatedFormDetails(int formNo, string keyValue)
        {
            return WSHelper.GetTranslatedFormDetails(formNo, keyValue).FormCallTranslate;
        }

        #endregion

        #endregion

        #region Override Methods

        /// <summary>
        /// Override Method for OnRunStarted
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        #endregion
    }
}
