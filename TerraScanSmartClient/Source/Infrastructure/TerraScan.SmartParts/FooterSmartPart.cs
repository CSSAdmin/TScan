//--------------------------------------------------------------------------------------------
// <copyright file="FooterSmartPart.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the FooterSmartPart.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 16 Feb 06		JYOTHI		        Created
//*********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TerraScan.Common;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.Utility;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.CompositeUI;
using TerraScan.Infrastructure.Interface.Constants;

namespace TerraScan.SmartParts
{
    

    /// <summary>
    /// HelpSmartPart control
    /// </summary>
    [SmartPart]
    public partial class FooterSmartPart : PrimaryBaseSmartPart
    {
        #region Properties

        /// <summary>
        /// Assigning Empty to FormId
        /// </summary>
        private string formId = String.Empty;

        /// <summary>
        /// variable Holds the ParentWorkItem
        /// </summary>
        private WorkItem parentWorkItem;
        
        /// <summary>
        /// Assigning Empty to FormId
        /// </summary>
        private string auditLinkText = String.Empty;

        /// <summary>
        /// Assigning Empty to keyId
        /// </summary>
        private int? keyId;

        /// <summary>
        /// Assigning Formno
        /// </summary>
        private int formno;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FooterSmartPart"/> class.
        /// </summary>
        public FooterSmartPart()
        {
            InitializeComponent();
            this.HelpLinkLabel.Visible = false;
            this.HelpButton.Visible = true;
        }
        #endregion

        #region Event Publication

        [EventPublication(EventTopicNames.AuditLinkClick, PublicationScope.WorkItem)]
        public event EventHandler<DataEventArgs<LinkLabel>> AuditLinkClick;

        /// <summary>
        /// event publication for panel link label click
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;

        /// <summary>
        /// Sets the active key id.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        [EventSubscription(EventTopics.D9001_TerrascanSmartParts_FooterSmartPart_GetActiveKeyid, Thread = ThreadOption.UserInterface)]
        public void SetActiveKeyId(object sender, DataEventArgs<int[]> e)
        {
         this.keyId = e.Data[0];
         this.formno = e.Data[1]; 
        }

        #endregion Event Publication

        #region Property

        /// <summary>
        /// Property to Set and Get the ParentWorkItem
        /// </summary>
        public WorkItem ParentWorkItem
        {
            get { return this.parentWorkItem; }
            set { this.parentWorkItem = value; }
        }

        /// <summary>
        /// Gets or sets the FormId
        /// </summary>
        /// <value>The FormId.</value>
        [Description("Display Data based on form Name.")]
        public string FormId
        {
            set
            {
                this.FormIDLabel.Text = value;
                this.formId = value;
            }

            get
            {
                return this.formId;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [visible help link button].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [visible help link button]; otherwise, <c>false</c>.
        /// </value>
        [Description("Set the visible property of Help Link Button.")]
        public bool VisibleHelpLinkButton
        {
            set { this.HelpLinkLabel.Visible = value; }
            get { return this.HelpLinkLabel.Visible; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [visible help button].
        /// </summary>
        /// <value><c>true</c> if [visible help button]; otherwise, <c>false</c>.</value>
        [Description("Set the visible property of Help Button")]
        public bool VisibleHelpButton
        {
            set { this.HelpButton.Visible = value; }
            get { return this.HelpButton.Visible; }
        }

        /// <summary>
        /// Property to Set and Get the ParentWorkItem
        /// </summary>
        public string AuditLinkText
        {
            get
            {
                return this.auditLinkText;
            }

            set
            {
                this.AuditlinkLabel.Text = value;
                this.auditLinkText = value;
            }
        }

        /// <summary>
        /// Property to Set and Get the ParentWorkItem
        /// </summary>
        public int? KeyId
        {
            get
            {
                return this.keyId;
            }

            set
            {
                this.keyId = value;
                this.EnableAuditLink();                
            }
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the HelpButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void HelpButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowHelp();
               
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the HelpLinkLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelpLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.ShowHelp();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Shows the help.
        /// </summary>
        private void ShowHelp()
        {
            string formName = ParentForm.Text;
            if (!string.IsNullOrEmpty(this.FormId))
            {
                HelpEngine.Show(formName, this.formId);
                //HelpEngine.getFormName(formName, this.formId);
            }
        }

        private void EnableAuditLink()
        {
            if (this.keyId.HasValue)
            {
                this.AuditlinkLabel.Text = this.auditLinkText + this.KeyId;
                this.AuditlinkLabel.Enabled = true;
            }
            else
            {
                this.AuditlinkLabel.Text = this.auditLinkText;
                this.AuditlinkLabel.Enabled = false;
            }
         }

        protected virtual void OnAuditLinkClick(DataEventArgs<LinkLabel> eventArgs)
        {
            if (AuditLinkClick != null)
            {
                AuditLinkClick(this, eventArgs);
            }
        }

        private void AuditlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormInfo formInfo;
                formInfo = TerraScanCommon.GetFormInfo(90101);
                formInfo.optionalParameters = new object[2];
                formInfo.optionalParameters[0] = this.FormId;
                formInfo.optionalParameters[1] = this.keyId;
                this.ShowForm(this, new DataEventArgs<FormInfo>(formInfo));
                ////this.OnAuditLinkClick(new DataEventArgs<LinkLabel>(sender as LinkLabel));
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        private void HelpMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.ShowHelp();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
    }
}
