using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using TerraScan.BusinessEntities; 
using TerraScan.DataLayer;

namespace TerraScan.Dal
{
    /// <summary>
    /// 
    /// </summary>
    public static class F25000FieldUseComp
    {
        /// <summary>
        /// F25000_GetCheckOutDetails
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F25000FieldUseData F25000_GetCheckOutDetails(int snapShotId, string snapShotValue)
        {
            F25000FieldUseData fieldUseData = new F25000FieldUseData();
            string[] tableNames = {   fieldUseData.tAA_Crop.TableName
                                    , fieldUseData.tAA_MA_Type1.TableName
                                    , fieldUseData.tAA_MA_Type10.TableName
                                    , fieldUseData.tAA_MA_Type11.TableName
                                    , fieldUseData.tAA_MA_Type12.TableName
                             ///       , fieldUseData.tAA_MA_Type2.TableName
                                    , fieldUseData.tAA_MA_Type3.TableName
                                    , fieldUseData.tAA_MA_Type4.TableName
                                    , fieldUseData.tAA_MA_Type6.TableName
                                    , fieldUseData.tAA_MA_Type7.TableName
                                    , fieldUseData.tAA_MA_Type8.TableName
                                    , fieldUseData.tAA_MA_Type9.TableName
                                    , fieldUseData.tAA_MAD_Type1.TableName
                                    , fieldUseData.tAA_MAD_Type10.TableName
                                    , fieldUseData.tAA_MAD_Type11.TableName
                                    , fieldUseData.tAA_MAD_Type12.TableName
                                    , fieldUseData.tAA_MAD_Type2.TableName
                                    , fieldUseData.tAA_MAD_Type3.TableName
                                    , fieldUseData.tAA_MAD_Type4.TableName
                                    , fieldUseData.tAA_MAD_Type6.TableName
                                    , fieldUseData.tAA_MAD_Type7.TableName
                                    , fieldUseData.tAA_MAD_Type8.TableName
                                    , fieldUseData.tAA_MAD_Type9.TableName
                                    , fieldUseData.tAA_Parcel_InspectionType.TableName
                                    , fieldUseData.tAA_Parcel_PLandType.TableName
                                    , fieldUseData.tAA_Parcel_PImprovement.TableName
                                    , fieldUseData.tAA_SliceTypeOrder.TableName
                                    , fieldUseData.tAA_LandUnit.TableName
                                    , fieldUseData.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutXML", ht, tableNames);
            return fieldUseData;
        }



        public static F25000FieldUseData F25000_ChkOutType2XML(int snapShotId, string snapShotValue)
        {
            F25000FieldUseData fieldUseDataSet = new F25000FieldUseData();
            string[] tableNames = { fieldUseDataSet.tAA_MA_Type2.TableName
                                    , fieldUseDataSet.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseDataSet, "f3230_pcget_ChkOutType2XML", ht, tableNames);
            return fieldUseDataSet;
        }

        /// <summary>
        /// F25000_ChkOutParcelValueXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F25000FieldUseData F25000_ChkOutParcelValueXML(int snapShotId, string snapShotValue)
        {
            F25000FieldUseData fieldUseDataSet = new F25000FieldUseData();
            string[] tableNames = {   fieldUseDataSet.tAA_ParcelValue.TableName
                                    , fieldUseDataSet.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseDataSet, "f3230_pcget_ChkOutParcelValueXML", ht, tableNames);
            return fieldUseDataSet;
        }

        /// <summary>
        /// F25000_ChkOutLandXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F25000FieldUseData F25000_ChkOutLandXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F25000FieldUseData fieldUseData = new F25000FieldUseData();
            RowendValue = 0;
            string[] tableNames = {  fieldUseData.tAA_Land.TableName
                                    ,fieldUseData.tTS_TableXML.TableName
                                  };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutLandXML", ht, tableNames);
            return fieldUseData;
        }

        /// <summary>
        /// F25000_ChkOutAssessmentTypeXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F25000FieldUseData F25000_ChkOutAssessmentTypeXML(int snapShotId, string snapShotValue)
        {
            F25000FieldUseData fieldUseDataSet = new F25000FieldUseData();
            string[] tableNames = {  fieldUseDataSet.tAA_AssessmentType.TableName
                                    , fieldUseDataSet.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseDataSet, "f3230_pcget_ChkOutAssessmentTypeXML", ht, tableNames);
            return fieldUseDataSet;
        }

        /// <summary>
        /// F25000_ChkOutVersionXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <param name="TableName"></param>
        /// <param name="StartRow"></param>
        /// <param name="RowendValue"></param>
        /// <returns></returns>
        public static F25000FieldUseData F25000_ChkOutVersionXML(int snapShotId, string snapShotValue, string TableName, int StartRow, out int RowendValue)
        {
            F25000FieldUseData fieldUseData = new F25000FieldUseData();
            RowendValue = 0;
            string[] tableNames = {  fieldUseData.tAA_MSC_Version.TableName
                                    ,fieldUseData.tTS_TableXML.TableName
                                  };
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            ht.Add("@TableName", TableName);
            ht.Add("@StartRow", StartRow);
            IList returnList = Utility.LoadDataSet(fieldUseData, "f3230_pcget_ChkOutVersionXML", ht, tableNames);
            RowendValue = Convert.ToInt32(((System.Data.SqlClient.SqlParameter[])(returnList))[4].Value.ToString());
            return fieldUseData;
        }

        /// <summary>
        /// F25000_ChkOutSeniorExemptionXML
        /// </summary>
        /// <param name="snapShotId"></param>
        /// <param name="snapShotValue"></param>
        /// <returns></returns>
        public static F25000FieldUseData F25000_ChkOutSeniorExemptionXML(int snapShotId, string snapShotValue)
        {
            F25000FieldUseData fieldUseDataSet = new F25000FieldUseData();
            string[] tableNames = {   fieldUseDataSet.tTS_SeniorExemption.TableName
                                    , fieldUseDataSet.tTS_TableXML.TableName};
            Hashtable ht = new Hashtable();
            ht.Add("@SnapShotID", snapShotId);
            ht.Add("@SnapShotElements", snapShotValue);
            Utility.LoadDataSet(fieldUseDataSet, "f3230_pcget_ChkOutSeniorExemptionXML", ht, tableNames);
            return fieldUseDataSet;
        }
    }
}
