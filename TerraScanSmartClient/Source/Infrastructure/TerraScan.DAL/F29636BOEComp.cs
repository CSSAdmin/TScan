
namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

   public static class F29636BOEComp
    {

        /// <summary>
        /// F29636_s the get BOE details.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns></returns>
       public static F29636BOEData F29636_GetBOEDetails(int eventId)
       {
           F29636BOEData objBOE = new F29636BOEData();
           Hashtable ht = new Hashtable();
           ht.Add("@EventID", eventId);
           string[] tableName = new string[] { objBOE.f29636_GetBOEDetailsTable.TableName, objBOE.f29636_GetBOEGridTable.TableName };
           Utility.LoadDataSet(objBOE, "f29636_pcget_BOE_NE", ht, tableName);
           return objBOE;
       }

       /// <summary>
       /// F29636_s the BOE type details.
       /// </summary>
       /// <returns></returns>
       public static F29636BOEData F29636_BOETypeDetails()
       {
           F29636BOEData objType = new F29636BOEData();
           Hashtable ht = new Hashtable();
           Utility.LoadDataSet(objType.f29636_BOETypeComboTable, "f29636_pclst_BOEType_NE", ht);
           return objType;
       }

       /// <summary>
       /// F29636_s the save BOE details.
       /// </summary>
       /// <param name="boeElemenets">The boe elemenets.</param>
       /// <param name="boeValues">The boe values.</param>
       /// <param name="userId">The user id.</param>
       public static void F29636_SaveBOEDetails(string boeElemenets,string boeValues, int userId)
       {           
           Hashtable ht = new Hashtable();
           ht.Add("@BOEElements", boeElemenets);
           ht.Add("@BOEValues", boeValues);
           ht.Add("@UserID", userId);
           DataProxy.ExecuteSP("f29636_pcins_BOE_NE", ht);
       }

       /// <summary>
       /// F29636_s the push BOE details.
       /// </summary>
       /// <param name="boeId">The boe id.</param>
       /// <param name="userId">The user id.</param>
       /// <returns></returns>
       public static string F29636_PushBOEDetails(int boeId,int userId)
       {
           Hashtable ht = new Hashtable();
           ht.Add("@BOEID", boeId);           
           ht.Add("@UserID", userId);
           return Utility.CustomFetchSPKeyString("f29636_pcexe_BoePushValue_NE", ht);           
       }

       /// <summary>
       /// F29636_s the delete BOE details.
       /// </summary>
       /// <param name="boeId">The boe id.</param>
       /// <param name="userId">The user id.</param>
       public static void F29636_DeleteBOEDetails(int boeId, int userId)
       {
           Hashtable ht = new Hashtable();
           ht.Add("@BOEID", boeId);
           ht.Add("@UserID", userId);
           DataProxy.ExecuteSP("f29636_pcdel_BOE_NE", ht);
       }
    }
}
