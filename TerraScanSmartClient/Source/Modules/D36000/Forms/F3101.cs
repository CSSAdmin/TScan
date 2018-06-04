//--------------------------------------------------------------------------------------------
// <copyright file="F3101.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Recipting.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//          		KUPPUSAMY.B	    Created
//*********************************************************************************/

namespace D36000
{
    #region namespace

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
    using D36000;

    #endregion namespace

    /// <summary>
    /// F3101
    /// </summary>
    public partial class F3101 : Form
    {
        #region Variable 
        
        /// <summary>
        /// houseTypeDataSet
        /// </summary>
        private DataSet houseTypeDataset = new DataSet();

        /// <summary>
        /// lowQuality
        /// </summary>
        private double lowQuality;

        /// <summary>
        /// highQuality
        /// </summary>
        private double highQuality;

        /// <summary>
        /// Used to store houseType(keyid)
        /// </summary>
        private int houseType;

        /// <summary>
        /// houseTypeXml
        /// </summary>
        private string houseTypeXml;

        /// <summary>
        /// keyId
        /// </summary>
        private int keyId;

        /// <summary>
        /// returnStringvalue
        /// </summary>
        private string returnStringvalue;

        /// <summary>
        /// sectionKey
        /// </summary>
        ////private int sectionKey;

        /// <summary>
        /// squareFeet
        /// </summary>
        private int squareFeet;

        /// <summary>
        /// baseQuality
        /// </summary>
        private double baseQuality;

        /// <summary>
        /// baseDescription
        /// </summary>
        private string baseDescription;

        /// <summary>
        /// baseDescription
        /// </summary>
        private string sectionDescription = string.Empty;

        /// <summary>
        /// PageMode Local variable.
        /// </summary>
        private TerraScanCommon.PageModeTypes pageMode;

        /// <summary>
        /// An object for Dataset - TagolongDataTable
        /// </summary>
        private DataTable tagolongDataTable = new DataTable();

        /// <summary>
        /// Used to store tagAlongWidth
        /// </summary>
        private string tagAlongWidth;

        /// <summary>
        /// Used to store the tagAlongLength
        /// </summary>
        private string tagAlongLength;

        /// <summary>
        /// Used to store the tagAlongMaxWidth
        /// </summary>
        private int tagAlongminVal1;

        /// <summary>
        /// Used to store the tagAlongMinWidth
        /// </summary>
        private int tagAlongminVal2;

        /// <summary>
        /// Used to store the tagAlongMaxLength
        /// </summary>
        private int tagAlongmaxVal1;

        /// <summary>
        /// Used to store the tagAlongMinLength
        /// </summary>
        private int tagAlongmaxVal2;

        /// <summary>
        /// Used to store the firstLoad value
        /// </summary>
        private bool firstLoad;

        /// <summary>
        /// Used to store the tagAlongMaxLength
        /// </summary>
        private int tagAlongMaxLength;

        /// <summary>
        /// Used to store the tagAlongMInlength
        /// </summary>
        private int tagAlongMInlength;

        /// <summary>
        /// Used to store the sectionKeyValue
        /// </summary>
        private int sectionKeyValue;

        /// <summary>
        /// Used to store the sectionGroupId
        /// </summary>
        private int sectionGroupId;
       
        #endregion Variable

        #region Constructor 
        
        /// <summary>
        /// Initializes a new instance of the <see cref="F3101"/> class.
        /// </summary>
        /// <param name="xmlType">Type of the XML.</param>
        /// <param name="sectionKey">The section key.</param>
        /// <param name="groupId">The group id.</param>
        public F3101(string xmlType, int sectionKey, int groupId)
        {
            InitializeComponent();
            this.pageMode = TerraScanCommon.PageModeTypes.New;
            this.houseTypeXml = xmlType;
            this.sectionKeyValue = sectionKey;
            this.sectionGroupId = groupId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F3101"/> class.
        /// </summary>
        /// <param name="sectionKey">The section key.</param>
        /// <param name="squareFeet">The square feet.</param>
        /// <param name="baseQuality">The base quality.</param>
        /// <param name="qualityDescription">The quality description.</param>
        /// <param name="description">The description.</param>
        /// <param name="tagAlongWidth">Width of the tag along.</param>
        /// <param name="tagAlongLength">Length of the tag along.</param>
        /// <param name="xmlType">Type of the XML.</param>
        /// <param name="groupId">The group id.</param>
        public F3101(int sectionKey, int squareFeet, double baseQuality, string qualityDescription, string description, string tagAlongWidth, string tagAlongLength, string xmlType, int groupId)
        {
            InitializeComponent();
            this.pageMode = TerraScanCommon.PageModeTypes.Edit;
            this.houseTypeXml = xmlType;
            this.sectionKeyValue = sectionKey;
            this.squareFeet = squareFeet;
            this.baseQuality = baseQuality;
            this.baseDescription = qualityDescription;
            this.tagAlongWidth = tagAlongWidth;
            this.tagAlongLength = tagAlongLength;
            this.sectionDescription = description;
            this.sectionGroupId = groupId;
        }        

        #endregion Constructor 

        #region Property 

        /// <summary>
        /// SectionReturn XML Value
        /// </summary>
        public string SectionReturnValue
        {
            get { return this.returnStringvalue; }
            set { this.returnStringvalue = value; }
        }        
          
        #endregion Property        

        #region Events 

        /// <summary>
        /// Handles the Load event of the F3101 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F3101_Load(object sender, EventArgs e)
         {
             try
             {
                 this.AcceptButton = this.OkButton;
                 this.CancelButton = this.MSCancelButton;

                 if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                 {
                     StringReader stringXmlReader = new StringReader(this.houseTypeXml.ToString());
                     System.Xml.XmlTextReader textReaderHouseType = new System.Xml.XmlTextReader(stringXmlReader);
                     this.houseTypeDataset.ReadXml(textReaderHouseType);
                     this.LoadHouseTypeCombo();
                     this.LoadDescription();
                     int.TryParse(this.houseTypeDataset.Tables["ResidenceType"].Rows[0]["Key"].ToString(), out this.houseType);
                     this.lowQuality = Convert.ToDouble(this.houseTypeDataset.Tables["LowQuality"].Rows[0]["ID"].ToString());
                     this.highQuality = Convert.ToDouble(this.houseTypeDataset.Tables["HighQuality"].Rows[0]["ID"].ToString());
                     this.QualityValueTextBox.Text = this.houseTypeDataset.Tables["ResidenceTypeQuality"].Rows[2]["Key"].ToString();
                     this.QualityValueLabel.Text = this.houseTypeDataset.Tables["ResidenceTypeQuality"].Rows[2]["Description"].ToString();

                     if (this.houseType == 6)
                     {
                         this.GetTagAlongSize();
                     }
                 }
                 else if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                 {
                     StringReader stringXmlReader = new StringReader(this.houseTypeXml.ToString());
                     System.Xml.XmlTextReader textReaderHouseType = new System.Xml.XmlTextReader(stringXmlReader);
                     this.houseTypeDataset.ReadXml(textReaderHouseType);
                     this.LoadHouseTypeCombo();
                     this.SectionTypeCombo.SelectedValue = this.sectionGroupId;
                     ////this.SectionTypeCombo.SelectedText = this.sectionDescription.Trim();             
                     this.lowQuality = Convert.ToDouble(this.houseTypeDataset.Tables["LowQuality"].Rows[0]["ID"].ToString());
                     this.highQuality = Convert.ToDouble(this.houseTypeDataset.Tables["HighQuality"].Rows[0]["ID"].ToString());
                     this.SizeTextBox.Text = this.squareFeet.ToString();
                     this.QualityValueTextBox.Text = this.baseQuality.ToString().Trim();
                     this.QualityValueLabel.Text = this.baseDescription;
                     this.DescriptionTextBox.Text = this.sectionDescription.Trim();
                     if (this.sectionKeyValue.ToString().Equals("10"))
                     {
                         this.firstLoad = true;
                     }
                 }

                 if (this.SectionTypeCombo.Text == "Tagalong")
                 {
                     this.TagAlongpanel.BringToFront();
                     this.Sizepanel.Enabled = false;

                     if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                     {
                         this.WidthTextBox.Text = string.Empty;
                         this.TALengthTextBox.Text = string.Empty;
                     }
                     else if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                     {
                         this.DescriptionTextBox.Text = this.sectionDescription.Trim();
                         this.WidthTextBox.Text = this.tagAlongWidth;
                         this.TALengthTextBox.Text = this.tagAlongLength;
                     }
                 }
                 else
                 {
                     if (!string.IsNullOrEmpty(this.sectionDescription))
                     {
                         this.DescriptionTextBox.Text = this.sectionDescription.Trim();
                     }

                     if (this.squareFeet != 0)
                     {
                         this.SizeTextBox.Text = this.squareFeet.ToString();
                     }

                     this.Sizepanel.BringToFront();
                     this.TagAlongpanel.Enabled = false;
                 }

                 this.SectionTypepanel.Focus();
                 this.SectionTypeCombo.Focus();
                 this.ActiveControl = this.SectionTypeCombo;
             }
             catch (Exception ex)
             {
                 ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
             }
        }       

        /// <summary>
        /// Handles the Click event of the OkButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            try 
            {
                string errorMessage = string.Empty;
                this.Cursor = Cursors.WaitCursor;

                if (this.DescriptionTextBox.Text.Trim() == string.Empty)
                {
                    errorMessage = "Description must not be blank";
                    MessageBox.Show(errorMessage, "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.DescriptionTextBox.Focus();
                }
                else
                {
                    if (this.SectionTypeCombo.Text == "Tagalong")
                    {
                        this.CheckMaxMinvalue();
                        this.ToValidateReturnXMLString();
                    }
                    else
                    {
                        ////this.ValidateSize();
                        if (this.SizeTextBox.Text == string.Empty)
                        {
                            this.ToValidateReturnXMLString();
                        }
                        else
                        {
                            this.ToValidateReturnXMLString();
                        }
                    }
                }               

                ////if (this.SizeTextBox.Text == string.Empty)
                ////{
                ////    this.ToValidateReturnXMLString();
                ////}
                ////else
                ////{
                ////    this.ToValidateReturnXMLString();
                ////}               
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the SectionTypeCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SectionTypeCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                this.LoadDescription();
                string selectedText = "";
                selectedText = this.sectionDescription;
                this.DescriptionTextBox.Text = selectedText;
                if (this.pageMode == TerraScanCommon.PageModeTypes.Edit)
                {
                    if (this.firstLoad == true)
                    {
                        this.SizeTextBox.Text = string.Empty;
                        this.firstLoad = false;
                    }
                    
                    DataRow[] seletRows = this.houseTypeDataset.Tables["ResidenceGroupType"].Select("Description = '" + this.DescriptionTextBox.Text + "'", "Description ASC");
                    if (seletRows.GetUpperBound(0) >= 0)
                    {
                        this.DescriptionTextBox.Text = this.SectionTypeCombo.Text;
                    }
                    else
                    {
                        this.DescriptionTextBox.Text = this.sectionDescription;
                    }                    
                }

                if (this.SectionTypeCombo.Text == "Tagalong")
                {
                    this.TagAlongpanel.BringToFront();
                    this.Sizepanel.Enabled = false;
                    this.TagAlongpanel.Enabled = true;
                }
                else
                {
                    this.TagAlongpanel.Enabled = false;
                    this.Sizepanel.Enabled = true;
                    this.Sizepanel.BringToFront();
                }

                if (this.pageMode == TerraScanCommon.PageModeTypes.New)
                {
                    this.LoadDescription();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the QualityValueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
        private void QualityValueTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                QualityValueTextBox.TextCustomFormat = "#,##0.00";
                QualityValueTextBox.ApplyCFGFormat = false;
                double qualityValue = 0.0;
                if (!string.IsNullOrEmpty(this.QualityValueTextBox.Text.Trim()))
                {
                    double.TryParse(this.QualityValueTextBox.Text.Trim().ToString(), out qualityValue);
                    this.QualityValueLabel.Text = this.BaseQualityDescription(qualityValue);
                }
                else
                {
                    this.QualityValueLabel.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }        

        /// <summary>
        /// Handles the Leave event of the QualityValueTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void QualityValueTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.ValidateQuality();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the SizeTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SizeTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                this.ValidateSize();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the WidthTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void WidthTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int tempWidthTextBoxValue;
                this.GetTagAlongSize();
                int.TryParse(this.WidthTextBox.Text.Trim(), out tempWidthTextBoxValue);
                if (((tempWidthTextBoxValue >= this.tagAlongminVal1) && (tempWidthTextBoxValue <= this.tagAlongminVal2)) || ((tempWidthTextBoxValue >= this.tagAlongmaxVal1) && (tempWidthTextBoxValue <= this.tagAlongmaxVal2)))
                {
                    /////MessageBox.Show("Tagalong Width (" + this.tagAlongminVal1 + "-" + this.tagAlongminVal2 + ") or (" + this.tagAlongmaxVal1 + "-" + this.tagAlongmaxVal2 + ") must be " + this.tagAlongminVal1 + "through" + this.tagAlongmaxVal2 + ".", "Resditaila estimor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Tagalong Width (" + this.tagAlongminVal1 + "-" + this.tagAlongminVal2 + ") or (" + this.tagAlongmaxVal1 + "-" + this.tagAlongmaxVal2 + ") must be " + this.tagAlongminVal1 + " through " + this.tagAlongmaxVal2 + ".", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.WidthTextBox.Focus();
                }

                int outInt = 0;
                if (this.WidthTextBox.Text != null && !string.IsNullOrEmpty(this.WidthTextBox.Text.ToString()))
                {
                    string val = this.WidthTextBox.Text.ToString();
                    if (int.TryParse(val, out outInt))
                    {
                        if (outInt.ToString().Contains("-"))
                        {
                            this.WidthTextBox.Text = String.Concat("(", Decimal.Negate(outInt).ToString("#,##0"), ")");
                        }
                        else
                        {
                            this.WidthTextBox.Text = outInt.ToString("#,##0");
                        }
                    }
                    else
                    {
                        this.WidthTextBox.Text = "0";
                    }
                }
                else
                {
                    this.WidthTextBox.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Leave event of the TALengthTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TALengthTextBox_Leave(object sender, EventArgs e)
        {
            try
            {
                int tempLengthtextBoxvalue;

                int.TryParse(this.TALengthTextBox.Text.Trim(), out tempLengthtextBoxvalue);

                if ((tempLengthtextBoxvalue > this.tagAlongMaxLength) || (tempLengthtextBoxvalue < this.tagAlongMInlength))
                {
                    this.GetTagAlongSize();
                    MessageBox.Show("Tagalong Length must be " + this.tagAlongMInlength + " through " + this.tagAlongMaxLength + ".", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.TALengthTextBox.Focus();
                }

                int outInt = 0;
                if (this.TALengthTextBox.Text != null && !string.IsNullOrEmpty(this.TALengthTextBox.Text.ToString()))
                {
                    string val = this.TALengthTextBox.Text.ToString();
                    if (int.TryParse(val, out outInt))
                    {
                        if (outInt.ToString().Contains("-"))
                        {
                            this.TALengthTextBox.Text = String.Concat("(", Decimal.Negate(outInt).ToString("#,##0"), ")");
                        }
                        else
                        {
                            this.TALengthTextBox.Text = outInt.ToString("#,##0");
                        }
                    }
                    else
                    {
                        this.TALengthTextBox.Text = "0";
                    }
                }
                else
                {
                    this.TALengthTextBox.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events         

        #region Methods 

        /// <summary>
        /// Loads the house type combo.
        /// </summary>
        private void LoadHouseTypeCombo()
        {
            DataRow[] houseTypeDataRow;
            houseTypeDataRow = this.houseTypeDataset.Tables["ResidenceGroupType"].Select("Key > 1");
            this.SectionTypeCombo.DataSource = this.DataRowToDataTable(houseTypeDataRow);
            this.SectionTypeCombo.DisplayMember = this.houseTypeDataset.Tables["ResidenceGroupType"].Columns["Description"].ColumnName;
            this.SectionTypeCombo.ValueMember = this.houseTypeDataset.Tables["ResidenceGroupType"].Columns["Key"].ColumnName;            
        }

        /// <summary>
        /// Loads the description.
        /// </summary>
        private void LoadDescription()
        {
            string selectedValue = string.Empty;
            selectedValue = this.SectionTypeCombo.Text;
            this.DescriptionTextBox.Text = selectedValue;
        }

        /// <summary>
        /// Bases the quality description.
        /// </summary>
        /// <param name="keyValue">The key value.</param>
        /// <returns>BaseQualityDescription</returns>
        private string BaseQualityDescription(double keyValue)
        {
            DataRow[] qualityRow;
            DataRow[] decimalQualityRow;
            DataTable qualityDataTable = new DataTable();
            DataTable decimalQualityDataTable = new DataTable();

            string description = string.Empty;
            int numericPart = Convert.ToInt32(Math.Floor(keyValue));
            double decimalPart = keyValue - Math.Floor(keyValue);

            if (keyValue >= this.lowQuality && keyValue <= this.highQuality)
            {
                if (decimalPart > 0)
                {
                    qualityRow = this.houseTypeDataset.Tables["ResidenceTypeQuality"].Select("Key = " + "'" + numericPart + "'");
                    decimalQualityRow = this.houseTypeDataset.Tables["ResidenceTypeQuality"].Select("Key = " + "'" + (numericPart + 1) + "'");
                    qualityDataTable = this.DataRowToDataTable(qualityRow);
                    decimalQualityDataTable = this.DataRowToDataTable(decimalQualityRow);
                    description = qualityDataTable.Rows[0]["Description"].ToString() + " / " + decimalQualityDataTable.Rows[0]["Description"].ToString();
                    return description;                    
                }
                else
                {
                    qualityRow = this.houseTypeDataset.Tables["ResidenceTypeQuality"].Select("Key = " + "'" + numericPart + "'");
                    qualityDataTable = this.DataRowToDataTable(qualityRow);
                    description = qualityDataTable.Rows[0]["Description"].ToString();
                    return description;                    
                }
            }
            else
            {
                return description;                
            }
        }

        /// <summary>
        /// Datas the row to data table.
        /// </summary>
        /// <param name="tempDataRow">The temp data row.</param>
        /// <returns>tempDataRow</returns>
        private DataTable DataRowToDataTable(DataRow[] tempDataRow)
        {
            DataSet convertedDataSet = new DataSet();
            convertedDataSet.Merge(tempDataRow);
            return convertedDataSet.Tables[0];
        }

        /// <summary>
        /// Returns the XML string.
        /// </summary>
        private void ReturnXMLString()
        {
                decimal tempTagalongWidth;
                decimal tempTagalongLength;
                decimal tempSquareFeetValue;

                DataTable sectionDataTable = new DataTable();
                DataColumn[] sectionColumn = new DataColumn[] { new DataColumn("Description"), new DataColumn("ResidenceGroupType_Id"), new DataColumn("SectionKey"), new DataColumn("SquareFeet"), new DataColumn("BaseQuality"), new DataColumn("QualityDescription"), new DataColumn("TagalongWidth"), new DataColumn("TagalongLength"), new DataColumn("GroupID") };
                sectionDataTable.Columns.AddRange(sectionColumn);
                DataRow sectionDataRow = sectionDataTable.NewRow();
                int.TryParse(this.SectionTypeCombo.SelectedValue.ToString(), out this.keyId);

                string findexp = "Key = " + "'" + this.keyId + "'";
                DataRow[] fromAssign = this.houseTypeDataset.Tables["ResidenceGroupType"].Select(findexp);

                foreach (DataRow dataRow in fromAssign)
                {
                    tempTagalongWidth = 0;
                    tempTagalongLength = 0;
                    tempSquareFeetValue = 0;

                    //// sectionDataRow["GroupID"] = this.sectionGroupId + 1;
                    sectionDataRow["GroupID"] = this.SectionTypeCombo.SelectedValue.ToString();  
                    sectionDataRow["Description"] = this.DescriptionTextBox.Text.Trim();
                    sectionDataRow["ResidenceGroupType_Id"] = dataRow.ItemArray[4].ToString();
                    ////sectionDataRow["SectionKey"] = dataRow.ItemArray[0].ToString();
                    sectionDataRow["SectionKey"] = this.sectionKeyValue.ToString();
                    sectionDataRow["BaseQuality"] = this.QualityValueTextBox.Text.Trim();
                    sectionDataRow["QualityDescription"] = this.QualityValueLabel.Text.Trim();

                    if (this.SectionTypeCombo.Text.Trim() == "Tagalong")
                    {
                        sectionDataRow["TagalongWidth"] = this.WidthTextBox.Text.Trim();
                        decimal.TryParse(this.WidthTextBox.Text.Trim(), out tempTagalongWidth);

                        sectionDataRow["TagalongLength"] = this.TALengthTextBox.Text.Trim();
                        decimal.TryParse(this.TALengthTextBox.Text.Trim(), out tempTagalongLength);

                        tempSquareFeetValue = tempTagalongWidth * tempTagalongLength;

                        sectionDataRow["SquareFeet"] = tempSquareFeetValue.ToString();                        
                    }
                    else
                    {
                        sectionDataRow["SquareFeet"] = this.SizeTextBox.Text.Trim();
                        sectionDataRow["TagalongWidth"] = string.Empty;
                        sectionDataRow["TagalongLength"] = string.Empty;
                    }

                    sectionDataTable.Rows.Add(sectionDataRow);
                }

                this.returnStringvalue = TerraScanCommon.GetXmlString(sectionDataTable);            
        }
       
        /// <summary>
        /// Validates the size.
        /// </summary>
        /// <returns>true</returns>
        private bool ValidateSize()
        {
            int primaryWidthValue = 0;
            int typedSizeValue = 0;
            int maxSize = 0;
            int minSize = 0;
            string widthDescription = string.Empty;
            int.TryParse(this.SectionTypeCombo.SelectedValue.ToString(), out primaryWidthValue);
            
            DataRow[] primaryWidthDataRow;
            primaryWidthDataRow = this.houseTypeDataset.Tables["ResidenceGroupType"].Select("key =" + "'" + primaryWidthValue + "'");
            
            if (this.SectionTypeCombo.Text.Trim() == "Tagalong")
            {
                decimal tempTagalongWidth = 0;
                decimal tempTagalongLength = 0;
                decimal tempSquareFeetValue = 0;                
               
                decimal.TryParse(this.WidthTextBox.Text.Trim(), out tempTagalongWidth);
               
                decimal.TryParse(this.TALengthTextBox.Text.Trim(), out tempTagalongLength);

                tempSquareFeetValue = tempTagalongWidth * tempTagalongLength;

                if (primaryWidthDataRow.Length > 0)
                {
                    foreach (DataRow widthRow in primaryWidthDataRow)
                    {
                        int.TryParse(widthRow.ItemArray[2].ToString(), out maxSize);
                        int.TryParse(widthRow.ItemArray[3].ToString(), out minSize);
                        widthDescription = widthRow.ItemArray[1].ToString();
                    }

                    if (tempSquareFeetValue >= minSize && tempSquareFeetValue <= maxSize)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                int outInt = 0;
                if (this.SizeTextBox.Text != null && !string.IsNullOrEmpty(this.SizeTextBox.Text.ToString()))
                {
                    string val = this.SizeTextBox.Text.ToString();
                    if (int.TryParse(val.Replace(",", ""), out outInt))
                    {
                        if (outInt.ToString().Contains("-"))
                        {
                            this.SizeTextBox.Text = String.Concat("(", Decimal.Negate(outInt).ToString("#,##0"), ")");
                        }
                        else
                        {
                            this.SizeTextBox.Text = outInt.ToString("#,##0");
                        }
                    }
                    else
                    {
                        this.SizeTextBox.Text = "0";
                    }
                }
                else
                {
                    this.SizeTextBox.Text = string.Empty;
                }

                int.TryParse(this.SizeTextBox.Text.Trim().ToString().Replace(",", ""), out typedSizeValue);

                if (primaryWidthDataRow.Length > 0)
                {
                    foreach (DataRow widthRow in primaryWidthDataRow)
                    {
                        int.TryParse(widthRow.ItemArray[2].ToString(), out maxSize);
                        int.TryParse(widthRow.ItemArray[3].ToString(), out minSize);
                        widthDescription = widthRow.ItemArray[1].ToString();
                    }

                    if (typedSizeValue >= minSize && typedSizeValue <= maxSize)
                    {
                        return false;
                    }
                    else
                    {
                        MessageBox.Show("Size must be " + minSize + " through " + maxSize, SharedFunctions.GetResourceString("TerraScan T2"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.SizeTextBox.Focus();
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// To validate and Return XML string.
        /// </summary>
        private void ToValidateReturnXMLString()
        {
            if (this.ValidateSize() || this.ValidateQuality())
            {
                return;
            }
            else
            {
                this.ReturnXMLString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Validates the quality.
        /// </summary>
        /// <returns>bool val</returns>
        private bool ValidateQuality()
        {
            double leaveQuality = 0.0;
            Decimal outDecimal;
            double.TryParse(this.QualityValueTextBox.Text.Trim(), out leaveQuality);
            
            if (this.QualityValueTextBox.Text != null && !string.IsNullOrEmpty(this.QualityValueTextBox.Text.ToString()))
            {
                string val = this.QualityValueTextBox.Text.ToString();
                if (Decimal.TryParse(val, out outDecimal))
                {
                    if (outDecimal.ToString().Contains("-"))
                    {
                        this.QualityValueTextBox.Text = String.Concat("(", Decimal.Negate(outDecimal).ToString("#,##0.0000"), ")");
                    }
                    else
                    {
                        this.QualityValueTextBox.Text = outDecimal.ToString("#,##0.00");
                    }
                }
                else
                {
                    this.QualityValueTextBox.Text = "0.00";
                }
            }
            else
            {
                this.QualityValueTextBox.Text = string.Empty;
            }

            if (leaveQuality >= this.lowQuality && leaveQuality <= this.highQuality)
            {
                return false;
            }
            else
            {
                MessageBox.Show("Quality must be " + this.lowQuality + " through " + this.highQuality, SharedFunctions.GetResourceString("TerraScan T2"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.QualityValueTextBox.Focus();
                return true;
            }
        }

        /// <summary>
        /// Gets the size of the tag along.
        /// </summary>
        /// <returns>DataRow</returns>
        private DataRow GetTagAlongSize()
        {
            string values = string.Empty;
            string lengthValues = string.Empty;
            DataRow[] tempTableDataRow = this.houseTypeDataset.Tables["ResidenceStyle"].Copy().Select("StyleFlag = 2", "MaxWidth ASC");
            foreach (DataRow tempRow in tempTableDataRow)
            {
                if (String.IsNullOrEmpty(values))
                {
                    values = tempRow[3].ToString();
                }
                else
                {
                    values = values + ",";
                    values = values + tempRow[3].ToString();
                }

                values = values + "," + tempRow[4].ToString();

                if (String.IsNullOrEmpty(lengthValues))
                {
                    lengthValues = tempRow[5].ToString();                                        
                }
                else
                {
                    lengthValues = lengthValues + ",";
                    lengthValues = lengthValues + tempRow[5].ToString();
                }

                lengthValues = lengthValues + "," + tempRow[6].ToString();
            }

           string[] tempValues = values.Split(new char[] { ',' });
           string[] tempLengthValues = lengthValues.Split(new char[] { ',' });
           int.TryParse(tempValues[1], out this.tagAlongminVal1);
           int.TryParse(tempValues[0], out this.tagAlongminVal2);
           int.TryParse(tempValues[3], out this.tagAlongmaxVal1);
           int.TryParse(tempValues[2], out this.tagAlongmaxVal2);
           int.TryParse(tempLengthValues[0], out this.tagAlongMaxLength);
           int.TryParse(tempLengthValues[1], out this.tagAlongMInlength);

           return tempTableDataRow[0];
        }

        /// <summary>
        /// Checks the max minvalue.
        /// </summary>
        /// <returns>bool value</returns>
        private bool CheckMaxMinvalue()
        {
                int tempWidthTextBoxValue;
                this.GetTagAlongSize();
                int.TryParse(this.WidthTextBox.Text.Trim(), out tempWidthTextBoxValue);
                if (((tempWidthTextBoxValue >= this.tagAlongminVal1) && (tempWidthTextBoxValue <= this.tagAlongminVal2)) || ((tempWidthTextBoxValue >= this.tagAlongmaxVal1) && (tempWidthTextBoxValue <= this.tagAlongmaxVal2)))
                {
                    int tempLengthtextBoxvalue;

                    int.TryParse(this.TALengthTextBox.Text.Trim(), out tempLengthtextBoxvalue);

                    if ((tempLengthtextBoxvalue > this.tagAlongMaxLength) || (tempLengthtextBoxvalue < this.tagAlongMInlength))
                    {
                        MessageBox.Show("Tagalong Length must be " + this.tagAlongMInlength + " through " + this.tagAlongMaxLength + ".", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.TALengthTextBox.Focus();
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("Tagalong Width (" + this.tagAlongminVal1 + "-" + this.tagAlongminVal2 + ") or (" + this.tagAlongmaxVal1 + "-" + this.tagAlongmaxVal2 + ") must be " + this.tagAlongminVal1 + " through " + this.tagAlongmaxVal2 + ".", "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.WidthTextBox.Focus();
                    return false;
                }            
        }

        #endregion Methods       
    }
}