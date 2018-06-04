// -------------------------------------------------------------------------------------------
// <copyright file="F29505CreateSubdivisionComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F29505CreateSubdivisionComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
// 30/12/08             Malliga             Created
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
    /// F29505CreateSubdivision Class File.
    /// </summary>
    public static class F29505CreateSubdivisionComp
    {

        /// <summary>
        /// F429505_s the list all comoboxes.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns></returns>
        public static F29505CreateSubdivisionData F429505_ListAllComoboxes(int eventId)
        {
            F29505CreateSubdivisionData listAllComboboxitems = new F29505CreateSubdivisionData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            string[] tableName = new string[] { listAllComboboxitems.F29505_StateCodeComboDetails.TableName, listAllComboboxitems.F29505_DistrictComboDetails.TableName, listAllComboboxitems.F29505_NeighborhoodComboDetails.TableName, listAllComboboxitems.F29505_SubdivisionComboDetails.TableName, listAllComboboxitems.F29505_LandType1ComboDetails.TableName, listAllComboboxitems.F29505_LandType2ComboDetails.TableName, listAllComboboxitems.F29505_LandType3ComboDetails.TableName };
            Utility.LoadDataSet(listAllComboboxitems, "f29505_pclst_ComboDetails", ht, tableName);
            return listAllComboboxitems;
        }

        /// <summary>
        /// F36035_s the list all LandCodes.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns></returns>
        public static F29505CreateSubdivisionData ListLandCodes(int nbhdId,int rollyear)
        {
            F29505CreateSubdivisionData ListLandCodes = new F29505CreateSubdivisionData();
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDID", nbhdId);
            ht.Add("@RollYear",rollyear);
            Utility.LoadDataSet(ListLandCodes.f36035ListLandCodes, "f36035_pclst_LandCodes", ht);
            return ListLandCodes;
        }

        /// <summary>
        /// F29500_s the get base parcel value.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <returns></returns>
        public static F29505CreateSubdivisionData F29505_GetBaseParcelValue(int eventId)
        {
            F29505CreateSubdivisionData subdivisionSplitDataSet = new F29505CreateSubdivisionData();
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            string[] tableName = new string[] { subdivisionSplitDataSet.F29505_Get_SubdivisionHeaderDetails.TableName, subdivisionSplitDataSet.F29505_Get_SubdivisionGridDetails.TableName};
            Utility.LoadDataSet(subdivisionSplitDataSet, "f29505_pcget_SubdivisionDetails", ht, tableName);
            return subdivisionSplitDataSet;
        }

        /// <summary>
        /// F29505_s the create parcel.
        /// </summary>
        /// <param name="makeSubId">The make sub id.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>Result message</returns>
        public static string F29505_CreateParcel(int makeSubId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@MakeSubID", makeSubId);
            ht.Add("@UserID", userId);
            //Utility.ExecuteSP("f29505_pcexe_CreateParcels", ht);
            return Utility.FetchSingleOuputParameter("f29505_pcexe_CreateParcels", ht, "@Results");

        }

        /// <summary>
        /// F29505_s the save subdivision.
        /// </summary>
        /// <param name="eventId">The event id.</param>
        /// <param name="makeSubItemsXml">The make sub items XML.</param>
        /// <param name="makeSubParcelsXml">The make sub parcels XML.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F29505_SaveDivisionParcels(int eventId, string makeSubItemsXml, string makeSubParcelsXml, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@EventID", eventId);
            ht.Add("@MakeSubItems", makeSubItemsXml);
            ht.Add("@MakeSubParcels", makeSubParcelsXml);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f29505_pcins_DivisionParcels", ht);
        }

        /// <summary>
        /// F29505_s the save sub division.
        /// </summary>
        /// <param name="makeSubID">The make sub ID.</param>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public static int F29505_SaveSubDivision(int makeSubID, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@MakeSubID", makeSubID);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f29505_pcins_CreateSubDivision", ht);
        }
        
        /// <summary>
        /// F29505_s the get land code.
        /// </summary>
        /// <param name="landType1">The land type1.</param>
        /// <param name="landType2">The land type2.</param>
        /// <param name="landType3">The land type3.</param>
        /// <param name="nbhdid">The nbhdid.</param>
        /// <param name="rollYear">The roll year.</param>
        /// <returns></returns>
        public static F29505CreateSubdivisionData F29505_GetLandCode(int landType1, int landType2, int landType3, int nbhdid ,int rollYear)
        {
            F29505CreateSubdivisionData getlandcode = new F29505CreateSubdivisionData();
            Hashtable ht = new Hashtable();
            ht.Add("@LandTypeID1", landType1);
            ht.Add("@LandTypeID2", landType2);
            ht.Add("@LandTypeID3", landType3);
            ht.Add("@NBHDID", nbhdid);
            ht.Add("@Rollyear", rollYear);
            string[] tableName = new string[] { getlandcode.Get_LandCodeAllValue.TableName, getlandcode.Get_LandCodeValue.TableName };
            Utility.LoadDataSet(getlandcode, "f29505_pcget_LandCode", ht, tableName);
            return getlandcode;
        }

    }
}
