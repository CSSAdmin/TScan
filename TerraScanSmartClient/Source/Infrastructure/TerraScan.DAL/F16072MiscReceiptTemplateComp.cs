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
    /// F16072MiscReceiptTemplateComp
    /// </summary>
   public static class F16072MiscReceiptTemplateComp
    {
        /// <summary>
        /// F16072_s the get miscteplate details.
        /// </summary>
        /// <param name="misctemplateId">The misctemplate id.</param>
        /// <returns></returns>
       public static F16072MiscReceiptTemplate F16072_GetMiscteplateDetails(int misctemplateId)
       {
           F16072MiscReceiptTemplate MiscObject = new F16072MiscReceiptTemplate();
           Hashtable ht = new Hashtable();
           ht.Add("@MiscTemplateID", misctemplateId);
           string[] tableNames = new string[] { MiscObject.f11072_Get_MiscReceiptTemplate.TableName, MiscObject.f11072Get_MiscGridDetailsTable.TableName };
           Utility.LoadDataSet(MiscObject, "f11072_pcget_MiscReceiptTemplate", ht, tableNames);
           //Utility.LoadDataSet(MiscObject.f11072_Get_MiscReceiptTemplate, "f11072_pcget_MiscReceiptTemplate", ht);         
           return MiscObject;
       }

       /// <summary>
       /// F16072_s the save misc receipt template.
       /// </summary>
       /// <param name="misctemplateId">The misctemplate id.</param>
       /// <param name="miscHeaderDetails">The misc header details.</param>
       /// <param name="accountDetails">The account details.</param>
       /// <param name="userId">The user id.</param>
       /// <returns></returns>
       public static int F16072_SaveMiscReceiptTemplate(int? misctemplateId, string miscHeaderDetails, string accountDetails, int userId)
       {
           Hashtable ht = new Hashtable();
           ht.Add("@MiscTemplateID", misctemplateId);
           ht.Add("@TemplateHeaderItems", miscHeaderDetails);
           ht.Add("@TemplateAccountItems", accountDetails);
           ht.Add("@UserID", userId);
           return Utility.FetchSPExecuteKeyId("f11072_pcins_MiscReceiptTemplate", ht);
       }

       /// <summary>
       /// F16072_s the delete misctemplate details.
       /// </summary>
       /// <param name="misctemplateId">The misctemplate id.</param>
       /// <param name="userId">The user id.</param>
       public static void F16072_DeleteMisctemplateDetails(int misctemplateId, int userId)
       {           
           Hashtable ht = new Hashtable();
           ht.Add("@MiscTemplateID", misctemplateId);
           ht.Add("@UserID", userId);
           DataProxy.ExecuteSP("f11072_pcdel_MiscReceiptTemplate", ht);
       }

       /// <summary>
       /// F16072_s the delete misc gridtemplate.
       /// </summary>
       /// <param name="misctemplateId">The misctemplate id.</param>
       /// <param name="miscIds">The misc ids.</param>
       /// <param name="userId">The user id.</param>
       public static void F16072_DeleteMiscGridtemplate(int misctemplateId, string miscIds, int userId)
       {
           Hashtable ht = new Hashtable();
           ht.Add("@MiscTemplateID", misctemplateId);
           ht.Add("@MiscItemIDs", miscIds);
           ht.Add("@UserID", userId);
           DataProxy.ExecuteSP("f16072_pcdel_MiscReceiptTemplateGrid", ht);
       }
    }
}
