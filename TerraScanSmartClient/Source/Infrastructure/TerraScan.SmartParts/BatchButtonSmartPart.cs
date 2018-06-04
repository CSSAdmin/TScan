//--------------------------------------------------------------------------------------------
// <copyright file="BatchButtonSmartPart.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods to access BatchButtonSmartPart
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 10 Dec 06       M.Vijayakumar      Created
// 
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
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.CompositeUI;
    using TerraScan.Helper;
    using TerraScan.UI.Controls;
    using TerraScan.Utilities;
    using System.Web.Services.Protocols;

    /// <summary>
    /// BatchButtonSmartPart
    /// </summary>
    public partial class BatchButtonSmartPart : UserControl
    {
        #region Variables 

        /// <summary>
        /// Used to store the snapShotId
        /// </summary>
        private int snapShotId;

        /// <summary>
        /// Used to store the total Items Count in the current SnapShot Batch
        /// </summary>
        private int currentFormBatchItemCount;

        /// <summary>
        /// currentParentFormPermission
        /// </summary>
        private bool currentParentFormPermission;

        /// <summary>
        /// Used to store the currentParentFormBatchButtonStatusMode
        /// </summary>
        private BatchButtonStatusMode currentParentFormBatchButtonStatusMode;       

        /// <summary>
        /// variable Holds the ParentWorkItem
        /// </summary>
        private WorkItem parentWorkItem;

        /// <summary>
        /// used to store the parentFormNo
        /// </summary>
        private int formSliceFormNo;

        /// <summary>
        /// Used to store the current user id
        /// </summary>
        private int currentBatchButtonUserId;

        /// <summary>
        /// Used to store the inserted Current recipt id
        /// </summary>
        private int currentreceiptId;

        /// <summary>
        /// Used to store the currentFormSlicePermission
        /// </summary>
        private PermissionFields currentFormSlicePermission;      

        #endregion Variables     

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BatchButtonSmartPart"/> class.
        /// </summary>
        public BatchButtonSmartPart()
        {
            this.InitializeComponent();
        }

        #endregion Constructor

        #region Enum

        /// <summary>
        /// Enumerator for Batch Button Status Mode
        /// when 0 = Closed  
        /// when 1 = Open
        /// when 2 = Both open and closed
        /// </summary>
        public enum BatchButtonStatusMode
        {
            /// <summary>
            /// Load
            /// </summary>
            Load = 0,

            /// <summary>
            /// Pause
            /// </summary>
            Pause = 1,

            /// <summary>
            /// Run
            /// </summary>
            Run = 2,
        }

        #endregion Enum

        #region Properties

        /// <summary>
        /// Property to Set and Get the ParentWorkItem
        /// </summary>
        public WorkItem ParentWorkItem
        {
            get 
            { 
                return this.parentWorkItem; 
            }

            set 
            { 
                this.parentWorkItem = value; 
            }
        }

        /// <summary>
        /// Gets or sets the parent form no.
        /// </summary>
        /// <value>The parent form no.</value>
        public int FormSliceFormNo
        {
            get 
            { 
                return this.formSliceFormNo; 
            }

            set 
            { 
                this.formSliceFormNo = value; 
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [current parent form permission].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [current parent form permission]; otherwise, <c>false</c>.
        /// </value>
        public bool CurrentParentFormPermission
        {
            get
            {
                return this.currentParentFormPermission;
            }

            set
            {
                this.currentParentFormPermission = value;              
            }
        }

        /// <summary>
        /// Gets or sets the current parent form batch button status mode.
        /// </summary>
        /// <value>The current parent form batch button status mode.</value>
        public BatchButtonStatusMode CurrentParentFormBatchButtonStatusMode
        {
            get 
            { 
                return this.currentParentFormBatchButtonStatusMode; 
            }

            set 
            { 
                this.currentParentFormBatchButtonStatusMode = value;
                this.SetBatchButton(this.currentParentFormBatchButtonStatusMode);
            }
        }

        /// <summary>
        /// Gets or sets the parent form no.
        /// </summary>
        /// <value>The parent form no.</value>
        public int CurrentBatchButtonUserId
        {
            get
            {
                return this.currentBatchButtonUserId;
            }

            set
            {
                this.currentBatchButtonUserId = value;
            }
        }

        /// <summary>
        /// Gets or sets the currentreceipt id.
        /// </summary>
        /// <value>The currentreceipt id.</value>
        public int CurrentreceiptId
        {
            get 
            { 
                return this.currentreceiptId; 
            }

            set
            {
                this.currentreceiptId = value;

                if (this.currentParentFormBatchButtonStatusMode.Equals(BatchButtonStatusMode.Pause))
                {
                    ////db call is used insert the receipt id to insert or update  snapshot item collection
                    this.currentFormBatchItemCount = WSHelper.F1440_SaveRecieptinSnapShotBatchButtonControl(this.snapShotId, this.currentreceiptId, this.currentBatchButtonUserId);

                    this.GetBatchButtonToolTip(this.currentParentFormBatchButtonStatusMode);
                }
            }
        }

        /// <summary>
        /// Gets or sets the current form slice permission.
        /// </summary>
        /// <value>The current form slice permission.</value>
        public PermissionFields CurrentFormSlicePermission
        {
            get { return this.currentFormSlicePermission; }
            set { this.currentFormSlicePermission = value; }
        }

        #endregion Properties       

        #region Methods

        /// <summary>
        /// Used to change the Batch Button ToolTip Text on Batch Button Mode change.
        /// </summary>
        /// <param name="currentBatchButtonMode">The form batch button mode.</param>
        private void GetBatchButtonToolTip(BatchButtonStatusMode currentBatchButtonMode)
        {
            switch (currentBatchButtonMode)
            {
                case BatchButtonStatusMode.Pause:
                    {
                        if (this.currentFormBatchItemCount > 0)
                        {
                            this.RunBatchButton.AccessibleName = SharedFunctions.GetResourceString("F1440PauseReceipt") + " " + this.currentFormBatchItemCount + SharedFunctions.GetResourceString("F1440ItemsinBatch");
                            this.StopBatchButton.AccessibleName = SharedFunctions.GetResourceString("F1440CloseReceipt") + " " + this.currentFormBatchItemCount + SharedFunctions.GetResourceString("F1440ItemsinBatch");
                        }
                        else
                        {
                            this.RunBatchButton.AccessibleName = SharedFunctions.GetResourceString("F1440PauseReceipt");
                            this.StopBatchButton.AccessibleName = SharedFunctions.GetResourceString("F1440CloseReceipt");
                        }

                        break;
                    }

                case BatchButtonStatusMode.Run:
                    {
                        if (this.currentFormBatchItemCount > 0)
                        {
                            this.RunBatchButton.AccessibleName = SharedFunctions.GetResourceString("F1440StartReceipt") + " " + this.currentFormBatchItemCount + SharedFunctions.GetResourceString("F1440ItemsinBatch");
                            this.StopBatchButton.AccessibleName = SharedFunctions.GetResourceString("F1440CloseReceipt") + " " + this.currentFormBatchItemCount + SharedFunctions.GetResourceString("F1440ItemsinBatch");
                        }
                        else
                        {
                            this.RunBatchButton.AccessibleName = SharedFunctions.GetResourceString("F1440StartReceipt");
                            this.StopBatchButton.AccessibleName = SharedFunctions.GetResourceString("F1440CloseReceipt");
                        }

                        break;
                    }

                default:
                    {
                        this.RunBatchButton.AccessibleName = SharedFunctions.GetResourceString("F1440StartNewReceipt");
                        this.StopBatchButton.AccessibleName = SharedFunctions.GetResourceString("F1440NoReceipt");

                        break;
                    }
            }
        }

        /// <summary>
        /// Used to change the Batch Button Image on Batch Button Mode change.
        /// </summary>
        /// <param name="formBatchButtonMode">The form batch button mode.</param>
        private void SetBatchButton(BatchButtonStatusMode formBatchButtonMode)
        {
            switch (formBatchButtonMode)
            {
                case BatchButtonStatusMode.Pause:
                    {
                        this.RunBatchButton.Image = this.BatchButtonTypeImage.Images[2];
                        this.StopBatchButton.Image = this.BatchButtonTypeImage.Images[3];

                        this.GetBatchButtonToolTip(formBatchButtonMode);
                        break;
                    }

                case BatchButtonStatusMode.Run:
                    {
                        this.RunBatchButton.Image = this.BatchButtonTypeImage.Images[4];
                        this.StopBatchButton.Image = this.BatchButtonTypeImage.Images[3];

                        this.GetBatchButtonToolTip(formBatchButtonMode);

                        break;
                    }

                default:
                    {
                        this.RunBatchButton.Image = this.BatchButtonTypeImage.Images[0];
                        this.StopBatchButton.Image = this.BatchButtonTypeImage.Images[1];

                        this.GetBatchButtonToolTip(formBatchButtonMode);

                        break;
                    }
            }
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the Click event of the RunBatchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RunBatchButton_Click(object sender, EventArgs e)
        {
            try
            {
                switch (this.currentParentFormBatchButtonStatusMode)
                {
                    ////when the BatchButtonStatusMode is Pause
                    case BatchButtonStatusMode.Pause:
                        {
                            this.currentParentFormBatchButtonStatusMode = BatchButtonStatusMode.Run;
                            this.SetBatchButton(BatchButtonStatusMode.Run);
                            break;
                        }

                    ////when the BatchButtonStatusMode is Run
                    case BatchButtonStatusMode.Run:
                        {
                            this.currentParentFormBatchButtonStatusMode = BatchButtonStatusMode.Pause;
                            this.SetBatchButton(BatchButtonStatusMode.Pause);
                            break;
                        }

                    ////when the BatchButtonStatusMode is load
                    default:
                        {
                            object[] optionalParameter;
                            optionalParameter = new object[] { this.formSliceFormNo, this.currentFormSlicePermission };
                            Form snapShotForm = new Form();
                            snapShotForm = TerraScanCommon.GetForm(9040, optionalParameter, this.parentWorkItem);
                            if (snapShotForm != null)
                            {
                                DialogResult dialogResult = snapShotForm.ShowDialog();

                                if (dialogResult.Equals(DialogResult.OK))
                                {
                                    this.snapShotId = Convert.ToInt32(TerraScanCommon.GetValue(snapShotForm, "CurrentSnapShotId"));                                   
                                }
                                else
                                {
                                    ////if no record is select in the F9040 snapshot dialog form set it as zero
                                    this.snapShotId = 0;
                                }
                            }

                            ////if the F9040 snapShot form returns snapshot id, chnage the Batch Button status
                            if (this.snapShotId > 0)
                            {
                                ////db call is used get the no of count in snapshot item collection
                                this.currentFormBatchItemCount = WSHelper.F1440_SaveRecieptinSnapShotBatchButtonControl(this.snapShotId, null, this.currentBatchButtonUserId);
                                this.currentParentFormBatchButtonStatusMode = BatchButtonStatusMode.Pause;
                                this.SetBatchButton(BatchButtonStatusMode.Pause);
                            }

                            break;
                        }
                }               
            }
            catch (SoapException soapException)
            {
                ExceptionManager.ManageException(soapException, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the Click event of the StopBatchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StopBatchButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.currentParentFormBatchButtonStatusMode.Equals(BatchButtonStatusMode.Load))
                {
                    this.currentParentFormBatchButtonStatusMode = BatchButtonStatusMode.Load;
                    this.SetBatchButton(BatchButtonStatusMode.Load);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the RunBatchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RunBatchButton_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.BatchButtonStatusToolTip.SetToolTip(this.RunBatchButton, this.RunBatchButton.AccessibleName);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Handles the MouseHover event of the StopBatchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void StopBatchButton_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.BatchButtonStatusToolTip.SetToolTip(this.StopBatchButton, this.StopBatchButton.AccessibleName);
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        #endregion Events       
    }
}
