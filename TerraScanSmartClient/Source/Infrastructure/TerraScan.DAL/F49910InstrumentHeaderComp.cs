// -------------------------------------------------------------------------------------------
// <copyright file="F49910InstrumentHeaderComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F49910InstrumentHeaderComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
//31 Jan 2008       Ramya.D              Created
// -------------------------------------------------------------------------------------------
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
    /// F49910InstrumentHeaderComp
    /// </summary>
    public class F49910InstrumentHeaderComp
    {
        #region List Instrumentheader details

        /// <summary>
        /// F49910_GetInstrumentHeaderDetails
        /// </summary>
        /// <param name="instrumentId">instrumentId</param>
        /// <returns>DataSet</returns>
        public static F49910InstrumentHeaderDataSet F49910_GetInstrumentHeaderDetails(int instrumentId)
        {
            F49910InstrumentHeaderDataSet instrumentHeaderData = new F49910InstrumentHeaderDataSet();
            Hashtable ht = new Hashtable();
            
            ht.Add("@InstID", instrumentId);
           
            string[] tableNames = new string[] 
            {
                instrumentHeaderData.f49901RecorderDetailsDataTable.TableName, 
                instrumentHeaderData.f49901PaymentDetailsDataTable.TableName,
                instrumentHeaderData.F49911GrantorDetails.TableName,
                instrumentHeaderData.F49911GranteeDetails.TableName,
                instrumentHeaderData.F49912LegalFieldListing.TableName,
                instrumentHeaderData.F49912ListLegalSubDivision.TableName
            };
            Utility.LoadDataSet(instrumentHeaderData, "f49901_pcget_RecorderDetails", ht, tableNames);
            return instrumentHeaderData;
        }
        #endregion List Instrumentheader details

        #region ListInstrumentType

        /// <summary>
        /// F49910_GetInstrumentTypeDetails
        /// </summary>
        /// <returns>DataSet</returns>
        public static F49910InstrumentHeaderDataSet F49910_GetInstrumentTypeDetails()
        {
            F49910InstrumentHeaderDataSet instrumentHeaderData = new F49910InstrumentHeaderDataSet();
            Hashtable ht = new Hashtable();
            ////ht.Add("@@InstID", instrumentId);
            string[] tableNames = new string[] 
            { 
                instrumentHeaderData.f49910InstrumentTypeDataTable.TableName, 
                instrumentHeaderData.f49910_pclst_CustomerValue.TableName,
                instrumentHeaderData.f49910_pclst_GrantList.TableName,
                instrumentHeaderData.f49910_pclst_ExemptionType.TableName,
                instrumentHeaderData.f49910_pclst_TenderType.TableName
            };
            Utility.LoadDataSet(instrumentHeaderData, "f49910_pclst_InstrumentValues", ht, tableNames);
            return instrumentHeaderData;
        }

        #endregion ListInstrumentType

        #region SaveInstrumentHeader Details

        /// <summary>
        /// F49910_SaveInstrumentHeaderDetails
        /// </summary>
        /// <param name="instId">instId</param>
        /// <param name="instrumentItems">instrumentItems</param>
        /// <param name="paymentItems">paymentItems</param>
        /// <param name="userId">userId</param>
        /// <returns>Int</returns>
        public static int F49910_SaveInstrumentHeaderDetails(int instId, string instrumentItems, string paymentItems, int userId)
        {
            Hashtable ht = new Hashtable();
            if (instId > 0)
            {
                ht.Add("@InstID", instId);
            }
            else
            {
                ht.Add("@InstID", null);
            }

            ht.Add("@InstrumentItems", instrumentItems);
            ht.Add("@PaymentItems", paymentItems);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f49910_pcins_InstrumentValues", ht);
        }
        #endregion SaveInstrumentHeader Details

        #region F49910CheckInstrumentHeader Deatils

        /// <summary>
        /// F49910CheckInstrumentHeaderDetails
        /// Used to validate whether the records can be saved
        /// </summary>        
        /// <param name="instId"></param>
        /// <param name="instrumentItems"></param>
        /// <param name="paymentItems"></param>
        /// <param name="userId"></param>
        /// <returns>
        /// 0 - When the records can be saved
        /// -1 - when Instrument Number already exists in the Database
        /// </returns>
        public static int F49910CheckInstrumentHeaderDetails(int instId, string instrumentItems, string paymentItems, int userId)
        {
            Hashtable ht = new Hashtable();
            if (instId > 0)
            {
                ht.Add("@InstID", instId);
            }
            else
            {
                ht.Add("@InstID", null);
            }

            ht.Add("@InstrumentItems", instrumentItems);

            if (!string.IsNullOrEmpty(paymentItems))
            {
                ht.Add("@PaymentItems", paymentItems);
            }

            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f49910_pcchk_InstrumentValues", ht);
        }

        #endregion F49910CheckInstrumentHeader Deatils

        #region DeleteInstrumentheader Details

        /// <summary>
        /// F49910_DeleteInstrumentHeader
        /// </summary>
        /// <param name="instId">instId</param>
        /// <param name="userId">userId</param>
        /// <returns>int</returns>
        public static int F49910_DeleteInstrumentHeader(int instId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@InstID", instId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f49910_pcdel_InstrumentValues", ht);
        }
        #endregion DeleteInstrumentheader Details

        #region CopyInstrumentDetails

        /// <summary>
        /// F49910_CopyInstrumentHeaderDetails
        /// </summary>
        /// <param name="instrumentId">instrumentId</param>
        /// <param name="instrumentValue">instrumentValue</param>
        /// <param name="grantorValue">grantorValue</param>
        /// <param name="granteeValue">granteeValue</param>
        /// <param name="legalValue">legalValue</param>
        /// <returns>DataSet</returns>
        public static F49910InstrumentHeaderDataSet F49910_CopyInstrumentHeaderDetails(int instrumentId, int instrumentValue, int grantorValue, int granteeValue, int legalValue)
        {
            F49910InstrumentHeaderDataSet instrumentHeaderData = new F49910InstrumentHeaderDataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@InstID", instrumentId);
            ht.Add("@InstrumentValue", instrumentValue);
            ht.Add("@GrantorValue", grantorValue);
            ht.Add("@GranteeValue", granteeValue);
            ht.Add("@LegalValue", legalValue);
            string[] tableNames = new string[] 
            { 
                instrumentHeaderData.f4991InstrumentCopyDataTable.TableName, 
                instrumentHeaderData.f4991GranteeCopyDataTable.TableName,
                instrumentHeaderData.f4991GrantorCopyDataTable.TableName, 
                instrumentHeaderData.f4991LegalCopyDataTable.TableName, 
            };
            Utility.LoadDataSet(instrumentHeaderData, "f4991_pcget_InstrumentCopy", ht, tableNames);
            return instrumentHeaderData;
        }

        #endregion CopyInstrumentDetails

        #region GetFeeDetails

        /// <summary>
        /// F49910_GetFeeDetails
        /// </summary>
        /// <param name="instypeId">instypeId</param>
        /// <returns>DataSet</returns>
        public static F49910InstrumentHeaderDataSet F49910_GetFeeDetails(int instypeId)
        {
            F49910InstrumentHeaderDataSet instrumentHeaderData = new F49910InstrumentHeaderDataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@ITypeID", instypeId);
            Utility.LoadDataSet(instrumentHeaderData.f49910feeDetailsTable, "f49910_pcget_InstrumentType", ht);
            return instrumentHeaderData;
        }

        #endregion GetFeeDetails

        #region GetGranterAddress Details

        /// <summary>
        /// F49910_GetGranterAddressDetails
        /// </summary>
        /// <param name="grantId">grantId</param>
        /// <returns>DataSet</returns>
        public static F49910InstrumentHeaderDataSet F49910_GetGranterAddressDetails(int grantId)
        {
            F49910InstrumentHeaderDataSet instrumentHeaderData = new F49910InstrumentHeaderDataSet();
            Hashtable ht = new Hashtable();
            ht.Add("@GrantID", grantId);
            Utility.LoadDataSet(instrumentHeaderData.f49910GrantListValues, "f49910_pcget_GrantListValues", ht);
            return instrumentHeaderData;
        }
       
        #endregion GetGranterAddress Details

        #region F49911 PartiesField Listing

        #region List PartiesField
        /// <summary>
        /// F49911_s the list parties field.
        /// </summary>
        /// <returns>Dataset</returns>
        public static F49910InstrumentHeaderDataSet F49911_ListPartiesField()
        {
            F49910InstrumentHeaderDataSet listPartiesFieldData = new F49910InstrumentHeaderDataSet();
            Hashtable ht = new Hashtable();
            Utility.LoadDataSet(listPartiesFieldData, "f49911_pclst_FSParties", ht);
            return listPartiesFieldData;
        }
        #endregion List PartiesField

        #region Insert PartiesField

        /// <summary>
        /// F49911_s the insert parties field details.
        /// </summary>
        /// <param name="instid">The instid.</param>
        /// <param name="grantorItems">The grantor items.</param>
        /// <param name="granteeItems">The grantee items.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="isCopy">The is copy.</param>
        /// <returns>The saved record status.</returns>
        public static int F49911_InsertPartiesFieldDetails(int instid, string grantorItems, string granteeItems, int userId, int isCopy)
        {
            Hashtable ht = new Hashtable();
            if (instid == 0)
            {
                ht.Add("@InstID", DBNull.Value);
            }
            else
            {
                ht.Add("@InstID", instid);
            }

            ht.Add("@GrantorItems", grantorItems);
            ht.Add("@GranteeItems", granteeItems);
            ht.Add("@UserID", userId);
            ht.Add("@IsCopy", isCopy);

            int partiesDetailsID;
            partiesDetailsID = Utility.FetchSPExecuteKeyId("f49911_pcins_PartiesValues", ht);
            return partiesDetailsID;
        }

        #endregion Insert PartiesField

        #endregion F49911 PartiesField Listing

        #region F49912 LegalField Listing

        #region List
        /// <summary>
        /// F49912_s the list legal field.
        /// </summary>
        /// <returns>DataSet</returns>
        public static F49910InstrumentHeaderDataSet F49912_ListLegalField(int InstID)
        {
            Hashtable ht = new Hashtable();
            F49910InstrumentHeaderDataSet instrumentHeaderData = new F49910InstrumentHeaderDataSet();
            ht.Add("@InstID", InstID);

            string[] tableNames = new string[] 
            { 
                /*commented bcos of creating a new layer*.this layer is not in use*/
                //instrumentHeaderData.SubDivisionTable.TableName,
                //instrumentHeaderData.NEDetailsTable.TableName, 
                //instrumentHeaderData.NWDetailsTable.TableName, 
                //instrumentHeaderData.SWDetailsTable.TableName,  
                //instrumentHeaderData.SEDetailsTable.TableName,
                //instrumentHeaderData.CommentsDetailsTable.TableName
            };
            Utility.LoadDataSet(instrumentHeaderData, "f49901_pcget_RecorderDetails", ht, tableNames);
            return instrumentHeaderData;

            
        }
        #endregion List       

        #endregion F49912 LegalField Listing
    }
}
