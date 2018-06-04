// -------------------------------------------------------------------------------------------
// <copyright file="F3205ApexSketchComp.cs" company="Congruent">
//     Copyright (c) Congruent Infotech.  All rights reserved.
// </copyright>
// <summary>This files provides the various methods to access and Update Check Detail</summary>
// Release history
// **********************************************************************************
// Date              Author            Description
// ----------       ---------          ---------------------------------------------------------
// 20120114          Manoj Kumar      Created
// -------------------------------------------------------------------------------------------



namespace TerraScan.Dal
{
    using System.Collections;
    using TerraScan.DataLayer;
    using TerraScan.BusinessEntities;
    using System.Data;

    /// <summary>
    /// F3205 Apex Sketch comp
    /// </summary>
    public static class F3205ApexSketchComp
    {
        #region Sketch File Path
        
        /// <summary>
        /// F3205 pcget Sketch FilePath.
        /// </summary>
        /// <param name="ParcelId">The Parcel id.</param>
        /// <param name="UserId">The User id.</param>
        /// <returns>getApexSketch Data</returns>
        public static F3205ApexSketchData F3205pcgetSketchFilePath(int parcelId,int userId)
        {
            F3205ApexSketchData getApexSketchData = new F3205ApexSketchData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(getApexSketchData.F3205pcgetSketchFilePath, "f3205_pcget_SketchFilePath", ht);
            return getApexSketchData;
        }

        #endregion Sketch File Path

        #region SketchLinkList

        /// <summary>
        ///F3205 pcget SketchLinks Exist.
        /// </summary>
        public static F3205ApexSketchData F3205pcgetSketchLinksExist(int parcelId, int userId)
        {
            F3205ApexSketchData getApexSketchData = new F3205ApexSketchData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@UserID", userId);
            Utility.LoadDataSet(getApexSketchData.F3205pcgetSketchLinksExist, "f3205_pcget_SketchLinksExist", ht);
            return getApexSketchData;
        }
        
        #endregion SketchLinkList

        #region sketchImagePath

        /// <summary>
        /// Saves the sketch Image Path.
        /// </summary>
        /// <param name="statementItems">The statement items.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>typed dataset</returns>
        public static F3205ApexSketchData F3205pcinsSketchImage(int parcelId,int userId,int pageCount)
        {
            F3205ApexSketchData getApexSketchData = new F3205ApexSketchData();
            Hashtable ht = new Hashtable();
            ht.Add("@ParcelID", parcelId);
            ht.Add("@UserID", userId);
            ht.Add("@PageCount", pageCount);
            Utility.LoadDataSet(getApexSketchData.F3205pcinsSketchImage, "f3205_pcins_SketchImage", ht);
            return getApexSketchData;
        }

         #endregion sketchImagePath

        #region insert Apex Sketch
        
        /// <summary>
        /// insert Apex Sketch
        /// </summary>
        /// <param name="SketchDataXML">The SketchData XML.</param>
        /// <param name="ParcelId">The Parcel Id.</param>
        /// <param name="userId">The userId.</param>
        public static void SaveApexSketch(string SketchDataXML, int parcelId, int userId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@SketchDataXML", SketchDataXML);
            ht.Add("@ParcelID", parcelId);
            ht.Add("@UserID", userId);
            Utility.ImplementProcedure("f3205_pcins_ApexSketch", ht);
        }
        
        #endregion Save Apex Sketch

        #region ReCalcValues

        /// <summary>
        ///ReCalculate RCN Values
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public static string  F3205_pcexeReCalcValues(int userId, int parcelId)
        {
            Hashtable ht = new Hashtable();
            ht.Add("@UserID", userId);
            ht.Add("@ParcelID", parcelId);
            string result;
            result = Utility.FetchSingleOuputParameter("f3205_pcexe_RecalcValues", ht, "@Message");
            return result; 
        }


        #endregion ReCalcValues

    }
}
