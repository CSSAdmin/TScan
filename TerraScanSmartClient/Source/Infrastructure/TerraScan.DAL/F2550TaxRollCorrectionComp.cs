// -------------------------------------------------------------------------------------------
// <copyright file="F2550TaxRollCorrectionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F2550TaxRollCorrectionComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 20/08/07         JYOTHI             Created
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

    #endregion Namespace

    /// <summary>
    /// F2550TaxRollCorrectionComp Class
    /// </summary>
    public static class F2550TaxRollCorrectionComp
    {
        #region ListParcelDetails

        /// <summary>
        /// F2550_s the list parcel details.
        /// </summary>
        /// <param name="parcelId">The parcel id.</param>
        /// <param name="scheduleId">The schedule id.</param>
        /// <returns>The tax roll correction dataset.</returns>
        public static F2550TaxRollCorrectionData F2550_ListParcelDetails(string parcelId, string scheduleId, string stateId,string centralXmlIds)
        {
            F2550TaxRollCorrectionData taxRollCorrectionData = new F2550TaxRollCorrectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcellIDs", parcelId);
            ht.Add("@ScheduleIDs", scheduleId);
            ht.Add("@StateIDs", stateId);
            ht.Add("@CentralItemIDs", centralXmlIds);
            Utility.LoadDataSet(taxRollCorrectionData.ListParcelDetailsTable, "f2550_pclst_ParcelDetails", ht); 
            return taxRollCorrectionData;
        }

        #endregion ListParcelDetails

        #region ExecTaxRollCorrections

        /// <summary>
        /// F2550_s the exec tax roll corrections.
        /// </summary>
        /// <param name="parcelItems">The parcel items.</param>
        /// <param name="userId">userId</param>
        /// <returns>Intgeer Value</returns>
        public static int F2550_ExecTaxRollCorrections(string parcelItems, int userId)
        {
            int primaryOutput;
           //// F2550TaxRollCorrectionData taxRollCorrectionData = new F2550TaxRollCorrectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelItems", parcelItems);
            ht.Add("@UserID", userId);
            primaryOutput = Utility.FetchSPExecuteKeyId("f2550_pcexe_TaxRollCorrections", ht);
            return primaryOutput;
        }

        #endregion ExecTaxRollCorrections
       
        #region ListAttachmentDetails

        /// <summary>
        /// List attachment details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        /// <param name="keyIds">The key ids.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="moduleId">The module id.</param>
        /// <returns>Typed DataSet</returns>
        public static F2550TaxRollCorrectionData F2550_ListAttachmentDetails(int formId, string keyIds, int userId, int moduleId)
        {
            F2550TaxRollCorrectionData taxRollCorrectionData = new F2550TaxRollCorrectionData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", formId);
            ht.Add("@KeyIDs", keyIds);
            ht.Add("@UserID", userId);
            ht.Add("@ModuleID", moduleId);
            Utility.LoadDataSet(taxRollCorrectionData.ListAttachmentDetailsTable, "f2550_pcget_ParcelAttachment", ht);
            return taxRollCorrectionData;
        }

        #endregion ListAttachmentDetails

        #region DeleteAttachmentDetails

        /// <summary>
        /// Delete the attachment details.
        /// </summary>
        /// <param name="formId">The form id.</param>
        public static void F2550_DeleteAttachmentDetails(int formId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@Form", formId);
            Utility.ImplementProcedure("f2550_pcdel_ParcelAttachment", ht);
        }

        #endregion DeleteAttachmentDetails

        #region List Correction Code Details
        /// <summary>
        /// F2550_s the correction code.
        /// </summary>
        /// <returns></returns>
        public static F2550TaxRollCorrectionData F2550_CorrectionCode()
        {
            F2550TaxRollCorrectionData correctionCode = new F2550TaxRollCorrectionData();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(correctionCode.ListCorrectionCode, "f2550_pclst_CorrectionCode", ht);
            return correctionCode;
        }

        #endregion

        #region Insert Correction Parcels Temp Table
        /// <summary>
        /// F2550_s the correction parcels temp.
        /// </summary>
        /// <param name="correctionId">The correction id.</param>
        /// <param name="correctionTempItems">The correction temp items.</param>
        /// <param name="corrParcelIds">The corr parcel ids.</param>
        /// <param name="statementsIds">The statements ids.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
       public static int F2550_InsertCorrectionParcelsTemp(int? correctionId, string correctionTempItems,string corrParcelIds,string statementsIds,int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@CorrectionID", correctionId);
            ht.Add("@CorrectionTempItems", correctionTempItems);
            ht.Add("@CorrParcelIDs", corrParcelIds);
            ht.Add("@StatementIDs", statementsIds);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f2550_pcins_CorrParcelsTemp", ht);
        }
        #endregion


       /// <summary>
       /// F2550_s the state of the get configured.
       /// </summary>
       /// <returns></returns>
       public static F2550TaxRollCorrectionData F2550_GetConfiguredState()
       {
           F2550TaxRollCorrectionData ConfiguredState = new F2550TaxRollCorrectionData();
           Hashtable ht = new Hashtable();
           Utility.LoadDataSet(ConfiguredState.ConfiguredState, "f2550_pcget_ConfiguredStateAndAutoComplete", ht);
           return ConfiguredState;
       }
    }
}
