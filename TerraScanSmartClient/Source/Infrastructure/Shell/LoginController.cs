//--------------------------------------------------------------------------------------------
// <copyright file="LoginController.cs" company="Congruent">
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
//*********************************************************************************/
namespace TerraScan.UI
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.ObjectBuilder;
    using System.Data;
    using TerraScan.BusinessEntities;

    /// <summary>
    /// LoginController
    /// </summary>
    [CLSCompliantAttribute(false)]
    public class LoginController : Controller
    {
        #region Properties

        /// <summary>
        /// Get Property to Retrieve WorkItem
        /// </summary>
        public new MainWorkItem WorkItem
        {
            get { return base.WorkItem as MainWorkItem; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Method To Validate The User
        /// </summary>
        /// <param name="userName">userName to Validate</param>
        /// <param name="password">password to Validate</param>
        /// <param name="domainName">domainName to Validate</param>
        /// <param name="authenticationState">if set to <c>true</c> [authentication state].</param>
        /// <returns>returns the login status</returns>
        public static string ValidateUser(string userName, string password, string domainName, bool authenticationState)
        {
            return MainWorkItem.AuthorizeUser(userName, password, domainName, authenticationState);
        }
        /// <summary>
        /// <param name="userFullName">User FullName</param>
        /// <returns>NetName for a particular fullname</returns>
        public static DataSet GetUserNetName(string userFullName)
        {
            return MainWorkItem.GetUserNetName(userFullName);
        }

        /// <summary>
        /// <param name="userFullName">User FullName</param>
        /// <returns>NetName for a particular fullname</returns>
        public static CommentsData LoginConfigDetails(string ConfigName)
        {
            return MainWorkItem.LoginConfigDetails(ConfigName);
        }

        #endregion
    }
}
