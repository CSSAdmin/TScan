//--------------------------------------------------------------------------------------------
// <copyright file="ExciseTaxStatementHeader.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the AttachmentForm.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 
//*********************************************************************************/

namespace D1100
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using Microsoft.Practices.CompositeUI.WinForms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    
    /// <summary>
    /// ExciseTaxStatementHeader class file
    /// </summary>
    [SmartPart]
    public partial class ExciseTaxStatementHeader : UserControl
    {
        /// <summary>
        /// class for F1100 controller
        /// </summary>
        private F1100Controller form1100Control;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ExciseTaxStatementHeader"/> class.
        /// </summary>
        public ExciseTaxStatementHeader()
        {
            this.InitializeComponent();
            this.StatementPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(this.StatementPictureBox.Height, this.StatementPictureBox.Width, "Statement",153,153,153);
        }

        /// <summary>
        /// Gets or sets the F1100 control.
        /// </summary>
        /// <value>The F1100 control.</value>
        [CreateNew]
        public F1100Controller F1100Control
        {
            get { return this.form1100Control as F1100Controller; }
            set { this.form1100Control = value; }
        }

        ////private void jj()
        ////{
        ////    exsiceTaxStatementHeaderDataSet = f1100Control.WorkItem.GetExciseTaxStatement(101);
        ////}

        /// <summary>
        /// Handles the Load event of the ExciseTaxStatementHeader control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void ExciseTaxStatementHeader_Load(object sender, EventArgs e)
        {
            ////exsiceTaxStatementHeaderDataSet = f1100Control.WorkItem.GetExciseTaxStatement(221);
            ////if (this.exsiceTaxStatementHeaderDataSet != null)
            ////{
            ////    if (this.exsiceTaxStatementHeaderDataSet.Tables.Count > 0)
            ////    {
            ////        if (this.exsiceTaxStatementHeaderDataSet.Tables[0].Rows.Count > 0)
            ////        {
            ////            this.StatementIDTextBox.Text = exsiceTaxStatementHeaderDataSet.Tables[0].Rows[0]["StatementId"].ToString();
            ////            this.ParcelNumberTextBox.Text = exsiceTaxStatementHeaderDataSet.Tables[0].Rows[0]["ParcelNumber"].ToString();
            ////            this.SaleDateTextBox.Text = exsiceTaxStatementHeaderDataSet.Tables[0].Rows[0]["DocDate"].ToString();
            ////            this.PaymentDateTextBox.Text = exsiceTaxStatementHeaderDataSet.Tables[0].Rows[0]["PmtDate"].ToString();
            ////            this.FromDateTextBox.Text = exsiceTaxStatementHeaderDataSet.Tables[0].Rows[0]["FormDate"].ToString();
            ////            this.MobileHomeTextBox.Text = exsiceTaxStatementHeaderDataSet.Tables[0].Rows[0]["IsMObileHome"].ToString();
            ////            this.ReceiptNumberLinkLabel.Text = exsiceTaxStatementHeaderDataSet.Tables[0].Rows[0]["ReceiptNumber"].ToString();
            ////           this.DistrictLinkLabel.Text = exsiceTaxStatementHeaderDataSet.Tables[0].Rows[0]["District"].ToString();
            ////            this.SaleAmountTextBox.Text = exsiceTaxStatementHeaderDataSet.Tables[0].Rows[0]["TaxableSalePrice"].ToString();
            ////            this.TaxCodeTextBox.Text = exsiceTaxStatementHeaderDataSet.Tables[0].Rows[0]["IsExempt"].ToString();
            ////            this.GranteeLinkLabel.Text = exsiceTaxStatementHeaderDataSet.Tables[0].Rows[0]["Grantor"].ToString();
            ////            this.GrantorLinkLabel.Text = exsiceTaxStatementHeaderDataSet.Tables[0].Rows[0]["Grantee"].ToString();
            ////            //this.StatementIDTextBox.Text = exsiceTaxStatementHeaderDataSet.Tables[0].Rows[0]["PPaymentID"].ToString();
            ////            //this.jj();
            ////        }
            ////        else
            ////        {
            ////            MessageBox.Show("No data found");
            ////        }
            ////    }
            ////}
        }
    }
}
