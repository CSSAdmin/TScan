//--------------------------------------------------------------------------------------------
// <copyright file="F9002WorkItem.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1100.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                              	    Created// 
//*********************************************************************************/
namespace D9002
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using System.Data;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;

    #endregion

    /// <summary>
    /// F9002WorkItem class file
    /// </summary>
    public class F9002WorkItem : WorkItem
    {
        #region User

        #region GetUserGroupDetails

        /// <summary>
        /// Lists the history information of the statement.
        /// </summary>
        /// <returns>
        /// The dataset containing the history information of the statementid.
        /// </returns>
        public UserManagementData GetUserGroupDetails
        {
           get 
           { 
               return WSHelper.GetUserGroupDetails(TerraScanCommon.ApplicationId);
           }
        }

        #endregion

        #region Group

        #region GetGroupDetails

        /// <summary>
        /// List The Group Inforamtion
        /// </summary>
        /// <returns>
        /// The dataset containing Details of Group and User.
        /// </returns>
        public UserManagementData GetGroupDetails
        {
            get { return WSHelper.GetGroupDetails(TerraScanCommon.UserId); }
        }

        #endregion

        #region GetGroupPermissionDetails

        /// <summary>
        /// Gets the group permission details.
        /// </summary>
        /// <returns>DataSet With Forms name and realitve permissions</returns>
        public UserManagementData GetGroupPermissionDetails
        {
            get { return WSHelper.GetGroupPermissionDetails(TerraScanCommon.UserId); }
        }

        #endregion

        #region InsertGroupDetails

        /// <summary>
        /// Insert the user Detials
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="description">The description.</param>
        /// <param name="userGroup">The user group.</param>
        /// <returns>DataSet 1 for success insert 0 for error</returns>
        public static UserManagementData.GetErrorMessageDataTable InsertGroupDetails(int groupId, string groupName, string description, string userGroup, int userId)
        {
            return WSHelper.InsertGroupDetails(groupId, groupName, description, userGroup, userId).GetErrorMessage;
        }

        #endregion

        #region DeleteGroupDetails

        /// <summary>
        /// Delete the Group Detials
        /// </summary>
        /// <param name="groupId">The group id.</param>
        public static void DeleteGroupDetails(int groupId,int userID)
        {
            WSHelper.DeleteGroupDetails(groupId,userID);
        }

        #endregion

        #endregion

        #region InsertUserDetails

        /// <summary>
        /// Insert the user Detials
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="nameDisplay">The name display.</param>
        /// <param name="nameFull">The name full.</param>
        /// <param name="nameNet">The name net.</param>
        /// <param name="email">The email.</param>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <param name="administrator">if set to <c>true</c> [administrator].</param>
        /// <returns>Return Dataset 0 valid insert or 1</returns>
        public static UserManagementData.GetErrorMessageDataTable SaveUserDetails(int userId, string nameDisplay, string nameFull, string nameNet, string email, int active, int administrator,int appraiser, int applicationId, int loginUserID)
        {
            return WSHelper.SaveUserDetails(userId, nameDisplay, nameFull, nameNet, email, active, administrator,appraiser, applicationId, loginUserID).GetErrorMessage;
        }

        #endregion

        #region DeleteUserDetails
        /// <summary>
        /// Delete the user Detials
        /// </summary>
        /// <param name="userId">The user id.</param>
        public static void DeleteUserDetails(int userId,int loginUserID)
        {
            WSHelper.DeleteUserDetails(userId,loginUserID);
        }

        #endregion

        #endregion

        #region Permission

        #region SaveGroupPermissionDetails

        /// <summary>
        /// Save the group permission details.
        /// </summary>
        /// <param name="groupId">The group id.</param>
        /// <param name="formpermissions">The formpermissions.</param>
        public static void SaveGroupPermissionDetails(int groupId, string formpermissions, int userId)
        {
            WSHelper.SaveGroupPermissionDetails(groupId, formpermissions, userId);
        }

        #endregion

        #endregion

        /// <summary>
        /// Fires the <see cref="E:Microsoft.Practices.CompositeUI.WorkItem.RunStarted"/> event. Derived classes can override this
        /// method to place custom business logic to execute when the <see cref="M:Microsoft.Practices.CompositeUI.WorkItem.Run"/>
        /// method is called on the <see cref="T:Microsoft.Practices.CompositeUI.WorkItem"/>.
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Fires the <see cref="E:Microsoft.Practices.CompositeUI.WorkItem.Activated"/> event.
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }
    }
}
