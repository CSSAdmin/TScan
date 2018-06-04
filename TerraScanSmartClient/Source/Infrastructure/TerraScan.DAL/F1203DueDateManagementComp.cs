

namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    public static class F1203DueDateManagementComp
    {

        /// <summary>
        /// F1203s the load due date management.
        /// </summary>
        /// <returns></returns>
         public static F1203DueDateManagementData  F1203LoadDueDateManagement()
         {
            F1203DueDateManagementData LoadDueDateManagement = new F1203DueDateManagementData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(LoadDueDateManagement.f1203_pcget_PostTypeDueDate, "f1203_pcget_PostTypeDueDate", ht);
            return LoadDueDateManagement;
        }

         /// <summary>
         /// F1203_s the save due date management.
         /// </summary>
         /// <param name="userId">The user id.</param>
         /// <param name="dueDateXML">The due date XML.</param>
         public static void F1203_SaveDueDateManagement(int userId,string dueDateXML)
         {
             Hashtable ht = new Hashtable();            
             ht.Add("@UserID", userId);
             ht.Add("@PostTypeData", dueDateXML);
             DataProxy.ExecuteSP("f1203_pcins_PostTypeDueDate", ht);
         }
    }
}
