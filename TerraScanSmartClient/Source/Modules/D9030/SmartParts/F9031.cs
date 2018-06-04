//--------------------------------------------------------------------------------------------
// <copyright file="F9031.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains Property for the WorkItem .
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// 25 Sep 06        Suganth Mani       Modified for stylecop changes
//*********************************************************************************/

namespace D9030
{
    #region NameSpaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using TerraScan.Infrastructure.Interface.Constants;

    #endregion NameSpaces

    /// <summary>
    /// F9031 class implimentation of formslice test
    /// </summary>
    public partial class F9031 : UserControl
    {
        /// <summary>
        /// operation from master
        /// </summary>
        private int operation;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9031"/> class.
        /// </summary>
        public F9031()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9031"/> class.
        /// </summary>
        /// <param name="formSliceInformationRow">The form slice information row.</param>
        /// <param name="keyID">The key ID.</param>
        public F9031(FormMasterData.FormSliceInformationListRow formSliceInformationRow, int keyID)
        {
            try
            {
                this.InitializeComponent();
                if (formSliceInformationRow != null)
                {
                    if (!formSliceInformationRow.IsIsPermissionOpenNull())
                    {
                        if (formSliceInformationRow.IsPermissionOpen)
                        {
                            this.OpenLabel.ForeColor = Color.Red;
                        }
                        else
                        {
                            this.OpenLabel.ForeColor = Color.Black;
                        }
                    }

                    if (!formSliceInformationRow.IsIsPermissionAddNull())
                    {
                        if (formSliceInformationRow.IsPermissionAdd)
                        {
                            this.AddLabel.ForeColor = Color.Red;
                        }
                        else
                        {
                            this.AddLabel.ForeColor = Color.Black;
                        }
                    }

                    if (!formSliceInformationRow.IsIsPermissionEditNull())
                    {
                        if (formSliceInformationRow.IsPermissionEdit)
                        {
                            this.EditLabel.ForeColor = Color.Red;
                        }
                        else
                        {
                            this.EditLabel.ForeColor = Color.Black;
                        }
                    }

                    if (!formSliceInformationRow.IsIsPermissionDeleteNull())
                    {
                        if (formSliceInformationRow.IsPermissionDelete)
                        {
                            this.PermissionDeleteLabel.ForeColor = Color.Red;
                        }
                        else
                        {
                            this.PermissionDeleteLabel.ForeColor = Color.Black;
                        }
                    }

                    if (!formSliceInformationRow.IsIsExpandedNull())
                    {
                        if (formSliceInformationRow.IsExpanded)
                        {
                            this.ExpandedLabel.ForeColor = Color.Red;
                        }
                        else
                        {
                            this.ExpandedLabel.ForeColor = Color.Black;
                        }
                    }

                    this.KeyIDLabel.Text += " " + keyID.ToString();

                    if (!formSliceInformationRow.IsIsExpandedNull())
                    {
                        this.ExpandedLabel.Text += " " + formSliceInformationRow.IsExpanded.ToString();
                    }

                    if (!formSliceInformationRow.IsMenuNameNull())
                    {
                        this.TabLabel.Text += " " + formSliceInformationRow.MenuName.ToString();
                    }

                    if (!formSliceInformationRow.IsTabColorNull())
                    {
                        this.TabColor.Text += " " + formSliceInformationRow.TabColor.ToString();
                    }

                    if (!formSliceInformationRow.IsIsPermissionAddNull())
                    {
                        this.AddLabel.Text += " " + formSliceInformationRow.IsPermissionAdd.ToString();
                    }

                    if (!formSliceInformationRow.IsIsPermissionOpenNull())
                    {
                        this.OpenLabel.Text += " " + formSliceInformationRow.IsPermissionOpen.ToString();
                    }

                    if (!formSliceInformationRow.IsIsPermissionEditNull())
                    {
                        this.EditLabel.Text += " " + formSliceInformationRow.IsPermissionEdit.ToString();
                    }

                    if (!formSliceInformationRow.IsIsPermissionDeleteNull())
                    {
                        this.PermissionDeleteLabel.Text += " " + formSliceInformationRow.IsPermissionDelete.ToString();
                    }

                    this.sectionIndicatorPictureBox.Image = ExtendedGraphics.GenerateVerticalImage(106, 42, formSliceInformationRow.SubForm.ToString(), formSliceInformationRow.Red, formSliceInformationRow.Green, formSliceInformationRow.Blue);
                    this.OperationTestAlertTimer.Stop();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Called when [D9030_ F9030_ enable new method].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_EnableNewMethod, ThreadOption.UserInterface)]
        public void OnD9030_F9030_EnableNewMethod(object sender, Microsoft.Practices.CompositeUI.Utility.DataEventArgs<int> eventArgs)
        {
            this.operation = 1;
            this.ClearHighlighting();
            this.NewLabel.Text = "New = True";
            this.NewLabel.ForeColor = Color.Red;
            this.OperationTestAlertTimer.Start();
        }

        /// <summary>
        /// Called when [D9030_ F9030_ save slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_SaveSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_SaveSliceInformation(object sender, Microsoft.Practices.CompositeUI.Utility.DataEventArgs<int> eventArgs)
        {
            this.operation = 2;
            this.ClearHighlighting();
            this.SaveLabel.Text = "Save = True";
            this.SaveLabel.ForeColor = Color.Red;
            this.OperationTestAlertTimer.Start();
        }

        /// <summary>
        /// Called when [D9030_ F9030_ cancel slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_CancelSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_CancelSliceInformation(object sender, Microsoft.Practices.CompositeUI.Utility.DataEventArgs<int> eventArgs)
        {
            this.operation = 3;
            this.ClearHighlighting();
            this.CancelLabel.Text = "Cancel = True";
            this.CancelLabel.ForeColor = Color.Red;
            this.OperationTestAlertTimer.Start();
        }

        /// <summary>
        /// Called when [D9030_ F9030_ delete slice information].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event args.</param>
        [EventSubscription(EventTopicNames.D9030_F9030_DeleteSliceInformation, ThreadOption.UserInterface)]
        public void OnD9030_F9030_DeleteSliceInformation(object sender, Microsoft.Practices.CompositeUI.Utility.DataEventArgs<int> eventArgs)
        {
            this.operation = 4;
            this.ClearHighlighting();
            this.DeleteLabel.Text = "Delete = True";
            this.DeleteLabel.ForeColor = Color.Red;
            this.OperationTestAlertTimer.Start();
        }

        /// <summary>
        /// Handles the Tick event of the OperationTestAlertTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void OperationTestAlertTimer_Tick(object sender, EventArgs e)
        {
            this.DisableHighlightedLabel();
            this.OperationTestAlertTimer.Stop();
        }

        /// <summary>
        /// Disables the highlighted label.
        /// </summary>
        private void DisableHighlightedLabel()
        {
            if (this.operation == 1)
            {
                this.NewLabel.Text = "New = False";
                this.NewLabel.ForeColor = Color.Black;
            }
            else if (this.operation == 2)
            {
                this.SaveLabel.Text = "Save = False";
                this.SaveLabel.ForeColor = Color.Black;
            }
            else if (this.operation == 3)
            {
                this.CancelLabel.Text = "Cancel = False";
                this.CancelLabel.ForeColor = Color.Black;
            }
            else if (this.operation == 4)
            {
                this.DeleteLabel.Text = "Delete = False";
                this.DeleteLabel.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Clears the highlighting.
        /// </summary>
        private void ClearHighlighting()
        {
            this.NewLabel.Text = "New = False";
            this.NewLabel.ForeColor = Color.Black;
            this.SaveLabel.Text = "Save = False";
            this.SaveLabel.ForeColor = Color.Black;
            this.CancelLabel.Text = "Cancel = False";
            this.CancelLabel.ForeColor = Color.Black;
            this.DeleteLabel.Text = "Delete = False";
            this.DeleteLabel.ForeColor = Color.Black;
        }

        /// <summary>
        /// Handles the Load event of the FormSliceTest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FormSliceTest_Load(object sender, EventArgs e)
        {
            this.InformatiomLabel.Text = "This Subform (D9030.F9031) \n" + " Is designed to help test the \n" + " Configuration options of the Master/Slice \n" + " concept described in D9030.9030.";
        }
    }
}
