// -------------------------------------------------------------------------------------------
// <copyright file="F35100NeighborhoodComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>
// This files provides the various methods to access F35100NeighborhoodComp.cs methods
// </summary>
// Release history
//********************************************************************************************
// Date			    Author		       Description
// ----------		---------		   -------------------------------------------------------
//16 May 2007       Ramya.D              Created
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
    /// Data Access Layer which talks to the DB directly for F35100
    /// </summary>
    public static class F35100NeighborhoodComp
    {
        ///// <summary>
        ///// F35100_GetNeighborhoodHeaderDetails
        ///// </summary>
        ///// <param name="neighborId">neighborId</param>
        ///// <returns>neighborhoodHeaderData</returns>
        ////public static F35100NeighborhoodHeaderData F35100_GetNeighborhoodHeaderDetails(int neighborId)
        ////{
        ////    F35100NeighborhoodHeaderData neighborhoodHeaderData = new F35100NeighborhoodHeaderData();
        ////    Hashtable ht = new Hashtable();
        ////    ht.Add("@NBHDID", neighborId);
        ////    Utility.FillDataSet(neighborhoodHeaderData.f35100GetNeighborhoodHeaderDataTable, "f35100_pcget_NeighborhoodHeader", ht);
        ////    return neighborhoodHeaderData;
        ////}

        /// <summary>
        /// F35100NeighborhoodHeaderData
        /// </summary>
        /// <param name="applicationId">applicationId</param>
        /// <returns>Typed dataset</returns>
        public static F35100NeighborhoodHeaderData F35100_GetNeighborhoodHeaderUserDetails(int applicationId)
        {
            F35100NeighborhoodHeaderData neighborhoodHeaderData = new F35100NeighborhoodHeaderData();
            Hashtable ht = new Hashtable();
            ht.Add("@ApplicationID", applicationId);
            Utility.LoadDataSet(neighborhoodHeaderData.f35100GetUserDetailsDataTable, "f35100_pclst_UserDetails", ht);
            return neighborhoodHeaderData;
        }

        public static int F3511_ExeNeighborhoodDetails(int nbhId, string newnbhdName, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDID ", nbhId);
            ht.Add("@@NewNBHDName", newnbhdName);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f3511_pcexe_CopyNeighborhood", ht);
        }

        ////public static F35100NeighborhoodHeaderData F35100_GetNeighborhoodGroupDetails(int rollYear)
        ////{
        ////    F35100NeighborhoodHeaderData neighborhoodHeaderData = new F35100NeighborhoodHeaderData();
        ////    Hashtable ht = new Hashtable();
        ////    ht.Add("@RollYear", rollYear);
        ////    Utility.FillDataSet(neighborhoodHeaderData.f35100NeighborhoodGroupDataTable, "f35100_pclst_NeighborhoodGroup", ht);
        ////    return neighborhoodHeaderData;
        ////}

        /// <summary>
        /// F35100_SaveNeighborhoodHeaderDetails
        /// </summary>
        /// <param name="nbhId">nbhId</param>
        /// <param name="nbhDetails">nbhDetails</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer value</returns>
        public static int F35100_SaveNeighborhoodHeaderDetails(int nbhId, string nbhDetails, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDID", nbhId);
            ht.Add("@NewNBHDName", nbhDetails);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f3511_pcexe_CopyNeighborhood", ht);
        }

        #region Delete Neighborhood Group Header

        /// <summary>
        /// To Delete F35101 Neighborhood Group Header
        /// </summary>
        /// <param name="nbhdId">The nbhdId</param>
        /// <param name="userId">userId</param>
        public static void F35100_DeleteNeighborhoodHeader(int nbhdId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDID", nbhdId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f35100_pcdel_NeighborhoodHeader", ht);
        }

        #endregion

        #region Neighborhood

        #region GetNeighborhood Details
        /// <summary>
        /// GetNeighborhoodHeaderDetails
        /// </summary>
        /// <param name="neighborId">neighborId</param>
        /// <returns>neighborhoodHeaderData</returns>
        public static F35100NeighborhoodHeaderData GetNeighborhoodHeaderDetails(int neighborId)
        {
            F35100NeighborhoodHeaderData neighborhoodHeaderData = new F35100NeighborhoodHeaderData();
            
           // NeighborhoodHeaderData.f35100NeighborhoodHeaderDataTablef
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDID", neighborId);
            Utility.LoadDataSet(neighborhoodHeaderData.f35100NeighborhoodHeaderDataTable, "f35100_pcget_NeighborhoodHeader", ht);
            return neighborhoodHeaderData;
        }
        #endregion GetNeighborhood Details
        #region ParentDetails

        /// <summary>
        /// GetParentNeighborhoodHeaderDetails
        /// </summary>
        /// <param name="rollYear">rollYear</param>
        /// <param name="type">type</param>
        /// <param name="parentneighborhood">parentneighborhood</param>
        /// <returns>Typed dataset</returns>
        public static F35100NeighborhoodHeaderData GetParentNeighborhoodHeaderDetails(int rollYear, int type, int parentneighborhood)
        {
            F35100NeighborhoodHeaderData neighborhoodHeaderData = new F35100NeighborhoodHeaderData();
            neighborhoodHeaderData.EnforceConstraints = false;
            DataSet ds = new DataSet(); 
            Hashtable ht = new Hashtable();
            ht.Add("@RollYear", rollYear);
            ht.Add("@NBHDType", type);
            ht.Add("@ParentNeighborhood", parentneighborhood);
            string[] tableNames = new string[] { neighborhoodHeaderData.f35100pclstNeighborhoodDataTable.TableName, neighborhoodHeaderData.f35100GrpNeighborhood.TableName };
            Utility.LoadDataSet(neighborhoodHeaderData, "f35100_pclst_Neighborhood", ht, tableNames);
            ////   Utility.FillDataSet(neighborhoodHeaderData, "f35100_pclst_Neighborhood", ht);
            Utility.LoadDataSet(ds, "f35100_pclst_Neighborhood", ht);  
            return neighborhoodHeaderData;
        }
        #endregion ParentDetails
        #region save NeighborhoodHeader

        /// <summary>
        /// SaveNeighborhoodHeaderDetails
        /// </summary>
        /// <param name="nbhId">nbhId</param>
        /// <param name="nbhdetails">nbhdetails</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer value</returns>
        public static int SaveNeighborhoodHeaderDetails(int nbhId, string nbhdetails, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDID", nbhId);
            ht.Add("@NeighborhoodHeader", nbhdetails);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f35100_pcins_NeighborhoodHeader", ht);
        }

        #endregion save NeighborhoodHeader

        #region Delete NeighborhoodRecord
        /// <summary>
        /// To Delete F35101 Neighborhood Group Header
        /// </summary>
        /// <param name="nbhdId">The nbhdId</param>
        /// <param name="userId">userId</param>
        /// <returns>Integer value</returns>
        public static int DeleteNeighborhoodHeader(int nbhdId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDID", nbhdId);
            ht.Add("@UserID", userId);
            return Utility.FetchSPExecuteKeyId("f35100_pcdel_NeighborhoodHeader", ht);
        }

        #endregion Delete NeighborhoodRecord

        /// <summary>
        /// DuplicateNeighborhoodHeaderCheck
        /// </summary>
        /// <param name="nbhId">nbhId</param>
        /// <param name="nbhdetails">nbhdetails</param>
        /// <returns>Integer value</returns>
        public static int DuplicateNeighborhoodHeaderCheck(int nbhId, string nbhdetails)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@NBHDID", nbhId);
            ht.Add("@NeighborhoodHeader", nbhdetails);
            return Utility.FetchSPExecuteKeyId("f35100_pcchk_NeighborhoodHeader", ht);
        }

        #endregion  Neighborhood
    }
}
