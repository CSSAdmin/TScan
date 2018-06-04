// -------------------------------------------------------------------------------------------
// <copyright file="F2551EditStatementComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F2551EditStatementComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 27 Sep 2011         Manoj Kumar. P             Created
// -------------------------------------------------------------------------------------------


namespace TerraScan.Dal
{
    #region Namespace

    using System;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.BusinessEntities;
    using System.Collections;
    using TerraScan.DataLayer;
    using System.Data;
    
    #endregion Namespace

    /// <summary>
    /// F2551EditStatementComp Class
    /// </summary>
    public static class F2551EditStatementComp
    {
        #region ListEditStatementDetails

        /// <summary>
        /// F2551_s the list EditStatement details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="StatementId">The Statement id.</param>
        /// <param name="OwnerId">The Owner id.</param>
        /// <param name="TypeId">TheType id.</param>
        /// <returns>The Edit Statement dataset.</returns>
        public static F2551EditStmtData F2551_ListEditStatementDetails(int parcelId, short typeId, int statementId, int ownerId, int userId)
        {
            F2551EditStmtData EditStatementData = new F2551EditStmtData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@TypeID", typeId);
            ht.Add("@StatementID", statementId);
            ht.Add("@OwnerID", ownerId);
            ht.Add("@UserID", userId);
            string[] tableName = new string[] {EditStatementData.EditHeaderDatatable.TableName,EditStatementData.EditItemDataTable.TableName,EditStatementData.StateLoad.TableName  };
            Utility.LoadDataSet(EditStatementData, "f2551_pcget_EditStatement", ht, tableName);
            return EditStatementData;
        }

        #endregion ListEditStatementDetails

        #region ExecuteLoadGrid
       
        /// <summary>
        /// F2551_s the list ExecuteLoadGrid details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="StatementId">The Statement id.</param>
        /// <param name="OwnerId">The Owner id.</param>
        /// <param name="TypeId">The Type id.</param>
        /// <param name="ChangeXML">The Change XML.</param>
        /// <param name="ItemsXML">The ItemsXML.</param>
        /// <param name="UserId">The User id.</param>
        /// <returns>The Edit Statement dataset.</returns>
        public static F2551EditStmtData F2551_LoadStatementGridDetails(int parcelId, short typeId, int statementId, int ownerId,string itemXML,string changeXML, int userId)
        {
            F2551EditStmtData EditStatementData = new F2551EditStmtData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@TypeID", typeId);
            ht.Add("@StatementID", statementId);
            ht.Add("@OwnerID", ownerId);
            ht.Add("@ItemsXML", itemXML);
            ht.Add("@ChangeXML", changeXML);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(EditStatementData.EditItemDataTable, "f2551_pcexe_EditStatementItems", ht);
            return EditStatementData;
        }
        #endregion ExecuteLoadGrid
      
        #region SaveOperationProcess
        /// <summary>
        /// F2551_s the list ExecuteLoadGrid details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="StatementId">The Statement id.</param>
        /// <param name="OwnerId">The Owner id.</param>
        /// <param name="TypeId">The Type id.</param>
        /// <param name="itemXML">The item XML.</param>
        /// <param name="headerXML">The headerXML.</param>
        /// <param name="UserId">The User id.</param>
        /// <returns>The Edit Statement dataset.</returns>
        public static int SaveEditStatementtDetails(int parcelId, short typeId, int statementId, int ownerId, string itemXML, string headerXML, int userId)
        {

            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@TypeID", typeId);
            ht.Add("@StatementID", statementId);
            ht.Add("@OwnerID", ownerId);
            ht.Add("@ItemsXML", itemXML);
            ht.Add("@HeaderXML", headerXML);
            ht.Add("@UserID", userId);
           return Utility.FetchSPExecutedReturnValue("f2551_pcupd_EditStatement", ht);
               
        
        }


        #endregion SaveOperationProcess


    }
}
