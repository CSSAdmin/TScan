//--------------------------------------------------------------------------------------------
// <copyright file="F1423.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the F1423.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 19 Dec 06        RANJANI              Created
// 05 Mar 06        RANJANI              Code Review Issue Fixed
//*********************************************************************************/

namespace D11020
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
    using TerraScan.Utilities;
    using TerraScan.Common;
    using System.Configuration;
    using System.Web.Services.Protocols;

    /// <summary>
    /// F1423 Form
    /// </summary>
    public partial class F1423 : BasePage
    {
        #region Private Variables

        /// <summary>
        /// form1423Control Variable
        /// </summary>
        private F1423Controller form1423Control;

        /// <summary>
        /// DataSet Contains Institution Detail 
        /// </summary>
        private F11020RealPropertyData realProperty = new F11020RealPropertyData();

        /// <summary>
        /// pageLoadStatus variable is used to find the pageLoadStatus. 
        /// </summary>   
        private bool pageLoadStatus;        

        /// <summary>
        /// statementId variable is used to store statementId. - default value - '-999'(invalid value)
        /// </summary>   
        private int statementId = -999;
        
        #endregion   

        #region  Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1423"/> class.
        /// </summary>
        public F1423()
        {
            this.InitializeComponent();            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F1423"/> class.
        /// </summary>
        /// <param name="statementId">The statement id.</param>
        /// <param name="parentFormId">The parent form id.</param>
        public F1423(int statementId, int parentFormId)
        {
            this.InitializeComponent();                     
            ////assign default value
            this.ParentFormId = parentFormId;
            this.statementId = statementId;            
            this.SetMaxLength();   
            ////set short cut key
            this.CancelButton = this.CancelStatementButton;
            this.SaveMenuToolStripMenuItem.Click += new EventHandler(this.SaveStatementButton_Click);
            ////set information
            this.InformationLabel.Text = SharedFunctions.GetResourceString("EditStatementInformation");
        }

        #endregion

        #region Event Publication

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        #endregion

        #region Properties
        
        /// <summary>
        /// Gets or sets the form1423 control.
        /// </summary>
        /// <value>The form1423 control.</value>
        [CreateNew]
        public F1423Controller F1423Control
        {
            get { return this.form1423Control as F1423Controller; }
            set { this.form1423Control = value; }
        }

        #endregion

        #region Form Load     

        /// <summary>
        /// Handles the Load event of the F1423 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F1423_Load(object sender, EventArgs e)
        {
            try
            {
                ////set pageLoadStatus - suppress textchanged event
                this.pageLoadStatus = true;
                ////load comboboxes
                this.LoadComboBox();
                ////load values               
                this.GetStatementDetail();
                this.SaveStatementButton.Enabled = false;
                ////reset pageLoadStatus - trigger textchanged event
                this.pageLoadStatus = false;
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

        #endregion   

        #region Private Methods

        #region Get Real Property Statement

        /// <summary>
        /// Gets the Statement detail
        /// </summary>
        private void GetStatementDetail()
        {
            this.realProperty.Clear();
            this.realProperty = this.form1423Control.WorkItem.F11020_GetRealPropertyStatement(this.statementId);

            if (this.realProperty.GetRealPropertyStatement.Rows.Count > 0)
            {
                this.SitusTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.SitusColumn].ToString();
                this.MapNumberTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.MapNumberColumn].ToString();
                this.LegalTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.LegalColumn].ToString();
                this.LoanNumberTextBox.Text = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.LoanNumberColumn].ToString();
                if (!string.IsNullOrEmpty(this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.MortgageIDColumn].ToString()))
                {
                    this.MortgageCompanyComboBox.SelectedValue = this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.MortgageIDColumn].ToString();
                }
                ////Set form name
                this.Text = string.Concat(ConfigurationWrapper.ApplicationName, " - ", SharedFunctions.GetResourceString("EditStatementName"), this.realProperty.GetRealPropertyStatement.Rows[0][this.realProperty.GetRealPropertyStatement.StatementNumberColumn].ToString());
                ////set permission
                this.SetFieldsPermission();
                ////set default focus
                this.ActiveControl = this.SitusTextBox;
            }
            else
            {
                this.ClearStatement();
            }
        }

        #endregion

        #region Clear Real Property Statement

        /// <summary>
        /// Method will Clear the Real Property Statement
        /// </summary>       
        private void ClearStatement()
        {
            ////statement related fields
            this.SitusTextBox.Text = String.Empty;
            this.MapNumberTextBox.Text = String.Empty;
            this.LegalTextBox.Text = String.Empty;
            this.LoanNumberTextBox.Text = String.Empty;
            this.MortgageCompanyComboBox.SelectedIndex = -1;
            ////Set form name
            this.Text = ConfigurationWrapper.ApplicationName;
            ////disable panel
            this.RealPropertyPanel.Enabled = false;
        }

        #endregion

        #region Save

        /// <summary>
        /// Handles the Click event of the SaveStatementButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void SaveStatementButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveStatement();
            }
            catch (SoapException ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }  

        #endregion        

        #region User Defined Funtion

        /// <summary>
        /// This Method used to load combobox datasource
        /// LoadComboBox
        /// </summary>
        private void LoadComboBox()
        {
            ////customize MortgageCompany ComboBox - loads MortgageCompany name
            this.realProperty = this.form1423Control.WorkItem.F1423_ListMortgageName();
            F11020RealPropertyData.ListMortgageNameRow mortgageNameDataRow = this.realProperty.ListMortgageName.NewListMortgageNameRow();
            mortgageNameDataRow.MortgageID = -999;
            mortgageNameDataRow.MortgageName = String.Empty;
            this.realProperty.ListMortgageName.Rows.InsertAt(mortgageNameDataRow, 0);
            this.MortgageCompanyComboBox.DataSource = this.realProperty.ListMortgageName.Copy();
            this.MortgageCompanyComboBox.ValueMember = this.realProperty.ListMortgageName.MortgageIDColumn.ToString();
            this.MortgageCompanyComboBox.DisplayMember = this.realProperty.ListMortgageName.MortgageNameColumn.ToString();
        }

        /// <summary>
        /// Sets the max length for the editable textboxes.
        /// </summary>
        private void SetMaxLength()
        {
            this.SitusTextBox.MaxLength = this.realProperty.GetRealPropertyStatement.SitusColumn.MaxLength;
            this.MapNumberTextBox.MaxLength = this.realProperty.GetRealPropertyStatement.MapNumberColumn.MaxLength;
            this.LegalTextBox.MaxLength = this.realProperty.GetRealPropertyStatement.LegalColumn.MaxLength;
            this.LoanNumberTextBox.MaxLength = this.realProperty.GetRealPropertyStatement.LoanNumberColumn.MaxLength;
        }

        /// <summary>
        /// Handles the TextChanged event of the EditTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void EditControl_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!this.pageLoadStatus && !this.SaveStatementButton.Enabled)
                {
                    this.SaveStatementButton.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }  

        /// <summary>
        /// Sets the fields permission - set edit permission.
        /// </summary>
        private void SetFieldsPermission()
        {
            ////lock text box control
            this.SitusTextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;
            this.MapNumberTextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;
            this.LegalTextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;
            this.LoanNumberTextBox.LockKeyPress = !this.EditPermissionButton.ActualPermission;
            ////enable or disable 
            this.MortgageCompanyComboBox.Enabled = this.EditPermissionButton.ActualPermission;            
        }

        /// <summary>
        /// Handles the LinkClicked event of the TaxRollCorrectionLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void TaxRollCorrectionLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ////try
            ////{
            ////    ////check for unsaved changes
            ////    if (this.SaveStatementButton.Enabled)
            ////    {
            ////        DialogResult dialogResult = MessageBox.Show(string.Concat(SharedFunctions.GetResourceString("CancelForm"), SharedFunctions.GetResourceString("StatementName"), "?"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            ////        if (dialogResult == DialogResult.Cancel)
            ////        {
            ////            return;
            ////        }
            ////        else if (dialogResult == DialogResult.Yes)
            ////        {
            ////            ////save changes
            ////            this.SaveStatementButton_Click(null, EventArgs.Empty);
            ////        }
            ////        else
            ////        {
            ////            ////close current form
            ////            this.Close();
            ////        }
            ////    }
            ////    else
            ////    {
            ////        ////close current form and open new form
            ////        this.Close();
            ////    }

            ////    this.Cursor = Cursors.WaitCursor;
            ////    ////close current form
            ////    this.Visible = false; 
            ////    ////Tax Roll Correction Form - FormID - 1550
            ////    FormInfo formInfo = TerraScanCommon.GetFormInfo(1550);
            ////    this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
            ////}
            ////catch (Exception ex)
            ////{
            ////    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            ////}
            ////finally
            ////{
            ////    this.Cursor = Cursors.Default;
            ////}
        }


        /// <summary>
        /// Handles the FormClosing event of the F1423 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void F1423_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.ShowMessageClosing();
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        #endregion           

        /// <summary>
        /// Shows the message closing.
        /// </summary>
        private void ShowMessageClosing()
        {
            if (this.SaveStatementButton.Enabled)
            {
                switch (MessageBox.Show(SharedFunctions.GetResourceString("CancelForm") + this.Text + "?", ConfigurationManager.AppSettings["ApplicationName"].ToString(), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case DialogResult.Yes:
                        {
                            this.SaveStatement();
                            this.Close();
                            break;
                        }

                    case DialogResult.No:
                        {
                            this.DialogResult = DialogResult.Cancel;
                            this.SaveStatementButton.Enabled = false;
                            this.Close();
                            break;
                        }

                    case DialogResult.Cancel:
                        {
                            this.DialogResult = DialogResult.None;
                            break;
                        }
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        /// <summary>
        /// Saves the statement.
        /// </summary>
        private void SaveStatement()
        {
            if (this.SaveStatementButton.Enabled)
            {
                this.SaveStatementButton.Focus();
                this.Cursor = Cursors.WaitCursor;

                ////update real property statement
                this.realProperty.SaveStatement.Rows.Clear();
                F11020RealPropertyData.SaveStatementRow statementDataRow = this.realProperty.SaveStatement.NewSaveStatementRow();

                statementDataRow.Situs = this.SitusTextBox.Text.Trim();
                statementDataRow.MapNumber = this.MapNumberTextBox.Text.Trim();
                statementDataRow.LoanNumber = this.LoanNumberTextBox.Text.Trim();
                statementDataRow.Legal = this.LegalTextBox.Text.Trim();
                if (this.MortgageCompanyComboBox.SelectedIndex > 0)
                {
                    ////this.MortgageCompanyComboBox.SelectedValue datatype is int
                    statementDataRow.MortgageID = Convert.ToInt32(this.MortgageCompanyComboBox.SelectedValue);
                }

                this.realProperty.SaveStatement.Rows.Add(statementDataRow);

                ////save statement details 
                this.form1423Control.WorkItem.F1423_UpdateRealPropertyStatement(this.statementId, Utility.GetXmlString(this.realProperty.SaveStatement.Copy()), TerraScanCommon.UserId);

                this.SaveStatementButton.Enabled = false;
                ////modified flag 
                //this.DialogResult = DialogResult.Yes;
                //this.Close();
            }
        }

        #endregion       
    }
}