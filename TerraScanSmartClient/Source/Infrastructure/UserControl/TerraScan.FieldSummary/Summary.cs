//--------------------------------------------------------------------------------------------
// <copyright file="SummaryUserControl.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Summary Tab Load.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20120705           Manoj Kumar P        	    Created
//-----------------------------------------------------------------------------------------------



namespace TerraScan.FieldSummary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.Helper;
    using TerraScan.Utilities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.ObjectBuilder;
    using System.Collections;
    using System.Threading;

    /// <summary>
    /// Usercontrol for Summary User Control
    /// </summary>
    public partial class SummaryUserControl : UserControl
    {
        private DataTable SummarySet;
        
        #region properities
        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public DataTable SummaryDataSet
        {
            get
            {
                return this.SummarySet;
            }
            set
            {
                this.SummarySet = value;
            }
        }
        #endregion properities

        public SummaryUserControl()
        {
            InitializeComponent();
        }

        private void ExemptionTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SummaryUserControl_Load(object sender, EventArgs e)
        {
            if (this.SummarySet.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["PMRollYear"].ToString()))
                {
                    string rollYear = this.SummarySet.Rows[0]["PMRollYear"].ToString();
                    rollYear=rollYear.Remove(0, 2);
                  this.PreviousMarketlabel.Text = '\''+rollYear+" Market";
                }
                else
                {
                    this.PreviousMarketlabel.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["PARollYear"].ToString()))
                {
                    string rollYear = this.SummarySet.Rows[0]["PARollYear"].ToString();
                    rollYear = rollYear.Remove(0, 2);
                   this.PreviousAssessedLabel.Text = '\'' + rollYear + " Assessed";
                }
                else
                {
                    this.PreviousAssessedLabel.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["newConstYear"].ToString()))
                {
                    string rollYear = this.SummarySet.Rows[0]["newConstYear"].ToString();
                    rollYear = rollYear.Remove(0, 2);
                   this.constantLabel.Text = '\'' + rollYear + " New Const";
                }
                else
                {
                    this.constantLabel.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["MRollYear"].ToString()))
                {
                    string rollYear = this.SummarySet.Rows[0]["MRollYear"].ToString();
                    rollYear = rollYear.Remove(0, 2);
                    this.currentMarketLabel.Text = '\'' + rollYear + " Market";
                }
                else
                {
                    this.currentMarketLabel.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["AMarket"].ToString()))
                {
                    string rollYear = this.SummarySet.Rows[0]["AMarket"].ToString();
                    rollYear = rollYear.Remove(0, 2);
                    this.CurrentAssessedLabel.Text = '\'' + rollYear + " Assessed";
                }
                else
                {
                   this.CurrentAssessedLabel.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["TotalPM"].ToString()))
                {
                    string totalPM= this.SummarySet.Rows[0]["TotalPM"].ToString();
                    string [] ss= totalPM.Split('.');
                    int totconst;
                    int.TryParse(ss[0], out totconst);
                    var currencyformat = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencySymbol;
                    currencyformat = "##,##0";
                    string val = totconst.ToString(currencyformat);
                    this.totalPreviousMarketLabel.Text = val; 
                }
                else
                {
                    this.totalPreviousMarketLabel.Text = string.Empty; 
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["TotalsPA"].ToString()))
                {
                    string TotalsPA = this.SummarySet.Rows[0]["TotalsPA"].ToString();
                    string[] ss = TotalsPA.Split('.');
                    int totconst;
                    int.TryParse(ss[0], out totconst);
                    var currencyformat = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencySymbol;
                    currencyformat = "##,##0";
                    string val = totconst.ToString(currencyformat);
                   this.totalPreviousAssessedLabel.Text = val; 
                    
                }
                else
                {
                    this.totalPreviousAssessedLabel.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["NewConstTot"].ToString()))
                {
                    string NewConstTot = this.SummarySet.Rows[0]["NewConstTot"].ToString();
                    string[] ss = NewConstTot.Split('.');
                    int totconst;
                    int.TryParse(ss[0],out totconst);
                    var currencyformat = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencySymbol;
                    currencyformat = "##,##0";
                    string val = totconst.ToString(currencyformat);
                    this.Totalconstlabel.Text = val;
                }
                else
                {
                   this.Totalconstlabel.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["TotalsM"].ToString()))
                {
                    string TotalsM = this.SummarySet.Rows[0]["TotalsM"].ToString();
                    string[] ss = TotalsM.Split('.');
                    int totconst;
                    int.TryParse(ss[0], out totconst);
                    var currencyformat = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencySymbol;
                    currencyformat = "##,##0";
                    string val = totconst.ToString(currencyformat);
                   this.TotalCurrentMarketlabel.Text = val; 
                    
                }
                else
                {
                    this.TotalCurrentMarketlabel.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["TotalsA"].ToString()))
                {
                    string LandAssses = this.SummarySet.Rows[0]["TotalsA"].ToString();
                    string[] ss = LandAssses.Split('.');
                    int totconst;
                    int.TryParse(ss[0], out totconst);
                    var currencyformat = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencySymbol;
                    currencyformat = "##,##0";
                    string val = totconst.ToString(currencyformat);
                    this.totalCurrentAssessedLabel.Text = val;

                }
                else
                {
                    this.totalCurrentAssessedLabel.Text = string.Empty;
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["FrozenValue"].ToString()))
                {
                    this.frozenTextBox.Text = this.SummarySet.Rows[0]["FrozenValue"].ToString();
                }
                else
                {
                    this.frozenTextBox.Text = string.Empty; 
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["RegularLevyValue"].ToString()))
                {
                this.RegularLevyTextBox.Text = this.SummarySet.Rows[0]["RegularLevyValue"].ToString();
                }
                else
                {
                    this.RegularLevyTextBox.Text = string.Empty; 
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["ExcessLevyValue"].ToString()))
                {
                this.ExcessLevyTextBox.Text = this.SummarySet.Rows[0]["ExcessLevyValue"].ToString();
                }
                else
                {
                    this.ExcessLevyTextBox.Text = string.Empty; 
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["Exemptions"].ToString()))
                {
                this.ExemptionTextBox.Text = this.SummarySet.Rows[0]["Exemptions"].ToString();
                }
                else
                {
                    this.ExemptionTextBox.Text = string.Empty; 
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["LandPM"].ToString()))
                {
                this.PreviousMarketLandTextBox.Text = this.SummarySet.Rows[0]["LandPM"].ToString();
                }
                else
                {
                    this.PreviousMarketLandTextBox.Text = string.Empty; 
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["ImprovementsPM"].ToString()))
                {
                this.PreviousMarketImprTextBox.Text = this.SummarySet.Rows[0]["ImprovementsPM"].ToString();
                 }
                else
                {
                    this.PreviousMarketImprTextBox.Text = string.Empty; 
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["PermanentCropsPM"].ToString()))
                {
                this.PreviousMarketPermCropTextBox.Text = this.SummarySet.Rows[0]["PermanentCropsPM"].ToString();
                 }
                else
                {
                    this.PreviousMarketPermCropTextBox.Text = string.Empty; 
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["PermanentCropsPA"].ToString()))
                {
                this.PreviousAssessedPermCropTextBox.Text = this.SummarySet.Rows[0]["PermanentCropsPA"].ToString();
                                            }
                else
                {
                    this.PreviousAssessedPermCropTextBox.Text = string.Empty; 
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["LandPA"].ToString()))
                {
                this.PreviousAssessedLandTextBox.Text = this.SummarySet.Rows[0]["LandPA"].ToString();
                                                }
                else
                {
                    this.PreviousAssessedLandTextBox.Text = string.Empty; 
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["ImprovementsPA"].ToString()))
                {
                this.PreviousAssessedImprTextBox.Text = this.SummarySet.Rows[0]["ImprovementsPA"].ToString();
                                                    }
                else
                {
                    this.PreviousAssessedImprTextBox.Text = string.Empty; 
                }
               if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["newConstImpr"].ToString()))
                {
                this.ConstImprTextBox.Text = this.SummarySet.Rows[0]["newConstImpr"].ToString();
                }
                else
                {
                    this.ConstImprTextBox.Text = string.Empty; 
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["newConstLand"].ToString()))
                {
                this.constLandTextBox.Text = this.SummarySet.Rows[0]["newConstLand"].ToString();
                }
                else
                {
                    this.constLandTextBox.Text = string.Empty; 
                }
               if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["newConstCrop"].ToString()))
                {
                this.ConstPermCropTextBox.Text = this.SummarySet.Rows[0]["newConstCrop"].ToString();
                 }
                else
                {
                    this.ConstPermCropTextBox.Text = string.Empty; 
                }
               if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["ImprovementsA"].ToString()))
                {
                this.CurrentAssessedImprTextBox.Text = this.SummarySet.Rows[0]["ImprovementsA"].ToString();
                 }
                else
                {
                    this.CurrentAssessedImprTextBox.Text = string.Empty; 
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["LandA"].ToString()))
                {
                this.CurrentAssessedLandTextBox.Text = this.SummarySet.Rows[0]["LandA"].ToString();
                }
                else
                {
                    this.CurrentAssessedLandTextBox.Text = string.Empty; 
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["PermanentCropsA"].ToString()))
                {
                this.CurrentAssessedPermCropTextBox.Text = this.SummarySet.Rows[0]["PermanentCropsA"].ToString();
                 }
                else
                {
                    this.CurrentAssessedPermCropTextBox.Text = string.Empty; 
                }
                 if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["ImprovementsM"].ToString()))
                {
                this.CurrentMarketImprTextBox.Text = this.SummarySet.Rows[0]["ImprovementsM"].ToString();
                }
                else
                {
                    this.CurrentMarketImprTextBox.Text = string.Empty; 
                }
               if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["LandM"].ToString()))
                {
                this.CurrentMarketLandTextBox.Text =this.SummarySet.Rows[0]["LandM"].ToString();
                }
               else
                {
                    this.CurrentMarketLandTextBox.Text = string.Empty; 
                }
                if (!string.IsNullOrEmpty(this.SummarySet.Rows[0]["PermanentCropsM"].ToString()))
                {
                this.CurrentMarketPermCropTextBox.Text =this.SummarySet.Rows[0]["PermanentCropsM"].ToString();
                }
                else
                {
                    this.CurrentMarketPermCropTextBox.Text = string.Empty;
                }

            }
        }
    }
}
