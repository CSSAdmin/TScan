//--------------------------------------------------------------------------------------------
// <copyright file="StatusBarSmartPart.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the ToolBoxSmartPart.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// ---------        ---------       Created

//*********************************************************************************/

namespace TerraScan.SmartParts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.Common;

    /// <summary>
    /// StatusBarSmartPart
    /// </summary>
    [SmartPart]
    public partial class StatusBarSmartPart : PrimaryBaseSmartPart
    {
        #region Private Variables    
     
        /// <summary>
        /// variable Holds the statusBarEntity
        /// </summary>
        private StatusBarEntity statusBarEntity = new StatusBarEntity();          

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="T:StatusBarSmartPart"/> class.
        /// </summary>
        public StatusBarSmartPart()
        {
            this.InitializeComponent();
        }   

        #region Publications and Subcriptions

        /// <summary>
        /// FilteredButtonClick Event
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_StatusBarSmartPart_FilteredButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> FilteredButtonClick;

        /// <summary>
        /// DelinquentButtonClick Event
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_StatusBarSmartPart_DelinquentButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<Button>> DelinquentButtonClick;

        /// <summary>
        /// AutoPrintOnButtonClick Event
        /// </summary>
        [EventPublication(EventTopics.D9001_TerrascanSmartParts_StatusBarSmartPart_AutoPrintOnButtonClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<bool>> AutoPrintOnButtonClick;

        #endregion Publications and Subcriptions

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether [filter status].
        /// </summary>
        /// <value><c>true</c> if [filter status]; otherwise, <c>false</c>.</value>
        public bool FilteredButtonFilterStatus
        {
            get 
            {
                return this.FilteredButton.FilterStatus; 
            }

            set 
            {
                this.FilteredButton.FilterStatus = value; 
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [delinquent button status].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [delinquent button status]; otherwise, <c>false</c>.
        /// </value>
        public bool DelinquentButtonStatus
        {
            get
            {
                return this.DelinquentButton.StatusIndicator;
            }

            set
            {
                this.DelinquentButton.StatusIndicator = value;
            }
        }

        /// <summary>
        /// Gets or sets the status bar entity.
        /// </summary>
        /// <value>The status bar entity.</value>
        public StatusBarEntity StatusBarEntity
        {
            get { return this.statusBarEntity; }
            set { this.statusBarEntity = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable filtered button].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [enable filtered button]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableFilteredButton
        {
            get
            {
                return this.FilteredButton.Enabled;
            }

            set
            {
                this.FilteredButton.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable delinquent button].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [enable delinquent button]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableDelinquentButton
        {
            get
            {
                return this.DelinquentButton.Enabled;
            }

            set
            {
                this.DelinquentButton.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable auto print button].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [enable auto print button]; otherwise, <c>false</c>.
        /// </value>
        public bool EnableAutoPrintButton
        {
            get
            {
                return this.AutoPrintOnButton.Enabled;
            }

            set
            {
                this.AutoPrintOnButton.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [visible filtered button].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [visible filtered button]; otherwise, <c>false</c>.
        /// </value>
        public bool VisibleFilteredButton
        {
            get
            {
                return this.FilteredButton.Visible;
            }

            set
            {
                this.FilteredButton.Visible = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable delinquent button].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [enable delinquent button]; otherwise, <c>false</c>.
        /// </value>
        public bool VisibleDelinquentButton
        {
            get
            {
                return this.DelinquentButton.Visible;
            }

            set
            {
                this.DelinquentButton.Visible = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [enable auto print button].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [enable auto print button]; otherwise, <c>false</c>.
        /// </value>
        public bool VisibleAutoPrintButton
        {
            get
            {
                return this.AutoPrintOnButton.Visible;
            }

            set
            {
                this.AutoPrintOnButton.Visible = value;
            }
        }       

        #endregion

        /// <summary>
        /// Sets the auto print.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D1100_F1100_ExciseTaxSmartPart_SetAutoPrint, Thread = ThreadOption.UserInterface)]
        public void SetAutoPrint(object sender, DataEventArgs<bool> e)
        {
            this.AutoPrintOnButton.EnableAutoPrint = e.Data;
        }

        /// <summary>
        /// Shows the requery form on Filtered Button Click.
        /// </summary>
        public void ShowRequeryForm()
        {
            ////shows form 
            if (this.statusBarEntity.CalledForm == null)
            {
                ////9052 - requery form
                this.statusBarEntity.CalledForm = TerraScanCommon.GetForm(9052, this.statusBarEntity.OptionalInputParameter, this.statusBarEntity.ParentWorkItem);
            }

            if (this.statusBarEntity.CalledForm != null && this.statusBarEntity.CalledForm.ShowDialog() == DialogResult.Yes)
            {
                this.statusBarEntity.CalledFormStatus = true;
                this.statusBarEntity.WhereCondition = TerraScanCommon.GetValue(this.statusBarEntity.CalledForm, "CurrentQueryWhereCondition");
                this.statusBarEntity.UserDefinedWhereCondition = TerraScanCommon.GetValue(this.statusBarEntity.CalledForm, "UserDefinedWhereCondition");
            }
            else
            {
                this.statusBarEntity.CalledFormStatus = false;
                this.statusBarEntity.CalledForm = null;
            }
        }

        /// <summary>
        /// Handles the Click event of the FilteredButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FilteredButton_Click(object sender, EventArgs e)
        {
            try
            {
                ////reinitialize statusBarEntity
                this.statusBarEntity = new StatusBarEntity();
                this.FilteredButtonClick(this, new DataEventArgs<Button>(sender as Button));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }      

        /// <summary>
        /// Handles the Click event of the DelinquentButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DelinquentButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.DelinquentButtonClick(this, new DataEventArgs<Button>(sender as Button));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the AutoPrintOnButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void AutoPrintOnButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.AutoPrintOnButton.EnableAutoPrint)
                {
                    this.AutoPrintOnButton.EnableAutoPrint = false;
                }
                else
                {
                    this.AutoPrintOnButton.EnableAutoPrint = true;
                }

                this.AutoPrintOnButtonClick(this, new DataEventArgs<bool>(this.AutoPrintOnButton.EnableAutoPrint));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }   
    }
}
