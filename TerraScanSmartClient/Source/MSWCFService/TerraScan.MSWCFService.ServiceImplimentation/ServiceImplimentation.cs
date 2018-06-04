

namespace TerraScan.MSWCFService.ServiceImplimentation
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TerraScan.MSWCFService.ServiceContract;
    using System.Data;
    using System.IO;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.Configuration;
    using System.Security;
    using System.Web.Security;
    using System.Security.Cryptography;
    using System.Reflection;
    using System.Collections;
    using System.Xml;
    using System.Data.SqlClient;

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class ServiceImplimentation : IServiceImplimentation
    {
        #region Variable

        #endregion

        #region Property

        #endregion

        #region Enum

        /// <summary>
        /// HouseType
        /// </summary>
        private enum HouseType
        {
            /// <summary>
            /// SingleFamilyResidence
            /// </summary>
            SingleFamilyResidence = 1,

            /// <summary>
            /// LowRiseMultiple
            /// </summary>
            LowRiseMultiple = 2,

            /// <summary>
            /// TownHouseEndUnit
            /// </summary>
            TownHouseEndUnit = 3,

            /// <summary>
            /// TownHouseInsideUnit
            /// </summary>
            TownHouseInsideUnit = 4,

            /// <summary>
            /// Duplex
            /// </summary>
            Duplex = 5,

            /// <summary>
            /// ManufacturedHousing
            /// </summary>
            ManufacturedHousing = 6
        }

        #endregion

        #region Methods

        /// <summary>
        /// F36000_s the get HTC XML.
        /// </summary>
        /// <returns>HillsideZoneCollection</returns>
        ////public T2RE7Engine.T2RE7EngineInterFace.HillsideZoneCollection F36000_GetHTCXml()
        ////{

        ////    RE7Engine.ResidentialEstimator oRE7 = new RE7Engine.ResidentialEstimator();
        ////    T2RE7Engine.T2RE7EngineInterFace.CommonFunctions s = new T2RE7Engine.T2RE7EngineInterFace.CommonFunctions();
        ////    s.ConnectionProvider = 1;// Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["dbProvider"].ToString());
        ////    if (s.ConnectionProvider != 1)
        ////    {
        ////        s.MSAccessCalculationConnectionString = string.Empty;//System.Configuration.ConfigurationManager.AppSettings["DataConnectionString"];
        ////        s.MSAccessUserConnectionString = string.Empty;  //System.Configuration.ConfigurationManager.AppSettings["UserConnectionString"];

        ////    }
        ////    else
        ////    {
        ////        s.SqlCalcualtionConnectionString = "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=TerrascanRECOST;Data Source=dotnet2005";
        ////        s.SqlUserConnectionString = "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=TerrascanREUSER;Data Source=dotnet2005";

        ////    }
        ////    s.SetDBConnection(ref oRE7);

        ////    //// creating  HillsideZone XML

        ////    RE7Engine.HillsideZones hillSideCollection = oRE7.get_Data().HillsideZones;
        ////    T2RE7Engine.T2RE7EngineInterFace.HillsideZoneCollection hillSideData = new T2RE7Engine.T2RE7EngineInterFace.HillsideZoneCollection(hillSideCollection);
        ////    return hillSideData;

        ////}

        /// <summary>
        /// F36000_s the save marshall swift.
        /// </summary>
        public void F36000_SaveMarshallSwift(Hashtable saveElement, string componentXml, string groupXML, bool newMode)
        {
            RE7Engine.ResidentialEstimator objRE = new RE7Engine.ResidentialEstimator();
            RE7Engine.Estimate objEstimate = new RE7Engine.Estimate();

            objEstimate = this.AssignRE7Object(saveElement, componentXml, groupXML, newMode);

             ////if (!string.IsNullOrEmpty(saveElement["RCNValue"].ToString()))
            if (!string.IsNullOrEmpty(saveElement["RCNValue"].ToString()) && (saveElement["RCNValue"].ToString() != null))
            {
                bool setTrue = true;
                bool setFalse = false;

                objEstimate.Calculate(ref setFalse, ref setTrue);
            } 

            string userDatabase = string.Empty;
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                userDatabase = ConfigurationManager.AppSettings["ConnectionString"];
            }

            string calculationDatabase = saveElement["ConnectionString"].ToString();
            ////string calculationDatabase = "Provider=sqloledb;Integrated Security=SSPI;Initial Catalog=TerrascanRECOST;Data Source=dotnet2005";
            ////string userDatabase = "Provider=sqloledb;Integrated Security=SSPI;Database=T2TerraScan;Server=dotnet2005;";
            objRE.set_CalculationDatabase_ConnectionString(ref calculationDatabase);
            objRE.set_UserDatabase_ConnectionString(ref userDatabase);

            objEstimate.Save();
        }

        /// <summary>
        /// F36000_s the calculate RCN.
        /// </summary>
        /// <param name="fieldHashTable">The field hash table.</param>
        /// <param name="componentXml">The component XML.</param>
        /// <returns>string</returns>
        public string F36000_CalculateRCN(Hashtable fieldHashTable, string componentXml, string groupXML, bool newMode)
        {
            bool setTrue = true;
            bool setFalse = false;
            RE7Engine.Estimate objEstimate = AssignRE7Object(fieldHashTable, componentXml, groupXML, newMode);

            objEstimate.Calculate(ref setFalse, ref setTrue);

            return objEstimate.RCN.Total.ToString();
        }

        /// <summary>
        ///  Returns estimate object as hashtable
        /// </summary>
        /// <param name="estimateId"></param>
        /// <returns>Hashtable</returns>
        public Hashtable F36000_GetEstimateObject(int estimateId, string connectionString)
        {
            Hashtable estimateHashTable = new Hashtable();
            RE7Engine.ConstructionType objectConstType;
            RE7Engine.EstimateComponent objectComponent;
            RE7Engine.Estimate objectEstimate = new RE7Engine.Estimate();

            RE7Engine.ResidentialEstimator objectResidenceEstimator = new RE7Engine.ResidentialEstimator();

            string userDatabase = string.Empty;
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                userDatabase = ConfigurationManager.AppSettings["ConnectionString"];
            }

            string calculationDatabase = connectionString;
            ////string calculationDatabase = "Provider=sqloledb;Integrated Security=SSPI;Initial Catalog=TerrascanRECOST;Data Source=dotnet2005";
            ////string userDatabase = "Provider=sqloledb;Integrated Security=SSPI;Database=T2TerraScan;Server=dotnet2005;";
            objectResidenceEstimator.set_CalculationDatabase_ConnectionString(ref calculationDatabase);
            objectResidenceEstimator.set_UserDatabase_ConnectionString(ref userDatabase);

            // Fill Estimate object
            objectEstimate = objectResidenceEstimator.OpenEstimateID(estimateId);

            // Get Construction Type
            estimateHashTable.Add("ConstructionType", objectEstimate.ConstructionType.Key.ToString());

            // Get Zip Code
            estimateHashTable.Add("ZipCode", objectEstimate.Zip);

            // set House Type value
            estimateHashTable.Add("HouseType", objectEstimate.HouseType.Key);

            // The entered quality values must be between objRe
            estimateHashTable.Add("QualityValue", objectEstimate.QualityValue);
            estimateHashTable.Add("QualityDescription", objectEstimate.QualityDescription);

            // Set LowRiseMultiplieUnits
            estimateHashTable.Add("LowRiseMultiplieUnits", objectEstimate.LowRiseMultiplieUnits);

            // RCN value
            if (objectEstimate.RCN != null)
            {
                estimateHashTable.Add("RCNValue", objectEstimate.RCN.Total);
            }

            // Story Height
            estimateHashTable.Add("StoryHeight", objectEstimate.EstimateAdjustment.StoryHeightOverride);

            if (objectEstimate.ConstructionType.Key == "1")
            {
                // TotalFloor Area
                estimateHashTable.Add("FloorArea", objectEstimate.FloorArea);

                // PrimaryStyle
                estimateHashTable.Add("PrimaryStyleValue", objectEstimate.PrimaryStyle.Key);
                estimateHashTable.Add("PrimaryStyleText", objectEstimate.PrimaryStyle.Description);
                estimateHashTable.Add("PrimaryStylePercent", objectEstimate.PrimaryStylePercent);

                if (objectEstimate.SecondaryStyle != null)
                {
                    // SecondaryStyle
                    estimateHashTable.Add("SecondaryStyleKey", objectEstimate.SecondaryStyle.Key);
                    estimateHashTable.Add("SecondaryStyleValue", objectEstimate.SecondaryStyle.Description);
                    estimateHashTable.Add("SecondaryStylePercent", objectEstimate.SecondaryStylePercent);
                }

                // EnergyZone
                objectEstimate.EstimateAdjustment.EnergyZoneOverrideFlag = true;
                estimateHashTable.Add("EnergyAdjustmentKey", objectEstimate.EstimateAdjustment.EnergyZone.Key);
                estimateHashTable.Add("EnergyAdjustmentValue", objectEstimate.EstimateAdjustment.EnergyZone.Description);

                // FoundationZone
                objectEstimate.EstimateAdjustment.FoundationZoneOverrideFlag = true;
                estimateHashTable.Add("FoundationAdjustmentKey", objectEstimate.EstimateAdjustment.FoundationZone.Key);
                estimateHashTable.Add("FoundationAdjustmentValue", objectEstimate.EstimateAdjustment.FoundationZone.Description);

                // HillsideZone
                objectEstimate.EstimateAdjustment.HillsideZoneOverrideFlag = true;
                estimateHashTable.Add("HillSideAdjustmentKey", objectEstimate.EstimateAdjustment.HillsideZone.Key);
                estimateHashTable.Add("HillSideAdjustmentValue", objectEstimate.EstimateAdjustment.HillsideZone.Description);

                // SeismicZone
                objectEstimate.EstimateAdjustment.SeismicZoneOverrideFlag = true;
                estimateHashTable.Add("SeismicAdjustmentKey", objectEstimate.EstimateAdjustment.SeismicZone.Key);
                estimateHashTable.Add("SeismicAdjustmentValue", objectEstimate.EstimateAdjustment.SeismicZone.Description);

                // WindZone
                objectEstimate.EstimateAdjustment.EnergyZoneOverrideFlag = true;
                estimateHashTable.Add("WindAdjustmentKey", objectEstimate.EstimateAdjustment.WindZone.Key);
                estimateHashTable.Add("WindAdjustmentValue", objectEstimate.EstimateAdjustment.WindZone.Description);
            }
            else
            {
                // Manufacturing Type
                // TotalFloor Area
                estimateHashTable.Add("FloorArea", objectEstimate.FloorArea);

                // PrimaryStyle
                estimateHashTable.Add("PrimaryStyleValue", objectEstimate.PrimaryStyle.Description);
                estimateHashTable.Add("PrimaryStyleKey", objectEstimate.PrimaryStyle.Key);
                estimateHashTable.Add("PrimaryStyleLength", objectEstimate.PrimaryStyleLength);
                estimateHashTable.Add("PrimaryStyleWidth", objectEstimate.PrimaryStyleWidth);
                estimateHashTable.Add("PrimaryStylePercent", objectEstimate.PrimaryStylePercent);

                if (objectEstimate.SecondaryStyle != null)
                {
                    // SecondaryStyle
                    estimateHashTable.Add("SecondaryStyleValue", objectEstimate.SecondaryStyle.Description);
                    estimateHashTable.Add("SecondaryStyleKey", objectEstimate.SecondaryStyle.Key);
                    estimateHashTable.Add("SecondaryStyleLength", objectEstimate.SecondaryStyleLength);
                    estimateHashTable.Add("SecondaryStyleWidth", objectEstimate.SecondaryStyleWidth);
                    estimateHashTable.Add("SecondaryStylePercent", objectEstimate.SecondaryStylePercent);
                }

                // WallEnergyZone
                objectEstimate.EstimateAdjustment.WallEnergyZoneOverrideFlag = true;
                estimateHashTable.Add("WallEnergyAdjustmentKey", objectEstimate.EstimateAdjustment.WallEnergyZone.Key);
                estimateHashTable.Add("WallEnergyAdjustmentValue", objectEstimate.EstimateAdjustment.WallEnergyZone.Description);
            }

            // CostMultipler
            estimateHashTable.Add("CostMultipler", objectEstimate.EstimateAdjustment.LocalMultiplierOverride);

            #region Groups

            // Group Xml Data
            DataSet groupComponentDataSet = new DataSet();
            DataTable groupComponentDataTable = new DataTable();
            DataColumn[] groupComponentColumn = new DataColumn[] { new DataColumn("Description"), new DataColumn("ResidenceGroupType_Id"), new DataColumn("SectionKey"), new DataColumn("SquareFeet"), new DataColumn("BaseQuality"), new DataColumn("QualityDescription"), new DataColumn("TagalongWidth"), new DataColumn("TagalongLength"), new DataColumn("GroupID") };
            groupComponentDataTable.Columns.AddRange(groupComponentColumn);
            object groupCode = new object();
            string groupDescription = string.Empty;
            int groupKey = 0;
            int groupSectionKey = 0;
            ////int componentRecordCount = 0;

            // selected Component 
            DataSet selectedComponentDataSet = new DataSet();
            DataTable selectedComponentDataTable = new DataTable();
            DataColumn[] selectedComponentColumn = new DataColumn[] { new DataColumn("Code"), new DataColumn("SelectedSystem"), new DataColumn("SelectedSystemDescription"), new DataColumn("Units"), new DataColumn("Percentage"), new DataColumn("QualityID"), new DataColumn("QualityDescription"), new DataColumn("AllowQualityChangeFlag"), new DataColumn("PercentMaximum"), new DataColumn("PercentMinimum"), new DataColumn("UnitMaximum"), new DataColumn("UnitMinimum"), new DataColumn("SectionKeyValue"), new DataColumn("SystemKeyValue"), new DataColumn("SectionGroupID") };
            selectedComponentDataTable.Columns.AddRange(selectedComponentColumn);
            int componentRowCount = 1;

            for (int count = 1; count <= objectEstimate.Groups.Count; count++)
            {
                object groupId = count;
                groupCode = objectEstimate.Groups.get_Item(ref groupId).ID.ToString();

                groupDescription = objectEstimate.Groups.get_Item(ref groupCode).get_Description().ToString();
                int.TryParse(objectEstimate.Groups.get_Item(ref groupCode).Key.ToString(), out groupKey);

                DataRow selectedGroupRow = groupComponentDataTable.NewRow();
                selectedGroupRow["GroupID"] = objectEstimate.Groups.get_Item(ref groupId).get_GroupType().Key.ToString();
                selectedGroupRow["Description"] = objectEstimate.Groups.get_Item(ref groupCode).get_Description().ToString();

                int.TryParse(objectEstimate.Groups.get_Item(ref groupCode).get_GroupType().Key.ToString(), out groupSectionKey);

                if (groupKey > 0)
                {
                    selectedGroupRow["ResidenceGroupType_Id"] = string.Empty;
                }

                selectedGroupRow["SectionKey"] = objectEstimate.Groups.get_Item(ref groupCode).Key;
                selectedGroupRow["SquareFeet"] = objectEstimate.Groups.get_Item(ref groupCode).Units.ToString();

                // Quality
                ////double groupQuality = 0.0;
                ////double.TryParse(objEstimate.Groups.get_Item(ref groupCode).QualityValueOverride.ToString(),out groupQuality);
                ////if (groupQuality != 0.0)
                ////{
                ////    selectedGroupRow["BaseQuality"] = groupQuality.ToString("#,##0.00");
                ////}

                selectedGroupRow["BaseQuality"] = objectEstimate.Groups.get_Item(ref groupCode).QualityValueOverride.ToString("#,##0.00");
                selectedGroupRow["QualityDescription"] = objectEstimate.Groups.get_Item(ref groupCode).QualityDescription.ToString();

                if (groupSectionKey.ToString().Equals("10"))
                {
                    selectedGroupRow["TagalongWidth"] = objectEstimate.Groups.get_Item(ref groupCode).TagalongWidth.ToString();
                    selectedGroupRow["TagalongLength"] = objectEstimate.Groups.get_Item(ref groupCode).TagalongLength.ToString();
                }
                else
                {
                    selectedGroupRow["TagalongWidth"] = string.Empty;
                    selectedGroupRow["TagalongLength"] = string.Empty;
                }

                #region Component

                object code = new object();

                for (int componentCount = 1; componentCount <= objectEstimate.Groups.get_Item(ref groupId).GroupComponents.Count; componentCount++)
                {
                    object componentId = componentRowCount;
                    code = objectEstimate.EstimateComponents.get_Item(ref componentId).ID.ToString();

                    DataRow selectedRow = selectedComponentDataTable.NewRow();
                    selectedRow["Code"] = objectEstimate.EstimateComponents.get_Item(ref code).get_Component().Key.ToString();
                    selectedRow["SelectedSystem"] = objectEstimate.EstimateComponents.get_Item(ref code).get_Component().Parent.Description.ToString();
                    selectedRow["SelectedSystemDescription"] = objectEstimate.EstimateComponents.get_Item(ref code).get_Component().Description.ToString();

                    // Percentage Value
                    if (objectEstimate.EstimateComponents.get_Item(ref code).get_Component().PercentMaximum != 0.0)
                    {
                        double percentageValue = 0.0;
                        double.TryParse(objectEstimate.EstimateComponents.get_Item(ref code).Percent.ToString(), out percentageValue);
                        if (percentageValue != 0.0)
                        {
                            selectedRow["Percentage"] = percentageValue;
                        }
                        else
                        {
                            selectedRow["Percentage"] = string.Empty;
                        }
                    }
                    else
                    {
                        selectedRow["Percentage"] = string.Empty;
                    }

                    // Units 
                    if (objectEstimate.EstimateComponents.get_Item(ref code).get_Component().UnitMaximum != 0.0)
                    {
                        double unitValue = 0.0;
                        double.TryParse(objectEstimate.EstimateComponents.get_Item(ref code).Units.ToString(), out unitValue);
                        if (unitValue != 0.0)
                        {
                            selectedRow["Units"] = unitValue;
                        }
                        else
                        {
                            selectedRow["Units"] = string.Empty;
                        }
                    }
                    else
                    {
                        selectedRow["Units"] = string.Empty;
                    }

                    // Quality
                    if (objectEstimate.EstimateComponents.get_Item(ref code).get_Component().AllowQualityChangeFlag)
                    {
                        double qualityValue = 0.0;
                        double.TryParse(objectEstimate.EstimateComponents.get_Item(ref code).QualityValueOverride.ToString(), out qualityValue);

                        if (qualityValue > 0.0)
                        {
                            selectedRow["QualityID"] = qualityValue.ToString("#,##0.00");
                            selectedRow["QualityDescription"] = objectEstimate.EstimateComponents.get_Item(ref code).QualityOverrideDescription.ToString();
                        }
                        else
                        {
                            selectedRow["QualityID"] = string.Empty;
                            selectedRow["QualityDescription"] = string.Empty;
                        }
                    }
                    else
                    {
                        selectedRow["QualityID"] = string.Empty;
                        selectedRow["QualityDescription"] = string.Empty;
                    }

                    selectedRow["AllowQualityChangeFlag"] = objectEstimate.EstimateComponents.get_Item(ref code).get_Component().AllowQualityChangeFlag.ToString();
                    selectedRow["PercentMaximum"] = objectEstimate.EstimateComponents.get_Item(ref code).get_Component().PercentMaximum.ToString();
                    selectedRow["PercentMinimum"] = objectEstimate.EstimateComponents.get_Item(ref code).get_Component().PercentMinimum.ToString();
                    selectedRow["UnitMaximum"] = objectEstimate.EstimateComponents.get_Item(ref code).get_Component().UnitMaximum.ToString();
                    selectedRow["UnitMinimum"] = objectEstimate.EstimateComponents.get_Item(ref code).get_Component().UnitMinimum.ToString();
                    selectedRow["SectionKeyValue"] = objectEstimate.EstimateComponents.get_Item(ref code).get_Group().Key;
                    ////componentRecordCount = componentRecordCount + 1;
                    ////selectedRow["SectionKeyValue"] = componentRecordCount;
                    selectedRow["SystemKeyValue"] = objectEstimate.EstimateComponents.get_Item(ref code).get_Component().Parent.Key.ToString();
                    selectedRow["SectionGroupID"] = objectEstimate.Groups.get_Item(ref groupId).get_GroupType().Key.ToString();

                    selectedComponentDataTable.Rows.Add(selectedRow);
                    componentRowCount++;
                }

                #endregion Component

                groupComponentDataTable.Rows.Add(selectedGroupRow);
            }

            // Component
            selectedComponentDataSet.Tables.Add(selectedComponentDataTable);
            string selectedComponentXml = string.Empty;
            selectedComponentXml = selectedComponentDataSet.GetXml();
            estimateHashTable.Add("SelectedComponent", selectedComponentXml);

            // Group
            groupComponentDataSet.Tables.Add(groupComponentDataTable);
            string groupComponentXml = string.Empty;
            groupComponentXml = groupComponentDataSet.GetXml();
            estimateHashTable.Add("SelectedGroup", groupComponentXml);

            #endregion Groups

            return estimateHashTable;
        }

        /// <summary>
        /// Assigns the R e7 object.
        /// </summary>
        /// <param name="fieldHashTable">The field hash table.</param>
        /// <param name="componentXml">The component XML.</param>
        /// <returns>RE7Engine.Estimate</returns>
        public RE7Engine.Estimate AssignRE7Object(Hashtable fieldHashTable, string componentXml, string groupXml, bool editMode)
        {
            RE7Engine.ConstructionType objectConstType;
            RE7Engine.EstimateComponent objectComponent;
            RE7Engine.Estimate objectEstimate = new RE7Engine.Estimate();
            RE7Engine.ResidentialEstimator objectResidenceEsitmator = new RE7Engine.ResidentialEstimator();

            string userDatabase = string.Empty;
            if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                userDatabase = ConfigurationManager.AppSettings["ConnectionString"];
            }

            string calculationDatabase = fieldHashTable["ConnectionString"].ToString();
            objectResidenceEsitmator.set_CalculationDatabase_ConnectionString(ref calculationDatabase);
            objectResidenceEsitmator.set_UserDatabase_ConnectionString(ref userDatabase);

            if (fieldHashTable.Count != 0)
            {
                int estimateId = 0;
                if (fieldHashTable["EstimateID"] != null)
                {
                    int.TryParse(fieldHashTable["EstimateID"].ToString(), out estimateId);
                }

                // set Construction Type
                object refConstructionType = fieldHashTable["ConstructionType"];
                int constructionType;
                int.TryParse(fieldHashTable["ConstructionType"].ToString(), out constructionType);

                // Estimate new Construction value
                objectConstType = objectResidenceEsitmator.get_Data().ConstructionTypes.get_Item(ref refConstructionType);

                if (!editMode)
                {
                    objectEstimate = objectResidenceEsitmator.NewEstimate(ref objectConstType);
                }
                else
                {
                    if (estimateId != 0)
                    {
                        objectEstimate = objectResidenceEsitmator.OpenEstimateID(estimateId);
                    }
                    else
                    {
                        objectEstimate = objectResidenceEsitmator.NewEstimate(ref objectConstType);
                    }
                }

                // User Id
                objectEstimate.UserText4  = fieldHashTable["UserID"].ToString();

                // set Zip Code
                objectEstimate.Zip = fieldHashTable["ZipCode"].ToString();
                objectEstimate.UserText5 = fieldHashTable["ValueSliceID"].ToString();

                // set House Type value
                object refHouseType = fieldHashTable["HouseType"].ToString();
                objectEstimate.HouseType = objectConstType.AllowedHouseTypes.get_Item(ref refHouseType);

                // The entered quality values must be between objRe
                double qualityValue;
                double.TryParse(fieldHashTable["QualityValue"].ToString().Replace(",", ""), out qualityValue);
                objectEstimate.QualityValue = qualityValue;

                // Set LowRiseMultiplieUnits
                int lowRiseMultiplieUnits;
                int.TryParse(fieldHashTable["LowRiseMultiplieUnits"].ToString().Replace(",", ""), out lowRiseMultiplieUnits);
                objectEstimate.LowRiseMultiplieUnits = lowRiseMultiplieUnits;

                // Set StoryHeight
                double stroyHeight;
                double.TryParse(fieldHashTable["StoryHeight"].ToString().Replace(",", ""), out stroyHeight);

                if (stroyHeight > 0.0)
                {
                    objectEstimate.EstimateAdjustment.StoryHeightOverrideFlag = true;
                    objectEstimate.EstimateAdjustment.StoryHeightOverride = stroyHeight;
                }

                // TotalFloorArea
                int floorArea = 0;

                // Check for the Construction Type n Assign the Zone Values
                if (constructionType == 1)
                {
                    // Set house square footage                    
                    int.TryParse(fieldHashTable["FloorArea"].ToString().Replace(",", ""), out floorArea);
                    objectEstimate.FloorArea = floorArea;

                    // Set Primary Style values
                    object refPrimartStyle = fieldHashTable["PrimaryStyleValue"];
                    double primaryStylePercent;
                    double.TryParse(fieldHashTable["PrimaryStylePercent"].ToString(), out primaryStylePercent);
                    objectEstimate.PrimaryStyle = objectEstimate.HouseType.AllowedStyles.get_Item(ref refPrimartStyle).Style;

                    objectEstimate.PrimaryStylePercent = primaryStylePercent;

                    if (!fieldHashTable["SecondaryStyleValue"].ToString().Equals("-1"))
                    {
                        // set secondary stylr Values
                        object refSecondaryStyle = fieldHashTable["SecondaryStyleValue"];
                        double secondaryStylePercent;
                        double.TryParse(fieldHashTable["SecondaryStylePercent"].ToString().Replace(",", ""), out secondaryStylePercent);
                        objectEstimate.SecondaryStyle = objectEstimate.HouseType.AllowedSecondaryStyles.get_Item(ref refSecondaryStyle).Style;
                    }

                    // EnergyZone Adjustments  
                    object eneryKey = fieldHashTable["EnergyAdjustmentKey"];
                    RE7Engine.EnergyZone objEnergyZone = objectResidenceEsitmator.get_Data().EnergyZones.get_Item(ref eneryKey);
                    objectEstimate.EstimateAdjustment.set_EnergyZoneOverride(ref objEnergyZone);
                    objectEstimate.EstimateAdjustment.EnergyZoneOverrideFlag = true;

                    // FoundationZone Adjustments
                    object foundationKey = fieldHashTable["FoundationAdjustmentKey"];
                    RE7Engine.FoundationZone objFoundationZone = objectResidenceEsitmator.get_Data().FoundationZones.get_Item(ref foundationKey);
                    objectEstimate.EstimateAdjustment.set_FoundationZoneOverride(ref objFoundationZone);
                    objectEstimate.EstimateAdjustment.FoundationZoneOverrideFlag = true;

                    // HillSide AdjustmentKey
                    object hillSideKey = fieldHashTable["HillSideAdjustmentKey"];
                    RE7Engine.HillsideZone objHillSideZone = objectResidenceEsitmator.get_Data().HillsideZones.get_Item(ref hillSideKey);
                    objectEstimate.EstimateAdjustment.set_HillsideZoneOverride(ref objHillSideZone);
                    objectEstimate.EstimateAdjustment.HillsideZoneOverrideFlag = true;

                    // Selsmic AdjustmentKey
                    object seismicKey = fieldHashTable["SelsmicAdjustmentKey"];
                    RE7Engine.SeismicZone objSeismicZone = objectResidenceEsitmator.get_Data().SeismicZones.get_Item(ref seismicKey);
                    objectEstimate.EstimateAdjustment.set_SeismicZoneOverride(ref objSeismicZone);
                    objectEstimate.EstimateAdjustment.SeismicZoneOverrideFlag = true;

                    // Wind AdjustmentKey
                    object windKey = fieldHashTable["WindAdjustmentKey"];
                    RE7Engine.WindZone objWindZone = objectResidenceEsitmator.get_Data().WindZones.get_Item(ref windKey);
                    objectEstimate.EstimateAdjustment.set_WindZoneOverride(ref objWindZone);
                    objectEstimate.EstimateAdjustment.WindZoneOverrideFlag = true;
                }
                else
                {
                    // WallEnergy AdjustmentKey
                    object wallEnergy = fieldHashTable["WallEnergyAdjustmentKey"];
                    RE7Engine.WallEnergyZone objWallEnergyZone = objectResidenceEsitmator.get_Data().WallEnergyZones.get_Item(ref wallEnergy);
                    objectEstimate.EstimateAdjustment.set_WallEnergyZoneOverride(ref objWallEnergyZone);
                    objectEstimate.EstimateAdjustment.EnergyZoneOverrideFlag = true;

                    // Sets the PrimaryStyle
                    object refPrimaryStyle = fieldHashTable["PrimaryStyleValue"];
                    objectEstimate.PrimaryStyle = objectEstimate.HouseType.AllowedStyles.get_Item(ref refPrimaryStyle).Style;

                    // Sets manufacturePrimaryLength
                    float manufacturePrimaryLength;
                    float.TryParse(fieldHashTable["PrimaryStyleLength"].ToString(), out manufacturePrimaryLength);
                    objectEstimate.PrimaryStyleLength = manufacturePrimaryLength;

                    // Sets manufacturePrimaryWidth
                    float manufacturePrimaryWidth;
                    float.TryParse(fieldHashTable["PrimaryStyleWidth"].ToString(), out manufacturePrimaryWidth);
                    objectEstimate.PrimaryStyleWidth = manufacturePrimaryWidth;

                    // Sets Secondary Style
                    object refSecondaryStyle = fieldHashTable["SecondaryStyleValue"];
                    int secondaryStylevalue;
                    int.TryParse(fieldHashTable["SecondaryStyleValue"].ToString(), out secondaryStylevalue);

                    if (secondaryStylevalue != -1)
                    {
                        objectEstimate.SecondaryStyle = objectEstimate.HouseType.AllowedSecondaryStyles.get_Item(ref refSecondaryStyle).Style;

                        // sets Secondary Length
                        float manufactureSecondaryLength;
                        float.TryParse(fieldHashTable["SecondaryStyleLength"].ToString(), out manufactureSecondaryLength);
                        objectEstimate.SecondaryStyleLength = manufactureSecondaryLength;

                        // sets secondary width
                        float manufactureSecondaryWidth;
                        float.TryParse(fieldHashTable["SecondaryStyleWidth"].ToString(), out manufactureSecondaryWidth);
                        objectEstimate.SecondaryStyleWidth = manufactureSecondaryWidth;
                    }
                }

                // CostMultipler
                double localMultiplier;
                double.TryParse(fieldHashTable["CostMultipler"].ToString().Replace(",", ""), out localMultiplier);

                if (localMultiplier > 0.0)
                {
                    objectEstimate.EstimateAdjustment.LocalMultiplierOverrideFlag = true;
                    objectEstimate.EstimateAdjustment.LocalMultiplierOverride = localMultiplier;
                }

                // Convert xml to dataTable
                DataSet componentData = new DataSet();
                DataSet groupXmlData = new DataSet();
                if (!string.IsNullOrEmpty(componentXml))
                {
                    StringReader componentXmlString = new StringReader(componentXml);
                    XmlTextReader componentXmlReader = new XmlTextReader(componentXmlString);
                    componentData.ReadXml(componentXmlReader);
                }

                if (!string.IsNullOrEmpty(groupXml))
                {
                    StringReader groupXmlString = new StringReader(groupXml);
                    XmlTextReader groupXmlReader = new XmlTextReader(groupXmlString);
                    groupXmlData.ReadXml(groupXmlReader);
                }

                List<RE7Engine.Component> newComponetn = new List<RE7Engine.Component>();
                int newComponentCount = 0;

                if (groupXmlData != null)
                {
                    if (groupXmlData.Tables.Count > 0)
                    {
                        if (groupXmlData.Tables[0].Rows.Count > 0)
                        {
                            for (int groupRow = 0; groupRow < groupXmlData.Tables[0].Rows.Count; groupRow++)
                            {
                                int groupId;
                                int.TryParse(groupXmlData.Tables[0].Rows[groupRow]["GroupID"].ToString(), out groupId);
                                object refGroupId = groupRow + 1;
                                 //// object refGroupId = groupId;
                                
                                if (groupId > 1)
                                {
                                    RE7Engine.GroupType groupType;
                                    RE7Engine.Group group;

                                    // Creates the New Group
                                    object refGroupType = groupId.ToString();
                                    groupType = objectEstimate.HouseType.AllowedGroupTypes.get_Item(ref refGroupType);
                                    string groupDes = groupXmlData.Tables[0].Rows[groupRow]["Description"].ToString();
                                    group = objectEstimate.Groups.NewGroup(ref groupDes, ref groupType);

                                    int sectionKeyValue;
                                    int groupFloorArea;
                                    int groupWidth;
                                    int groupLength;
                                    double groupQualityValue = 0.0;
                                    int.TryParse(groupXmlData.Tables[0].Rows[groupRow]["SectionKey"].ToString(), out sectionKeyValue);

                                    // Checks Wheather Group Type is Tagalong
                                    if (refGroupType.ToString().Equals("10"))
                                    {
                                        int.TryParse(groupXmlData.Tables[0].Rows[groupRow]["TagalongWidth"].ToString(), out groupWidth);
                                        int.TryParse(groupXmlData.Tables[0].Rows[groupRow]["TagalongLength"].ToString(), out groupLength);

                                        double.TryParse(groupXmlData.Tables[0].Rows[groupRow]["BaseQuality"].ToString(), out groupQualityValue);

                                        if (groupQualityValue > 0.0)
                                        {
                                            group.QualityValueOverrideFlag = true;
                                            group.QualityValueOverride = groupQualityValue;
                                        }

                                        group.TagalongWidth = groupWidth;
                                        group.TagalongLength = groupLength;
                                    }
                                    else
                                    {
                                        int.TryParse(groupXmlData.Tables[0].Rows[groupRow]["SquareFeet"].ToString().Replace(",", ""), out groupFloorArea);

                                        group.Units = groupFloorArea;

                                        double.TryParse(groupXmlData.Tables[0].Rows[groupRow]["BaseQuality"].ToString(), out groupQualityValue);
                                        if (groupQualityValue > 0.0)
                                        {
                                            group.QualityValueOverrideFlag = true;
                                            group.QualityValueOverride = groupQualityValue;
                                        }

                                        group.Units = groupFloorArea;
                                    }

                                    if (componentData.Tables.Count > 0)
                                    {
                                        if (componentData.Tables[0].Rows.Count > 0)
                                        {
                                            // Add new component
                                            for (int code = 0; code < componentData.Tables[0].Rows.Count; code++)
                                            {
                                                int componentGroupId;
                                                int.TryParse(componentData.Tables[0].Rows[code]["SectionGroupID"].ToString(), out componentGroupId);

                                                int componentSectionId;
                                                int.TryParse(componentData.Tables[0].Rows[code]["SectionKeyValue"].ToString(), out componentSectionId);

                                                if (groupId.Equals(componentGroupId) && sectionKeyValue.Equals(componentSectionId))
                                                {
                                                    object refCode = componentData.Tables[0].Rows[code]["Code"].ToString();
                                                    object refSystemId = componentData.Tables[0].Rows[code]["SystemKeyValue"].ToString();
                                                    double refComponentPercent = 0.0;
                                                    double.TryParse(componentData.Tables[0].Rows[code]["Percentage"].ToString(), out refComponentPercent);
                                                    int refComponentUnit = 0;
                                                    int.TryParse(componentData.Tables[0].Rows[code]["Units"].ToString(), out refComponentUnit);

                                                    // Creates the New Components to the Corresponding Group
                                                    newComponetn.Add(objectEstimate.Groups.get_Item(ref refGroupId).get_GroupType().Systems.get_Item(ref refSystemId).Components.get_Item(ref refCode));
                                                    RE7Engine.Component newComponentobject = newComponetn[newComponentCount];
                                                    objectComponent = objectEstimate.Groups.get_Item(ref refGroupId).GroupComponents.NewComponent(ref newComponentobject);

                                                    if (objectEstimate.Groups.get_Item(ref refGroupId).get_GroupType().Systems.get_Item(ref refSystemId).Components.get_Item(ref refCode).PercentMaximum != 0.0)
                                                    {
                                                        if (refComponentPercent != 0)
                                                        {
                                                            objectComponent.Percent = refComponentPercent;
                                                        }
                                                    }

                                                    if (objectEstimate.Groups.get_Item(ref refGroupId).get_GroupType().Systems.get_Item(ref refSystemId).Components.get_Item(ref refCode).UnitMaximum != 0.0)
                                                    {
                                                        // Sets the componentUnit
                                                        if (refComponentUnit != 0.0)
                                                        {
                                                            objectComponent.Units = refComponentUnit;
                                                        }
                                                    }

                                                    if (objectEstimate.Groups.get_Item(ref refGroupId).get_GroupType().Systems.get_Item(ref refSystemId).Components.get_Item(ref refCode).AllowQualityChangeFlag)
                                                    {
                                                        // Sets the QualityValue
                                                        double componentQualityValue;
                                                        double.TryParse(componentData.Tables[0].Rows[code]["QualityID"].ToString(), out componentQualityValue);

                                                        if (componentQualityValue > 0.0)
                                                        {
                                                            objectComponent.QualityValueOverrideFlag = true;
                                                            objectComponent.QualityValueOverride = componentQualityValue;
                                                        }
                                                    }

                                                    newComponentCount++;
                                                }
                                            }
                                        }
                                    }
                                    ////else
                                    ////{
                                    ////    if (objectEstimate.EstimateComponents.Count > 0)
                                    ////    {
                                    ////        int componentCount = objectEstimate.EstimateComponents.Count;
                                    ////        for (int componentId = 1; componentId <= componentCount; componentId++)
                                    ////        {
                                    ////            string outComponent = componentId.ToString();
                                    ////            object objComponent = componentId;
                                    ////            //outComponent = objectEstimate.EstimateComponents.get_Item(ref objComponent).get_Component().Key;
                                    ////            objectEstimate.EstimateComponents.RemoveComponent(ref outComponent);
                                    ////        }
                                    ////    }
                                    ////}
                                }
                                else
                                {
                                    if (qualityValue > 0.0)
                                    {
                                        objectEstimate.Groups.get_Item(ref refGroupId).QualityValueOverrideFlag = true;
                                        objectEstimate.Groups.get_Item(ref refGroupId).QualityValueOverride = qualityValue;
                                    }

                                    objectEstimate.Groups.get_Item(ref refGroupId).Units = floorArea;

                                    if (componentData.Tables.Count > 0)
                                    {
                                        DataRow[] tempDataRow;
                                        tempDataRow = componentData.Tables[0].Select("SectionKeyValue = '" + refGroupId + "'");

                                        if (tempDataRow.Length > 0)
                                        {
                                            DataSet tempMainDataSet = new DataSet();
                                            tempMainDataSet.Merge(tempDataRow);

                                            if (tempMainDataSet.Tables[0].Rows.Count > 0)
                                            {
                                                // Add New Component
                                                for (int code = 0; code < tempMainDataSet.Tables[0].Rows.Count; code++)
                                                {
                                                    int mainComponentGroupId;
                                                    int.TryParse(tempMainDataSet.Tables[0].Rows[code]["SectionGroupID"].ToString(), out mainComponentGroupId);

                                                    if (groupId.Equals(mainComponentGroupId))
                                                    {
                                                        object refCode = tempMainDataSet.Tables[0].Rows[code]["Code"].ToString();
                                                        object refSystemId = tempMainDataSet.Tables[0].Rows[code]["SystemKeyValue"].ToString();
                                                        double refComponentPercent;
                                                        double.TryParse(tempMainDataSet.Tables[0].Rows[code]["Percentage"].ToString(), out refComponentPercent);
                                                        int refComponentUnit = 0;
                                                        int.TryParse(tempMainDataSet.Tables[0].Rows[code]["Units"].ToString(), out refComponentUnit);

                                                        // Creates the New Components to the Corresponding Group
                                                        newComponetn.Add(objectEstimate.Groups.get_Item(ref refGroupId).get_GroupType().Systems.get_Item(ref refSystemId).Components.get_Item(ref refCode));
                                                        RE7Engine.Component newComponentobject = newComponetn[newComponentCount];
                                                        objectComponent = objectEstimate.Groups.get_Item(ref refGroupId).GroupComponents.NewComponent(ref newComponentobject);

                                                        if (objectEstimate.Groups.get_Item(ref refGroupId).get_GroupType().Systems.get_Item(ref refSystemId).Components.get_Item(ref refCode).PercentMaximum != 0.0)
                                                        {
                                                            if (refComponentPercent != 0.0)
                                                            {
                                                                objectComponent.Percent = refComponentPercent;
                                                            }
                                                        }

                                                        if (objectEstimate.Groups.get_Item(ref refGroupId).get_GroupType().Systems.get_Item(ref refSystemId).Components.get_Item(ref refCode).UnitMaximum != 0.0)
                                                        {
                                                            // Sets the componentUnit
                                                            if (refComponentUnit != 0.0)
                                                            {
                                                                objectComponent.Units = refComponentUnit;
                                                            }
                                                        }

                                                        if (objectEstimate.Groups.get_Item(ref refGroupId).get_GroupType().Systems.get_Item(ref refSystemId).Components.get_Item(ref refCode).AllowQualityChangeFlag)
                                                        {
                                                            // Sets the QualityValue
                                                            double componentQualityValue;
                                                            double.TryParse(tempMainDataSet.Tables[0].Rows[code]["QualityID"].ToString(), out componentQualityValue);

                                                            if (componentQualityValue > 0.0)
                                                            {
                                                                objectComponent.QualityValueOverrideFlag = true;
                                                                objectComponent.QualityValueOverride = componentQualityValue;
                                                            }
                                                        }

                                                        newComponentCount++;
                                                    }
                                                }

                                            }
                                        }
                                        else
                                        {
                                            if (objectEstimate.EstimateComponents.Count > 0)
                                            {
                                                int estimateCount = objectEstimate.EstimateComponents.Count;
                                                int seqNo = 1;
                                                for (int componentId = 1; componentId <= estimateCount; componentId++)
                                                {
                                                    object objCompId = componentId;
                                                    int compCount = objectEstimate.EstimateComponents.get_Item(ref objCompId).get_Group().ID;

                                                    if (compCount == 1)
                                                    {
                                                        string outComponent = seqNo.ToString();
                                                        object tempGroupId = 1;
                                                        objectEstimate.Groups.get_Item(ref tempGroupId).GroupComponents.RemoveComponent(ref outComponent);
                                                        estimateCount--;
                                                        componentId--;
                                                        seqNo++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (objectEstimate.EstimateComponents.Count > 0)
                                        {
                                            int componentCount = objectEstimate.EstimateComponents.Count;
                                            for (int componentId = 1; componentId <= componentCount; componentId++)
                                            {
                                                string outComponent = componentId.ToString();
                                                object objComponent = componentId;
                                                //outComponent = objectEstimate.EstimateComponents.get_Item(ref objComponent).get_Component().Key;
                                                objectEstimate.EstimateComponents.RemoveComponent(ref outComponent);
                                            }
                                        }
                                    }

                                    ////int groupCount = objEstimate.Groups.Count;
                                    for (int deleteGroup = 2; deleteGroup <= objectEstimate.Groups.Count; deleteGroup++)
                                    {
                                        if (deleteGroup > 1)
                                        {
                                            object deletedGroupId = deleteGroup;
                                            string refGroupString = objectEstimate.Groups.get_Item(ref deletedGroupId).ID.ToString();
                                            objectEstimate.Groups.RemoveGroup(ref refGroupString);
                                            deleteGroup--;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            ////objEstimate.EstimateAdjustment.ReportDateOverrideFlag = true;
            ////objEstimate.EstimateAdjustment.ReportDateOverride = Convert.ToDateTime("03/01/2006");
            object depType = 5;
            objectEstimate.DepreciationType = objectResidenceEsitmator.get_Data().Depreciations.get_Item(ref depType);

            return objectEstimate;
        }

        #endregion

        #region F36001 Methods

        /// <summary>
        /// F36001_s the calculate RCN.
        /// </summary>
        /// <param name="saveElementsHashTable">The save elements hash table.</param>
        /// <returns></returns>
        public string F36001_CalculateRcn(Hashtable saveElementsHashTable)
        {
            // TotalCoat
            object calcualteTotal = 0;

            if (saveElementsHashTable.Count > 0)
            {
                DataSet occupancyXmlDataSet = new DataSet();
                DataSet componentXmlDataSet = new DataSet();
                DataSet buildingDataXmlDataSet = new DataSet();
                DataSet defaultXmlDataSet = new DataSet();

                // Loading occupancyXml dataset
                StringReader stringReaderOccupancyXml = new StringReader(saveElementsHashTable["OccupancyXml"].ToString());
                XmlTextReader textReaderOccupancyXml = new XmlTextReader(stringReaderOccupancyXml);
                occupancyXmlDataSet.ReadXml(textReaderOccupancyXml);

                // Loading componentXml dataset
                StringReader stringReaderComponentXml = new StringReader(saveElementsHashTable["ComponentXml"].ToString());
                XmlTextReader textReaderComponentXml = new XmlTextReader(stringReaderComponentXml);
                componentXmlDataSet.ReadXml(textReaderComponentXml);

                // Loading buildingDataXml dataset
                StringReader stringReaderBuildingDataXml = new StringReader(saveElementsHashTable["BuildingDataXml"].ToString());
                XmlTextReader textReaderBuildingDataXml = new XmlTextReader(stringReaderBuildingDataXml);
                buildingDataXmlDataSet.ReadXml(textReaderBuildingDataXml);

                // Loading DefaultXml dataset
                StringReader stringReaderDefaultDataXml = new StringReader(saveElementsHashTable["DefaultXml"].ToString());
                XmlTextReader textReaderDefaultDataXml = new XmlTextReader(stringReaderDefaultDataXml);
                defaultXmlDataSet.ReadXml(textReaderDefaultDataXml);

                // Get connection
                CESTEngine.CCESTEngineClass objectCestEngine = new CESTEngine.CCESTEngineClass();

                ////string costDatabase = string.Empty;
                ////if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["CommercialString"]))
                ////{
                ////    costDatabase = ConfigurationManager.AppSettings["CommercialString"];
                ////}
                string connectionString = saveElementsHashTable["ConnectionString"].ToString();
                ////Commented by Biju on 08/08/2007
                ////RDO.rdoEngineClass rdoEngine = new RDO.rdoEngineClass();
                ////RDO.rdoEnvironment rdoEnvironment;
                ////rdoEnvironment = rdoEngine.rdoEnvironments[0];
                ////object connectStingObject = new object();
                ////connectStingObject = (object)connectionString;
                ////object forward = (object)RDO.ResultsetTypeConstants.rdOpenForwardOnly;
                ////object prompt = (object)RDO.PromptConstants.rdDriverNoPrompt;
                ////RDO.rdoConnection rdoConnection = rdoEnvironment.OpenConnection(null, prompt, forward, connectStingObject, null);

                ////object rdoObject = new object();
                ////rdoObject = (object)rdoConnection;

                ////object connectionExit = false;
                ////objectCestEngine.SetRDOConnection(ref rdoObject, connectionExit).ToString();
                ////Commented by Biju on 08/08/2007 ----till here

                ////Added by Biju on 08/08/2007 
                ADODB.Connection adoCon = new ADODB.ConnectionClass();
                adoCon.ConnectionString = connectionString;
                object connectStingObject = new object();
                object connectionExit = false;
                connectStingObject = (object)adoCon;
                objectCestEngine.SetADOConnection(ref connectStingObject, connectionExit );
                ////Added by by Biju on 08/08/2007 ----till here

                ////Added by Latha
                DataTable sectionData = new DataTable();
                sectionData.Columns.AddRange(new DataColumn[] { new DataColumn("SectionType"), new DataColumn("SectionObject") });
                object verfiyEntry = true;
                bool basementApplied = false;
                ////Ends here

                double defaultRankValue = 0.0;
                double defaultlocalMultiplier = 0.0;
                double defaultarchitectFee = 0.0;
                double defaultshape = 0.0;
                double defaultbuildingStories = 0.0;
                int defaultrounding = 0;

                if (defaultXmlDataSet != null)
                {
                    if (defaultXmlDataSet.Tables.Count > 0)
                    {
                        // To get Default values
                        // Rank Default Value                    
                        double.TryParse(defaultXmlDataSet.Tables[0].Rows[0]["Rank"].ToString(), out defaultRankValue);

                        // LM                    
                        double.TryParse(defaultXmlDataSet.Tables[0].Rows[0]["LocalMultiplier"].ToString(), out defaultlocalMultiplier);

                        // ArchFee                    
                        double.TryParse(defaultXmlDataSet.Tables[0].Rows[0]["ArchitectFee"].ToString(), out defaultarchitectFee);

                        // Shape                    
                        double.TryParse(defaultXmlDataSet.Tables[0].Rows[0]["Shape"].ToString(), out defaultshape);

                        // buildingStories                    
                        double.TryParse(defaultXmlDataSet.Tables[0].Rows[0]["BuildingStories"].ToString(), out defaultbuildingStories);

                        // Rounding                    
                        int.TryParse(defaultXmlDataSet.Tables[0].Rows[0]["Rounding"].ToString(), out defaultrounding);
                    }
                }

                object zipCodeObject = saveElementsHashTable["ZipCode"].ToString();

                double localMultiplier = 0.0;
                double architectFee = 0.0;
                long rounding = 0;

                // Add Basement            
                double numberLevels = 0.0;
                int basementPerimeter = 0;
                double basementShape = 0.0;

                // Add Section
                long totalFloorArea = 0;
                double storySection = 0.0;
                double storyBuilding = 0.0;
                int sectionPerimeter = 0;
                double sectionShape = 0.0;

                // Get the values of Building Data
                if (buildingDataXmlDataSet != null)
                {
                    if (buildingDataXmlDataSet.Tables.Count > 0)
                    {
                        double.TryParse(buildingDataXmlDataSet.Tables[0].Rows[0]["LocalMultiplier"].ToString(), out localMultiplier);
                        double.TryParse(buildingDataXmlDataSet.Tables[0].Rows[0]["ArchFeePercentage"].ToString(), out architectFee);
                        long.TryParse(buildingDataXmlDataSet.Tables[0].Rows[0]["Rounding"].ToString(), out rounding);
                        long.TryParse(buildingDataXmlDataSet.Tables[0].Rows[0]["SectionArea"].ToString(), out totalFloorArea);
                        double.TryParse(buildingDataXmlDataSet.Tables[0].Rows[0]["StorySection"].ToString(), out storySection);
                        double.TryParse(buildingDataXmlDataSet.Tables[0].Rows[0]["StoryBuilding"].ToString(), out storyBuilding);
                        int.TryParse(buildingDataXmlDataSet.Tables[0].Rows[0]["StoryPerimeter"].ToString(), out sectionPerimeter);
                        double.TryParse(buildingDataXmlDataSet.Tables[0].Rows[0]["StoryShape"].ToString(), out sectionShape);
                        int.TryParse(buildingDataXmlDataSet.Tables[0].Rows[0]["BasementPerimeter"].ToString(), out basementPerimeter);
                        double.TryParse(buildingDataXmlDataSet.Tables[0].Rows[0]["BasementShape"].ToString(), out basementShape);
                        double.TryParse(buildingDataXmlDataSet.Tables[0].Rows[0]["BasementLevel"].ToString(), out numberLevels);
                    }
                }

                object localMultiplierObject = null;
                if (localMultiplier > 0.0)
                {
                    localMultiplierObject = localMultiplier;
                }
                else
                {
                    localMultiplierObject = defaultlocalMultiplier;
                }

                object storySectionObject = null;
                if (storySection > 0.0)
                {
                    storySectionObject = storySection;
                }
                else
                {
                    storySectionObject = defaultbuildingStories;
                }

                object storyBuildingObject = null;
                if (storyBuilding > 0.0)
                {
                    storyBuildingObject = storyBuilding;
                }
                else
                {
                    storyBuildingObject = defaultbuildingStories;
                }

                object architectFeeObject = null;
                if (architectFee > 0.0)
                {
                    architectFeeObject = architectFee;
                }
                else
                {
                    architectFeeObject = defaultarchitectFee;
                }

                object roundingObject = null;
                if (rounding > 0.0)
                {
                    roundingObject = rounding;
                }
                else
                {
                    roundingObject = defaultrounding;
                }

                object reportDateObject = "";
                object singleLineBackDateObject = "";
                object compondedDeprFlagObject = 0;
                object tfaOverrideObject = true;

                // Add Basement
                object basementNumberObject = 0;
                object numberLevelsObject = null;
                if (numberLevels > 0.0)
                {
                    numberLevelsObject = numberLevels;
                }
                else
                {
                    numberLevelsObject = 1.0;
                }

                object basementPerimeterObject = null;
                if (basementPerimeter > 0)
                {
                    basementPerimeterObject = basementPerimeter;
                }
                else
                {
                    basementPerimeterObject = -1;
                }

                object basementShapeObject = null;
                if (basementShape > 0.0)
                {
                    basementShapeObject = basementShape;
                }
                else
                {
                    basementShapeObject = -1;
                }

                object sectionPerimeterObject = null;
                if (sectionPerimeter > 0)
                {
                    sectionPerimeterObject = sectionPerimeter;
                }
                else
                {
                    sectionPerimeterObject = -1;
                }

                object sectionShapeObject = null;
                if (sectionShape > 0.0)
                {
                    sectionShapeObject = sectionShape;
                }
                else
                {
                    sectionShapeObject = -1;
                }

                // Add Section
                object totalFloorAreaObject = totalFloorArea;
                object sectionNumberObject = 0;
                object nonBuildingFlagObject = false;
                object fireProofObject = true;

                // Null Fields
                object effectiveAgeObject = 0;
                object basedateObject = "";
                object adjustmentObject = 0;
                object deprOverAllObject = 0;
                object deprPhyObject = 0;
                object deprFuntionalObject = 0;
                object deprExternalObject = 0;
                object deprTypeObject = 0;
                object typicalLifeObject = 0;
                object addtionalDeprObject = 0;

                // Error Msg
                object errorMsg = "";

                // Create Estimate
                bool createEstimate = objectCestEngine.CreateEstimate(CESTEngine.CESTTypes.ceAssessorType, zipCodeObject, localMultiplierObject, architectFeeObject, roundingObject, reportDateObject, singleLineBackDateObject, compondedDeprFlagObject, tfaOverrideObject);

                ////Added by Latha
                
                if (buildingDataXmlDataSet != null)
                {
                    if (buildingDataXmlDataSet.Tables.Count > 0 && buildingDataXmlDataSet.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < buildingDataXmlDataSet.Tables[0].Rows.Count; i++)
                        {
                            long.TryParse(buildingDataXmlDataSet.Tables[0].Rows[i]["SectionArea"].ToString(), out totalFloorArea);
                            double.TryParse(buildingDataXmlDataSet.Tables[0].Rows[i]["StorySection"].ToString(), out storySection);
                            double.TryParse(buildingDataXmlDataSet.Tables[0].Rows[i]["StoryBuilding"].ToString(), out storyBuilding);
                            int.TryParse(buildingDataXmlDataSet.Tables[0].Rows[i]["StoryPerimeter"].ToString(), out sectionPerimeter);
                            double.TryParse(buildingDataXmlDataSet.Tables[0].Rows[i]["StoryShape"].ToString(), out sectionShape);

                            if (storySection > 0.0)
                            {
                                storySectionObject = storySection;
                            }
                            else
                            {
                                storySectionObject = defaultbuildingStories;
                            }

                            if (storyBuilding > 0.0)
                            {
                                storyBuildingObject = storyBuilding;
                            }
                            else
                            {
                                storyBuildingObject = defaultbuildingStories;
                            }

                            if (sectionPerimeter > 0)
                            {
                                sectionPerimeterObject = sectionPerimeter;
                            }
                            else
                            {
                                sectionPerimeterObject = -1;
                            }

                            if (sectionShape > 0.0)
                            {
                                sectionShapeObject = sectionShape;
                            }
                            else
                            {
                                sectionShapeObject = -1;
                            }

                            totalFloorAreaObject = totalFloorArea;

                            bool addSection = objectCestEngine.AddSection(totalFloorArea, ref sectionNumberObject, storySectionObject, storyBuildingObject, sectionPerimeterObject, sectionShapeObject, effectiveAgeObject, basedateObject, adjustmentObject, deprOverAllObject, deprPhyObject, deprFuntionalObject, deprExternalObject, nonBuildingFlagObject, typicalLifeObject, deprTypeObject, addtionalDeprObject);

                            DataRow sectionRow = sectionData.NewRow();
                            sectionRow["SectionType"] = buildingDataXmlDataSet.Tables[0].Rows[i]["SectionID"].ToString();
                            sectionRow["SectionObject"] = sectionNumberObject.ToString();
                            sectionData.Rows.Add(sectionRow);
                            /////----Test
                           
                            #region Occupancy

                            if (occupancyXmlDataSet != null)
                            {
                                if (occupancyXmlDataSet.Tables.Count > 0)
                                {
                                    DataRow[] occupancySectionRow;
                                    DataSet occupancySectionDataSet = new DataSet();
                                    //occupancySectionRow = occupancyXmlDataSet.Tables[0].Select("SectionType = '2'");
                                    ////Added by Latha
                                    occupancySectionRow = occupancyXmlDataSet.Tables[0].Select("SectionType = '" + buildingDataXmlDataSet.Tables[0].Rows[i]["SectionID"].ToString() + "'");

                                    if (occupancySectionRow.Length > 0)
                                    {
                                        occupancySectionDataSet.Merge(occupancySectionRow);
                                        int occupancySectionCode = 0;
                                        int occupancySectionPercentage = 0;
                                        double occupancySectionRank = 0.0;
                                        double occupancySectionHeight = 0.0;

                                        if (occupancySectionDataSet.Tables.Count > 0)
                                        {
                                            for (int count = 0; count < occupancySectionDataSet.Tables[0].Rows.Count; count++)
                                            {
                                                int.TryParse(occupancySectionDataSet.Tables[0].Rows[count]["Code"].ToString(), out occupancySectionCode);
                                                int.TryParse(occupancySectionDataSet.Tables[0].Rows[count]["OccupancyPercentage"].ToString(), out occupancySectionPercentage);
                                                double.TryParse(occupancySectionDataSet.Tables[0].Rows[count]["QualityID"].ToString(), out occupancySectionRank);
                                                double.TryParse(occupancySectionDataSet.Tables[0].Rows[count]["DefaultHeight"].ToString(), out  occupancySectionHeight);

                                                // Add Occupancy Section                                
                                                object occupancySectionCodeObject = occupancySectionCode;
                                                object occupancySectionClassObject = occupancySectionDataSet.Tables[0].Rows[count]["Class"].ToString();

                                                // SectionPercentage
                                                object occupancySectionPercentageObject = null;
                                                if (occupancySectionPercentage > 0.0)
                                                {
                                                    occupancySectionPercentageObject = occupancySectionPercentage;
                                                }
                                                else
                                                {
                                                    occupancySectionPercentageObject = -1;
                                                }

                                                // SectionRank
                                                object occupancySectionRankObject = null;
                                                if (occupancySectionRank > 0.0)
                                                {
                                                    occupancySectionRankObject = occupancySectionRank;
                                                }
                                                else
                                                {
                                                    occupancySectionRankObject = defaultRankValue;
                                                }

                                                object occupancySectionHeightObject = occupancySectionHeight;

                                              /*  ////Added by Latha
                                                for (int occupCount = 0; occupCount < sectionData.Rows.Count; occupCount++)
                                                {
                                                    if (occupancySectionDataSet.Tables[0].Rows[count]["SectionType"].ToString() == sectionData.Rows[occupCount]["SectionType"].ToString())
                                                    {
                                                        sectionNumberObject = sectionData.Rows[occupCount]["SectionObject"].ToString();
                                                    }
                                                }
                                                ////Ends here*/

                                                bool occupancySection = objectCestEngine.AddOccupancyToSection(sectionNumberObject, occupancySectionCodeObject, occupancySectionPercentageObject, occupancySectionClassObject, occupancySectionRankObject, occupancySectionHeightObject, verfiyEntry, nonBuildingFlagObject);
                                            }
                                        }
                                    }

                                    DataRow[] occupancyBasementRow;
                                    DataSet occupancyBasementDataSet = new DataSet();
                                    occupancyBasementRow = occupancyXmlDataSet.Tables[0].Select("SectionType = '1'");

                                    if (occupancyBasementRow.Length > 0)
                                    {
                                        // Add Basement
                                        // bool addBasement = objectCestEngine.AddBasement(sectionNumberObject, ref basementNumberObject, numberLevels, basementPerimeter, basementShape, fireProofObject);
                                        bool addBasement = objectCestEngine.AddBasement(sectionNumberObject, ref basementNumberObject, numberLevelsObject, basementPerimeterObject, basementShapeObject, fireProofObject);
                                        basementApplied = true;

                                        occupancyBasementDataSet.Merge(occupancyBasementRow);

                                        object occupancyBasementDeprObject = null;
                                        int occupancyBasementCode = 0;
                                        long occupancyBasementArea = 0;
                                        double occupancyBasementRank = 0.0;
                                        double occupancyBasementDepth = 0.0;

                                        if (occupancyBasementDataSet.Tables.Count > 0)
                                        {
                                            for (int occBasementCount = 0; occBasementCount < occupancyBasementDataSet.Tables[0].Rows.Count; occBasementCount++)
                                            {
                                                int.TryParse(occupancyBasementDataSet.Tables[0].Rows[occBasementCount]["Code"].ToString(), out occupancyBasementCode);
                                                long.TryParse(occupancyBasementDataSet.Tables[0].Rows[occBasementCount]["BasementArea"].ToString(), out occupancyBasementArea);
                                                double.TryParse(occupancyBasementDataSet.Tables[0].Rows[occBasementCount]["QualityID"].ToString(), out occupancyBasementRank);
                                                double.TryParse(occupancyBasementDataSet.Tables[0].Rows[occBasementCount]["DefaultDepth"].ToString(), out occupancyBasementDepth);
                                                string basementType = occupancyBasementDataSet.Tables[0].Rows[occBasementCount]["BasementType"].ToString();

                                                // Add Occupancy Basement
                                                object occupancyBasementCodeObject = occupancyBasementCode;
                                                object occupancyBasementAreaObject = occupancyBasementArea;
                                                object occupancyBasementClassObject = occupancyBasementDataSet.Tables[0].Rows[occBasementCount]["Class"].ToString();

                                                object occupancyBasementRankObject = null;
                                                if (occupancyBasementRank > 0.0)
                                                {
                                                    occupancyBasementRankObject = occupancyBasementRank;
                                                }
                                                else
                                                {
                                                    occupancyBasementRankObject = defaultRankValue;
                                                }

                                                object occupancyBasementDepthObject = occupancyBasementDepth;

                                                switch (basementType.ToLower().Trim())
                                                {
                                                    case "unfinished":
                                                        {
                                                            bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceUnfinishedBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                            break;
                                                        }
                                                    case "finished":
                                                        {
                                                            bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceFinishedBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                            break;
                                                        }
                                                    case "semifinished":
                                                        {
                                                            bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceSemifinishedBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                            break;
                                                        }
                                                    case "display":
                                                        {
                                                            bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceDisplayBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                            break;
                                                        }
                                                    case "office":
                                                        {
                                                            bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceOfficeBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                            break;
                                                        }
                                                    case "parking":
                                                        {
                                                            bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceParkingBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                            break;
                                                        }
                                                    case "resident living":
                                                        {
                                                            bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceResidentLivingBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                            break;
                                                        }
                                                    case "laboratory":
                                                        {
                                                            bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceLaboratoryBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                            break;
                                                        }
                                                    case "classroom":
                                                        {
                                                            bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceClassroomBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                            bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceUnfinishedBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                            break;
                                                        }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion Occupancy
                            #region Component

                            if (componentXmlDataSet != null)
                            {
                                if (componentXmlDataSet.Tables.Count > 0)
                                {
                                    if (componentXmlDataSet.Tables[0].Rows.Count > 0)
                                    {
                                        DataRow[] componentSectionRow;
                                        DataSet componentSectionDataSet = new DataSet();
                                        //componentSectionRow = componentXmlDataSet.Tables[0].Select("SectionType = '2'");
                                        ////(R)Added by Latha
                                        componentSectionRow = componentXmlDataSet.Tables[0].Select("SectionType = '" + buildingDataXmlDataSet.Tables[0].Rows[i]["SectionID"].ToString() + "'");
                                        if (componentSectionRow.Length > 0)
                                        {
                                            componentSectionDataSet.Merge(componentSectionRow);

                                            int componentSectionCode = 0;
                                            double componentSectionPercentage = 0.0;
                                            double componentSectionUnit = 0.0;
                                            double componentSectionRank = 0.0;
                                            double componentSectionOther2 = 0.0;

                                            object depreciation = 0.0;
                                            object size = 0.0;
                                            object effAge = 0.0;
                                            object typeLife = 0.0;

                                            if (componentSectionDataSet.Tables.Count > 0)
                                            {
                                                for (int compSectionCount = 0; compSectionCount < componentSectionDataSet.Tables[0].Rows.Count; compSectionCount++)
                                                {
                                                    // Add Component Section
                                                    int.TryParse(componentSectionDataSet.Tables[0].Rows[compSectionCount]["SystemCode"].ToString(), out componentSectionCode);
                                                    double.TryParse(componentSectionDataSet.Tables[0].Rows[compSectionCount]["Percentage"].ToString(), out componentSectionPercentage);
                                                    double.TryParse(componentSectionDataSet.Tables[0].Rows[compSectionCount]["Units"].ToString(), out componentSectionUnit);
                                                    double.TryParse(componentSectionDataSet.Tables[0].Rows[compSectionCount]["ComponentQualityID"].ToString(), out componentSectionRank);
                                                    double.TryParse(componentSectionDataSet.Tables[0].Rows[compSectionCount]["Other2"].ToString(), out componentSectionOther2);

                                                    object componentSectionCodeObject = componentSectionCode;

                                                    // SectionPercentage
                                                    object componentSectionPercentageObject = null;
                                                    if (componentSectionPercentage > 0)
                                                    {
                                                        componentSectionPercentageObject = componentSectionPercentage;
                                                    }
                                                    else
                                                    {
                                                        componentSectionPercentageObject = -1;
                                                    }

                                                    // SectionOther2
                                                    object componentSectionOther2Object = null;
                                                    if (componentSectionOther2 > 0)
                                                    {
                                                        componentSectionOther2Object = componentSectionOther2;
                                                    }
                                                    else
                                                    {
                                                        componentSectionOther2Object = -1;
                                                    }

                                                    // SectionUnit
                                                    object componentSectionUnitObject = null;
                                                    if (componentSectionUnit > 0)
                                                    {
                                                        componentSectionUnitObject = componentSectionUnit;
                                                    }
                                                    else
                                                    {
                                                        componentSectionUnitObject = -1;
                                                    }

                                                    // SectionRank
                                                    object componentSectionRankObject = null;
                                                    if (componentSectionRank > 0)
                                                    {
                                                        componentSectionRankObject = componentSectionRank;
                                                    }
                                                    else
                                                    {
                                                        componentSectionRankObject = defaultRankValue;
                                                    }
/*
                                                    ////Added by Latha
                                                    for (int occupCount = 0; occupCount < sectionData.Rows.Count; occupCount++)
                                                    {
                                                        if (componentSectionDataSet.Tables[0].Rows[occupCount]["SectionType"].ToString() == sectionData.Rows[occupCount]["SectionType"].ToString())
                                                        {
                                                            sectionNumberObject = sectionData.Rows[occupCount]["SectionObject"].ToString();
                                                        }
                                                    }
                                                    ////Ends here*/

                                                    bool componentSection = objectCestEngine.AddComponentToSection(sectionNumberObject, componentSectionCodeObject, componentSectionPercentageObject, componentSectionUnitObject, componentSectionRankObject, componentSectionOther2Object, depreciation, verfiyEntry, size, effAge, typeLife, nonBuildingFlagObject);
                                                }
                                            }
                                        }

                                        DataRow[] componentBasementRow;
                                        DataSet componentBasementDataSet = new DataSet();
                                        componentBasementRow = componentXmlDataSet.Tables[0].Select("SectionType = '1'");

                                        if (componentBasementRow.Length > 0)
                                        {
                                            if (!basementApplied)
                                            {
                                                // Add Basement
                                                bool addBasement = objectCestEngine.AddBasement(sectionNumberObject, ref basementNumberObject, numberLevelsObject, basementPerimeterObject, basementShapeObject, fireProofObject);
                                            }

                                            componentBasementDataSet.Merge(componentBasementRow);

                                            int componentBasementCode = 0;
                                            double componentBasementPercentage = 0.0;
                                            double componentBasementUnit = 0.0;
                                            double componentBasementRank = 0.0;
                                            double componentBasementOther2 = 0.0;

                                            object depreciation = 0;
                                            object size = 0;
                                            object effAge = 0;
                                            object typeLife = 0;

                                            if (componentBasementDataSet.Tables.Count > 0)
                                            {
                                                for (int compBasementCount = 0; compBasementCount < componentBasementDataSet.Tables[0].Rows.Count; compBasementCount++)
                                                {
                                                    int.TryParse(componentBasementDataSet.Tables[0].Rows[compBasementCount]["SystemCode"].ToString(), out componentBasementCode);
                                                    double.TryParse(componentBasementDataSet.Tables[0].Rows[compBasementCount]["Percentage"].ToString(), out componentBasementPercentage);
                                                    double.TryParse(componentBasementDataSet.Tables[0].Rows[compBasementCount]["Units"].ToString(), out componentBasementUnit);
                                                    double.TryParse(componentBasementDataSet.Tables[0].Rows[compBasementCount]["ComponentQualityID"].ToString(), out componentBasementRank);
                                                    double.TryParse(componentBasementDataSet.Tables[0].Rows[compBasementCount]["Other2"].ToString(), out componentBasementOther2);

                                                    // Add Component Basement
                                                    object componentBasementCodeObject = componentBasementCode;

                                                    // Percentage
                                                    object componentBasementPercentageObject = null;
                                                    if (componentBasementPercentage > 0.0)
                                                    {
                                                        componentBasementPercentageObject = componentBasementPercentage;
                                                    }
                                                    else
                                                    {
                                                        componentBasementPercentageObject = -1;
                                                    }

                                                    // Other2
                                                    object componentBasementOther2Object = null;
                                                    if (componentBasementOther2 > 0.0)
                                                    {
                                                        componentBasementOther2Object = componentBasementOther2;
                                                    }
                                                    else
                                                    {
                                                        componentBasementOther2Object = -1;
                                                    }

                                                    // Units
                                                    object componentBasementUnitObject = null;
                                                    if (componentBasementUnit > 0.0)
                                                    {
                                                        componentBasementUnitObject = componentBasementUnit;
                                                    }
                                                    else
                                                    {
                                                        componentBasementUnitObject = -1;
                                                    }

                                                    // Rank
                                                    object componentBasementRankObject = null;
                                                    if (componentBasementRank > 0.0)
                                                    {
                                                        componentBasementRankObject = componentBasementRank;
                                                    }
                                                    else
                                                    {
                                                        componentBasementRankObject = defaultRankValue;
                                                    }


                                                    bool componentBasement = objectCestEngine.AddComponentToBasement(basementNumberObject, componentBasementCodeObject, componentBasementPercentageObject, componentBasementUnitObject, componentBasementRankObject, componentBasementOther2Object, depreciation, verfiyEntry, size, effAge, typeLife);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion Component
                            /////----Test
                        }
                    }
                }
                else
                {
                    // Add Section
                    bool addSection = objectCestEngine.AddSection(totalFloorArea, ref sectionNumberObject, storySectionObject, storyBuildingObject, sectionPerimeterObject, sectionShapeObject, effectiveAgeObject, basedateObject, adjustmentObject, deprOverAllObject, deprPhyObject, deprFuntionalObject, deprExternalObject, nonBuildingFlagObject, typicalLifeObject, deprTypeObject, addtionalDeprObject);
                    // bool addSection = objectCestEngine.AddSection(5000, ref sectionNumber, 10, 10, 15.0, 0, effectiveAge, basedate, adjustment, deprOverAll, deprPhy, deprFuntional, deprExternal, nonBuildingFlag, typicalLife, deprType, addtionalDepr);
                    DataRow sectionRow = sectionData.NewRow();
                    sectionRow["SectionType"] = buildingDataXmlDataSet.Tables[0].Rows[0]["SectionID"].ToString();
                    sectionRow["SectionObject"] = sectionNumberObject.ToString();
                    sectionData.Rows.Add(sectionRow);
                }

                #region test
                /*
                //object verfiyEntry = true;
                //bool basementApplied = false;

                #region Occupancy

                if (occupancyXmlDataSet != null)
                {
                    if (occupancyXmlDataSet.Tables.Count > 0)
                    {
                        DataRow[] occupancySectionRow;
                        DataSet occupancySectionDataSet = new DataSet();
                        //occupancySectionRow = occupancyXmlDataSet.Tables[0].Select("SectionType = '2'");
                        ////Added by Latha
                        occupancySectionRow = occupancyXmlDataSet.Tables[0].Select("SectionType <> '1'");

                        if (occupancySectionRow.Length > 0)
                        {
                            occupancySectionDataSet.Merge(occupancySectionRow);
                            int occupancySectionCode = 0;
                            int occupancySectionPercentage = 0;
                            double occupancySectionRank = 0.0;
                            double occupancySectionHeight = 0.0;

                            if (occupancySectionDataSet.Tables.Count > 0)
                            {
                                for (int count = 0; count < occupancySectionDataSet.Tables[0].Rows.Count; count++)
                                {
                                    int.TryParse(occupancySectionDataSet.Tables[0].Rows[count]["Code"].ToString(), out occupancySectionCode);
                                    int.TryParse(occupancySectionDataSet.Tables[0].Rows[count]["OccupancyPercentage"].ToString(), out occupancySectionPercentage);
                                    double.TryParse(occupancySectionDataSet.Tables[0].Rows[count]["QualityID"].ToString(), out occupancySectionRank);
                                    double.TryParse(occupancySectionDataSet.Tables[0].Rows[count]["DefaultHeight"].ToString(), out  occupancySectionHeight);

                                    // Add Occupancy Section                                
                                    object occupancySectionCodeObject = occupancySectionCode;
                                    object occupancySectionClassObject = occupancySectionDataSet.Tables[0].Rows[count]["Class"].ToString();

                                    // SectionPercentage
                                    object occupancySectionPercentageObject = null;
                                    if (occupancySectionPercentage > 0.0)
                                    {
                                        occupancySectionPercentageObject = occupancySectionPercentage;
                                    }
                                    else
                                    {
                                        occupancySectionPercentageObject = -1;
                                    }

                                    // SectionRank
                                    object occupancySectionRankObject = null;
                                    if (occupancySectionRank > 0.0)
                                    {
                                        occupancySectionRankObject = occupancySectionRank;
                                    }
                                    else
                                    {
                                        occupancySectionRankObject = defaultRankValue;
                                    }

                                    object occupancySectionHeightObject = occupancySectionHeight;

                                    ////Added by Latha
                                    for (int occupCount = 0; occupCount < sectionData.Rows.Count; occupCount++)
                                    {
                                        if (occupancySectionDataSet.Tables[0].Rows[count]["SectionType"].ToString() == sectionData.Rows[occupCount]["SectionType"].ToString())
                                        {
                                            sectionNumberObject = sectionData.Rows[occupCount]["SectionObject"].ToString();
                                        }
                                    }
                                    ////Ends here

                                    bool occupancySection = objectCestEngine.AddOccupancyToSection(sectionNumberObject, occupancySectionCodeObject, occupancySectionPercentageObject, occupancySectionClassObject, occupancySectionRankObject, occupancySectionHeightObject, verfiyEntry, nonBuildingFlagObject);
                                }
                            }
                        }

                        DataRow[] occupancyBasementRow;
                        DataSet occupancyBasementDataSet = new DataSet();
                        occupancyBasementRow = occupancyXmlDataSet.Tables[0].Select("SectionType = '1'");

                        if (occupancyBasementRow.Length > 0)
                        {
                            // Add Basement
                            // bool addBasement = objectCestEngine.AddBasement(sectionNumberObject, ref basementNumberObject, numberLevels, basementPerimeter, basementShape, fireProofObject);
                            bool addBasement = objectCestEngine.AddBasement(sectionNumberObject, ref basementNumberObject, numberLevelsObject, basementPerimeterObject, basementShapeObject, fireProofObject);
                            basementApplied = true;

                            occupancyBasementDataSet.Merge(occupancyBasementRow);

                            object occupancyBasementDeprObject = null;
                            int occupancyBasementCode = 0;
                            long occupancyBasementArea = 0;
                            double occupancyBasementRank = 0.0;
                            double occupancyBasementDepth = 0.0;

                            if (occupancyBasementDataSet.Tables.Count > 0)
                            {
                                for (int occBasementCount = 0; occBasementCount < occupancyBasementDataSet.Tables[0].Rows.Count; occBasementCount++)
                                {
                                    int.TryParse(occupancyBasementDataSet.Tables[0].Rows[occBasementCount]["Code"].ToString(), out occupancyBasementCode);
                                    long.TryParse(occupancyBasementDataSet.Tables[0].Rows[occBasementCount]["BasementArea"].ToString(), out occupancyBasementArea);
                                    double.TryParse(occupancyBasementDataSet.Tables[0].Rows[occBasementCount]["QualityID"].ToString(), out occupancyBasementRank);
                                    double.TryParse(occupancyBasementDataSet.Tables[0].Rows[occBasementCount]["DefaultDepth"].ToString(), out occupancyBasementDepth);
                                    string basementType = occupancyBasementDataSet.Tables[0].Rows[occBasementCount]["BasementType"].ToString();

                                    // Add Occupancy Basement
                                    object occupancyBasementCodeObject = occupancyBasementCode;
                                    object occupancyBasementAreaObject = occupancyBasementArea;
                                    object occupancyBasementClassObject = occupancyBasementDataSet.Tables[0].Rows[occBasementCount]["Class"].ToString();

                                    object occupancyBasementRankObject = null;
                                    if (occupancyBasementRank > 0.0)
                                    {
                                        occupancyBasementRankObject = occupancyBasementRank;
                                    }
                                    else
                                    {
                                        occupancyBasementRankObject = defaultRankValue;
                                    }

                                    object occupancyBasementDepthObject = occupancyBasementDepth;

                                    switch (basementType.ToLower().Trim())
                                    {
                                        case "unfinished":
                                            {
                                                bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceUnfinishedBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                break;
                                            }
                                        case "finished":
                                            {
                                                bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceFinishedBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                break;
                                            }
                                        case "semifinished":
                                            {
                                                bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceSemifinishedBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                break;
                                            }
                                        case "display":
                                            {
                                                bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceDisplayBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                break;
                                            }
                                        case "office":
                                            {
                                                bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceOfficeBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                break;
                                            }
                                        case "parking":
                                            {
                                                bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceParkingBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                break;
                                            }
                                        case "resident living":
                                            {
                                                bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceResidentLivingBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                break;
                                            }
                                        case "laboratory":
                                            {
                                                bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceLaboratoryBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                break;
                                            }
                                        case "classroom":
                                            {
                                                bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceClassroomBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                break;
                                            }
                                        default:
                                            {
                                                bool occupancyBasement = objectCestEngine.AddOccupancyToBasement(basementNumberObject, occupancyBasementCodeObject, occupancyBasementClassObject, CESTEngine.BasementTypes.ceUnfinishedBSMT, occupancyBasementAreaObject, occupancyBasementDepthObject, occupancyBasementRankObject, occupancyBasementDeprObject, verfiyEntry);
                                                break;
                                            }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion Occupancy

                #region Component

                if (componentXmlDataSet != null)
                {
                    if (componentXmlDataSet.Tables.Count > 0)
                    {
                        if (componentXmlDataSet.Tables[0].Rows.Count > 0)
                        {
                            DataRow[] componentSectionRow;
                            DataSet componentSectionDataSet = new DataSet();
                            //componentSectionRow = componentXmlDataSet.Tables[0].Select("SectionType = '2'");
                            ////(R)Added by Latha
                            componentSectionRow = componentXmlDataSet.Tables[0].Select("SectionType <> '1'");
                            if (componentSectionRow.Length > 0)
                            {
                                componentSectionDataSet.Merge(componentSectionRow);

                                int componentSectionCode = 0;
                                double componentSectionPercentage = 0.0;
                                double componentSectionUnit = 0.0;
                                double componentSectionRank = 0.0;
                                double componentSectionOther2 = 0.0;

                                object depreciation = 0.0;
                                object size = 0.0;
                                object effAge = 0.0;
                                object typeLife = 0.0;

                                if (componentSectionDataSet.Tables.Count > 0)
                                {
                                    for (int compSectionCount = 0; compSectionCount < componentSectionDataSet.Tables[0].Rows.Count; compSectionCount++)
                                    {
                                        // Add Component Section
                                        int.TryParse(componentSectionDataSet.Tables[0].Rows[compSectionCount]["SystemCode"].ToString(), out componentSectionCode);
                                        double.TryParse(componentSectionDataSet.Tables[0].Rows[compSectionCount]["Percentage"].ToString(), out componentSectionPercentage);
                                        double.TryParse(componentSectionDataSet.Tables[0].Rows[compSectionCount]["Units"].ToString(), out componentSectionUnit);
                                        double.TryParse(componentSectionDataSet.Tables[0].Rows[compSectionCount]["ComponentQualityID"].ToString(), out componentSectionRank);
                                        double.TryParse(componentSectionDataSet.Tables[0].Rows[compSectionCount]["Other2"].ToString(), out componentSectionOther2);

                                        object componentSectionCodeObject = componentSectionCode;

                                        // SectionPercentage
                                        object componentSectionPercentageObject = null;
                                        if (componentSectionPercentage > 0)
                                        {
                                            componentSectionPercentageObject = componentSectionPercentage;
                                        }
                                        else
                                        {
                                            componentSectionPercentageObject = -1;
                                        }

                                        // SectionOther2
                                        object componentSectionOther2Object = null;
                                        if (componentSectionOther2 > 0)
                                        {
                                            componentSectionOther2Object = componentSectionOther2;
                                        }
                                        else
                                        {
                                            componentSectionOther2Object = -1;
                                        }

                                        // SectionUnit
                                        object componentSectionUnitObject = null;
                                        if (componentSectionUnit > 0)
                                        {
                                            componentSectionUnitObject = componentSectionUnit;
                                        }
                                        else
                                        {
                                            componentSectionUnitObject = -1;
                                        }

                                        // SectionRank
                                        object componentSectionRankObject = null;
                                        if (componentSectionRank > 0)
                                        {
                                            componentSectionRankObject = componentSectionRank;
                                        }
                                        else
                                        {
                                            componentSectionRankObject = defaultRankValue;
                                        }

                                        ////Added by Latha
                                        for (int occupCount = 0; occupCount < sectionData.Rows.Count; occupCount++)
                                        {
                                            if (componentSectionDataSet.Tables[0].Rows[occupCount]["SectionType"].ToString() == sectionData.Rows[occupCount]["SectionType"].ToString())
                                            {
                                                sectionNumberObject = sectionData.Rows[occupCount]["SectionObject"].ToString();
                                            }
                                        }
                                        ////Ends here

                                        bool componentSection = objectCestEngine.AddComponentToSection(sectionNumberObject, componentSectionCodeObject, componentSectionPercentageObject, componentSectionUnitObject, componentSectionRankObject, componentSectionOther2Object, depreciation, verfiyEntry, size, effAge, typeLife, nonBuildingFlagObject);
                                    }
                                }
                            }

                            DataRow[] componentBasementRow;
                            DataSet componentBasementDataSet = new DataSet();
                            componentBasementRow = componentXmlDataSet.Tables[0].Select("SectionType = '1'");

                            if (componentBasementRow.Length > 0)
                            {
                                if (!basementApplied)
                                {
                                    // Add Basement
                                    bool addBasement = objectCestEngine.AddBasement(sectionNumberObject, ref basementNumberObject, numberLevelsObject, basementPerimeterObject, basementShapeObject, fireProofObject);
                                }

                                componentBasementDataSet.Merge(componentBasementRow);

                                int componentBasementCode = 0;
                                double componentBasementPercentage = 0.0;
                                double componentBasementUnit = 0.0;
                                double componentBasementRank = 0.0;
                                double componentBasementOther2 = 0.0;

                                object depreciation = 0;
                                object size = 0;
                                object effAge = 0;
                                object typeLife = 0;

                                if (componentBasementDataSet.Tables.Count > 0)
                                {
                                    for (int compBasementCount = 0; compBasementCount < componentBasementDataSet.Tables[0].Rows.Count; compBasementCount++)
                                    {
                                        int.TryParse(componentBasementDataSet.Tables[0].Rows[compBasementCount]["SystemCode"].ToString(), out componentBasementCode);
                                        double.TryParse(componentBasementDataSet.Tables[0].Rows[compBasementCount]["Percentage"].ToString(), out componentBasementPercentage);
                                        double.TryParse(componentBasementDataSet.Tables[0].Rows[compBasementCount]["Units"].ToString(), out componentBasementUnit);
                                        double.TryParse(componentBasementDataSet.Tables[0].Rows[compBasementCount]["ComponentQualityID"].ToString(), out componentBasementRank);
                                        double.TryParse(componentBasementDataSet.Tables[0].Rows[compBasementCount]["Other2"].ToString(), out componentBasementOther2);

                                        // Add Component Basement
                                        object componentBasementCodeObject = componentBasementCode;

                                        // Percentage
                                        object componentBasementPercentageObject = null;
                                        if (componentBasementPercentage > 0.0)
                                        {
                                            componentBasementPercentageObject = componentBasementPercentage;
                                        }
                                        else
                                        {
                                            componentBasementPercentageObject = -1;
                                        }

                                        // Other2
                                        object componentBasementOther2Object = null;
                                        if (componentBasementOther2 > 0.0)
                                        {
                                            componentBasementOther2Object = componentBasementOther2;
                                        }
                                        else
                                        {
                                            componentBasementOther2Object = -1;
                                        }

                                        // Units
                                        object componentBasementUnitObject = null;
                                        if (componentBasementUnit > 0.0)
                                        {
                                            componentBasementUnitObject = componentBasementUnit;
                                        }
                                        else
                                        {
                                            componentBasementUnitObject = -1;
                                        }

                                        // Rank
                                        object componentBasementRankObject = null;
                                        if (componentBasementRank > 0.0)
                                        {
                                            componentBasementRankObject = componentBasementRank;
                                        }
                                        else
                                        {
                                            componentBasementRankObject = defaultRankValue;
                                        }


                                        bool componentBasement = objectCestEngine.AddComponentToBasement(basementNumberObject, componentBasementCodeObject, componentBasementPercentageObject, componentBasementUnitObject, componentBasementRankObject, componentBasementOther2Object, depreciation, verfiyEntry, size, effAge, typeLife);
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion Component
                */
                #endregion test
                // calculate
                bool calcualteEstimate = objectCestEngine.CalcEstimate(true);
                objectCestEngine.GetLastError(ref errorMsg);

                if (calcualteEstimate)
                {
                    object calcualteLineCode = "H000634";
                    object calcualteUnits = 0;
                    object calcualteCost = 0;
                    object calcualtePercentage = 0;
                    bool totalRcn = objectCestEngine.GetReportLine(CESTEngine.TotalTypes.ceTotalForEstimate, calcualteLineCode, ref calcualteUnits, ref calcualteCost, ref calcualteTotal, ref calcualtePercentage);
                    return calcualteTotal.ToString();
                }
                else
                {
                    return errorMsg.ToString();
                }
            }

            return calcualteTotal.ToString();
        }

        #endregion
    }
}
