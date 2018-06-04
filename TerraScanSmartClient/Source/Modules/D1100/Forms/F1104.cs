//--------------------------------------------------------------------------------------------
// <copyright file="F1104.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the 1016NextNumberForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 20 July 06       M.VIJAYAKUMAR       Created
// 
//*********************************************************************************/

namespace D1100
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.BusinessEntities;
    using TerraScan.SmartParts;
    using TerraScan.Utilities;
    using TerraScan.Common;

    /// <summary>
    /// Excise District Copy
    /// </summary>
    public partial class F1104 : Form
    {
        #region Variables

        /// <summary>
        /// Excise Id 
        /// </summary>
        private const int ExciserateId = 1;

        /// <summary>
        /// Gets the return value from the DB
        /// 0 = When The record is successfully saved.
        /// 1 = When Invalid Source Record
        /// 2 = When Invalid Destination Record
        /// </summary>
        private int saveExciseDistrict;

        /// <summary>
        /// controller F1104
        /// </summary>
        private F1104Controller form1104Control;               

        /// <summary>
        /// ExciseDistrict Copy Instances
        /// </summary>
        private ExciseDistrictCopyData exciseDistrictCopyData = new ExciseDistrictCopyData();      

        #endregion Variables

        #region Constructor

        /// <summary>
        /// Initializes Excise District Copy
        /// </summary>
        public F1104()
        {
            this.InitializeComponent();
            this.CancelButton = this.CancelExciseDistrictButton;
        }

        #endregion

        #region Properties

        /// <summary>
        /// For F1104Control
        /// </summary>
        [CreateNew]
        public F1104Controller Form1104Control
        {
            get { return this.form1104Control as F1104Controller; }
            set { this.form1104Control = value; }
        }       

        #endregion Properties        

        #region Events

        /// <summary>
        /// F1104 Load
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event</param>
        private void F1104_Load(object sender, EventArgs e)
        {
            this.ClearDistrictCopyDetails();
            this.exciseDistrictCopyData = this.Form1104Control.WorkItem.GetExciseDistrictCopy(ExciserateId);
            if (this.exciseDistrictCopyData.GetExciseRateDistrict.Rows.Count > 0)
            {
                this.DistrictTextBox.Text = this.exciseDistrictCopyData.GetExciseRateDistrict.Rows[0]["District"].ToString();
                this.BasedYearTextBox.Text = this.exciseDistrictCopyData.GetExciseRateDistrict.Rows[0]["BaseYear"].ToString();
                this.NewDistrictTextBox.Focus();
            }
            else
            {
                this.DistrictTextBox.Text = string.Empty;
                this.BasedYearTextBox.Text = string.Empty;
                this.DistrictTextBox.Focus();
            }
        }

        /// <summary>
        /// Click of Create Button
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">Event</param>
        private void CreateExciseDistrictButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DistrictTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.BasedYearTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.NewDistrictTextBox.Text.Trim()))
            {
                this.saveExciseDistrict = this.Form1104Control.WorkItem.SaveExciseDistrictCopy(Convert.ToInt32(this.DistrictTextBox.Text.Trim()), Convert.ToInt32(this.BasedYearTextBox.Text.Trim()), Convert.ToInt32(this.NewDistrictTextBox.Text.Trim()), TerraScanCommon.UserId); ;

                if (this.saveExciseDistrict == 0)
                {
                    // When Recor is Successfully Saved                    
                    MessageBox.Show(SharedFunctions.GetResourceString("DistrictCopySaved") + this.DistrictTextBox.Text + SharedFunctions.GetResourceString("DistrictCopySavedTwo") + this.NewDistrictTextBox.Text + ".", SharedFunctions.GetResourceString("DistrictCopySavedThree"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (this.saveExciseDistrict == 1)
                {
                    // When Invalid Source Record 
                    MessageBox.Show(SharedFunctions.GetResourceString("DistrictCopyInvalidRec") + this.DistrictTextBox.Text + SharedFunctions.GetResourceString("DistrictCopyInvalidRecOne") + this.BasedYearTextBox.Text + SharedFunctions.GetResourceString("DistrictCopyInvalidRecTwo"), SharedFunctions.GetResourceString("DistrictCopyInvalidRecThree"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (this.saveExciseDistrict == 2)
                {
                    // When Invalid Destination Record 
                    MessageBox.Show(SharedFunctions.GetResourceString("DistrictCopyInvalidDes") + this.DistrictTextBox.Text + SharedFunctions.GetResourceString("DistrictCopyInvalidDesTwo") + this.NewDistrictTextBox.Text + ".", SharedFunctions.GetResourceString("DistrictCopyInvalidDesThree"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {   // When Values are empty
                // MessageBox.Show("Fill All The Values.", "Enter Values", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(SharedFunctions.GetResourceString("RequiredFieldMissing"), SharedFunctions.GetResourceString("RequiredFieldMissingTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Events       

        #region Methods

        /// <summary>
        /// Clear The District Copy
        /// </summary>
        private void ClearDistrictCopyDetails()
        {
            this.DistrictTextBox.Text = string.Empty;
            this.BasedYearTextBox.Text = string.Empty;
            this.NewDistrictTextBox.Text = string.Empty;
        }              

        #endregion Methods        
    }
}