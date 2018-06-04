// -------------------------------------------------------------------------------------------
// <copyright file="FormMasterComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Excise Tax Rates</summary>
// Release history
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 13 Sept 06		Suganth Mani       Created
// -------------------------------------------------------------------------------------------

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
    /// Main class for FormMaster Component
    /// </summary>
    public static class FormMasterComp
    {
        #region GetSandwichAndItsSliceInformation
        
        /// <summary>
        /// Gets the sandwich and its slice information.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="keyId">The key ID.</param>
        /// <param name="userId">The user ID.</param>
        /// <returns>Typed Dataset</returns>
        public static FormMasterData GetSandwichAndItsSliceInformation(int form, int keyId, int userId)
        {
            FormMasterData formMasterDataSet = new FormMasterData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", form);
            ht.Add("@KeyID", keyId);
            ht.Add("@UserID", userId);
            string[] optionalParameter = new string[] { formMasterDataSet.FormSandwichDetails.TableName, formMasterDataSet.FormSubTitle1.TableName, formMasterDataSet.FormSubTitle2.TableName, formMasterDataSet.BackgroundColor.TableName, formMasterDataSet.FormSliceInformationList.TableName };
            Utility.LoadDataSet(formMasterDataSet, "f9030_pclst_FormSandSlice", ht, optionalParameter);
            return formMasterDataSet;
        }

        #endregion GetSandwichAndItsSliceInformation

        #region GetSandwichSubTitleInformation
        
        /// <summary>
        /// Gets the sandwich sub title information.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="keyId">The key id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Typed Dataset</returns>
        public static FormMasterData GetSandwichSubTitleInformation(int form, int keyId, int userId)
        {
            FormMasterData formMasterDataSet = new FormMasterData();
            Hashtable ht = new Hashtable();
            ht.Add("@Form", form);
            ht.Add("@KeyID", keyId);
            ht.Add("@UserID", userId);
            string[] optionalParameter = new string[] { formMasterDataSet.FormSandwichDetails.TableName, formMasterDataSet.FormSubTitle1.TableName, formMasterDataSet.FormSubTitle2.TableName, formMasterDataSet.BackgroundColor.TableName };
            Utility.LoadDataSet(formMasterDataSet, "f9030_pcget_FormMaster", ht, optionalParameter);
            return formMasterDataSet;
        }

        #endregion GetSandwichSubTitleInformation
    }
}
