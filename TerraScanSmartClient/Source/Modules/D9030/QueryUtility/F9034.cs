//--------------------------------------------------------------------------------------------
// <copyright file="F9034.cs" company="Congruent">
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
// 02-01-2007       Guhan.S             Created           
//*********************************************************************************/

namespace D9030
{
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
    using Infragistics.Win.UltraWinGrid;
    using System.Xml;
    using Infragistics.Win;

    /// <summary>
    /// F9034
    /// </summary>
    public partial class F9034 : BasePage
    {
        #region Variables

        /// <summary>
        /// form9033Control Control Name
        /// </summary>
        private F9034Controller form9034Control;     

        /// <summary>
        /// Used to store the unsavedChangeExists
        /// </summary>
        private bool unsavedChangeExists;

        /// <summary>
        /// hiddenColumnCountonLoad
        /// </summary>
        private int hiddenColumnCountonLoad;        

        #region Grid

        /// <summary>
        /// Define grid
        /// </summary>
        private UltraGridBase grid;       

        /// <summary>
        /// queryGrid
        /// </summary>
        private UltraGrid queryGrid;        

        #endregion // Grid

        #endregion Variables

        #region Constructors

        /// <summary>
        /// F9034
        /// </summary>
        public F9034()
        {
            this.InitializeComponent();
            this.AcceptButton = this.F9034OKButton;
            this.CancelButton = this.F9034CancelButton;
        }

        #endregion Constructors

        #region Property

        /// <summary>
        /// Gets or sets the UltraGrid instances whose columns are displayed in the column chooser.
        /// </summary>
        public UltraGridBase Grid
        {
            get
            {                
                    return this.grid;
            }

            set
            {
                if (value != this.grid)
                {                    
                    this.grid = value;
                    this.EngineFieldDataGrid.SourceGrid = this.grid;                    

                    ////to set the height of the form
                    if (this.grid.DisplayLayout.Bands[0] != null)
                    {
                        ////find the no of hidden columns
                        this.hiddenColumnCountonLoad = 0;
                        for (int i = 0; i < this.grid.DisplayLayout.Bands[0].Columns.Count; i++)
                        {
                            if (this.grid.DisplayLayout.Bands[0].Columns[i].Hidden)
                            {
                                this.hiddenColumnCountonLoad = this.hiddenColumnCountonLoad + 1;
                            }
                        }

                        this.SetFormSize(this.grid.DisplayLayout.Bands[0].Columns.Count);
                    }
                    else
                    {
                        ////normally this condtion is not used
                        this.SetFormSize(0);
                    }                   
                }
            }
        }       

        /// <summary>
        /// Returns the column chooser control.
        /// </summary>
        public UltraGridColumnChooser ColumnChooserControl
        {
            get
            {
                return this.EngineFieldDataGrid;
            }
        }

        /// <summary>
        /// Gets or sets the F1105 control.
        /// </summary>
        /// <value>The F1105 control.</value>
        [CreateNew]
        public F9034Controller F9034Control
        {
            get { return this.form9034Control as F9034Controller; }
            set { this.form9034Control = value; }
        }

        /// <summary>
        /// Gets or sets the F1105 control.
        /// </summary>
        /// <value>The F1105 control.</value>
        [CreateNew]
        public UltraGrid ReturnGrid
        {
            get 
            {
                return this.queryGrid; 
            }
            set 
            {
                this.queryGrid = value; 
            }
        }

        #region CurrentBand

        /// <summary>
        /// Gets or sets the band whose columns are being displayed.
        /// </summary>
        public UltraGridBand CurrentBand
        {
            get
            {
                return this.EngineFieldDataGrid.CurrentBand;
            }

            set
            {               
                if (null != value && (null == this.Grid || this.Grid != value.Layout.Grid))
                {
                    //this.queryGrid.DataSource = this.grid.DataSource;
                    throw new ArgumentException();
                }
            }
        }
       

        #endregion // CurrentBand

        #endregion Property      

        #region Methods

        /// <summary>
        /// Sets the size of the form.
        /// </summary>
        /// <param name="bandColumnCount">The band column count.</param>
        private void SetFormSize(int bandColumnCount)
        {
            this.ColumnChooserPanel.Top = this.label2.Bottom - 1;
            int increment = bandColumnCount * 22;
            int totalFormHeight = 20 + increment;
            int formHeight = 200 + increment;

            if (formHeight <= 503)
            {
                //this.EngineFieldDataGrid.Height = increment - 2;
                //this.ColumnChooserPanel.Height = increment - 2;
                this.EngineFieldDataGrid.Height = increment - (bandColumnCount-2);
                this.ColumnChooserPanel.Height = increment - (bandColumnCount - 2);
                this.Height = 180 + increment;
            }
            else
            {
                this.EngineFieldDataGrid.Height =  295;
                this.ColumnChooserPanel.Height = 295;
                this.Height = 483;
            }

            this.NewColumnButton.Top = this.ColumnChooserPanel.Bottom + 10;
            this.DeleteColumn.Top = this.ColumnChooserPanel.Bottom + 10;

            this.F9034OKButton.Top = this.NewColumnButton.Bottom + 7;
            this.F9034CancelButton.Top = this.NewColumnButton.Bottom + 7;

            this.F9034FooterPanel.Top = this.F9034OKButton.Bottom + 5;
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Handles the Click event of the NewColumnButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void NewColumnButton_Click(object sender, EventArgs e)
        {
            Form newColumnForm = new Form();
            try
            {
                UltraGridBand selectedBand = this.EngineFieldDataGrid.CurrentBand;
                if (null == selectedBand)
                {
                    return;
                }
                else
                {
                    //for (int i = 0; i < selectedBand.Columns.Count; i++)
                    //{
                    //    if (!selectedBand.Columns[i].IsVisibleInLayout)
                    //    {
                         
                    //    }
                    //}
                }
                this.Cursor = Cursors.WaitCursor;

                newColumnForm = this.form9034Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9035, null, this.form9034Control.WorkItem);

                if (newColumnForm != null)
                {
                    TerraScanCommon.SetValue(newColumnForm, "Band", selectedBand);

                    if (newColumnForm.ShowDialog() == DialogResult.OK)
                    {
                        this.queryGrid.DataSource = selectedBand;
                        this.SetFormSize(selectedBand.Columns.Count);
                        this.unsavedChangeExists = true;
                    }
                }

            }
            catch (InvalidCastException ex1)
            {
                ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this);
            }
            catch (Exception ex)
            {
                throw;
                /*//MessageBox.Show(this, "Invalid Format " , ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                // return false;
                // ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);*/
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Handles the Click event of the DeleteColumn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void DeleteColumn_Click(object sender, EventArgs e)
        {
            try
            {
                //// Delete the column that's currently selected in the column chooser control.
                UltraGridColumn column;

                column = this.EngineFieldDataGrid.CurrentSelectedItem as UltraGridColumn;

                ////column = (UltraGridColumn)this.EngineFieldDataGrid.CurrentBand.Columns.GetItem(2);
                if (null == column)
                {
                    MessageBox.Show(this, SharedFunctions.GetResourceString("ColumnDelete"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ////           MessageBox.Show(this, "Please select a column to delete.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (column.IsBound)
                {
                    MessageBox.Show(this, SharedFunctions.GetResourceString("UnboundColumns"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ////MessageBox.Show(this, "Only unbound columns can be deleted.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ////DialogResult dlgResult = MessageBox.Show(this, string.Format("Deleting {0} column. Continue?", column.Header.Caption), this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                DialogResult dlgResult = MessageBox.Show(this, SharedFunctions.GetResourceString("DeletingColumns"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == dlgResult)
                {
                    column.Band.Columns.Remove(column);
                    this.SetFormSize(this.EngineFieldDataGrid.CurrentBand.Columns.Count);
                    this.unsavedChangeExists = true;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the LinkClicked event of the HelpLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void HelpLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString()))
                {
                    HelpEngine.Show(this.AccessibleName, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Close Event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">EventArgs</param>
        private void NextNumberCloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the F9034OKButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9034OKButton_Click(object sender, EventArgs e)
        {
            this.unsavedChangeExists = false;
        }

        /// <summary>
        /// Handles the FormClosing event of the F9034 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void F9034_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.unsavedChangeExists)
            {
                ////When unsaved changes exists and F9035 form cancel button is clicked following message is raised
                if (MessageBox.Show(SharedFunctions.GetResourceString("Discard"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.unsavedChangeExists = false;
                }
                else
                {
                    ////cancel will not close the form
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Handles the Load event of the F9034 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9034_Load(object sender, EventArgs e)
        {
            try
            {
                (this.EngineFieldDataGrid.Controls[0] as Infragistics.Win.UltraWinGrid.ColumnChooserGrid).MouseDoubleClick += new MouseEventHandler(F9034_MouseDoubleClick);                
                (this.EngineFieldDataGrid.Controls[0] as Infragistics.Win.UltraWinGrid.ColumnChooserGrid).AfterCellUpdate += new CellEventHandler(F9034_AfterCellUpdate);
               
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the AfterCellUpdate event of the F9034 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinGrid.CellEventArgs"/> instance containing the event data.</param>
        private void F9034_AfterCellUpdate(object sender, CellEventArgs e)
        {
            try
            {
                if (e.Cell.Row.Index <= 1)
                {
                    this.grid.DisplayLayout.Bands[0].Columns[0].Hidden = false;
                    this.grid.DisplayLayout.Bands[0].Columns[1].Hidden = false;
                    // Exclude Checkbox(used for Pinning) column from column chooser
                    this.grid.DisplayLayout.Bands[0].Columns[0].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
                }

                if (!this.unsavedChangeExists)
                {
                    ////find the no of hidden columns
                    int currentHiddenColumnCount = 0;
                    for (int i = 0; i < this.grid.DisplayLayout.Bands[0].Columns.Count; i++)
                    {
                        if (this.grid.DisplayLayout.Bands[0].Columns[i].Hidden)
                        {
                            currentHiddenColumnCount = currentHiddenColumnCount + 1;
                        }
                    }

                    if (currentHiddenColumnCount != this.hiddenColumnCountonLoad)
                    {
                        this.unsavedChangeExists = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the F9034 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        private void F9034_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.X <= 222)
            {
                Form newColumnForm = new Form();
                try
                {
                    UltraGridColumn newcolumn;

                    newcolumn = this.EngineFieldDataGrid.CurrentSelectedItem as UltraGridColumn;

                    if (null == newcolumn)
                    {
                       // MessageBox.Show("Please select a custom column", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    if (newcolumn.IsBound)
                    {
                        MessageBox.Show("Please select a custom column", ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ////MessageBox.Show(this, "Only unbound columns can be deleted.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    UltraGridBand selectedBand = this.EngineFieldDataGrid.CurrentBand;

                    if (null == selectedBand)
                    {
                        return;
                    }

                    Hashtable addNewFieldsHashTable = new Hashtable();
                    addNewFieldsHashTable.Add("KeyName", newcolumn.Key);
                    addNewFieldsHashTable.Add("Type", newcolumn.DataType);
                    addNewFieldsHashTable.Add("Formula", newcolumn.Formula);
                    addNewFieldsHashTable.Add("Format", newcolumn.Format);
                    addNewFieldsHashTable.Add("Alignment", newcolumn.CellAppearance.TextHAlign);

                    this.Cursor = Cursors.WaitCursor;
                    ////column postion is sent
                    object[] optionalParameter = new object[] { addNewFieldsHashTable, newcolumn.Index };

                    newColumnForm = this.form9034Control.WorkItem.Services.Get<TerraScan.Infrastructure.Interface.Services.IFormEngineService>().GetForm(9035, optionalParameter, this.form9034Control.WorkItem);

                    if (newColumnForm != null)
                    {
                        TerraScanCommon.SetValue(newColumnForm, "Band", selectedBand);

                        if (newColumnForm.ShowDialog() == DialogResult.OK)
                        {
                            this.queryGrid.DataSource = selectedBand;
                            this.queryGrid.Refresh();
                        }
                    }
                }
                catch (InvalidCastException ex1)
                {
                    ExceptionManager.ManageException(ex1, ExceptionManager.ActionType.Display, this);
                }
                catch (Exception ex)
                {
                    throw;
                    /*//MessageBox.Show(this, "Invalid Format " , ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // return false;
                    // ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);*/
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }              

        /// <summary>
        /// Handles the Click event of the HelpStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void HelpStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.Tag.ToString()))
                {
                    HelpEngine.Show(this.AccessibleName, this.Tag.ToString());
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
            }
        }

        #endregion Events
    }
}
