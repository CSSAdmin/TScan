// -------------------------------------------------------------------------------------------
// <copyright file="UserManagementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access User and Group related information</summary>
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
    using TerraScan.BusinessEntities;

    /// <summary>
    /// Main Class for the UserManagement Component
    /// </summary>
    public static class UserManagementComp
    {
        #region Coding For User Tab
        #region GetUserGroupDetails

        /// <summary>
        /// Gets the comments based on the keyid, formid and userid.
        /// </summary>
        /// <param name="applicationId">applicationId</param>
        /// <returns>The dataset containing the comments.</returns>
        public static UserManagementData GetUserGroupDetails(int applicationId)
        {
            UserManagementData userManagementData = new UserManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@ApplicationID", applicationId);
            string[] optionalParameter = new string[] { userManagementData.ListUserDetail.TableName, userManagementData.ListUserGroupDetail.TableName };
            Utility.LoadDataSet(userManagementData, "f9002_pclst_UserInGroup", ht, optionalParameter);
            return userManagementData;
        }
        #endregion GetUserGroupDetails

        #region InsertUserDetails
        /// <summary>
        /// Gets the comments based on the keyid, formid and userid.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="nameDisplay">The name display.</param>
        /// <param name="nameFull">The name full.</param>
        /// <param name="nameNet">The name net.</param>
        /// <param name="email">The email.</param>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <param name="administrator">if set to <c>true</c> [administrator].</param>
        /// <param name="applicationId">applicationId</param>
        /// <param name="loginuserId">loginuserID</param>
        /// <returns>Return Dataset 0 valid insert or 1</returns>
        public static UserManagementData SaveUserDetails(int userId, string nameDisplay, string nameFull, string nameNet, string email, int active, int administrator,int appraiser, int applicationId, int loginuserId)
        {   
            UserManagementData userManagementData = new UserManagementData();
            Hashtable ht = new Hashtable();
            if (userId == 0)
            {
                ht.Add("@UserID", DBNull.Value);
            }
            else
            {
                ht.Add("@UserID", userId);
            }

            ht.Add("@Name_Display", nameDisplay);
            ht.Add("@Name_Full", nameFull);
            ht.Add("@Name_Net", nameNet);
            ht.Add("@Email", email);
            ht.Add("@Active", active);
            ht.Add("@IsAdministrator", administrator);
            ht.Add("@IsAppraiser", appraiser);
            ////ht.Add("@ApplicationID", 1); //// the 1 is temp hardcoded its should be removed
            ht.Add("@ApplicationID", applicationId);
            ht.Add("@LoginUserID", loginuserId);

            Utility.LoadDataSet(userManagementData.GetErrorMessage, "f9002_pcins_User", ht);
              
            return userManagementData;
        }
        #endregion

        #region DeleteUser
        /// <summary>
        /// Gets the comments based on the keyid, formid and userid.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="loginUserId">loginUserID</param>
        public static void DeleteUserDetails(int userId, int loginUserId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            ht.Add("@LoginUserID", loginUserId);
            Utility.ImplementProcedure("f9002_pcdel_User", ht);
            //// DataProxy.ExecuteSP("f9002_pcdel_User", ht);
        }

        #endregion
        #endregion

        #region Coding For Group Tab
        /// <summary>
        /// Gets the comments based on the keyid, formid and userid.
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>The dataset containing the comments.</returns>
        public static UserManagementData GetGroupDetails(int userId)
        {
            UserManagementData userManagementData = new UserManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            string[] optionalParameter = new string[] { userManagementData.ListGroupsGroupDetail.TableName, userManagementData.ListGroupDetail.TableName };
            Utility.LoadDataSet(userManagementData, "f9002_pclst_GroupInUser", ht, optionalParameter);
            return userManagementData;
        }

        /// <summary>
        /// Inserts the group details.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="description">The description.</param>
        /// <param name="userGroup">The user group.</param>
        /// <param name="userId">userId</param>
        /// <returns>DataSet 1 for success insert 0 for error</returns>
        public static UserManagementData InsertGroupDetails(int groupId, string groupName, string description, string userGroup, int userId)
        {
            UserManagementData userManagementData = new UserManagementData();
            Hashtable ht = new Hashtable();
            if (groupId == 0)
            {
                ht.Add("@GroupID", DBNull.Value);
            }
            else
            {
                ht.Add("@GroupID", groupId);
            }

            ht.Add("@GroupName", groupName);
            ht.Add("@Description", description);
            ht.Add("@UserID", userId); 
            ht.Add("@Users", userGroup);
            Utility.LoadDataSet(userManagementData.GetErrorMessage, "f9002_pcins_UserGroup", ht);
            return userManagementData;
        }

        /// <summary>
        /// Delete the group details.
        /// </summary>        
        /// <param name="groupId">The group Id.</param>
        /// <param name="userId">userId</param>
        public static void DeleteGroupDetails(int groupId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@GroupID", groupId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f9002_pcdel_UserGroup", ht);
            //// DataProxy.ExecuteSP("f9002_pcdel_UserGroup", ht);
        }

        #endregion

        #region Coding For PermissionsTab
        /// <summary>
        /// Gets the comments based on the keyid, formid and userid.
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>The dataset containing the comments.</returns>
        public static UserManagementData GetGroupPermissionDetails(int userId)
        {
            UserManagementData userManagementData = new UserManagementData();
            userManagementData.EnforceConstraints = false;
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            string[] optionalParameter = new string[] { userManagementData.ListPermissionGroupDetail.TableName, userManagementData.ListPermissionDetail.TableName };
            Utility.LoadDataSet(userManagementData, "f9002_pclst_GroupPermission", ht, optionalParameter);
            return userManagementData;
        }

        /// <summary>
        /// Saves the grou permission details.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="formPermissions">The form permissions.</param>
        /// <param name="userId">user id</param>
        public static void SaveGrouPermissionDetails(int groupId, string formPermissions, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@GroupID", groupId);
            ht.Add("@FormPermissions", formPermissions);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f9002_pcins_GroupPermission", ht);
            //// DataProxy.ExecuteSP("f9002_pcins_GroupPermission", ht);
        }
        #endregion

        #region Coding For GetUserDetails

        /// <summary>
        /// Gets the user details.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <returns>userManagementData</returns>
        public static UserManagementData F9002_GetUserDetails(int applicationId)
        {
            UserManagementData userManagementData = new UserManagementData();
            Hashtable ht = new Hashtable();
            ht.Add("@ApplicationID", applicationId);
            Utility.LoadDataSet(userManagementData.ListUserDetail, "f9002_pclst_User", ht);
            return userManagementData;
        }

        #endregion
    }
}
