//--------------------------------------------------------------------------------------------
// <copyright file="F3100.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F3100 Form Slice - AgencyFundMgmt 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 12-04-2007      Sriparameswari        Created
//*********************************************************************************/
namespace D36000
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.SmartParts;
    using System.Collections;
    using System.IO;
    using TerraScan.Utilities;
    using System.Configuration;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.BusinessEntities;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Helper;
    using TerraScan.UI.Controls;

    /// <summary>
    /// F3100 ClassFile
    /// </summary>
    public partial class F3100 : Form
    {
        #region Variable

        /// <summary>
        /// houseTypeXml
        /// </summary>
        private string houseTypeXml;

        /// <summary>
        /// componentFormxmlValue
        /// </summary>
        private string componentFormxmlValue;

        /// <summary>
        /// componentAssumptionsDataSet
        /// </summary>
        private DataSet componentAssumptionsDataSet = new DataSet();

        /// <summary>
        /// Used to store houseTypeId
        /// </summary>
        private int houseTypeId;

        /// <summary>
        /// Used to store val
        /// </summary>
        private int val;

        /// <summary>
        /// Used to store loadExterierMaxVal
        /// </summary>
        private int loadExterierMaxVal;

        /// <summary>
        /// Used to store loadExterierMinVal
        /// </summary>
        private int loadExterierMinVal;

        /// <summary>
        /// Used to store raisedSubfloorMax
        /// </summary>
        private string raisedSubfloorMax;

        /// <summary>
        /// Used to store raisedSubfloorMaxVal
        /// </summary>
        private int raisedSubfloorMaxVal;

        /// <summary>
        /// Used to store raisedSubfloorMin
        /// </summary>
        private string raisedSubfloorMin;

        /// <summary>
        /// Used to store raisedSubfloorMinVal
        /// </summary>
        private int raisedSubfloorMinVal;

        /// <summary>
        /// Used to store plumbingFixturesMax
        /// </summary>
        private string plumbingFixturesMax;

        /// <summary>
        /// Used to store plumbingFixturesMaxVal
        /// </summary>
        private int plumbingFixturesMaxVal;

        /// <summary>
        /// Used to store plumbingFixturesMaxVal
        /// </summary>
        private string plumbingFixturesMin;

        /// <summary>
        /// Used to store plumbingFixturesMinVal
        /// </summary>
        private int plumbingFixturesMinVal;

        /// <summary>
        /// Used to store roughInsMax
        /// </summary>
        private string roughInsMax;

        /// <summary>
        /// Used to store roughInsMaxVal
        /// </summary>
        private int roughInsMaxVal;

        /// <summary>
        /// Used to store roughInsMin
        /// </summary>
        private string roughInsMin;

        /// <summary>
        /// Used to store roughInsMinVal
        /// </summary>
        private int roughInsMinVal;

        /// <summary>
        /// Used to store partitionsDryWallMax
        /// </summary>
        private string partitionsDryWallMax;

        /// <summary>
        /// Used to store partitionsDryWallMax
        /// </summary>
        private int partitionsDryWallMaxVal;

        /// <summary>
        /// Used to store partitionsDryWallMin
        /// </summary>
        private string partitionsDryWallMin;

        /// <summary>
        /// Used to store partitionsDryWallMin
        /// </summary>
        private int partitionsDryWallMinVal;

        /// <summary>
        /// Used to store bIsBack
        /// </summary>
        private bool backVal;

        /// <summary>
        /// Used to store backControl
        /// </summary>
        private bool backControl;

        /// <summary>
        /// Used to store backTab
        /// </summary>
        private bool backTab;

        /// <summary>
        /// Used to store loadExterier
        /// </summary>
        private string loadExterier;

        /// <summary>
        /// binarcon
        /// </summary>
        private bool binarcon;

        /// <summary>
        /// Used to store loadPartitionsDryWallMin1
        /// </summary>
        private int loadPartitionsDryWallMin1;

        /// <summary>
        /// Used to store loadPartitionsDryWallMin1
        /// </summary>
        private int loadPartitionsDryWallMax1;

        /// <summary>
        /// Used to store loadPlumbingFixturesMax1
        /// </summary>
        private int loadPlumbingFixturesMax1;

        /// <summary>
        /// templateDataTable
        /// </summary>
        private DataTable templateDataTable = new DataTable();

        #endregion

        /// <summary>
        /// SectionReturn XML Value
        /// </summary>
        public string ComponentFormXmlValue
        {
            get { return this.componentFormxmlValue; }
            set { this.componentFormxmlValue = value; }
        }

        /// <summary>
        /// SectionReturn XML Value
        /// </summary>
        public DataTable TemplateDataTable
        {
            get { return this.templateDataTable; }
            set { this.templateDataTable = value; }
        }

        /// <summary>
        /// F3100
        /// </summary>
        /// <param name="xmlType">xmlType</param>
        public F3100(string xmlType)
        {
            InitializeComponent();
            this.houseTypeXml = xmlType;
        }

        /// <summary>
        /// F3100_Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e"> e</param>
        private void F3100_Load(object sender, EventArgs e)
        {
            this.AcceptButton = this.OkButton;
            this.CancelButton = this.CancelButtons;

            ////ExteriorTextBox.Text = "100 %";
            Automatic1checkBox.Checked = true;
            Automatic2checkBox.Checked = true;
            ExteriorWalls2Combo.Enabled = false;
            bool binarcon = true;
            StringReader stringXmlReader1 = new StringReader(this.houseTypeXml);
            System.Xml.XmlTextReader textReaderHouseType1 = new System.Xml.XmlTextReader(stringXmlReader1);
            this.componentAssumptionsDataSet.ReadXml(textReaderHouseType1);
            string houseTypeIds = string.Empty;
            houseTypeIds = this.componentAssumptionsDataSet.Tables["ResidenceType"].Rows[0]["Key"].ToString();
            this.houseTypeId = Convert.ToInt32(houseTypeIds);
            this.loadExterier = string.Empty;
            if (this.houseTypeId != 6)
            {
                Insidepanel.Visible = true;
            }
            else
            {
                Insidepanel.Visible = false;
            }

            try
            {
                StringReader stringXmlReader = new StringReader(this.houseTypeXml);
                System.Xml.XmlTextReader textReaderHouseType = new System.Xml.XmlTextReader(stringXmlReader);
                this.componentAssumptionsDataSet.ReadXml(textReaderHouseType);
                ////string aa = this.componentAssumptionsDataSet.Tables["ResidenceType"].Rows[0]["Keys"].ToString();
                ////this.componentAssumptionsDataSet.ReadXml(@"C://GetResidenceType_1.xml");

                ////string houseTypeIds = string.Empty;
                ////houseTypeIds = this.componentAssumptionsDataSet.Tables["ResidenceType"].Rows[0]["Key"].ToString();

                this.plumbingFixturesMax = "601";
                this.plumbingFixturesMin = "601";
                this.raisedSubfloorMax = "622";
                this.raisedSubfloorMin = "622";
                this.roughInsMax = "602";
                this.roughInsMin = "602";
                this.partitionsDryWallMax = "631";
                this.partitionsDryWallMin = "631";
                this.LoadExteriorWalls1Combo();
                //// this.LoadExteriorWalls2Combo();
                this.LoadRoofingCombo();
                this.LoadHeatingorCoolingCombo();

                if (!string.IsNullOrEmpty(ExteriorWalls1Combo.Text.Trim()))
                {
                    this.loadExterier = this.ExteriorWalls1Combo.SelectedValue.ToString();
                }

                this.loadExterierMaxVal = this.LoadExteriorTextBox(this.loadExterier);
                this.ExteriorTextBox.Text = this.loadExterierMaxVal.ToString() + " %";
                this.loadExterierMinVal = this.LoadExteriorslabel(this.loadExterier);
                this.Exteriorslabel.Text = this.loadExterierMinVal.ToString() + " %";
                this.plumbingFixturesMaxVal = this.LoadPlumbingFixturesMax(this.plumbingFixturesMax);
                this.plumbingFixturesMinVal = this.LoadPlumbingFixturesMin(this.plumbingFixturesMin);
                this.roughInsMaxVal = this.LoadRoughInsMax(this.roughInsMax);
                this.roughInsMinVal = this.LoadRoughInsMin(this.roughInsMin);
                this.raisedSubfloorMaxVal = this.LoadRaisedSubfloorMax(this.raisedSubfloorMax);
                this.raisedSubfloorMinVal = this.LoadRaisedSubfloorMin(this.raisedSubfloorMin);
                this.partitionsDryWallMinVal = this.LoadpartitionsDryWallMin(this.partitionsDryWallMin);
                //// PartitionsDrayWalllabel.Text = partitionsDryWallMaxVal.ToString();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Datas the row to data table.
        /// </summary>
        /// <param name="tempDataRow">The temp data row.</param>
        /// <returns>DataTable</returns>
        private DataTable DataRowToDataTable(DataRow[] tempDataRow)
        {
            DataSet convertedDataSet = new DataSet();
            convertedDataSet.Merge(tempDataRow);
            return convertedDataSet.Tables[0];
        }

        /// <summary>
        /// LoadExteriorWalls1Combo
        /// </summary>
        private void LoadExteriorWalls1Combo()
        {
            DataRow[] primaryStyleDataRow;
            primaryStyleDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select("Components_Id = 0");
            this.ExteriorWalls1Combo.DataSource = this.DataRowToDataTable(primaryStyleDataRow);
            this.ExteriorWalls1Combo.DisplayMember = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Columns["Description"].ColumnName;
            this.ExteriorWalls1Combo.ValueMember = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Columns["Key"].ColumnName;
            this.ExteriorWalls1Combo.SelectedIndex = 0;
        }

        /// <summary>
        /// Load ExteriorWalls2Combo.
        /// </summary>
        private void LoadExteriorWalls2Combo()
        {
            string loadExteriorWalls1ComboSelectedValues;
            ////int  LoadExteriorWalls1ComboSelectedValue;
            loadExteriorWalls1ComboSelectedValues = ExteriorWalls1Combo.SelectedValue.ToString();
            //// LoadExteriorWalls1ComboSelectedValue = Convert.ToInt32(loadExteriorWalls1ComboSelectedValues);
            DataRow[] primaryStyleDataRow;
            primaryStyleDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select("Components_Id = 0 AND key <> " + loadExteriorWalls1ComboSelectedValues + "");
            //// primaryStyleDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select("Components_Id = 0 AND key <> 101");
            this.ExteriorWalls2Combo.DataSource = this.DataRowToDataTable(primaryStyleDataRow);
            this.ExteriorWalls2Combo.DisplayMember = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Columns["Description"].ColumnName;
            this.ExteriorWalls2Combo.ValueMember = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Columns["Key"].ColumnName;
            this.ExteriorWalls2Combo.SelectedIndex = 0;
        }

        /// <summary>
        /// Load RoofingCombo.
        /// </summary>
        private void LoadRoofingCombo()
        {
            DataRow[] primaryStyleDataRow;
            primaryStyleDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select("Components_Id = 1");
            this.RoofingCombo.DataSource = this.DataRowToDataTable(primaryStyleDataRow);
            this.RoofingCombo.DisplayMember = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Columns["Description"].ColumnName;
            this.RoofingCombo.ValueMember = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Columns["Key"].ColumnName;
            this.RoofingCombo.SelectedIndex = 0;
        }

        /// <summary>
        /// Load   HeatingorCoolingCombo.
        /// </summary>
        private void LoadHeatingorCoolingCombo()
        {
            DataRow[] primaryStyleDataRow;
            primaryStyleDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select("Components_Id = 2");
            this.HeatingorCoolingCombo.DataSource = this.DataRowToDataTable(primaryStyleDataRow);
            this.HeatingorCoolingCombo.DisplayMember = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Columns["Description"].ColumnName;
            this.HeatingorCoolingCombo.ValueMember = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Columns["Key"].ColumnName;
            this.HeatingorCoolingCombo.SelectedIndex = 0;
        }

        /// <summary>
        /// LoadExteriorTextBox
        /// </summary>
        /// <param name="loadExterier">loadExterier</param>
        /// <returns>int</returns>
        private int LoadExteriorTextBox(string loadExterier)
        {
            DataRow[] primaryStyleDataRow;
            primaryStyleDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select("Key= '" + loadExterier + "'");
            string loadExterier1 = primaryStyleDataRow[0]["PercentMaximum"].ToString();
            int loadExteriorTextBox1 = Convert.ToInt32(loadExterier1);
            return loadExteriorTextBox1;
        }

        /// <summary>
        /// LoadExteriorslabel
        /// </summary>
        /// <param name="loadExterier">loadExterier</param>
        /// <returns>int</returns>
        private int LoadExteriorslabel(string loadExterier)
        {
            DataRow[] primaryStyleDataRow;
            primaryStyleDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select("Key= '" + loadExterier + "'");
            if (primaryStyleDataRow.Length != 0)
            {
                string loadExterier1 = primaryStyleDataRow[0]["PercentMinimum"].ToString();
                int exteriorslabel = Convert.ToInt32(loadExterier1);
                return exteriorslabel;
            }
            else
            {
                ExteriorTextBox.Enabled = false;
                return 0;
            }
        }

        /// <summary>
        /// LoadPlumbingFixturesMax
        /// </summary>
        /// <param name="plumbingFixturesMax">plumbingFixturesMax</param>
        /// <returns>int</returns>
        private int LoadPlumbingFixturesMax(string plumbingFixturesMax)
        {
            DataRow[] primaryStyleDataRow;
            primaryStyleDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select("Key= '" + plumbingFixturesMax + "'");
            if (primaryStyleDataRow.Length != 0)
            {
                plumbingFixturesMax = primaryStyleDataRow[0]["UnitMaximum"].ToString();
                int loadPlumbingFixturesMax1 = Convert.ToInt32(plumbingFixturesMax);
                return loadPlumbingFixturesMax1;
            }
            else
            {
                PlumbingTextBox.Enabled = false;
                return 0;
            }
        }

        /// <summary>
        /// LoadPlumbingFixturesMin
        /// </summary>
        /// <param name="plumbingFixturesMin">plumbingFixturesMin</param>
        /// <returns>int</returns>
        private int LoadPlumbingFixturesMin(string plumbingFixturesMin)
        {
            DataRow[] primaryStyleDataRow;
            primaryStyleDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select("Key= '" + plumbingFixturesMin + "'");
            if (primaryStyleDataRow.Length != 0)
            {
                plumbingFixturesMin = primaryStyleDataRow[0]["UnitMinimum"].ToString();
                int loadPlumbingFixturesMin1 = Convert.ToInt32(plumbingFixturesMin);
                return loadPlumbingFixturesMin1;
            }
            else
            {
                PlumbingTextBox.Enabled = false;
                return 0;
            }
        }

        /// <summary>
        /// LoadRoughInsMax
        /// </summary>
        /// <param name="roughInsMax">roughInsMax</param>
        /// <returns>int</returns>
        private int LoadRoughInsMax(string roughInsMax)
        {
            DataRow[] primaryStyleDataRow;
            primaryStyleDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select("Key= '" + roughInsMax + "'");
            primaryStyleDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select("Key= '" + roughInsMin + "'");
            if (primaryStyleDataRow.Length != 0)
            {
                roughInsMax = primaryStyleDataRow[0]["UnitMaximum"].ToString();
                int loadRoughInsMax1 = Convert.ToInt32(roughInsMax);
                return loadRoughInsMax1;
            }
            else
            {
                RoughTextBox.Enabled = false;
                return 0;
            }
        }

        /// <summary>
        /// LoadRoughInsMin
        /// </summary>
        /// <param name="roughInsMin">roughInsMin</param>
        /// <returns>int</returns>
        private int LoadRoughInsMin(string roughInsMin)
        {
            DataRow[] primaryStyleDataRow;
            primaryStyleDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select("Key= '" + roughInsMin + "'");
            if (primaryStyleDataRow.Length != 0)
            {
                roughInsMin = primaryStyleDataRow[0]["UnitMinimum"].ToString();
                int loadRoughInsMin1 = Convert.ToInt32(roughInsMin);
                return loadRoughInsMin1;
            }
            else
            {
                RoughTextBox.Enabled = false;
                return 0;
            }
        }

        /// <summary>
        /// LoadRaisedSubfloorMax
        /// </summary>
        /// <param name="raisedSubfloorMax">raisedSubfloorMax</param>
        /// <returns>int</returns>
        private int LoadRaisedSubfloorMax(string raisedSubfloorMax)
        {
            DataRow[] primaryStyleDataRow;
            primaryStyleDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select("Key= '" + raisedSubfloorMax + "'");
            if (primaryStyleDataRow.Length != 0)
            {
                raisedSubfloorMax = primaryStyleDataRow[0]["PercentMaximum"].ToString();
                int loadRaisedSubfloorMax1 = Convert.ToInt32(raisedSubfloorMax);
                return loadRaisedSubfloorMax1;
            }
            else
            {
                this.RaisedSubfloorTextBox.Enabled = false;
                return 0;
            }
        }

        /// <summary>
        /// LoadRaisedSubfloorMin
        /// </summary>
        /// <param name="raisedSubfloorMin">raisedSubfloorMin</param>
        /// <returns>int</returns>
        private int LoadRaisedSubfloorMin(string raisedSubfloorMin)
        {
            DataRow[] primaryStyleDataRow;
            primaryStyleDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select("Key= '" + raisedSubfloorMin + "'");
            if (primaryStyleDataRow.Length != 0)
            {
                raisedSubfloorMin = primaryStyleDataRow[0]["PercentMinimum"].ToString();
                int loadRaisedSubfloorMin1 = Convert.ToInt32(raisedSubfloorMin);
                return loadRaisedSubfloorMin1;
            }
            else
            {
                this.RaisedSubfloorTextBox.Enabled = false;
                return 0;
            }
        }

        /// <summary>
        /// LoadPartitionsDryWallMax
        /// </summary>
        /// <param name="partitionsDryWallMax">partitionsDryWallMax</param>
        /// <returns>int</returns>
        private int LoadPartitionsDryWallMax(string partitionsDryWallMax)
        {
            DataRow[] primaryStyleDataRow;
            primaryStyleDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select("Key= '" + partitionsDryWallMax + "'");
            if (primaryStyleDataRow.Length != 0)
            {
                partitionsDryWallMax = primaryStyleDataRow[0]["PercentMaximum"].ToString();
                this.loadPartitionsDryWallMax1 = Convert.ToInt32(partitionsDryWallMax);
                return this.loadPartitionsDryWallMax1;
            }
            else
            {
                PartitionsTextBox.Enabled = false;
                return 0;
            }
        }

        /// <summary>
        /// LoadpartitionsDryWallMin
        /// </summary>
        /// <param name="partitionsDryWallMin">partitionsDryWallMin</param>
        /// <returns>int</returns>
        private int LoadpartitionsDryWallMin(string partitionsDryWallMin)
        {
            DataRow[] primaryStyleDataRow;
            primaryStyleDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select("Key= '" + partitionsDryWallMin + "'");
            if (primaryStyleDataRow.Length != 0)
            {
                partitionsDryWallMin = primaryStyleDataRow[0]["PercentMinimum"].ToString();
                int loadPartitionsDryWallMin1 = Convert.ToInt32(partitionsDryWallMin);
                return loadPartitionsDryWallMin1;
            }
            else
            {
                PartitionsTextBox.Enabled = false;
                return 0;
            }
        }

        /// <summary>
        /// OkButton_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            ////string a = ExteriorWalls1Combo.SelectedValue.ToString();
            MessageBox.Show("Raised subfloor % must be " + this.raisedSubfloorMinVal + " through " + this.raisedSubfloorMaxVal, "TerraScan-Missing Required Fields",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        /// <summary>
        /// ExteriorWalls1Combo_SelectedIndexChanged
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ExteriorWalls1Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////string a = ExteriorWalls1Combo.SelectedValue.ToString();
        }

        /// <summary>
        /// ExteriorTextBox_TextChanged_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ExteriorTextBox_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
            if (this.ExteriorTextBox.Text.Length > 2)
            {
                ExteriorWalls2Combo.Enabled = false;
                DataTable dt = new DataTable();
                DataColumn dc = new DataColumn("Key", System.Type.GetType("System.String"));
                DataColumn dc1 = new DataColumn("Description", System.Type.GetType("System.String"));
                dt.Columns.Add(dc);
                dt.Columns.Add(dc1);
                DataRow dr;
                dr = dt.NewRow();
                dr["Key"] = "";
                dr["Description"] = "";
                dt.Rows.Add(dr);

                this.ExteriorWalls2Combo.DataSource = dt;
                this.ExteriorWalls2Combo.DisplayMember = "Description";
                this.ExteriorWalls2Combo.ValueMember = "Key";
                this.Exteriorslabel.Text = "";
            }
            else if ((Exteriorslabel.Text == string.Empty) || (Exteriorslabel.Text == " 0  %") || (ExteriorTextBox.Text == string.Empty) || (Exteriorslabel.Text == "0%"))
            {
                ExteriorWalls2Combo.Enabled = false;
                DataTable dt = new DataTable();
                DataColumn dc = new DataColumn("Key", System.Type.GetType("System.String"));
                DataColumn dc1 = new DataColumn("Description", System.Type.GetType("System.String"));
                dt.Columns.Add(dc);
                dt.Columns.Add(dc1);
                DataRow dr;
                dr = dt.NewRow();
                dr["Key"] = "";
                dr["Description"] = "";
                dt.Rows.Add(dr);

                this.ExteriorWalls2Combo.DataSource = dt;
                this.ExteriorWalls2Combo.DisplayMember = "Description";
                this.ExteriorWalls2Combo.ValueMember = "Key";

                ////this.ExteriorWalls2Combo.DisplayMember = "";
            }
            else
            {
                ExteriorWalls2Combo.Enabled = true;
                this.LoadExteriorWalls2Combo();
            }

            if ((ExteriorTextBox.Text != string.Empty))
            {
                ////if (ExteriorTextBox.Text.IndexOf("\t") > 0) 
                //// {
                ExteriorTextBox.Text = ExteriorTextBox.Text.Replace("\t", "").Trim().ToString();
                if (ExteriorTextBox.Text == "0 %")
                {
                    Exteriorslabel.Text =  "    %";
                }
                //// }
                else
                {
                    string exterierTrimValue = ExteriorTextBox.Text.Replace("%", "").Trim().ToString();
                    ////try
                    ////{
                    if (exterierTrimValue != string.Empty)
                    {
                        int val = 0;
                        ///this.val = Convert.ToInt32(exterierTrimValue);
                        int.TryParse(exterierTrimValue.ToString(), out val);
                        if (val == 0)
                        {
                            Exteriorslabel.Text = "100 %";
                            ExteriorTextBox.Text = "0 %";

                        }


                        else if ((val >= this.loadExterierMinVal) && (val <= this.loadExterierMaxVal))
                        {
                            ////int a1 = Convert.ToInt32(textBox1.Text);
                            int exterierTrimValues = (100 - val);
                            Exteriorslabel.Text = exterierTrimValues.ToString() + " %";
                            if ((Exteriorslabel.Text == string.Empty) || (Exteriorslabel.Text == "0 %") || (Exteriorslabel.Text.Trim() == " 0%"))
                            {
                                ExteriorWalls2Combo.Enabled = false;
                            }
                            else
                            {
                                ExteriorWalls2Combo.Enabled = true;
                                this.LoadExteriorWalls2Combo();
                            }
                        }
                        else
                        {
                            Exteriorslabel.Text = string.Empty;
                        }
                    }
                    else
                    {
                        ExteriorTextBox.Text = "0 %";
                        Exteriorslabel.Text = "100 %";
                    }
                }
            }
            else
            {
                //// ExteriorTextBox.Text = "0 %";
                Exteriorslabel.Text = string.Empty;
            }
        }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ExteriorTextBox_KeyDown_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ExteriorTextBox_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
            if (e.KeyCode == Keys.Back)
            {
                this.backVal = true;
            }
            else
            {
                this.backVal = false;
            }

            this.backControl = e.Control;
        }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ExteriorTextBox_KeyPress_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ExteriorTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
            if (this.backControl == true)
            {
                if (this.backTab == true)
                {
                    e.Handled = true;
                    return;
                }

                if ((this.backVal == true))
                {
                    e.Handled = true;
                    return;
                }
                else
                {
                    e.Handled = false;
                    return;
                }
            }

            if (this.backVal != true)
            {
                if (e.KeyChar == '%')
                {
                    if (ExteriorTextBox.Text.IndexOf('%') < 0)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
                else if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                else if (ExteriorTextBox.Text != string.Empty)
                {
                    string str = string.Empty;

                    if (ExteriorTextBox.SelectedText.Length == 0)
                    {
                        str = ExteriorTextBox.Text.Replace("%", "") + e.KeyChar.ToString();
                    }
                    else
                    {
                        str = ExteriorTextBox.Text.Replace("%", "").Replace(ExteriorTextBox.SelectedText, e.KeyChar.ToString());
                    }
                }
            }
        }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// ExteriorTextBox_Leave_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ExteriorTextBox_Leave_1(object sender, EventArgs e)
        {
            try
            {
            //int val = 0;
            //int.TryParse(ExteriorTextBox.Text, out val);
                    
            if ((ExteriorTextBox.Text.Length == 0) || (ExteriorTextBox.Text == "%") || (ExteriorTextBox.Text == "0"))
            {
                MessageBox.Show("Exterior % must be 1 through 100.", "TerraScan T2 - Missing Required Fields",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                ExteriorTextBox.Focus();
            }
            else
            {
                string str1 = ExteriorTextBox.Text;
                string str = str1.Replace("%", "");
                if (Convert.ToInt32(str) < 0 || Convert.ToInt32(str) > 100)
                {
                    MessageBox.Show("Exterior % must be 1 through 100.", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ExteriorTextBox.Focus();
                    Exteriorslabel.Text = string.Empty;
                }
                else
                {
                    if (ExteriorTextBox.Text.IndexOf('%') < 0)
                    {
                        ExteriorTextBox.AppendText(" %");
                    }
                    else if ((ExteriorTextBox.Text.IndexOf('%')) < (ExteriorTextBox.Text.Length - 1))
                    {
                        MessageBox.Show("Invalid", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                }
            }
        }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// PlumbingTextBox_KeyPress_2
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PlumbingTextBox_KeyPress_2(object sender, KeyPressEventArgs e)
        {
            try
            {
            if (this.backControl == true)
            {
                if (this.backTab == true)
                {
                    e.Handled = true;
                    return;
                }

                if ((this.backVal == true))
                {
                    e.Handled = true;
                    return;
                }
                else
                {
                    e.Handled = false;
                    return;
                }
            }

            if ((this.backVal == true))
            {
                e.Handled = false;
                return;
            }

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// PlumbingTextBox_Leave_2
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PlumbingTextBox_Leave_2(object sender, EventArgs e)
        {
            try
            {
            if ((PlumbingTextBox.Text != string.Empty))
            {
                int a1 = Convert.ToInt32(PlumbingTextBox.Text);

                if ((a1 > this.plumbingFixturesMaxVal) || (a1 < this.plumbingFixturesMinVal))
                {
                    MessageBox.Show("Fixtures must be from " + this.plumbingFixturesMinVal + " through " + this.plumbingFixturesMaxVal, "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PlumbingTextBox.Focus();
                }
                else
                {
                }
            }
            else
            {
                ////
            }
        }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// PlumbingTextBox_KeyDown_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e"> e</param>
        private void PlumbingTextBox_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
            this.backControl = e.Control;
            if (e.KeyCode == Keys.Tab)
            {
                this.backTab = true;
            }
            else
            {
                this.backTab = false;
            }

            if (e.KeyCode == Keys.Back)
            {
                this.backVal = true;
            }
            else
            {
                this.backVal = false;
            }
        }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// RoughTextBox_KeyPress_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void RoughTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
            if (this.backControl == true)
            {
                if (this.backTab == true)
                {
                    e.Handled = true;
                    return;
                }

                if ((this.backVal == true))
                {
                    e.Handled = true;
                    return;
                }
                else
                {
                    e.Handled = false;
                    return;
                }
            }

            if ((this.backVal == true))
            {
                e.Handled = false;
                return;
            }
            ////else
            ////{
            ////    e.Handled = true;
            ////    return;
            ////}
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// RoughTextBox_KeyDown_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void RoughTextBox_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
            this.backControl = e.Control;
            if (e.KeyCode == Keys.Tab)
            {
                this.backTab = true;
            }
            else
            {
                this.backTab = false;
            }

            if (e.KeyCode == Keys.Back)
            {
                this.backVal = true;
            }
            else
            {
                this.backVal = false;
            }
        }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// RoughTextBox_Leave_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void RoughTextBox_Leave_1(object sender, EventArgs e)
        {
            try
            {
            if ((RoughTextBox.Text != string.Empty))
            {
                int roughTextValue = Convert.ToInt32(RoughTextBox.Text);
                if ((roughTextValue > this.roughInsMaxVal) || (roughTextValue < this.roughInsMinVal))
                {
                    MessageBox.Show("Rough-ins must be from " + this.roughInsMinVal + " through " + this.roughInsMaxVal, "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RoughTextBox.Focus();
                }
                else
                {
                    ////  RaisedSubfloorTextBox.Focus();
                    //// RoughTextBox.ApplyFocusColor = false;
                }
            }
            else
            {
                //// RaisedSubfloorTextBox.Focus();
                // //RoughTextBox.ApplyFocusColor = false;
            }
        }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// RaisedSubfloorTextBox_KeyPress_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void RaisedSubfloorTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
            if (this.backControl == true)
            {
                if (this.backTab == true)
                {
                    e.Handled = true;
                    return;
                }

                if ((this.backVal == true))
                {
                    e.Handled = true;
                    return;
                }
                else
                {
                    e.Handled = false;
                    return;
                }
            }

            if (this.backVal != true)
            {
                if (e.KeyChar == '%')
                {
                    if (RaisedSubfloorTextBox.Text.IndexOf('%') < 0)
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
                else if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
                else if (RaisedSubfloorTextBox.Text != string.Empty)
                {
                    string str = string.Empty;

                    if (RaisedSubfloorTextBox.SelectedText.Length == 0)
                    {
                        str = RaisedSubfloorTextBox.Text.Replace("%", "") + e.KeyChar.ToString();
                    }
                    else
                    {
                        str = RaisedSubfloorTextBox.Text.Replace("%", "").Replace(RaisedSubfloorTextBox.SelectedText, e.KeyChar.ToString());
                    }
                }
            }
        }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// RaisedSubfloorTextBox_KeyDown_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void RaisedSubfloorTextBox_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
            if (e.KeyCode == Keys.Back)
            {
                this.backVal = true;
            }
            else
            {
                this.backVal = false;
            }

            this.backControl = e.Control;
        }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        ///  RaisedSubfloorTextBox_
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void RaisedSubfloorTextBox_Leave_1(object sender, EventArgs e)
        {
            try
            {
            if ((RaisedSubfloorTextBox.Text != string.Empty))
            {
                if ((RaisedSubfloorTextBox.Text.Length == 0) || (RaisedSubfloorTextBox.Text == "%"))
                {
                    RaisedSubfloorTextBox.Text = "0 %";
                    RaisedSubFloorlabel.Text = "100 %";
                    //// MessageBox.Show("RaisedSubfloor % must be" + loadExterierMinVal + " through" + loadExterierMaxVal, "Residential Estimator");
                    // // RaisedSubFloorlabel.Focus();

                    // //ExteriorTextBox.Text = "0%";
                }
                else
                {
                    string str1 = RaisedSubfloorTextBox.Text;
                    string str = str1.Replace("%", "");

                    if (Convert.ToInt32(str) < 0 || Convert.ToInt32(str) > 100)
                    {
                        MessageBox.Show("RaisedSubfloor % must be 0 through 100", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        RaisedSubfloorTextBox.Focus();
                        RaisedSubFloorlabel.Text = " %";
                    }
                    else
                    {
                        if (RaisedSubfloorTextBox.Text.IndexOf('%') < 0)
                        {
                            RaisedSubfloorTextBox.AppendText(" %");
                        }
                        else if ((RaisedSubfloorTextBox.Text.IndexOf('%')) < (RaisedSubfloorTextBox.Text.Length - 1))
                        {
                            MessageBox.Show("Invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                RaisedSubfloorTextBox.Text = "0 %";
                RaisedSubFloorlabel.Text = "100 %";
                ////  PartitionsTextBox.Focus();
            }
        }
        catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// RaisedSubfloorTextBox_TextChanged_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void RaisedSubfloorTextBox_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
            if ((RaisedSubfloorTextBox.Text != string.Empty))
            {
                ////if (ExteriorTextBox.Text.IndexOf("\t") > 0)
                //// {
                RaisedSubfloorTextBox.Text = RaisedSubfloorTextBox.Text.Replace("\t", "").Trim().ToString();
                if (RaisedSubfloorTextBox.Text == "0 %")
                {
                    RaisedSubFloorlabel.Text = "100 %";  
                }
                //// }
                string raisedValue = RaisedSubfloorTextBox.Text.Replace("%", "").Trim().ToString();
                ////try
                ////{
                if (raisedValue != string.Empty)
                {
                    
                    //// this.val = Convert.ToInt32(raisedValue);
                    int val = 0;
                    int.TryParse(raisedValue.ToString(), out val);
                    if (val == 0)
                    {
                        RaisedSubfloorTextBox.Text = "0 %";
                    }

                    else if ((val >= this.loadExterierMinVal) && (val <= this.loadExterierMaxVal))
                    {
                        int raisedValues = (100 - val);
                        RaisedSubFloorlabel.Text = raisedValues.ToString() + " %";
                    }
                    else
                    {
                        RaisedSubFloorlabel.Text = " %";
                    }
                }

                else
                {
                    this.RaisedSubfloorTextBox.Text = "0 %";
                    this.RaisedSubFloorlabel.Text = "100 %";
                }
            }
            else
            {
                RaisedSubFloorlabel.Text = "100%";
            }
        }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// PartitionsTextBox_TextChanged_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PartitionsTextBox_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                if ((PartitionsTextBox.Text != string.Empty))
                {
                    ////if (ExteriorTextBox.Text.IndexOf("\t") > 0)
                    //// {
                    PartitionsTextBox.Text = PartitionsTextBox.Text.Replace("\t", "").Trim().ToString();
                    if (PartitionsTextBox.Text == "0 %")
                    {
                        PartitionsDrayWalllabel.Text = "100 %";
                    }
                    //// }
                    string partitionsValue = PartitionsTextBox.Text.Replace("%", "").Trim().ToString();
                    ////try
                    ////{
                    if (partitionsValue != string.Empty)
                    {
                        //// this.val = Convert.ToInt32(partitionsValue);
                        int val = 0;
                        int.TryParse(partitionsValue.ToString(), out val);
                        if (val == 0)
                        {
                            PartitionsTextBox.Text = "0 %";
                        }

                        else if ((val >= this.loadExterierMinVal) && (val <= this.loadExterierMaxVal))
                        {
                            int partitionsValues = (100 - val);
                            PartitionsDrayWalllabel.Text = partitionsValues.ToString() + " %";
                        }
                        else
                        {
                            PartitionsDrayWalllabel.Text = " %";
                        }
                    }
                    else
                    {
                        this.PartitionsDrayWalllabel.Text = "100 %";
                        this.PartitionsTextBox.Text = "0 %";
                    }
                }
                else
                {
                    PartitionsDrayWalllabel.Text = "100%";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// PartitionsTextBox_KeyPress_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PartitionsTextBox_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (this.backControl == true)
                {
                    if (this.backTab == true)
                    {
                        e.Handled = true;
                        return;
                    }

                    if ((this.backVal == true))
                    {
                        e.Handled = true;
                        return;
                    }
                    else
                    {
                        e.Handled = false;
                        return;
                    }
                }

                if (this.backVal != true)
                {
                    if (e.KeyChar == '%')
                    {
                        if (PartitionsTextBox.Text.IndexOf('%') < 0)
                        {
                            e.Handled = false;
                        }
                        else
                        {
                            e.Handled = true;
                        }
                    }
                    else if (!char.IsDigit(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    else if (PartitionsTextBox.Text != string.Empty)
                    {
                        string str = string.Empty;
                        if (PartitionsTextBox.SelectedText.Length == 0)
                        {
                            str = PartitionsTextBox.Text.Replace("%", "") + e.KeyChar.ToString();
                        }
                        else
                        {
                            str = PartitionsTextBox.Text.Replace("%", "").Replace(PartitionsTextBox.SelectedText, e.KeyChar.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// PartitionsTextBox_Leave
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PartitionsTextBox_Leave_1(object sender, EventArgs e)
        {
            try
            {
                if ((PartitionsTextBox.Text != string.Empty))
                {
                    if ((PartitionsTextBox.Text.Length == 0) || (PartitionsTextBox.Text == "%"))
                    {
                        PartitionsTextBox.Text = "0 %";
                        PartitionsDrayWalllabel.Text = "100 %";
                    }
                    else
                    {
                        string partitionsValue = PartitionsTextBox.Text;
                        string partitionsValues = partitionsValue.Replace("%", "");

                        if (Convert.ToInt32(partitionsValues) < 0 || Convert.ToInt32(partitionsValues) > 100)
                        {
                            MessageBox.Show("Partitions % must be 0 through 100", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            PartitionsTextBox.Focus();
                            PartitionsDrayWalllabel.Text = " %";
                        }
                        else
                        {
                            if (PartitionsTextBox.Text.IndexOf('%') < 0)
                            {
                                PartitionsTextBox.AppendText(" %");
                            }
                            else if ((PartitionsTextBox.Text.IndexOf('%')) < (PartitionsTextBox.Text.Length - 1))
                            {
                                MessageBox.Show("Invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    PartitionsTextBox.Text = "0 %";
                    PartitionsDrayWalllabel.Text = "100 %";
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// PartitionsTextBox
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void PartitionsTextBox_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
            if (e.KeyCode == Keys.Back)
            {
                this.backVal = true;
            }
            else
            {
                this.backVal = false;
            }

            this.backControl = e.Control;
        }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// OkButton
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void OkButton_Click_1(object sender, EventArgs e)
        {
            try
            {

            this.ReturnXMLString();
            this.DialogResult = DialogResult.OK;
            ////string valuesXml = this.GetValuesXml();
           }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// GetValuesXml
        /// </summary>
        /// <returns>int</returns>
        private string GetValuesXml()
        {
            DataSet valuesDataSet = new DataSet("Root");
            valuesDataSet.Tables.Add(this.GetDataTable());
            return valuesDataSet.GetXml();
        }

        /// <summary>
        /// GetDataTable
        /// </summary>
        /// <returns>int</returns>
        private DataTable GetDataTable()
        {
            DataTable tempDataTable = new DataTable();
            DataColumn[] tempDataColumn = new DataColumn[] 
           {        
                 new DataColumn("ExteriorWalls1Combo"),
                  new DataColumn("ExteriorWalls2Combo"),
                  new DataColumn("RoofingCombo"), 
                  new DataColumn("HeatingorCoolingCombo"),
                  new DataColumn("ExteriorTextBox"), 
                  new DataColumn("Exteriorslabel"), 
                  new DataColumn("PlumbingTextBox"), 
                  new DataColumn("RoughTextBox"), 
                new DataColumn("RaisedSubfloorTextBox"), 
                new DataColumn("RaisedSubFloorlabel"),
                new DataColumn("PartitionsTextBox"), 
               new DataColumn("PartitionsDrayWalllabel"), 
                new DataColumn("AutomaticCheck1"), 
                new DataColumn("AutomaticCheck2")
            };

            tempDataTable.Columns.AddRange(tempDataColumn);

            ////Table 2
            DataRow tempDataRow = tempDataTable.NewRow();
            tempDataRow["ExteriorWalls1Combo"] = this.ExteriorWalls1Combo.SelectedValue.ToString();
            if (ExteriorWalls2Combo.Enabled == false)
            {
                tempDataRow["ExteriorWalls2Combo"] = string.Empty;
            }
            else
            {
                tempDataRow["ExteriorWalls2Combo"] = this.ExteriorWalls2Combo.SelectedValue.ToString();
            }

            tempDataRow["RoofingCombo"] = this.RoofingCombo.SelectedValue.ToString();
            tempDataRow["HeatingorCoolingCombo"] = this.HeatingorCoolingCombo.SelectedValue.ToString();
            tempDataRow["ExteriorTextBox"] = this.ExteriorTextBox.Text;
            tempDataRow["Exteriorslabel"] = this.Exteriorslabel.Text;
            if (PlumbingTextBox.Text == string.Empty)
            {
                tempDataRow["PlumbingTextBox"] = string.Empty;
            }
            else
            {
                tempDataRow["PlumbingTextBox"] = this.PlumbingTextBox.Text;
            }

            if (RoughTextBox.Text == string.Empty)
            {
                tempDataRow["RoughTextBox"] = string.Empty;
            }
            else
            {
                tempDataRow["RoughTextBox"] = this.RoughTextBox.Text;
            }

            if (RaisedSubfloorTextBox.Text == string.Empty)
            {
                tempDataRow["RaisedSubfloorTextBox"] = string.Empty;
            }
            else
            {
                tempDataRow["RaisedSubfloorTextBox"] = this.RaisedSubfloorTextBox.Text;
            }

            tempDataRow["RaisedSubFloorlabel"] = this.RaisedSubFloorlabel.Text;

            if (PartitionsTextBox.Text == string.Empty)
            {
                PartitionsTextBox.Text = string.Empty;
            }
            else
            {
                tempDataRow["PartitionsTextBox"] = this.PartitionsTextBox.Text;
            }

            tempDataRow["PartitionsDrayWalllabel"] = this.PartitionsDrayWalllabel.Text;

            if (Automatic1checkBox.Checked == true)
            {
                tempDataRow["AutomaticCheck1"] = "True";
            }
            else
            {
                tempDataRow["AutomaticCheck1"] = "False";
            }

            if (Automatic2checkBox.Checked == true)
            {
                tempDataRow["AutomaticCheck2"] = "True";
            }
            else
            {
                tempDataRow["AutomaticCheck2"] = "False";
            }

            tempDataTable.Rows.Add(tempDataRow);
            return tempDataTable;
        }

        /// <summary>
        /// ReturnXMLString
        /// </summary>
        private void ReturnXMLString()
        {
            try
            {
                int exteriorWalls1ComboKeyId;
                int exteriorWalls2ComboKeyId;
                int roofingComboKeyId;
                int heatingorCoolingComboKeyid;
                int slabOnGreadeValue;
                int plasterValue;
                DataTable sectionDataTable = new DataTable();
                DataColumn[] sectionColumn = new DataColumn[] { new DataColumn("Code"), new DataColumn("SelectedSystem"), new DataColumn("SelectedSystemDescription"), new DataColumn("Units"), new DataColumn("Percentage"), new DataColumn("QualityID"), new DataColumn("QualityDescription"), new DataColumn("AllowQualityChangeFlag"), new DataColumn("PercentMaximum"), new DataColumn("PercentMinimum"), new DataColumn("UnitMaximum"), new DataColumn("UnitMinimum"), new DataColumn("SectionKeyValue"), new DataColumn("SystemKeyValue"), new DataColumn("SectionGroupID") };
                sectionDataTable.Columns.AddRange(sectionColumn);

                #region  exteriorWalls1Combo

                DataRow sectionDataRow = sectionDataTable.NewRow();
                int.TryParse(this.ExteriorWalls1Combo.SelectedValue.ToString(), out exteriorWalls1ComboKeyId);
                string findexp = "Key = " + exteriorWalls1ComboKeyId + "AND Components_Id = 0";
                DataRow[] fromAssign = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select(findexp);
                foreach (DataRow dataRow in fromAssign)
                {
                    sectionDataRow["Code"] = dataRow.ItemArray[0].ToString();

                    sectionDataRow["SelectedSystem"] = "Exterior Walls";

                    sectionDataRow["SelectedSystemDescription"] = dataRow.ItemArray[1].ToString();

                    sectionDataRow["Units"] = string.Empty;

                    sectionDataRow["Percentage"] = this.ExteriorTextBox.Text.Replace("%", "").Trim();

                    sectionDataRow["QualityID"] = string.Empty;

                    sectionDataRow["QualityDescription"] = string.Empty;

                    sectionDataRow["AllowQualityChangeFlag"] = dataRow.ItemArray[5].ToString();

                    sectionDataRow["PercentMaximum"] = dataRow.ItemArray[8].ToString();

                    sectionDataRow["PercentMinimum"] = dataRow.ItemArray[9].ToString();

                    sectionDataRow["UnitMaximum"] = dataRow.ItemArray[10].ToString();

                    sectionDataRow["UnitMinimum"] = dataRow.ItemArray[11].ToString();

                    sectionDataRow["SectionKeyValue"] = 1;

                    sectionDataRow["SectionGroupID"] = 1;

                    string findResisdentKeyID = "Components_ID = " + dataRow.ItemArray[13].ToString();
                    int residenceKeyId = 0;
                    string systemKeyId;
                    DataRow[] getResidentSystemId = this.componentAssumptionsDataSet.Tables["Components"].Select(findResisdentKeyID);
                    int.TryParse(getResidentSystemId[0].ItemArray[1].ToString(), out residenceKeyId);

                    string findSystemKeyId = "ResidenceSystem_Id = " + residenceKeyId;
                    DataRow[] getSystemKeyId = this.componentAssumptionsDataSet.Tables["ResidenceSystem"].Select(findSystemKeyId);
                    sectionDataRow["SystemKeyValue"] = getSystemKeyId[0].ItemArray[0].ToString();
                    
                    //sectionDataRow["SystemKeyValue"] = dataRow.ItemArray[13].ToString();

                    sectionDataTable.Rows.Add(sectionDataRow);
                }
                #endregion exteriorWalls1Combo

                #region ExteriorWalls2
                if (ExteriorWalls2Combo.Enabled)
                {
                    DataRow exteriorWalls2CombSectionDataRow = sectionDataTable.NewRow();
                    int.TryParse(this.ExteriorWalls2Combo.SelectedValue.ToString(), out exteriorWalls2ComboKeyId);
                    string exteriorWalls2Combofindexp = "Key = " + exteriorWalls2ComboKeyId + "AND Components_Id = 0";
                    DataRow[] exteriorWalls2ComboDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select(exteriorWalls2Combofindexp);
                    foreach (DataRow dataRow in exteriorWalls2ComboDataRow)
                    {
                        exteriorWalls2CombSectionDataRow["Code"] = dataRow.ItemArray[0].ToString();

                        exteriorWalls2CombSectionDataRow["SelectedSystem"] = "Exterior Walls";

                        exteriorWalls2CombSectionDataRow["SelectedSystemDescription"] = dataRow.ItemArray[1].ToString();

                        exteriorWalls2CombSectionDataRow["Units"] = string.Empty;

                        exteriorWalls2CombSectionDataRow["Percentage"] = this.Exteriorslabel.Text.Replace("%", "").Trim();

                        exteriorWalls2CombSectionDataRow["QualityID"] = string.Empty;

                        exteriorWalls2CombSectionDataRow["QualityDescription"] = string.Empty;

                        exteriorWalls2CombSectionDataRow["AllowQualityChangeFlag"] = dataRow.ItemArray[5].ToString();

                        exteriorWalls2CombSectionDataRow["PercentMaximum"] = dataRow.ItemArray[8].ToString();

                        exteriorWalls2CombSectionDataRow["PercentMinimum"] = dataRow.ItemArray[9].ToString();

                        exteriorWalls2CombSectionDataRow["UnitMaximum"] = dataRow.ItemArray[10].ToString();

                        exteriorWalls2CombSectionDataRow["UnitMinimum"] = dataRow.ItemArray[11].ToString();

                        exteriorWalls2CombSectionDataRow["SectionKeyValue"] = 1;

                        exteriorWalls2CombSectionDataRow["SectionGroupID"] = 1;

                        string findResisdentKeyID = "Components_ID = " + dataRow.ItemArray[13].ToString();
                        int residenceKeyId = 0;
                        string systemKeyId;
                        DataRow[] getResidentSystemId = this.componentAssumptionsDataSet.Tables["Components"].Select(findResisdentKeyID);
                        int.TryParse(getResidentSystemId[0].ItemArray[1].ToString(), out residenceKeyId);

                        string findSystemKeyId = "ResidenceSystem_Id = " + residenceKeyId;
                        DataRow[] getSystemKeyId = this.componentAssumptionsDataSet.Tables["ResidenceSystem"].Select(findSystemKeyId);
                        exteriorWalls2CombSectionDataRow["SystemKeyValue"] = getSystemKeyId[0].ItemArray[0].ToString();

                        //exteriorWalls2CombSectionDataRow["SystemKeyValue"] = dataRow.ItemArray[13].ToString();

                        sectionDataTable.Rows.Add(exteriorWalls2CombSectionDataRow);
                    }
                }

                #endregion ExteriorWalls2

                #region roofingCombo

                DataRow roofingComboSectionDataRow = sectionDataTable.NewRow();
                int.TryParse(this.RoofingCombo.SelectedValue.ToString(), out roofingComboKeyId);
                string roofingCombofindexp = "Key = " + roofingComboKeyId + "AND Components_Id = 1";
                DataRow[] roofingComboDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select(roofingCombofindexp);
                foreach (DataRow dataRow in roofingComboDataRow)
                {
                    roofingComboSectionDataRow["Code"] = dataRow.ItemArray[0].ToString();

                    roofingComboSectionDataRow["SelectedSystem"] = "Roofing";

                    roofingComboSectionDataRow["SelectedSystemDescription"] = dataRow.ItemArray[1].ToString();

                    roofingComboSectionDataRow["Units"] = string.Empty;

                    roofingComboSectionDataRow["Percentage"] = "100";

                    roofingComboSectionDataRow["QualityID"] = string.Empty;

                    roofingComboSectionDataRow["QualityDescription"] = string.Empty;

                    roofingComboSectionDataRow["AllowQualityChangeFlag"] = dataRow.ItemArray[5].ToString();

                    roofingComboSectionDataRow["PercentMaximum"] = dataRow.ItemArray[8].ToString();

                    roofingComboSectionDataRow["PercentMinimum"] = dataRow.ItemArray[9].ToString();

                    roofingComboSectionDataRow["UnitMaximum"] = dataRow.ItemArray[10].ToString();

                    roofingComboSectionDataRow["UnitMinimum"] = dataRow.ItemArray[11].ToString();

                    roofingComboSectionDataRow["SectionKeyValue"] = 1;

                    roofingComboSectionDataRow["SectionGroupID"] = 1;

                    string findResisdentKeyID = "Components_ID = " + dataRow.ItemArray[13].ToString();
                    int residenceKeyId = 0;
                    string systemKeyId;
                    DataRow[] getResidentSystemId = this.componentAssumptionsDataSet.Tables["Components"].Select(findResisdentKeyID);
                    int.TryParse(getResidentSystemId[0].ItemArray[1].ToString(), out residenceKeyId);

                    string findSystemKeyId = "ResidenceSystem_Id = " + residenceKeyId;
                    DataRow[] getSystemKeyId = this.componentAssumptionsDataSet.Tables["ResidenceSystem"].Select(findSystemKeyId);
                    roofingComboSectionDataRow["SystemKeyValue"] = getSystemKeyId[0].ItemArray[0].ToString();

                    //roofingComboSectionDataRow["SystemKeyValue"] = dataRow.ItemArray[13].ToString();

                    sectionDataTable.Rows.Add(roofingComboSectionDataRow);
                }

                #endregion roofingCombo

                #region heatingorCoolingCombo

                DataRow heatingorCoolingComboSectionDataRow = sectionDataTable.NewRow();
                int.TryParse(this.HeatingorCoolingCombo.SelectedValue.ToString(), out heatingorCoolingComboKeyid);
                string heatingorCoolingCombofindexp = "Key = " + heatingorCoolingComboKeyid + "AND Components_Id = 2";
                DataRow[] heatingorCoolingComboDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select(heatingorCoolingCombofindexp);
                foreach (DataRow dataRow in heatingorCoolingComboDataRow)
                {
                    heatingorCoolingComboSectionDataRow["Code"] = dataRow.ItemArray[0].ToString();

                    heatingorCoolingComboSectionDataRow["SelectedSystem"] = "Heating/Cooling";

                    heatingorCoolingComboSectionDataRow["SelectedSystemDescription"] = dataRow.ItemArray[1].ToString();

                    heatingorCoolingComboSectionDataRow["Units"] = string.Empty;

                    heatingorCoolingComboSectionDataRow["Percentage"] = "100";

                    heatingorCoolingComboSectionDataRow["QualityID"] = string.Empty;

                    heatingorCoolingComboSectionDataRow["QualityDescription"] = string.Empty;

                    heatingorCoolingComboSectionDataRow["AllowQualityChangeFlag"] = dataRow.ItemArray[5].ToString();

                    heatingorCoolingComboSectionDataRow["PercentMaximum"] = dataRow.ItemArray[8].ToString();

                    heatingorCoolingComboSectionDataRow["PercentMinimum"] = dataRow.ItemArray[9].ToString();

                    heatingorCoolingComboSectionDataRow["UnitMaximum"] = dataRow.ItemArray[10].ToString();

                    heatingorCoolingComboSectionDataRow["UnitMinimum"] = dataRow.ItemArray[11].ToString();

                    heatingorCoolingComboSectionDataRow["SectionKeyValue"] = 1;

                    heatingorCoolingComboSectionDataRow["SectionGroupID"] = 1;

                    string findResisdentKeyID = "Components_ID = " + dataRow.ItemArray[13].ToString();
                    int residenceKeyId = 0;
                    string systemKeyId;
                    DataRow[] getResidentSystemId = this.componentAssumptionsDataSet.Tables["Components"].Select(findResisdentKeyID);
                    int.TryParse(getResidentSystemId[0].ItemArray[1].ToString(), out residenceKeyId);

                    string findSystemKeyId = "ResidenceSystem_Id = " + residenceKeyId;
                    DataRow[] getSystemKeyId = this.componentAssumptionsDataSet.Tables["ResidenceSystem"].Select(findSystemKeyId);
                    heatingorCoolingComboSectionDataRow["SystemKeyValue"] = getSystemKeyId[0].ItemArray[0].ToString();

                    //heatingorCoolingComboSectionDataRow["SystemKeyValue"] = dataRow.ItemArray[13].ToString();

                    sectionDataTable.Rows.Add(heatingorCoolingComboSectionDataRow);
                }

                #endregion heatingorCoolingCombo

                #region plumbingTextBox

                DataRow plumbingTextBoxSectionDataRow = sectionDataTable.NewRow();

                string plumbingTextBoxfindexp = "Key = 601 AND Components_Id = 5";
                DataRow[] plumbingTextBoxDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select(plumbingTextBoxfindexp);
                foreach (DataRow dataRow in plumbingTextBoxDataRow)
                {
                    plumbingTextBoxSectionDataRow["Code"] = dataRow.ItemArray[0].ToString();

                    plumbingTextBoxSectionDataRow["SelectedSystem"] = "Miscellaneous";

                    plumbingTextBoxSectionDataRow["SelectedSystemDescription"] = dataRow.ItemArray[1].ToString();

                    plumbingTextBoxSectionDataRow["Units"] = this.PlumbingTextBox.Text.Trim();

                    plumbingTextBoxSectionDataRow["Percentage"] = string.Empty;

                    plumbingTextBoxSectionDataRow["QualityID"] = string.Empty;

                    plumbingTextBoxSectionDataRow["QualityDescription"] = string.Empty;

                    plumbingTextBoxSectionDataRow["AllowQualityChangeFlag"] = dataRow.ItemArray[5].ToString();

                    plumbingTextBoxSectionDataRow["PercentMaximum"] = dataRow.ItemArray[8].ToString();

                    plumbingTextBoxSectionDataRow["PercentMinimum"] = dataRow.ItemArray[9].ToString();

                    plumbingTextBoxSectionDataRow["UnitMaximum"] = dataRow.ItemArray[10].ToString();

                    plumbingTextBoxSectionDataRow["UnitMinimum"] = dataRow.ItemArray[11].ToString();

                    plumbingTextBoxSectionDataRow["SectionKeyValue"] = 1;

                    plumbingTextBoxSectionDataRow["SectionGroupID"] = 1;

                    string findResisdentKeyID = "Components_ID = " + dataRow.ItemArray[13].ToString();
                    int residenceKeyId = 0;
                    string systemKeyId;
                    DataRow[] getResidentSystemId = this.componentAssumptionsDataSet.Tables["Components"].Select(findResisdentKeyID);
                    int.TryParse(getResidentSystemId[0].ItemArray[1].ToString(), out residenceKeyId);

                    string findSystemKeyId = "ResidenceSystem_Id = " + residenceKeyId;
                    DataRow[] getSystemKeyId = this.componentAssumptionsDataSet.Tables["ResidenceSystem"].Select(findSystemKeyId);
                    plumbingTextBoxSectionDataRow["SystemKeyValue"] = getSystemKeyId[0].ItemArray[0].ToString();

                    //plumbingTextBoxSectionDataRow["SystemKeyValue"] = dataRow.ItemArray[13].ToString();

                    sectionDataTable.Rows.Add(plumbingTextBoxSectionDataRow);
                }

                #endregion plumbingTextBox

                #region RoughTextBox

                DataRow roughTextBoxSectionDataRow = sectionDataTable.NewRow();

                string roughTextBoxfindexp = "Key = 602 AND Components_Id = 5";
                DataRow[] roughTextBoxDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select(roughTextBoxfindexp);
                foreach (DataRow dataRow in roughTextBoxDataRow)
                {
                    roughTextBoxSectionDataRow["Code"] = dataRow.ItemArray[0].ToString();

                    roughTextBoxSectionDataRow["SelectedSystem"] = "Miscellaneous";

                    roughTextBoxSectionDataRow["SelectedSystemDescription"] = dataRow.ItemArray[1].ToString();

                    roughTextBoxSectionDataRow["Units"] = this.RoughTextBox.Text.Trim();

                    roughTextBoxSectionDataRow["Percentage"] = string.Empty;

                    roughTextBoxSectionDataRow["QualityID"] = string.Empty;

                    roughTextBoxSectionDataRow["QualityDescription"] = string.Empty;

                    roughTextBoxSectionDataRow["AllowQualityChangeFlag"] = dataRow.ItemArray[5].ToString();

                    roughTextBoxSectionDataRow["PercentMaximum"] = dataRow.ItemArray[8].ToString();

                    roughTextBoxSectionDataRow["PercentMinimum"] = dataRow.ItemArray[9].ToString();

                    roughTextBoxSectionDataRow["UnitMaximum"] = dataRow.ItemArray[10].ToString();

                    roughTextBoxSectionDataRow["UnitMinimum"] = dataRow.ItemArray[11].ToString();

                    roughTextBoxSectionDataRow["SectionKeyValue"] = 1;

                    roughTextBoxSectionDataRow["SectionGroupID"] = 1;

                    string findResisdentKeyID = "Components_ID = " + dataRow.ItemArray[13].ToString();
                    int residenceKeyId = 0;
                    string systemKeyId;
                    DataRow[] getResidentSystemId = this.componentAssumptionsDataSet.Tables["Components"].Select(findResisdentKeyID);
                    int.TryParse(getResidentSystemId[0].ItemArray[1].ToString(), out residenceKeyId);

                    string findSystemKeyId = "ResidenceSystem_Id = " + residenceKeyId;
                    DataRow[] getSystemKeyId = this.componentAssumptionsDataSet.Tables["ResidenceSystem"].Select(findSystemKeyId);
                    roughTextBoxSectionDataRow["SystemKeyValue"] = getSystemKeyId[0].ItemArray[0].ToString();
                    
                    sectionDataTable.Rows.Add(roughTextBoxSectionDataRow);
                }

                #endregion RoughTextBox

                #region RaisedSubfloorTextBox

                DataRow raisedSubfloorTextBoxSectionDataRow = sectionDataTable.NewRow();

                string raisedSubfloorTextBoxfindexp = "Key = 622 AND Components_Id = 5";
                DataRow[] raisedSubfloorTextBoxDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select(raisedSubfloorTextBoxfindexp);
                foreach (DataRow dataRow in raisedSubfloorTextBoxDataRow)
                {
                    raisedSubfloorTextBoxSectionDataRow["Code"] = dataRow.ItemArray[0].ToString();

                    raisedSubfloorTextBoxSectionDataRow["SelectedSystem"] = "Miscellaneous";

                    raisedSubfloorTextBoxSectionDataRow["SelectedSystemDescription"] = dataRow.ItemArray[1].ToString();

                    raisedSubfloorTextBoxSectionDataRow["Units"] = string.Empty;

                    raisedSubfloorTextBoxSectionDataRow["Percentage"] = this.RaisedSubfloorTextBox.Text.Replace("%", "").Trim();

                    raisedSubfloorTextBoxSectionDataRow["QualityID"] = string.Empty;

                    raisedSubfloorTextBoxSectionDataRow["QualityDescription"] = string.Empty;

                    raisedSubfloorTextBoxSectionDataRow["AllowQualityChangeFlag"] = dataRow.ItemArray[5].ToString();

                    raisedSubfloorTextBoxSectionDataRow["PercentMaximum"] = dataRow.ItemArray[8].ToString();

                    raisedSubfloorTextBoxSectionDataRow["PercentMinimum"] = dataRow.ItemArray[9].ToString();

                    raisedSubfloorTextBoxSectionDataRow["UnitMaximum"] = dataRow.ItemArray[10].ToString();

                    raisedSubfloorTextBoxSectionDataRow["UnitMinimum"] = dataRow.ItemArray[11].ToString();

                    raisedSubfloorTextBoxSectionDataRow["SectionKeyValue"] = 1;

                    raisedSubfloorTextBoxSectionDataRow["SectionGroupID"] = 1;

                    string findResisdentKeyID = "Components_ID = " + dataRow.ItemArray[13].ToString();
                    int residenceKeyId = 0;
                    string systemKeyId;
                    DataRow[] getResidentSystemId = this.componentAssumptionsDataSet.Tables["Components"].Select(findResisdentKeyID);
                    int.TryParse(getResidentSystemId[0].ItemArray[1].ToString(), out residenceKeyId);

                    string findSystemKeyId = "ResidenceSystem_Id = " + residenceKeyId;
                    DataRow[] getSystemKeyId = this.componentAssumptionsDataSet.Tables["ResidenceSystem"].Select(findSystemKeyId);
                    raisedSubfloorTextBoxSectionDataRow["SystemKeyValue"] = getSystemKeyId[0].ItemArray[0].ToString();
                    
                    sectionDataTable.Rows.Add(raisedSubfloorTextBoxSectionDataRow);
                }

                #endregion RaisedSubfloorTextBox

                #region RaisedSubFloorlabel
                int.TryParse(this.RaisedSubFloorlabel.Text.Replace("%", "").Trim(), out slabOnGreadeValue);

                if (slabOnGreadeValue > 0)
                {
                    DataRow raisedSubFloorlabelSectionDataRow = sectionDataTable.NewRow();
                    string raisedSubFloorlabelfindexp = "Key = 621 AND Components_Id = 5";
                    DataRow[] raisedSubFloorlabelDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select(raisedSubFloorlabelfindexp);
                    foreach (DataRow dataRow in raisedSubFloorlabelDataRow)
                    {
                        raisedSubFloorlabelSectionDataRow["Code"] = dataRow.ItemArray[0].ToString();

                        raisedSubFloorlabelSectionDataRow["SelectedSystem"] = "Miscellaneous";

                        raisedSubFloorlabelSectionDataRow["SelectedSystemDescription"] = dataRow.ItemArray[1].ToString();

                        raisedSubFloorlabelSectionDataRow["Units"] = string.Empty;

                        raisedSubFloorlabelSectionDataRow["Percentage"] = this.RaisedSubFloorlabel.Text.Replace("%", "").Trim();

                        raisedSubFloorlabelSectionDataRow["QualityID"] = string.Empty;

                        raisedSubFloorlabelSectionDataRow["QualityDescription"] = string.Empty;

                        raisedSubFloorlabelSectionDataRow["AllowQualityChangeFlag"] = dataRow.ItemArray[5].ToString();

                        raisedSubFloorlabelSectionDataRow["PercentMaximum"] = dataRow.ItemArray[8].ToString();

                        raisedSubFloorlabelSectionDataRow["PercentMinimum"] = dataRow.ItemArray[9].ToString();

                        raisedSubFloorlabelSectionDataRow["UnitMaximum"] = dataRow.ItemArray[10].ToString();

                        raisedSubFloorlabelSectionDataRow["UnitMinimum"] = dataRow.ItemArray[11].ToString();

                        raisedSubFloorlabelSectionDataRow["SectionKeyValue"] = 1;

                        raisedSubFloorlabelSectionDataRow["SectionGroupID"] = 1;

                        string findResisdentKeyID = "Components_ID = " + dataRow.ItemArray[13].ToString();
                        int residenceKeyId = 0;
                        string systemKeyId;
                        DataRow[] getResidentSystemId = this.componentAssumptionsDataSet.Tables["Components"].Select(findResisdentKeyID);
                        int.TryParse(getResidentSystemId[0].ItemArray[1].ToString(), out residenceKeyId);

                        string findSystemKeyId = "ResidenceSystem_Id = " + residenceKeyId;
                        DataRow[] getSystemKeyId = this.componentAssumptionsDataSet.Tables["ResidenceSystem"].Select(findSystemKeyId);
                        raisedSubFloorlabelSectionDataRow["SystemKeyValue"] = getSystemKeyId[0].ItemArray[0].ToString();
                    
                        sectionDataTable.Rows.Add(raisedSubFloorlabelSectionDataRow);
                    }
                }

                #endregion RaisedSubFloorlabel

                #region PartitionsDrayWalllabel

                int.TryParse(this.PartitionsDrayWalllabel.Text.Replace("%", "").Trim(), out plasterValue);

                if (plasterValue > 0)
                {
                    DataRow partitionsDrayWalllabelSectionDataRow = sectionDataTable.NewRow();

                    string partitionsDrayWalllabelfindexp = "Key = 631 AND Components_Id = 34";
                    DataRow[] partitionsDrayWalllabelDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select(partitionsDrayWalllabelfindexp);
                    foreach (DataRow dataRow in partitionsDrayWalllabelDataRow)
                    {
                        partitionsDrayWalllabelSectionDataRow["Code"] = dataRow.ItemArray[0].ToString();

                        partitionsDrayWalllabelSectionDataRow["SelectedSystem"] = "Miscellaneous";

                        partitionsDrayWalllabelSectionDataRow["SelectedSystemDescription"] = dataRow.ItemArray[1].ToString();

                        partitionsDrayWalllabelSectionDataRow["Units"] = string.Empty;

                        partitionsDrayWalllabelSectionDataRow["Percentage"] = this.PartitionsDrayWalllabel.Text.Replace("%", "").Trim();

                        partitionsDrayWalllabelSectionDataRow["QualityID"] = string.Empty;

                        partitionsDrayWalllabelSectionDataRow["QualityDescription"] = string.Empty;

                        partitionsDrayWalllabelSectionDataRow["AllowQualityChangeFlag"] = dataRow.ItemArray[5].ToString();

                        partitionsDrayWalllabelSectionDataRow["PercentMaximum"] = dataRow.ItemArray[8].ToString();

                        partitionsDrayWalllabelSectionDataRow["PercentMinimum"] = dataRow.ItemArray[9].ToString();

                        partitionsDrayWalllabelSectionDataRow["UnitMaximum"] = dataRow.ItemArray[10].ToString();

                        partitionsDrayWalllabelSectionDataRow["UnitMinimum"] = dataRow.ItemArray[11].ToString();

                        partitionsDrayWalllabelSectionDataRow["SectionKeyValue"] = 1;

                        partitionsDrayWalllabelSectionDataRow["SectionGroupID"] = 1;

                        string findResisdentKeyID = "Components_ID = " + dataRow.ItemArray[13].ToString();
                        int residenceKeyId = 0;
                        string systemKeyId;
                        DataRow[] getResidentSystemId = this.componentAssumptionsDataSet.Tables["Components"].Select(findResisdentKeyID);
                        int.TryParse(getResidentSystemId[0].ItemArray[1].ToString(), out residenceKeyId);

                        string findSystemKeyId = "ResidenceSystem_Id = " + residenceKeyId;
                        DataRow[] getSystemKeyId = this.componentAssumptionsDataSet.Tables["ResidenceSystem"].Select(findSystemKeyId);
                        partitionsDrayWalllabelSectionDataRow["SystemKeyValue"] = getSystemKeyId[0].ItemArray[0].ToString();
                    
                        sectionDataTable.Rows.Add(partitionsDrayWalllabelSectionDataRow);
                    }
                }

                #endregion PartitionsDrayWalllabel

                #region Automatic Floor &Cover Allowance

                if (this.Automatic1checkBox.Checked)
                {
                    DataRow floorCoverAllownSectionDataRow = sectionDataTable.NewRow();

                    string floorCoverAllownfindexp = "Key = 502 AND Components_Id = 4";
                    DataRow[] floorCoverAllownDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select(floorCoverAllownfindexp);
                    foreach (DataRow dataRow in floorCoverAllownDataRow)
                    {
                        floorCoverAllownSectionDataRow["Code"] = dataRow.ItemArray[0].ToString();

                        floorCoverAllownSectionDataRow["SelectedSystem"] = "Appliances";

                        floorCoverAllownSectionDataRow["SelectedSystemDescription"] = dataRow.ItemArray[1].ToString();

                        floorCoverAllownSectionDataRow["Units"] = string.Empty;

                        floorCoverAllownSectionDataRow["Percentage"] = string.Empty;

                        floorCoverAllownSectionDataRow["QualityID"] = string.Empty;

                        floorCoverAllownSectionDataRow["QualityDescription"] = string.Empty;

                        floorCoverAllownSectionDataRow["AllowQualityChangeFlag"] = dataRow.ItemArray[5].ToString();

                        floorCoverAllownSectionDataRow["PercentMaximum"] = dataRow.ItemArray[8].ToString();

                        floorCoverAllownSectionDataRow["PercentMinimum"] = dataRow.ItemArray[9].ToString();

                        floorCoverAllownSectionDataRow["UnitMaximum"] = dataRow.ItemArray[10].ToString();

                        floorCoverAllownSectionDataRow["UnitMinimum"] = dataRow.ItemArray[11].ToString();

                        floorCoverAllownSectionDataRow["SectionKeyValue"] = 1;

                        floorCoverAllownSectionDataRow["SectionGroupID"] = 1;

                        string findResisdentKeyID = "Components_ID = " + dataRow.ItemArray[13].ToString();
                        int residenceKeyId = 0;
                        string systemKeyId;
                        DataRow[] getResidentSystemId = this.componentAssumptionsDataSet.Tables["Components"].Select(findResisdentKeyID);
                        int.TryParse(getResidentSystemId[0].ItemArray[1].ToString(), out residenceKeyId);

                        string findSystemKeyId = "ResidenceSystem_Id = " + residenceKeyId;
                        DataRow[] getSystemKeyId = this.componentAssumptionsDataSet.Tables["ResidenceSystem"].Select(findSystemKeyId);
                        floorCoverAllownSectionDataRow["SystemKeyValue"] = getSystemKeyId[0].ItemArray[0].ToString();

                        sectionDataTable.Rows.Add(floorCoverAllownSectionDataRow);
                    }
                }

                #endregion Automatic Floor &Cover Allowance

                #region Automatic Appliance Allo&wance

                if (this.Automatic2checkBox.Checked)
                {
                    DataRow autoAppAllownSectionDataRow = sectionDataTable.NewRow();

                    string autoAppAllownfindexp = "Key = 402 AND Components_Id = 3";
                    DataRow[] autoAppAllownDataRow = this.componentAssumptionsDataSet.Tables["ResidenceComponent"].Select(autoAppAllownfindexp);
                    foreach (DataRow dataRow in autoAppAllownDataRow)
                    {
                        autoAppAllownSectionDataRow["Code"] = dataRow.ItemArray[0].ToString();

                        autoAppAllownSectionDataRow["SelectedSystem"] = "Floor Cover";

                        autoAppAllownSectionDataRow["SelectedSystemDescription"] = dataRow.ItemArray[1].ToString();

                        autoAppAllownSectionDataRow["Units"] = string.Empty;

                        autoAppAllownSectionDataRow["Percentage"] = string.Empty;

                        autoAppAllownSectionDataRow["QualityID"] = string.Empty;

                        autoAppAllownSectionDataRow["QualityDescription"] = string.Empty;

                        autoAppAllownSectionDataRow["AllowQualityChangeFlag"] = dataRow.ItemArray[5].ToString();

                        autoAppAllownSectionDataRow["PercentMaximum"] = dataRow.ItemArray[8].ToString();

                        autoAppAllownSectionDataRow["PercentMinimum"] = dataRow.ItemArray[9].ToString();

                        autoAppAllownSectionDataRow["UnitMaximum"] = dataRow.ItemArray[10].ToString();

                        autoAppAllownSectionDataRow["UnitMinimum"] = dataRow.ItemArray[11].ToString();

                        autoAppAllownSectionDataRow["SectionKeyValue"] = 1;

                        autoAppAllownSectionDataRow["SectionGroupID"] = 1;

                        string findResisdentKeyID = "Components_ID = " + dataRow.ItemArray[13].ToString();
                        int residenceKeyId = 0;
                        string systemKeyId;
                        DataRow[] getResidentSystemId = this.componentAssumptionsDataSet.Tables["Components"].Select(findResisdentKeyID);
                        int.TryParse(getResidentSystemId[0].ItemArray[1].ToString(), out residenceKeyId);

                        string findSystemKeyId = "ResidenceSystem_Id = " + residenceKeyId;
                        DataRow[] getSystemKeyId = this.componentAssumptionsDataSet.Tables["ResidenceSystem"].Select(findSystemKeyId);
                        autoAppAllownSectionDataRow["SystemKeyValue"] = getSystemKeyId[0].ItemArray[0].ToString();
                        
                        sectionDataTable.Rows.Add(autoAppAllownSectionDataRow);
                    }
                }

                #endregion Automatic Appliance Allo&wance

                this.templateDataTable = sectionDataTable;
                this.componentFormxmlValue = TerraScanCommon.GetXmlString(sectionDataTable);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// ExteriorWalls1Combo_SelectedIndexChanged_1
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ExteriorWalls1Combo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
            if ((Exteriorslabel.Text == "") || (Exteriorslabel.Text == "0 %") || (Exteriorslabel.Text == "0%"))
            {
                ////if (this.ExteriorTextBox.Text.Length > 2)
                ////{
                ExteriorWalls2Combo.Enabled = false;
                DataTable dt = new DataTable();
                DataColumn dc = new DataColumn("Key", System.Type.GetType("System.String"));
                DataColumn dc1 = new DataColumn("Description", System.Type.GetType("System.String"));
                dt.Columns.Add(dc);
                dt.Columns.Add(dc1);
                DataRow dr;
                dr = dt.NewRow();
                dr["Key"] = "";
                dr["Description"] = "";
                dt.Rows.Add(dr);

                this.ExteriorWalls2Combo.DataSource = dt;
                this.ExteriorWalls2Combo.DisplayMember = "Description";
                this.ExteriorWalls2Combo.ValueMember = "Key";
                ////this.Exteriorslabel.Text = "";

                ////}
            }
            else
            {
                if (this.ExteriorWalls1Combo.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    this.LoadExteriorWalls2Combo();
                }
            }
        }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// CancelButtons_Click
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void CancelButtons_Click(object sender, EventArgs e)
        {
            try
            {
            this.Close();
             }
             catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PlumbingTextBox_TextChanged(object sender, EventArgs e)
        {
            try
          {
            if (PlumbingTextBox.Text == "0")
            {
                PlumbingTextBox.Text = "0";
            }
            else
            {
                int val = 0;
                int.TryParse(PlumbingTextBox.Text.ToString(), out val);
                if (val == 0)
                {
                    PlumbingTextBox.Text = string.Empty;
                }
            }
          }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void RoughTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                    if (RoughTextBox.Text == "0")
                    {
                        RoughTextBox.Text = "0";
                    }
                    else
                    {
                        int val = 0;
                        int.TryParse(RoughTextBox.Text.ToString(), out val);
                        if (val == 0)
                        {
                            RoughTextBox.Text = string.Empty;
                        }
                    }
           }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
    }
}
