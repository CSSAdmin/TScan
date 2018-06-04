//--------------------------------------------------------------------------------------------
// <copyright file="printform.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Login.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
//                              	    
// 24 Sept 06       Dinesh        
//*********************************************************************************/

namespace D9005
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing.Printing;
    using MODI;
    using TerraScan.Common;

    /// <summary>
    /// Prinbt Form
    /// </summary>
    public partial class Printform : Form
    {
        #region Variable

        /// <summary>
        /// endpage
        /// </summary>
        private int endpage;
        
        /// <summary>
        /// MODI Document Object
        /// </summary>
        private MODI.Document modidoc = new Document();

        #endregion

        #region Methods

        /// <summary>
        /// Print Form Constructor
        /// </summary>
        /// <param name="epage">end page</param>
        /// <param name="midoc">modi document object</param>
        public Printform(int epage, MODI.Document midoc)
        {
            this.InitializeComponent();
            this.endpage = epage;
            this.modidoc = midoc;
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the Load event of the Printform control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Printform_Load(object sender, EventArgs e)
        {
            try
            {
                PrintDocument prtdoc = new PrintDocument();
                string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;

                foreach (String strPrinter in PrinterSettings.InstalledPrinters)
                {
                    this.printercombobox.Items.Add(strPrinter);
                    if (strPrinter == strDefaultPrinter)
                    {
                        this.printercombobox.SelectedIndex = this.printercombobox.Items.IndexOf(strPrinter);
                    }
                }

                this.frompagetextbox.Text = "0";
                this.topagetextbox.Text = this.endpage.ToString();
                for (int j = 1; j < 10000; j++)
                {
                    this.domainUpDown1.Items.Add(j);
                }

                this.domainUpDown1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the SelectedItemChanged event of the DomainUpDown1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DomainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the Okbutton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Okbutton_Click(object sender, EventArgs e)
        {
            try
            {
                int intNumOfCopies = (int)this.domainUpDown1.SelectedItem;
                if (intNumOfCopies > 0)
                {
                    for (int k = 1; k <= intNumOfCopies; k++)
                    {
                        this.modidoc.PrintOut(Convert.ToInt32(this.frompagetextbox.Text), Convert.ToInt32(this.topagetextbox.Text), 1, this.printercombobox.SelectedItem.ToString(), "", true, MiPRINT_FITMODES.miPRINT_PAGE);
                    }

                    this.modidoc.Close(false);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        #endregion
    }
}