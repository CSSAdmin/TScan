//--------------------------------------------------------------------------------------------
// <copyright file="F3601.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains UI for F3601 
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 18-07-2007      M.Vijayakumar       Created
//*********************************************************************************/

namespace D36001
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
    using System.Xml;
    using System.Web.Services.Protocols;
    using TerraScan.Infrastructure.Interface.Constants;
    using TerraScan.Helper;
    using TerraScan.UI.Controls;

    /// <summary>
    /// F3601 class file
    /// </summary>
    public partial class F3601 : Form
    {
        #region Variables
       
        /// <summary>
        /// form3601Control
        /// </summary>
        private F3601Controller form3601Control;

        /// <summary>
        /// Used to store the editEnterComponentlableType
        /// </summary>
        private int editEnterComponentlableType;

        /// <summary>
        /// Used to store the componentDataset
        /// </summary>
        private DataSet componentDataset = new DataSet();

        /// <summary>
        /// Used to store the editEnterComponentDataset
        /// </summary>
        private DataSet editEnterComponentDataset = new DataSet();

        /// <summary>
        /// Used to store the componentId
        /// </summary>
        private int componentId;
        
        /// <summary>
        /// Used to store the components_Id
        /// </summary>
        private int componentsKeyId;

        /// <summary>
        /// constructionSystemId
        /// </summary>
        private int constructionSystemId;

        /// <summary>
        /// systemDescLabelText
        /// </summary>
        private string systemDescLabelText = string.Empty;

        /// <summary>
        /// used to store the unitMinValue
        /// </summary>
        private int unitMinValue;

        /// <summary>
        /// used to store the unitMaxValue
        /// </summary>
        private int unitMaxValue;

        /// <summary>
        /// Used to store the otherMinValue
        /// </summary>
        private decimal otherMinValue;

        /// <summary>
        /// Used to store the otherMaxValue
        /// </summary>
        private decimal otherMaxValue;

        /// <summary>
        /// Used to store the Other Value
        /// </summary>
        private string otherTextValue;

        /// <summary>
        /// Used to store the editEnterComponentsFormXmlValue
        /// </summary>
        private string editEnterComponentsFormXmlValue;

        /// <summary>
        /// Used to store the componentsDatasetXmlstring
        /// </summary>
        private string componentsDatasetXmlstring;

        /// <summary>
        /// Used to store the editEnterComponentsTextValue
        /// </summary>
        private Hashtable editEnterComponentsTextValue;

        /// <summary>
        /// Used to store the sectionTypeTableKeyid
        /// </summary>
        private int sectionTypeTableKeyid;
      
        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F3601"/> class.
        /// </summary>
        public F3601()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F3601"/> class.
        /// </summary>
        /// <param name="componentId">The component id.</param>
        /// <param name="componentsSystemId">The components system id.</param>
        /// <param name="sectionTypeKey">The section type key.</param>
        /// <param name="componentsDatasetXmlString">The components dataset XML string.</param>
        /// <param name="editEnterComponentValue">The edit enter component value.</param>
        public F3601(int componentId, int componentsSystemId, int sectionTypeKey, string componentsDatasetXmlString, Hashtable editEnterComponentValue)
        {
            InitializeComponent();
            this.componentId = componentId;
            this.constructionSystemId = componentsSystemId;
            this.sectionTypeTableKeyid = sectionTypeKey;
            this.componentsDatasetXmlstring = componentsDatasetXmlString;
            this.editEnterComponentsTextValue = editEnterComponentValue;
        }

        #endregion Constructor

        #region enumerator

        /// <summary>
        /// AreaType
        /// </summary>
        private enum AreaType
        {
            TotalAreaLabelCountTwo = 1,

            TotalAreaLabelCountThree = 2,

            MainLableOne = 3,

            MainlableTwo = 4,

            PercentUnitEmpty = 5,
            
            PercentOnly = 6
        }

        #endregion enumerator

        #region Property

        /// <summary>
        /// Gets or sets the F3601 controll.
        /// </summary>
        /// <value>The F3601 controll.</value>
        [CreateNew]
        public F3601Controller F3601Controll
        {
            get { return this.form3601Control as F3601Controller; }
            set { this.form3601Control = value; }
        }

        /// <summary>
        /// Used to store the EditEnterComponentsFormXmlValue
        /// </summary>
        public string EditEnterComponentsFormXmlValue
        {
            get { return this.editEnterComponentsFormXmlValue; }
            set { this.editEnterComponentsFormXmlValue = value; }
        }

        #endregion Property

        #region Methods

        /// <summary>
        /// To the assign edit enter component values.
        /// </summary>
        private void ToAssignEditEnetComponentValues()
        {
            try
            {
                this.ComponentNolabel.Text = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("ComponentIDColumnName")].ToString();
                this.ComponentDescriptionlabel.Text = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("NameColumnName")].ToString();
                this.SystemDescLabel.Text = this.systemDescLabelText;

                ////to set Max length to 10 when this is not percent text value
                this.MainPanel1TextBox.MaxLength = 10;

                if (editEnterComponentlableType == (int)AreaType.TotalAreaLabelCountThree)
                {
                    this.AreaOrLabel1.Text = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("PctInpTxtColumnName")].ToString();

                    if (this.editEnterComponentsTextValue.Count != 0)
                    {
                        this.AreaOrPanelRangeTextBox1.Text = this.editEnterComponentsTextValue["Percentage"].ToString();
                    }
                    else
                    {
                        this.AreaOrPanelRangeTextBox1.Text = string.Empty;
                    }                   
                    
                    this.AreaOrPanelRangeLabel1.Text = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("PctMinMaxTxtColumnName")].ToString();

                    this.AreaOrLabel2.Text = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("UnitsInpTxtColumnName")].ToString();

                    if (this.editEnterComponentsTextValue.Count != 0)
                    {
                        this.AreaOrPanelRangeTextBox2.Text = this.editEnterComponentsTextValue["Units"].ToString();
                    }
                    else
                    {
                        this.AreaOrPanelRangeTextBox2.Text = string.Empty;
                    }
                    
                    this.AreaOrPanelRangeLabel2.Text = "Required: " + this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("UnitsMinMaxTxtColumnName")].ToString();
                    
                    this.AreaExtraLabel.Text = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("OtherInpTxtColumnName")].ToString();

                    if (this.editEnterComponentsTextValue.Count != 0)
                    {
                        this.AreaExtraTextBox.Text = this.editEnterComponentsTextValue["Other2"].ToString();
                    }
                    else
                    {
                        this.AreaExtraTextBox.Text = string.Empty;
                    }
                   
                    
                    this.AreaExtraRangelabel.Text = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("OtherMinMaxTxtColumnName")].ToString();
                    this.otherTextValue = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("OtherTextColumnName")].ToString();

                    
                    int.TryParse(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("Max_ColumnName")].ToString(), out this.unitMaxValue);
                    int.TryParse(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("Min_ColumnName")].ToString(), out this.unitMinValue);

                    decimal.TryParse(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("OtherMaxColumnName")].ToString(), out this.otherMaxValue);
                    decimal.TryParse(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("OtherMinColumnName")].ToString(), out this.otherMinValue);

                }
                else if (editEnterComponentlableType == (int)AreaType.TotalAreaLabelCountTwo)
                {
                    this.AreaOrLabel1.Text = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("PctInpTxtColumnName")].ToString();

                    if (this.editEnterComponentsTextValue.Count != 0)
                    {
                        this.AreaOrPanelRangeTextBox1.Text = this.editEnterComponentsTextValue["Percentage"].ToString();
                    }
                    else
                    {
                        this.AreaOrPanelRangeTextBox1.Text = string.Empty;
                    }
                    
                    this.AreaOrPanelRangeLabel1.Text = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("PctMinMaxTxtColumnName")].ToString();

                    this.AreaOrLabel2.Text = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("UnitsInpTxtColumnName")].ToString();

                    if (this.editEnterComponentsTextValue.Count != 0)
                    {
                        this.AreaOrPanelRangeTextBox2.Text = this.editEnterComponentsTextValue["Units"].ToString();
                    }
                    else
                    {
                        this.AreaOrPanelRangeTextBox2.Text = string.Empty;
                    }                   
                    
                    this.AreaOrPanelRangeLabel2.Text = "Required: " +this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("UnitsMinMaxTxtColumnName")].ToString();

                    int.TryParse(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("Max_ColumnName")].ToString(), out this.unitMaxValue);
                    int.TryParse(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("Min_ColumnName")].ToString(), out this.unitMinValue);                    
                }
                else if (editEnterComponentlableType == (int)AreaType.MainLableOne)
                {
                    this.MainPanel1Label.Text = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("UnitsInpTxtColumnName")].ToString();

                    if (this.editEnterComponentsTextValue.Count != 0)
                    {
                        this.MainPanel1TextBox.Text = this.editEnterComponentsTextValue["Units"].ToString();
                    }
                    else
                    {
                        this.MainPanel1TextBox.Text = string.Empty;
                    }                   
                    
                    this.MainPanel1RangeLable.Text = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("UnitsMinMaxTxtColumnName")].ToString();

                    int.TryParse(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("Max_ColumnName")].ToString(), out this.unitMaxValue);
                    int.TryParse(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("Min_ColumnName")].ToString(), out this.unitMinValue);
                }
                else if (editEnterComponentlableType == (int)AreaType.MainlableTwo)
                {
                    this.MainPanel1Label.Text = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("UnitsInpTxtColumnName")].ToString();

                    if (this.editEnterComponentsTextValue.Count != 0)
                    {
                        this.MainPanel1TextBox.Text = this.editEnterComponentsTextValue["Units"].ToString();
                    }
                    else
                    {
                        this.MainPanel1TextBox.Text = string.Empty;
                    }                   

                    this.MainPanel1RangeLable.Text = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("UnitsMinMaxTxtColumnName")].ToString();

                    this.MainPanel2Label.Text =  this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("OtherInpTxtColumnName")].ToString();

                    if (this.editEnterComponentsTextValue.Count != 0)
                    {
                        this.MainPanel2TextBox.Text = this.editEnterComponentsTextValue["Other2"].ToString();
                    }
                    else
                    {
                        this.MainPanel2TextBox.Text = string.Empty;
                    }                   
                    
                    this.MainPanel2RangeLable.Text = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("OtherMinMaxTxtColumnName")].ToString();
                    this.otherTextValue = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("OtherTextColumnName")].ToString();

                    int.TryParse(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("Max_ColumnName")].ToString(), out this.unitMaxValue);
                    int.TryParse(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("Min_ColumnName")].ToString(), out this.unitMinValue);

                    decimal.TryParse(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("OtherMaxColumnName")].ToString(), out this.otherMaxValue);
                    decimal.TryParse(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("OtherMinColumnName")].ToString(), out this.otherMinValue);
                }
                else if (editEnterComponentlableType == (int)AreaType.PercentUnitEmpty)
                {
                    ////this.EmptyPercentLabel.Text = "Percent/Unit, Other and Size are empty";
                }
                else if (editEnterComponentlableType == (int)AreaType.PercentOnly)
                {
                    this.MainPanel1Label.Text = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("PctInpTxtColumnName")].ToString();

                    ////to set Max length to 3 when this percent text value
                    this.MainPanel1TextBox.MaxLength = 3;

                    if (this.editEnterComponentsTextValue.Count != 0)
                    {

                        this.MainPanel1TextBox.Text = this.editEnterComponentsTextValue["Percentage"].ToString();
                    }
                    else
                    {
                        this.MainPanel1TextBox.Text = string.Empty;
                    }
                   
                    
                    this.MainPanel1RangeLable.Text = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("PctMinMaxTxtColumnName")].ToString();
                }

                if (this.editEnterComponentsTextValue.Count != 0)
                {
                    this.RankTextBox.Text = this.editEnterComponentsTextValue["Quality"].ToString();
                }
                else
                {
                    this.RankTextBox.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// To check edit enter component lable.
        /// </summary>
        private void ToCheckEditEnterComponentlableType()
        {
            if ((string.Compare(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("PercentageAllowedColumnName")].ToString(), "0", true) == 0) && (string.Compare(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("UnitsAllowedColumnName")].ToString(), "0", true) == 0) && (string.Compare(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("OtherMaxColumnName")].ToString(), "0", true) == 0))
            {
                this.editEnterComponentlableType = 5;
            }
            else
            {
                if (string.Compare(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("PercentageAllowedColumnName")].ToString(), "0", true) == 0)
                {
                    if ((string.Compare(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("OtherMaxColumnName")].ToString(), "0", true) == 0))
                    {
                        this.editEnterComponentlableType = 3;
                    }
                    else
                    {
                        this.editEnterComponentlableType = 4;
                    }                   
                }
                else
                {
                    if ((string.Compare(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("UnitsAllowedColumnName")].ToString(), "0", true) == 0) && (string.Compare(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("OtherMaxColumnName")].ToString(), "0", true) == 0))
                    {
                        this.editEnterComponentlableType = 6;
                    }
                    else if ((string.Compare(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("UnitsAllowedColumnName")].ToString(), "0", true) != 0) && (string.Compare(this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Rows[0][SharedFunctions.GetResourceString("OtherMaxColumnName")].ToString(), "0", true) != 0))
                    {
                        this.editEnterComponentlableType = 2;
                    }
                    else
                    {
                        this.editEnterComponentlableType = 1;
                    }
                }
            }
        }

        /// <summary>
        /// To enable and disable area label.
        /// </summary>
        private void ToEnableAndDisableAreaLabel()
        {
            if(editEnterComponentlableType == (int)AreaType.TotalAreaLabelCountThree)
            {                
                this.AreaOrPanel1.Visible = true;
               ////this.OrPanel.Visible = true;
                this.AreaOrPanel2.Visible = true;
                this.AreaExtraPanel.Visible = true;

                this.MainPanel1.Visible = false;
                this.MainPanel2.Visible = false;

                this.EmptyPercentLabelPanel.Visible = false;
            }
            else if (editEnterComponentlableType == (int)AreaType.TotalAreaLabelCountTwo)
            {                
                this.AreaOrPanel1.Visible = true;
                ////this.OrPanel.Visible = true;
                this.AreaOrPanel2.Visible = true;
                this.AreaExtraPanel.Visible = false;

                this.MainPanel1.Visible = false;
                this.MainPanel2.Visible = false;

                this.EmptyPercentLabelPanel.Visible = false;
            }
            else if (editEnterComponentlableType == (int)AreaType.MainLableOne)
            {                
                this.AreaOrPanel1.Visible = false;
                ////this.OrPanel.Visible = false;
                this.AreaOrPanel2.Visible = false;
                this.AreaExtraPanel.Visible = false;

                this.MainPanel1.Visible = true;
                this.MainPanel2.Visible = false;

                this.EmptyPercentLabelPanel.Visible = false;
            }
            else if (editEnterComponentlableType == (int)AreaType.MainlableTwo)
            {                
                this.AreaOrPanel1.Visible = false;
                ////this.OrPanel.Visible = false;
                this.AreaOrPanel2.Visible = false;
                this.AreaExtraPanel.Visible = false;

                this.MainPanel1.Visible = true;
                this.MainPanel2.Visible = true;

                this.EmptyPercentLabelPanel.Visible = false;
            }
            else if (editEnterComponentlableType == (int)AreaType.PercentUnitEmpty)
            {
                this.AreaOrPanel1.Visible = false;
                ////this.OrPanel.Visible = false;
                this.AreaOrPanel2.Visible = false;
                this.AreaExtraPanel.Visible = false;

                this.MainPanel1.Visible = false;
                this.MainPanel2.Visible = false;

                this.EmptyPercentLabelPanel.Visible = true;
            }
            else if (editEnterComponentlableType == (int)AreaType.PercentOnly)
            {
                this.AreaOrPanel1.Visible = false;
                ////this.OrPanel.Visible = false;
                this.AreaOrPanel2.Visible = false;
                this.AreaExtraPanel.Visible = false;

                this.MainPanel1.Visible = true;
                this.MainPanel2.Visible = false;

                this.EmptyPercentLabelPanel.Visible = false;
            }
        }

        /// <summary>
        /// Validate the max min comopenet value.
        /// </summary>
        /// <returns></returns>
        private bool ValidMaxMinComopenetvalue()
        {
            bool hasValidvalues = false;

            if (editEnterComponentlableType == (int)AreaType.TotalAreaLabelCountThree)
            {
                if (!string.IsNullOrEmpty(this.AreaOrPanelRangeTextBox1.Text.Trim()) || !string.IsNullOrEmpty(this.AreaOrPanelRangeTextBox2.Text.Trim()))
                {
                    int tempAreavalue;
                    decimal tempOtherValue;

                    if (!string.IsNullOrEmpty(this.AreaOrPanelRangeTextBox1.Text.Trim()) && !string.IsNullOrEmpty(this.AreaOrPanelRangeTextBox2.Text.Trim()))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("F3601ValidationMessage1"), ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.AreaOrPanelRangeTextBox1.Text = string.Empty;
                        this.AreaOrPanelRangeTextBox2.Focus();
                        return hasValidvalues = false;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.AreaOrPanelRangeTextBox1.Text.Trim()))
                        {
                            int.TryParse(this.AreaOrPanelRangeTextBox1.Text.Trim(), out tempAreavalue);

                            if (tempAreavalue <= 100 && tempAreavalue > 0)
                            {
                                hasValidvalues = true;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("F3601ValidationMessage2"), ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.AreaOrPanelRangeTextBox1.Text = string.Empty;
                                this.AreaOrPanelRangeTextBox1.Focus();
                                return hasValidvalues = false;
                            }
                        }
                        else
                        {
                            int.TryParse(this.AreaOrPanelRangeTextBox2.Text.Trim(), out tempAreavalue);

                            if (tempAreavalue <= this.unitMaxValue && tempAreavalue >= this.unitMinValue)
                            {
                                hasValidvalues = true;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("F3601ValidationMessage3") + this.unitMinValue + " and " + this.unitMaxValue, ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.AreaOrPanelRangeTextBox2.Text = string.Empty;
                                this.AreaOrPanelRangeTextBox2.Focus();
                                return hasValidvalues = false;
                            }
                        }

                        if (!string.IsNullOrEmpty(this.AreaExtraTextBox.Text.Trim()))
                        {
                            decimal.TryParse(this.AreaExtraTextBox.Text.Trim(), out tempOtherValue);

                            if (tempOtherValue > this.otherMaxValue || tempOtherValue < this.otherMinValue)
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("F3601ValidationMessage4") + this.otherMinValue + " and " + this.otherMaxValue, ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.AreaExtraTextBox.Text = string.Empty;
                                this.AreaExtraTextBox.Focus();
                                return hasValidvalues = false;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F3601ValidationMessage5"), ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.AreaOrPanelRangeTextBox1.Focus();
                    return hasValidvalues = false;
                }
            }
            else if (editEnterComponentlableType == (int)AreaType.TotalAreaLabelCountTwo)
            {
                if (!string.IsNullOrEmpty(this.AreaOrPanelRangeTextBox1.Text.Trim()) || !string.IsNullOrEmpty(this.AreaOrPanelRangeTextBox2.Text.Trim()))
                {
                    int tempTotalAreaLabelCountTwoAreavalue;                    

                    if (!string.IsNullOrEmpty(this.AreaOrPanelRangeTextBox1.Text.Trim()) && !string.IsNullOrEmpty(this.AreaOrPanelRangeTextBox2.Text.Trim()))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("F3601ValidationMessage1"), ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.AreaOrPanelRangeTextBox1.Text = string.Empty;
                        this.AreaOrPanelRangeTextBox2.Focus();
                        return hasValidvalues = false;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(this.AreaOrPanelRangeTextBox1.Text.Trim()))
                        {
                            int.TryParse(this.AreaOrPanelRangeTextBox1.Text.Trim(), out tempTotalAreaLabelCountTwoAreavalue);

                            if (tempTotalAreaLabelCountTwoAreavalue <= 100 && tempTotalAreaLabelCountTwoAreavalue > 0)
                            {
                                hasValidvalues = true;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("F3601ValidationMessage2"), ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.AreaOrPanelRangeTextBox1.Text = string.Empty;
                                this.AreaOrPanelRangeTextBox1.Focus();
                                return hasValidvalues = false;
                            }
                        }
                        else
                        {
                            int.TryParse(this.AreaOrPanelRangeTextBox2.Text.Trim(), out tempTotalAreaLabelCountTwoAreavalue);

                            if (tempTotalAreaLabelCountTwoAreavalue <= this.unitMaxValue && tempTotalAreaLabelCountTwoAreavalue >= this.unitMinValue)
                            {
                                hasValidvalues = true;
                            }
                            else
                            {
                                MessageBox.Show(SharedFunctions.GetResourceString("F3601ValidationMessage3") + this.unitMinValue + " and " + this.unitMaxValue, ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.AreaOrPanelRangeTextBox2.Text = string.Empty;
                                this.AreaOrPanelRangeTextBox2.Focus();
                                return hasValidvalues = false;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F3601ValidationMessage5"), ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.AreaOrPanelRangeTextBox1.Focus();
                    return hasValidvalues = false;
                }
            }
            else if (editEnterComponentlableType == (int)AreaType.MainLableOne)
            {
                int tempMainLableOneUnitValue;

                if (!string.IsNullOrEmpty(this.MainPanel1TextBox.Text.Trim()))
                {
                    int.TryParse(this.MainPanel1TextBox.Text.Trim(), out tempMainLableOneUnitValue);

                    if(tempMainLableOneUnitValue <= this.unitMaxValue && tempMainLableOneUnitValue >= this.unitMinValue)                    
                    {
                        hasValidvalues = true;
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("F3601ValidationMessage3") + this.unitMinValue + " and " + this.unitMaxValue, ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                          this.MainPanel1TextBox.Text = string.Empty;
                          this.MainPanel1TextBox.Focus();
                          return hasValidvalues = false;
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F3601ValidationMessage6"), ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.MainPanel1TextBox.Text = string.Empty;
                    this.MainPanel1TextBox.Focus();
                    return hasValidvalues = false;
                }
            }
            else if (editEnterComponentlableType == (int)AreaType.MainlableTwo)
            {
                int tempMainlableTwoUnitValue;
                decimal tempMainlableTwoOtherValue;

                if (!string.IsNullOrEmpty(this.MainPanel1TextBox.Text.Trim()))
                {
                    int.TryParse(this.MainPanel1TextBox.Text.Trim(), out tempMainlableTwoUnitValue);

                    if(tempMainlableTwoUnitValue <= this.unitMaxValue && tempMainlableTwoUnitValue >= this.unitMinValue)                    
                    {
                        hasValidvalues = true;
                    }
                    else
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("F3601ValidationMessage3") + this.unitMinValue + " and " + this.unitMaxValue, ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                          this.MainPanel1TextBox.Text = string.Empty;
                          this.MainPanel1TextBox.Focus();
                          return hasValidvalues = false;
                    }

                    decimal.TryParse(this.MainPanel2TextBox.Text.Trim(), out tempMainlableTwoOtherValue);
                    if (!string.IsNullOrEmpty(this.MainPanel2TextBox.Text.Trim()))
                    {
                        if (tempMainlableTwoOtherValue > this.otherMaxValue || tempMainlableTwoOtherValue < this.otherMinValue)
                        {
                            MessageBox.Show(SharedFunctions.GetResourceString("F3601ValidationMessage7") + this.otherMinValue + " and " + this.otherMaxValue, ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.MainPanel2TextBox.Text = string.Empty;
                            this.MainPanel2TextBox.Focus();
                            return hasValidvalues = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F3601ValidationMessage6"), ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.MainPanel1TextBox.Text = string.Empty;
                    this.MainPanel1TextBox.Focus();
                    return hasValidvalues = false;
                }
            }
            else if (editEnterComponentlableType == (int)AreaType.PercentUnitEmpty)
            {
                hasValidvalues = true;
            }
            else if (editEnterComponentlableType == (int)AreaType.PercentOnly)
            {
                int tempPercentValue;

                int.TryParse(this.MainPanel1TextBox.Text.Trim(), out tempPercentValue);

                if (tempPercentValue <= 100 && tempPercentValue > 0)
                {
                    hasValidvalues = true;
                }
                else
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F3601ValidationMessage2"), ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.MainPanel1TextBox.Text = string.Empty;
                    this.MainPanel1TextBox.Focus();
                    return hasValidvalues = false;
                }
            }

            if (!string.IsNullOrEmpty(this.RankTextBox.Text.Trim()))
            {
                double tempRankValue;
                double.TryParse(this.RankTextBox.Text.Trim(), out tempRankValue);

                if (tempRankValue > 4 || tempRankValue < 0.5)
                {
                    MessageBox.Show(SharedFunctions.GetResourceString("F3601ValidationMessage8"), ConfigurationWrapper.ApplicationName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.RankTextBox.Text = string.Empty;
                    this.RankTextBox.Focus();
                    return hasValidvalues = false;
                }
            }

            return hasValidvalues;
        }

        /// <summary>
        /// To get components_id.
        /// </summary>
        private void ToGetComponentsKeyId()
        {
            try
            {
                string sectionTypeTableKeyid = string.Empty;
                string sectionTypeid = string.Empty;
                string constructionsystems_Id = string.Empty;
                string constructionsystem_Id = string.Empty;

                ////to get the components_id
                ////to get the SectionType_Id
                string findSectionType_id = "Key = '" + this.sectionTypeTableKeyid + "'";
                DataRow[] getSectionTypeid = this.componentDataset.Tables[SharedFunctions.GetResourceString("SectionTypeTableName")].Select(findSectionType_id);
                sectionTypeid = getSectionTypeid[0][SharedFunctions.GetResourceString("SectionType_IdColumnName")].ToString();

                ////to get the construction systemid
                string findconstructionSystemId = "SectionType_Id = '" + sectionTypeid + "'";
                DataRow[] getConstructionSystemId = this.componentDataset.Tables[SharedFunctions.GetResourceString("ConstructionSystemsTableName")].Select(findconstructionSystemId);
                constructionsystems_Id = getConstructionSystemId[0][SharedFunctions.GetResourceString("ConstructionSystems_IdColumnName")].ToString();

                DataTable tempConstructionSystemDatatable = this.componentDataset.Tables[SharedFunctions.GetResourceString("ConstructionSystemTableName")].Clone();
                DataTable tempConstructionSystemDatatable1 = this.componentDataset.Tables[SharedFunctions.GetResourceString("ConstructionSystemTableName")].Clone();

                string findConstructionSystemDataTable = "ConstructionSystems_Id = '" + constructionsystems_Id + "'";
                DataRow[] getConstructionSystemDataTable = this.componentDataset.Tables[SharedFunctions.GetResourceString("ConstructionSystemTableName")].Select(findConstructionSystemDataTable);

                foreach (DataRow dr2 in getConstructionSystemDataTable)
                {
                    tempConstructionSystemDatatable1.ImportRow(dr2);
                }

                tempConstructionSystemDatatable.Merge(tempConstructionSystemDatatable1);

                string findConsSystemid = "ConstructionSystemID = '" + this.constructionSystemId + "'";
                DataRow[] conssytemId = tempConstructionSystemDatatable.Select(findConsSystemid);
                constructionsystem_Id = conssytemId[0][SharedFunctions.GetResourceString("ConstructionSystem_IdColumnName")].ToString();

                string findComponentId = "Components_Id = '" + constructionsystem_Id + "'";
                DataRow[] getCompoentsId = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentsTableName")].Select(findComponentId);
                int.TryParse(getCompoentsId[0][SharedFunctions.GetResourceString("Components_IdColumnName")].ToString(), out this.componentsKeyId);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Methods

        #region Form Load Event

        /// <summary>
        /// Handles the Load event of the F3601 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F3601_Load(object sender, EventArgs e)
        {
            try
            {
                this.CancelButton = this.ComponentCloseButton;

                StringReader stringReaderBuildingDataXml = new StringReader(this.componentsDatasetXmlstring);
                XmlTextReader textReaderBuildingDataXml = new XmlTextReader(stringReaderBuildingDataXml);

                this.componentDataset.ReadXml(textReaderBuildingDataXml);
                DataTable dt = this.componentDataset.Tables["Component"].Clone();

                this.ToGetComponentsKeyId();                 
                
                string findExp = "ComponentID = '" + this.componentId + "' AND Components_Id = '" + this.componentsKeyId + "'";
                DataRow[] dr = this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Select(findExp);

                foreach (DataRow dr1 in dr)
                {
                    dt.ImportRow(dr1);
                }

                this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Clear();
                
                this.componentDataset.Tables[SharedFunctions.GetResourceString("ComponentTableName")].Merge(dt);                

                ////to get the systemDescLabelText
                string findExp1 = "ConstructionSystemID = '" + this.constructionSystemId + "'";
                DataRow[] systemDesRow = this.componentDataset.Tables[SharedFunctions.GetResourceString("ConstructionSystemTableName")].Select(findExp1);
                this.systemDescLabelText = systemDesRow[0][SharedFunctions.GetResourceString("NameColumnName")].ToString();
                
                this.ToCheckEditEnterComponentlableType();
                this.ToEnableAndDisableAreaLabel();
                this.ToAssignEditEnetComponentValues();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Form Load Event

        #region Events

        /// <summary>
        /// Handles the Click event of the ComponentCloseButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ComponentCloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the FormClosing event of the F3601 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void F3601_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ValidMaxMinComopenetvalue())
            {
                DataTable componentsDataTable = new DataTable();

                DataColumn[] componentsDataColumn = new DataColumn[] 
                { 
                    new DataColumn(SharedFunctions.GetResourceString("SystemCodeColumnName")), 
                    new DataColumn(SharedFunctions.GetResourceString("SelectedSystemColumnName")), 
                    new DataColumn(SharedFunctions.GetResourceString("ComponentColumnName")), 
                    new DataColumn(SharedFunctions.GetResourceString("F36000UnitsColumnName")), 
                    new DataColumn(SharedFunctions.GetResourceString("F36000PercentageColumnName")), 
                    new DataColumn(SharedFunctions.GetResourceString("Other1ColumnName")), 
                    new DataColumn(SharedFunctions.GetResourceString("Other2ColumnName")), 
                    new DataColumn(SharedFunctions.GetResourceString("Min_ColumnName")), 
                    new DataColumn(SharedFunctions.GetResourceString("Max_ColumnName")), 
                    new DataColumn(SharedFunctions.GetResourceString("PercentageAllowedColumnName")), 
                    new DataColumn(SharedFunctions.GetResourceString("UnitsAllowedColumnName")), 
                    new DataColumn(SharedFunctions.GetResourceString("OtherMinColumnName")), 
                    new DataColumn(SharedFunctions.GetResourceString("OtherMaxColumnName")), 
                    new DataColumn(SharedFunctions.GetResourceString("SectionTypeColumnName")),
                     new DataColumn("ComponentQualityID"), 
                    new DataColumn(SharedFunctions.GetResourceString("ConstructionSystemID")),                    
                };

                componentsDataTable.Columns.AddRange(componentsDataColumn);
                DataRow componentsDataRow = componentsDataTable.NewRow();

                componentsDataRow[SharedFunctions.GetResourceString("SystemCodeColumnName")] = this.ComponentNolabel.Text.Trim();
                componentsDataRow[SharedFunctions.GetResourceString("SelectedSystemColumnName")] = this.SystemDescLabel.Text.Trim();
                componentsDataRow[SharedFunctions.GetResourceString("ComponentColumnName")] = this.ComponentDescriptionlabel.Text.Trim();
                componentsDataRow[SharedFunctions.GetResourceString("SectionTypeColumnName")] = string.Empty;
                componentsDataRow[SharedFunctions.GetResourceString("ConstructionSystemID")] = this.constructionSystemId;

                if (editEnterComponentlableType == (int)AreaType.TotalAreaLabelCountThree)
                {
                    componentsDataRow[SharedFunctions.GetResourceString("F36000PercentageColumnName")] = this.AreaOrPanelRangeTextBox1.Text.Trim();
                    componentsDataRow[SharedFunctions.GetResourceString("F36000UnitsColumnName")] = this.AreaOrPanelRangeTextBox2.Text.Trim();
                    componentsDataRow[SharedFunctions.GetResourceString("Other1ColumnName")] = this.otherTextValue;
                    componentsDataRow[SharedFunctions.GetResourceString("Other2ColumnName")] = this.AreaExtraTextBox.Text.Trim();
                    componentsDataRow[SharedFunctions.GetResourceString("Min_ColumnName")] = this.unitMinValue;
                    componentsDataRow[SharedFunctions.GetResourceString("Max_ColumnName")] = this.unitMaxValue;
                    componentsDataRow[SharedFunctions.GetResourceString("PercentageAllowedColumnName")] = "1";
                    componentsDataRow[SharedFunctions.GetResourceString("UnitsAllowedColumnName")] = "1";
                    componentsDataRow[SharedFunctions.GetResourceString("OtherMinColumnName")] = this.otherMinValue;
                    componentsDataRow[SharedFunctions.GetResourceString("OtherMaxColumnName")] =  this.otherMaxValue;                    
                    componentsDataRow["ComponentQualityID"] = this.RankTextBox.Text.Trim();                    
                }
                else if (editEnterComponentlableType == (int)AreaType.TotalAreaLabelCountTwo)
                {
                    componentsDataRow[SharedFunctions.GetResourceString("F36000PercentageColumnName")] = this.AreaOrPanelRangeTextBox1.Text.Trim();
                    componentsDataRow[SharedFunctions.GetResourceString("F36000UnitsColumnName")] = this.AreaOrPanelRangeTextBox2.Text.Trim();
                    componentsDataRow[SharedFunctions.GetResourceString("Other1ColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("Other2ColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("Min_ColumnName")] = this.unitMinValue;
                    componentsDataRow[SharedFunctions.GetResourceString("Max_ColumnName")] = this.unitMaxValue;
                    componentsDataRow[SharedFunctions.GetResourceString("PercentageAllowedColumnName")] = "1";
                    componentsDataRow[SharedFunctions.GetResourceString("UnitsAllowedColumnName")] = "1";
                    componentsDataRow[SharedFunctions.GetResourceString("OtherMinColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("OtherMaxColumnName")] = string.Empty;                    
                    componentsDataRow["ComponentQualityID"] = this.RankTextBox.Text.Trim();                    
                }
                else if (editEnterComponentlableType == (int)AreaType.MainlableTwo)
                {
                    componentsDataRow[SharedFunctions.GetResourceString("F36000PercentageColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("F36000UnitsColumnName")] = this.MainPanel1TextBox.Text.Trim();
                    componentsDataRow[SharedFunctions.GetResourceString("Other1ColumnName")] = this.otherTextValue;
                    componentsDataRow[SharedFunctions.GetResourceString("Other2ColumnName")] = this.MainPanel2TextBox.Text.Trim();
                    componentsDataRow[SharedFunctions.GetResourceString("Min_ColumnName")] = this.unitMinValue;
                    componentsDataRow[SharedFunctions.GetResourceString("Max_ColumnName")] = this.unitMaxValue;
                    componentsDataRow[SharedFunctions.GetResourceString("PercentageAllowedColumnName")] = "0";
                    componentsDataRow[SharedFunctions.GetResourceString("UnitsAllowedColumnName")] = "1";
                    componentsDataRow[SharedFunctions.GetResourceString("OtherMinColumnName")] = this.otherMinValue;
                    componentsDataRow[SharedFunctions.GetResourceString("OtherMaxColumnName")] = this.otherMaxValue;                    
                    componentsDataRow["ComponentQualityID"] = this.RankTextBox.Text.Trim();                    
                }
                else if (editEnterComponentlableType == (int)AreaType.MainLableOne)
                {
                    componentsDataRow[SharedFunctions.GetResourceString("F36000PercentageColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("F36000UnitsColumnName")] = this.MainPanel1TextBox.Text.Trim();
                    componentsDataRow[SharedFunctions.GetResourceString("Other1ColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("Other2ColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("Min_ColumnName")] = this.unitMinValue;
                    componentsDataRow[SharedFunctions.GetResourceString("Max_ColumnName")] = this.unitMaxValue;
                    componentsDataRow[SharedFunctions.GetResourceString("PercentageAllowedColumnName")] = "0";
                    componentsDataRow[SharedFunctions.GetResourceString("UnitsAllowedColumnName")] = "1";
                    componentsDataRow[SharedFunctions.GetResourceString("OtherMinColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("OtherMaxColumnName")] = string.Empty;                    
                    componentsDataRow["ComponentQualityID"] = this.RankTextBox.Text.Trim();                    
                }
                else if (editEnterComponentlableType == (int)AreaType.PercentUnitEmpty)
                {
                    componentsDataRow[SharedFunctions.GetResourceString("F36000PercentageColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("F36000UnitsColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("Other1ColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("Other2ColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("Min_ColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("Max_ColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("PercentageAllowedColumnName")] = "0";
                    componentsDataRow[SharedFunctions.GetResourceString("UnitsAllowedColumnName")] = "0";
                    componentsDataRow[SharedFunctions.GetResourceString("OtherMinColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("OtherMaxColumnName")] = string.Empty;
                    componentsDataRow["ComponentQualityID"] = this.RankTextBox.Text.Trim();                   
                }
                else if (editEnterComponentlableType == (int)AreaType.PercentOnly)
                {
                    componentsDataRow[SharedFunctions.GetResourceString("F36000PercentageColumnName")] = this.MainPanel1TextBox.Text.Trim();
                    componentsDataRow[SharedFunctions.GetResourceString("F36000UnitsColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("Other1ColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("Other2ColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("Min_ColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("Max_ColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("PercentageAllowedColumnName")] = "1";
                    componentsDataRow[SharedFunctions.GetResourceString("UnitsAllowedColumnName")] = "0";
                    componentsDataRow[SharedFunctions.GetResourceString("OtherMinColumnName")] = string.Empty;
                    componentsDataRow[SharedFunctions.GetResourceString("OtherMaxColumnName")] = string.Empty;                    
                    componentsDataRow["ComponentQualityID"] = this.RankTextBox.Text.Trim();                    
                }

                componentsDataTable.Rows.Add(componentsDataRow);

                this.editEnterComponentsFormXmlValue = TerraScanCommon.GetXmlString(componentsDataTable);

                DialogResult = DialogResult.OK;
                e.Cancel = false;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
                e.Cancel = true;
            }
        }

        #endregion Events
    }
}