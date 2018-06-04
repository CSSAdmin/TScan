

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Collections;
    using TerraScan.BusinessEntities;
    using TerraScan.DataLayer;
    using System.Data;

    /// <summary>
    /// F29555PersonalPropertySaleComp
    /// </summary>
   public static  class F29555PersonalPropertySaleComp
    {

        /// <summary>
        /// F29555_s the deedtype combo box.
        /// </summary>
        /// <returns></returns>
       public static F29555PersonalPropertySaleData F29555_DeedtypeComboBox()
       {
           F29555PersonalPropertySaleData tempObject = new F29555PersonalPropertySaleData();
           Hashtable ht = new Hashtable();
           Utility.LoadDataSet(tempObject.f29555_pclst_DeedTypes, "f29555_pclst_DeedTypes", ht);
           return tempObject;
       }

       /// <summary>
       /// F29555_s the execute transfer ownership.
       /// </summary>
       /// <param name="eventId">The event id.</param>
       /// <param name="userId">The user id.</param>
       /// <returns></returns>
       public static string F29555_SaveTransferOwnership(int eventId, int userId)
       {
           Hashtable ht = new Hashtable();
           ht.Add("@EventID", eventId);           
           ht.Add("@UserID", userId);
           return Utility.FetchSingleOuputParameter("f29555_pcexe_TransferOwnership", ht, "@Result");
       }

       /// <summary>
       /// F29555_s the get personal sales owner.
       /// </summary>
       /// <param name="pSsaleId">The p ssale id.</param>
       /// <param name="ownerId">The owner id.</param>
       /// <param name="scheduleId">The schedule id.</param>
       /// <param name="userid">The userid.</param>
       /// <param name="scheduleString">The schedule string.</param>
       /// <returns></returns>
       public static F29555PersonalPropertySaleData F29555_GetPersonalSalesOwner(int? pSsaleId, int? ownerId,int? scheduleId, int userid,string scheduleString)
       {
           F29555PersonalPropertySaleData tempObject = new F29555PersonalPropertySaleData();
           Hashtable ht = new Hashtable();
           ht.Add("@PSaleID", pSsaleId);
           ht.Add("@OwnerID", ownerId);
           ht.Add("@ScheduleID", scheduleId);
           ht.Add("@UserID", userid);
           ht.Add("@Schedules", scheduleString); 
           Utility.LoadDataSet(tempObject.f29555_pcget_PSaleOwners, "f29555_pcget_PSaleOwners", ht);
           return tempObject;
       }

       /// <summary>
       /// F29555_s the get sales scheduleand owners.
       /// </summary>
       /// <param name="scheduleId">The schedule id.</param>
       /// <param name="scheduleIds">The schedule ids.</param>
       /// <param name="pSsaleId">The p ssale id.</param>
       /// <param name="userid">The userid.</param>
       /// <returns></returns>
       public static F29555PersonalPropertySaleData F29555_GetSalesScheduleandOwners( int? scheduleId, string scheduleIds ,int? pSsaleId, int userid)
       {
           F29555PersonalPropertySaleData tempObject = new F29555PersonalPropertySaleData();
           Hashtable ht = new Hashtable();
           ht.Add("@ScheduleID", scheduleId);
           ht.Add("@ScheduleIDs", scheduleIds);
           ht.Add("@PSaleID", pSsaleId);
           ht.Add("@UserID", userid);
           string[] tableNames = new string[] 
            {   
                tempObject.f29555_pcget_SaleSchedulesAndOwners.TableName,
                tempObject.f29555_pcget_PSaleOwners.TableName                              
                
            };
           Utility.LoadDataSet(tempObject,"f29555_pcget_SaleSchedulesAndOwners", ht,tableNames);
           return tempObject;
       }


       /// <summary>
       /// F29555_s the schedule sale tracking.
       /// </summary>
       /// <param name="eventId">The event id.</param>
       /// <param name="userid">The userid.</param>
       /// <returns></returns>
       public static F29555PersonalPropertySaleData F29555_ScheduleSaleTracking(int eventId, int userid)
       {
           F29555PersonalPropertySaleData tempObject = new F29555PersonalPropertySaleData();
           Hashtable ht = new Hashtable();
           ht.Add("@EventID", eventId);
           ht.Add("@UserID", userid);
           string[] tableNames = new string[] 
            {                
                tempObject.f29555_pcget_ScheduleSaleTracking.TableName,
                tempObject.f29555_pcget_SaleSchedulesAndOwners.TableName,
                tempObject.f29555_pcget_PSaleOwners.TableName                              
                
            };
           Utility.LoadDataSet(tempObject, "f29555_pcget_ScheduleSaleTracking", ht,tableNames);
           return tempObject;
       }

       /// <summary>
       /// F29555_s the save sales owner.
       /// </summary>
       /// <param name="pSaleId">The p sale id.</param>
       /// <param name="ownerDetails">The owner details.</param>
       /// <param name="userId">The user id.</param>
       public static void F29555_SaveSalesOwner(int pSaleId, string ownerDetails, int userId)
       {
           Hashtable ht = new Hashtable();
           ht.Add("@PSaleID", pSaleId);
           ht.Add("@OwnerItems", ownerDetails);
           ht.Add("@UserID", userId);
           DataProxy.ExecuteSP("f29555_pcins_SaleOwners", ht);
       }

       /// <summary>
       /// F29555_s the save sales schedule.
       /// </summary>
       /// <param name="pSaleId">The p sale id.</param>
       /// <param name="scheduleItems">The schedule items.</param>
       /// <param name="userId">The user id.</param>
       public static void F29555_SaveSalesSchedule(int pSaleId, string scheduleItems, int userId)
       {
           Hashtable ht = new Hashtable();
           ht.Add("@PSaleID", pSaleId);
           ht.Add("@ScheduleItems", scheduleItems);
           ht.Add("@UserID", userId);
           DataProxy.ExecuteSP("f29555_pcins_SaleSchedules", ht);
       }

       /// <summary>
       /// F29555_s the save schedule sale tracking.
       /// </summary>
       /// <param name="eventId">The event id.</param>
       /// <param name="pSaleItems">The p sale items.</param>
       /// <param name="scheduleItems">The schedule items.</param>
       /// <param name="ownerDetails">The owner details.</param>
       /// <param name="userId">The user id.</param>
       /// <returns></returns>
       public static int F29555_SaveScheduleSaleTracking(int eventId, string pSaleItems, string scheduleItems,string ownerDetails, int userId)
       {
           Hashtable ht = new Hashtable();
           ht.Add("@EventID", eventId);
           ht.Add("@PSaleItems", pSaleItems);
           ht.Add("@ScheduleItems", scheduleItems);
           ht.Add("@OwnerItems", ownerDetails);
           ht.Add("@UserID", userId);
           return Utility.FetchSPExecuteKeyId("f29555_pcins_ScheduleSaleTracking", ht);
       }


    }
}
