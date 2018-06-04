
namespace TerraScan.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Data;
    using TerraScan.DataLayer;
    using System.Collections;
    using TerraScan.BusinessEntities;

    public static class F16071JournalEntryTemplateComp
    {

        /// <summary>
        /// F16072_s the get miscteplate details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <returns></returns>
        public static F16071JournalEntryTemplateData F16071_GetJournalTeplateDetails(int templateId)
        {
            F16071JournalEntryTemplateData tempObject = new F16071JournalEntryTemplateData();
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            string[] tableNames = new string[] { tempObject.f11071_GetHeaderJETempalte.TableName, tempObject.f11071_GridJETempalte.TableName };
            Utility.LoadDataSet(tempObject, "f11071_pcget_JETempalte", ht, tableNames);         
            return tempObject;
        }


        /// <summary>
        /// F16071_s the save header template details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <param name="description">The description.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F16071_SaveHeaderTemplateDetails(int? templateId, int rollYear, string description, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            ht.Add("@Rollyear", rollYear);
            ht.Add("@Description", description);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f11071_pcins_JETemplate", ht);
        }


        /// <summary>
        /// F16071_s the save grid template details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="gridDetails">The grid details.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static void F16071_SaveGridTemplateDetails(int? templateId, string gridDetails, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            ht.Add("@JETemplateItems",gridDetails);            
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f11071_pcins_JETemplateItem", ht);
        }
                
        /// <summary>
        /// F16071_s the deletejournal header details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="userId">The user id.</param>
        public static void F16071_DeleteJournalHeaderDetails(int templateId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f11071_pcdel_JETemplate", ht);
        }

        /// <summary>
        /// F16071_s the deletejournal grid details.
        /// </summary>
        /// <param name="templateId">The template id.</param>
        /// <param name="gridDetails">The grid details.</param>
        /// <param name="userId">The user id.</param>
        public static void F16071_DeleteJournalGridDetails(int templateId, string gridDetails, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@TemplateID", templateId);
            ht.Add("@RemoveJETemplateItemIDs", gridDetails);
            ht.Add("@UserID", userId);
            DataProxy.ExecuteSP("f11071_pcdel_JETemplateItem", ht);
        }

    }
}
