

namespace D3200
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using System.Windows.Forms;
    using System.Data;
    using Microsoft.Practices.CompositeUI.Utility;
    using TerraScan.Helper;
    using TerraScan.BusinessEntities;
    
    public class F3205WorkItem: WorkItem 
    {
        #region WorkItemEvents

        /// <summary>
        /// Called when [run started].
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
        }

        /// <summary>
        /// Called when [activated].
        /// </summary>
        protected override void OnActivated()
        {
            base.OnActivated();
        }

        #endregion WorkItemEvents

        #region Sketch File Path

        /// <summary>
        /// F3205 pcget Sketch FilePath.
        /// </summary>
        /// <param name="ParcelId">The Parcel id.</param>
        /// <param name="UserId">The User id.</param>
        /// <returns>getApexSketch Data</returns>
        public F3205ApexSketchData F3205pcgetSketchFilePath(int parcelId, int userId)
        {
            return WSHelper.F3205pcgetSketchFilePath(parcelId, userId);
        }
        #endregion 

        #region Sketch Link List

         /// <summary>
        ///F3205 pcget SketchLinks Exist.
        /// </summary>
        public F3205ApexSketchData F3205pcgetSketchLinksExist(int parcelId, int userId)
        {
            return WSHelper.F3205pcgetSketchLinksExist(parcelId, userId);
        }

        #endregion Sketch Link List

        #region sketchImagePath

        /// <summary>
        /// Saves the sketch Image Path.
        /// </summary>
        /// <param name="statementItems">The statement items.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>typed dataset</returns>
        public  F3205ApexSketchData F3205pcinsSketchImage(int parcelId, int userId, int pageCount)
        {
            return WSHelper.F3205pcinsSketchImage(parcelId, userId, pageCount);
        }
        #endregion
        
        #region insert Apex Sketch

        /// <summary>
        /// insert Apex Sketch
        /// </summary>
        /// <param name="SketchDataXML">The SketchData XML.</param>
        /// <param name="ParcelId">The Parcel Id.</param>
        /// <param name="userId">The userId.</param>
        public  void SaveApexSketch(string SketchDataXML, int parcelId, int userId)
        {
            WSHelper.SaveApexSketch(SketchDataXML, parcelId, userId);
        }
        #endregion

        #region ReCalcValues
        /// <summary>
        ///ReCalculate RCN Values
        /// </summary>
        /// <param name="exciseRateId">The excise rate id.</param>
        /// <param name="userId">The userId.</param>
        /// <returns>The return value specifying status of the delete action.</returns>
        public  string F3205_pcexeReCalcValues(int userId, int parcelId)
        {
            return WSHelper.F3205_pcexeReCalcValues(userId, parcelId);
        }
        #endregion


        #region Generate Thumbnails for the form 

        ///<summary>
        ///Generate thumbnails for the form
        ///</summary>
        public void GenerateThumbnail(int? fileId, int userId, string fileIdXml)
        {
             WSHelper.GenerateThumbnail(fileId,userId,fileIdXml);     
        }
        

        #endregion Generate thumbails for the form
    }
}
