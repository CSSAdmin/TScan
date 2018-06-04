namespace TerraScan.Dal
{

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

   public static class F14062StatementPullListComp
    {

        /// <summary>
        /// F14062_s the grid result details.
        /// </summary>
        /// <param name="ownerIds">The owner ids.</param>
        /// <param name="statementIds">The statement ids.</param>
        /// <param name="parcelIds">The parcel ids.</param>
        /// <param name="scheduleIds">The schedule ids.</param>
        /// <param name="stateIds">The state ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
       public static F14062StatementPullListData F14062_GridResultDetails(string ownerIds,string statementIds,string parcelIds,string scheduleIds,string stateIds,int userId)
       {
           F14062StatementPullListData tempObject = new F14062StatementPullListData();
           Hashtable ht = new Hashtable();
           ht.Add("@OwnerIDs", ownerIds);
           ht.Add("@StatementIDs", statementIds);
           ht.Add("@ParcelIDs",parcelIds );
           ht.Add("@ScheduleIDs",scheduleIds );
           ht.Add("@StateIDs", stateIds);
           ht.Add("@UserID", userId);
           string[] tableNames = new string[] { tempObject.f14062_GridResultDetailsTable.TableName, tempObject.f14062_GetConfigDetailTable.TableName };           
           Utility.LoadDataSet(tempObject, "f14062_pclst_StatementDetails", ht,tableNames);
           return tempObject;
       }

       /// <summary>
       /// F14062_s the get statement pull list details.
       /// </summary>
       /// <returns></returns>
       public static F14062StatementPullListData F14062_GetStatementPullListDetails()
       {
           F14062StatementPullListData tempObject = new F14062StatementPullListData();
           Hashtable ht = new Hashtable();
           Utility.LoadDataSet(tempObject.f14062_pcget_StatementPullList, "f14062_pcget_StatementPullList", ht);
           return tempObject;
       }

       /// <summary>
       /// F1407_s the get pull list status.
       /// </summary>
       /// <returns></returns>
       public static F14062StatementPullListData F1407_GetPullListStatus()
       {
           F14062StatementPullListData tempObject = new F14062StatementPullListData();
           Hashtable ht = new Hashtable();
           Utility.LoadDataSet(tempObject.f1407_GetPullListStatus, "f1407_pclst_PullListStatus", ht);
           return tempObject;
       }

       /// <summary>
       /// F1407_s the type of the get pull list.
       /// </summary>
       /// <returns></returns>
       public static F14062StatementPullListData F1407_GetPullListType()
       {
           F14062StatementPullListData tempObject = new F14062StatementPullListData();
           Hashtable ht = new Hashtable();
           Utility.LoadDataSet(tempObject.f1407_GetPullListType, "f1407_pclst_PullListType", ht);
           return tempObject;
       }

       /// <summary>
       /// F14062_SaveGridDetails details.
       /// </summary>
       /// <param name="pullListItems">The pull list items.</param>
       /// <param name="userId">The user id.</param>
       public static void F14062_SaveGridDetails(string pullListItems, int userId)
       {
           Hashtable ht = new Hashtable();
           ht.Add("@PullListItems", pullListItems);
           ht.Add("@UserID", userId);
           DataProxy.ExecuteSP("f14062_pcins_StatementPullList", ht);
       }


       /// <summary>
       /// F14062_s the delete statement pull list.
       /// </summary>
       /// <param name="pullListItems">The pull list items.</param>
       /// <param name="userId">The user id.</param>
       /// <param name="isProcess">if set to <c>true</c> [is process].</param>
       /// <returns></returns>
       public static string F14062_DeleteStatementPullList(string pullListItems, int userId,bool isProcess)
       {
           Hashtable ht = new Hashtable();
           ht.Add("@PullListItems", pullListItems);
           ht.Add("@UserID", userId);
           ht.Add("@IsProcess", isProcess);
          // DataProxy.ExecuteSP("f14062_pcins_StatementPullList", ht);
           return Utility.CustomFetchSPKeyString("f14062_pcdel_PullList", ht);
       }
    }
}
