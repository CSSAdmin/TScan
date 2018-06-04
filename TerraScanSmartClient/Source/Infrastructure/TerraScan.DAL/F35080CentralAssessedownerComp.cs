
namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

   public static class F35080CentralAssessedownerComp
    {
        /// <summary>
        /// F35080_s the central assessed owner details.
        /// </summary>
        /// <param name="CentralId">The central id.</param>
        /// <returns></returns>
       public static F35080CentralAssessedOwner F35080_CentralAssessedOwnerDetails(int CentralId)
       {
           F35080CentralAssessedOwner CentralOwner = new F35080CentralAssessedOwner();
           Hashtable ht = new Hashtable();
           ht.Add("@CentralID", CentralId);
           Utility.LoadDataSet(CentralOwner.f35080CentrallyAssessedOwner, "f35080_pcget_CentrallyAssessedOwner", ht);
           return CentralOwner;
       }


       /// <summary>
       /// F35080_s the property class combo.
       /// </summary>
       /// <returns></returns>
       public static F35080CentralAssessedOwner F35080_PropertyClassCombo()
       {
           F35080CentralAssessedOwner OwnerObj = new F35080CentralAssessedOwner();
           Hashtable ht = new Hashtable();
           Utility.LoadDataSet(OwnerObj.f35080PropertyClass, "f35080_pclst_PropertyClass", ht);
           return OwnerObj;
       }

       /// <summary>
       /// F35080_s the delete owner details.
       /// </summary>
       /// <param name="centralId">The central id.</param>
       /// <param name="userId">The user id.</param>
       /// <returns></returns>
       public static void F35080_DeleteOwnerDetails(int centralId,int userId)
       {
           Hashtable ht = new Hashtable();
           ht.Add("@CentralID", centralId);
           ht.Add("@UserID", userId);
           DataProxy.ExecuteSP("f35080_pcdel_CentrallyAssessedOwner", ht);
       }

       /// <summary>
       /// F35080_s the insert owner central details.
       /// </summary>
       /// <param name="centralId">The central id.</param>
       /// <param name="centralXML">The central XML.</param>
       /// <param name="userId">The user id.</param>
       /// <returns></returns>
       public static int F35080_InsertOwnerCentralDetails(int? centralId, string centralXML, int userId)
       {
           Hashtable ht = new Hashtable();
           ht.Add("@CentralID", centralId);
           ht.Add("@CentralXML", centralXML);
           ht.Add("@UserID", userId);
           return Utility.FetchSPExecuteKeyId("f35080_pcins_CentrallyAssessedOwner", ht);
       }

       /// <summary>
       /// F35080_s the central assessed owner details.
       /// </summary>
       /// <param name="ownerId">The owner id.</param>
       /// <returns></returns>
       public static F35080CentralAssessedOwner F35080_OwnerDetails(int ownerId)
       {
           F35080CentralAssessedOwner CentralOwner = new F35080CentralAssessedOwner();
           Hashtable ht = new Hashtable();
           ht.Add("@OwnerID", ownerId);
           Utility.LoadDataSet(CentralOwner.f9101_CentralOwnerDetails, "f9101_pcget_CentralOwnerDetails", ht);
           return CentralOwner;
       }
    }
}
