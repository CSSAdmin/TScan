//--------------------------------------------------------------------------------------------
// <copyright file="F2010.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the State-Code selection.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 18/12/2007       KUPPUSAMY.B	        Created
// 17/06/2009       Biju I.G.           Implemented the CO 1212
//*********************************************************************************/

namespace D20000
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

    #endregion namespace

    /// <summary>
    /// public class F2010
    /// </summary>
    public partial class F2010 : Form
    {
        #region Variable

        /// <summary>
        /// keyId
        /// </summary>
        private int keyId;        

        /// <summary>
        /// Form2015Controller
        /// </summary>
        private F2010Controller form2010Control;

        /// <summary>
        /// Instance for the F2010StateCodeSelectionData
        /// </summary>
        private F2010StateCodeSelectionData stateCodeSelectionData = new F2010StateCodeSelectionData();

        /// <summary>
        /// Instance for the F2010_ListStateCodeDataTable
        /// </summary>
        private F2010StateCodeSelectionData.F2010_ListStateCodeDataTable listStateCodeDataTable = new F2010StateCodeSelectionData.F2010_ListStateCodeDataTable();

        /// <summary>
        /// pageLoadStatus variable is used to find the pageLoadStatus. 
        /// </summary>   
        private bool pageLoadStatus;

        /// <summary>
        /// int for the returning stateCodeid
        /// </summary>
        private int returnStateCode;

        /// <summary>
        /// string for the returning StateCodeName
        /// </summary>
        private string returnStateCodeName;

        /// <summary>
        /// string for the displaying text
        /// </summary>
        private string stateCodeText;

        #endregion Variable

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="F2010"/> class.
        /// </summary>
        public F2010()
        {
           this.InitializeComponent();          
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="F2010"/> class.
        /// </summary>
        /// <param name="stateCodeText">The state code text.</param>
        public F2010(string stateCodeText)
        {
            this.InitializeComponent();
            if (stateCodeText.Contains("&&"))
            {
               // string statecode = string.Empty;
                stateCodeText = stateCodeText.Replace("&&", "&"); 
            }
            this.stateCodeText = stateCodeText;
        }
        
        #endregion Constructor

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the F2010 control.
        /// </summary>
        /// <value>The F2010 control.</value>
        [CreateNew]
        public F2010Controller F2010Control
        {
            get { return this.form2010Control as F2010Controller; }
            set { this.form2010Control = value; }
        }

        /// <summary>
        /// Gets or sets the state code return value.
        /// </summary>
        /// <value>The state code return value.</value>
        public int StateCodeReturnValue
        {
            get { return this.returnStateCode; }
            set { this.returnStateCode = value; }
        }

        /// <summary>
        /// Gets or sets the state code name return value.
        /// </summary>
        /// <value>The state code name return value.</value>
        public string StateCodeNameReturnValue
        {
            get { return this.returnStateCodeName; }
            set { this.returnStateCodeName = value; }
        }
        #endregion Property        

        #region Events

        /// <summary>
        /// Handles the Load event of the F2010 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F2010_Load(object sender, EventArgs e)
        {
            try
            {                
                this.CancelButton = this.StateCodeCancelButton;
                /*set pageLoadStatus - suppress textchanged event*/
                this.pageLoadStatus = true;
                /*load combobox*/
                this.LoadStateCodeComboBox();  
                SendKeys.Send("{HOME}");
                this.StateCodeAcceptButton.Enabled = false;
                /*reset pageLoadStatus - trigger textchanged event*/
                this.pageLoadStatus = false;
                /*To implement the CO 1212, the hyperlink is made visible false*/
                this.StateCodeManagementLinkLabel.Visible = false;
            }
            catch (SoapException exc)
            {
                ExceptionManager.ManageException(exc, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the StateCodeAcceptButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void StateCodeAcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                string errorMessage = string.Empty;
                string statecodeText;

                /*check the selectedvalue of the combobox*/
                if (this.StateCodeComboBox.SelectedValue != null)
                {
                    int.TryParse(this.StateCodeComboBox.SelectedValue.ToString(), out this.keyId);
                    statecodeText = this.StateCodeComboBox.Text;
                    if (this.keyId > 0)
                    {
                        /*assign the keyid to pass it to the calling form*/
                        this.returnStateCode = this.keyId;
                        this.returnStateCodeName = statecodeText;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        ////errorMessage = "The entered code is not Valid";
                        ////MessageBox.Show(errorMessage, "TerraScan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.StateCodeComboBox.Focus();
                    }
                }
                else
                {
                    errorMessage="Select a State Code.";
                    MessageBox.Show(errorMessage, "TerraScan T2", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.StateCodeComboBox.Focus();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }       

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the StateCodeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void StateCodeComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                SendKeys.Send("{HOME}");
                /*Enables the Accept button*/
                if (!this.pageLoadStatus && !this.StateCodeAcceptButton.Enabled)
                {                   
                    this.StateCodeAcceptButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the StateCodeComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void StateCodeComboBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                /*Enables the Accept button*/
                if (!this.pageLoadStatus) // && !this.StateCodeAcceptButton.Enabled)
                {
                    //// this.StateCodeAcceptButton.Enabled = true;
                    if (string.IsNullOrEmpty(this.StateCodeComboBox.Text))
                    {
                        this.StateCodeAcceptButton.Enabled = false;
                    }
                    else
                    {
                        this.StateCodeAcceptButton.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the StateCodeManagementLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void StateCodeManagementLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                /*Calls the Form 20004-Statecode Management*/
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(20004);
                formInfo.optionalParameters = new object[1];
                formInfo.optionalParameters[0] = this.keyId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        #endregion Events
       
        #region Methods

        /// <summary>
        /// Loads the state code combo box.
        /// </summary>
        private void LoadStateCodeComboBox()
        {            
            /*binding with the datasource*/
            this.stateCodeSelectionData = this.form2010Control.WorkItem.F2010_ListStateCode();
            /*Merging with the Datatable*/
            this.listStateCodeDataTable.Merge(this.stateCodeSelectionData.F2010_ListStateCode);
            /*checks for the count and Load the combo box.*/
            if (this.listStateCodeDataTable.Rows.Count > 0)
            {
                this.StateCodeComboBox.DataSource = this.listStateCodeDataTable;
                this.StateCodeComboBox.DisplayMember = this.listStateCodeDataTable.StateCodeColumn.ColumnName;
                this.StateCodeComboBox.ValueMember = this.listStateCodeDataTable.StateCodeIDColumn.ColumnName;
            }

            /*To display defaultsetting on Load*/           
            this.StateCodeComboBox.Text = this.stateCodeText;
        }          
        #endregion Methods                      
    }
}
