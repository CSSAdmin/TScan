// -------------------------------------------------------------------------------------------
// <copyright file="LoginComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access login related information</summary>
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
    /// Main class for Login Component
    /// </summary>
    public static class LoginComp
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="applicationId">The application id.</param>
        /// <returns>The dataset containing the user information.</returns>
        public static DataSet GetUserInformation(string userName, int applicationId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@UserName", userName);
            ht.Add("@ApplicationID", applicationId);
            return DataProxy.FetchDataSet("f9002_pcget_User", ht);
        }

       /// <summary>
       /// Used To get the Net_Name for a particular User
       /// </summary>
       /// <param name="userFullName">User FullName</param>
       /// <returns>NetName for a particular fullname</returns>
        public static DataSet GetUserNetName(string userFullName)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@UserFullName", userFullName);
            return DataProxy.FetchDataSet("f9002_pcget_NetName", ht);
        }

        /// <summary>
        /// Gets the config information.
        /// </summary>
        /// <returns>returns Dataset</returns>
        public static DataSet GetConfigInformation()
        {
            return DataProxy.FetchDataSet("f9001_pcget_ApplicationConfiguration");
        }

        /// <summary>
        /// Gets the state of the authentication.
        /// </summary>
        /// <returns>The Dataset</returns>
        public static DataSet GetAuthenticationState()
        {
            return DataProxy.FetchDataSet("f9001_pcget_GetAuthenticationState");
        }
    }
}
