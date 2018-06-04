//--------------------------------------------------------------------------------------------
// <copyright file="F9035.cs" company="Congruent">
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
// 25-12-2006       Guhan              Created
//*********************************************************************************/


namespace D9030
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using Infragistics.Win;
    using Infragistics.Win.UltraWinGrid;
    using Infragistics.Win.UltraWinCalcManager;
    using Infragistics.Win.UltraWinCalcManager.FormulaBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Utilities;
    using Infragistics.Win.CalcEngine;

    /// <summary>
    /// F9035
    /// </summary>
    public partial class F9035 : BasePage
    {
        #region Variables

        /// <summary>
        /// keep a track of Changes
        /// </summary>
        private bool formatChanged;

        /// <summary>
        /// keep a track of Changes
        /// </summary>
        private bool newColumnCreated;

        /// <summary>
        /// Used to store whether UnSaved Cahnges exists
        /// </summary>
        private bool flagUnSavedChangesExists;

        /// <summary>
        ///  Used to store available data
        /// </summary>
        private CommonData availableTypeData;

        /// <summary>
        ///used to assign the dataset
        /// </summary>
        private UltraGridBand band;

        /// <summary>
        /// Used to store the Value like HeaderName, Data Type, Formula, Format and Alignment
        /// </summary>
        private Hashtable assignaddNewFieldsHashTable = new Hashtable();

        /// <summary>
        /// flagFormLoadOnprocess
        /// </summary>
        private bool flagFormLoadOnprocess;

        /// <summary>
        /// Used set whether the form data are to modifiy
        /// </summary>
        private bool flagFormDataModify;

        /// <summary>
        /// Used column Index of Band
        /// </summary>
        private int columnIndex;

        /// <summary>
        /// Used to set whether form can be closed
        /// </summary>
        private bool avoidFormClose;

        /// <summary>
        /// Flag for Formula changed event
        /// </summary>
        private bool hasFormulaChanged;

        #region CreatedColumn

        /// <summary>
        ///  CreatedColumn 
        /// </summary>
        private UltraGridColumn createdColumn;

        #endregion CreatedColumn

        #endregion Variables

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9035"/> class.
        /// </summary>
        public F9035()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:F9035"/> class.
        /// </summary>
        /// <param name="addNewFieldsHashTable">New Fields</param>
        /// <param name="columnIndex">Column Index</param>
        public F9035(Hashtable addNewFieldsHashTable, int columnIndex)
        {
            this.InitializeComponent();
            this.assignaddNewFieldsHashTable = addNewFieldsHashTable;
            this.columnIndex = columnIndex;
            this.flagFormDataModify = true;
        }

        #endregion Constructors

        #region properties

        /// <summary>
        /// Returns the new column that was created. It will return null if the user cancels the process.
        /// </summary>
        public UltraGridColumn CreatedColumn
        {
            get
            {
                return this.createdColumn;
            }
        }

        /// <summary>
        /// Gets or sets the band object to which the new column will be added.
        /// </summary>
        public UltraGridBand Band
        {
            get
            {
                return this.band;
            }

            set
            {
                this.band = value;
            }
        }

        #endregion

        #region Regular Expression

        /// <summary>
        /// Determines whether the specified value is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is valid; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValid(string value)
        {
            ////Format for - 
            ////String starts with i) varchar  ii) #   iii) _
            ////Dont allow special sharacters which is not supported by SQLServer
            string columnPattern = "^[#_a-zA-Z][^~!%^&*()-=+<>?,.:;" + "'{}| ]*$";

            ////Format to not allow characters
            string formatPattern = "^[^a-zA-Z]*$";

            ////Check valid column name
            bool validColumn = IsValidFormat(value, columnPattern);
            bool validFormat = true;

            ////Check valid Format value
            if (this.FormatTextBox.Text.Length > 0 && this.TypeCombo.SelectedIndex == 1)
            {
                validFormat = IsValidFormat(this.FormatTextBox.Text, formatPattern);
            }

            if (!validColumn || !validFormat)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether [is valid format] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="columnPattern">The column pattern.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid format] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsValidFormat(string value, string columnPattern)
        {
            return IsMatch(value, columnPattern);
        }


        /// <summary>
        /// Determines whether the specified value is match.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns>
        /// 	<c>true</c> if the specified value is match; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsMatch(string value, string pattern)
        {
            System.Text.RegularExpressions.Regex objRegex = new System.Text.RegularExpressions.Regex(@pattern);
            if (objRegex.IsMatch(value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Regular Expression

        #region Methods

        #region CreateColumnHelper

        /// <summary>
        /// Creates the column based on currently input data. If a column was previously created
        /// then reuses that.
        /// </summary>
        /// <returns>if created Column returns true else return false </returns>
        private bool CreateColumnHelper()
        {
            try
            {
                ValueListItem item = this.TypeCombo.SelectedItem;
                string columnName = this.FieldNameTextBox.Text;
                if (null != columnName)
                {
                    columnName = columnName.Trim();
                }

                // // Don't allow empty string as the column name.
                if (null == columnName || columnName.Length <= 0)
                {
                    MessageBox.Show(this, SharedFunctions.GetResourceString("ColumnName"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.FieldNameTextBox.Focus();
                    //// MessageBox.Show(this, "Please enter a column name.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                //// Make sure that a column by the same name doesn't exist already.
                if (this.band.Columns.Exists(columnName)
                    && (null == this.createdColumn || this.createdColumn.Key != columnName))
                {
                    MessageBox.Show(this, SharedFunctions.GetResourceString("ExistsColumnName"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                // If we haven't already created the column then create one.
                if (null == this.createdColumn)
                {
                    if (!string.IsNullOrEmpty(this.FormulaTextBox.Text.Trim()))
                    {
                        NewColCalcManager.Calculate(this.FormulaTextBox.Text);
                    }
                    this.createdColumn = this.band.Columns.Add(columnName);
                }
                else
                {
                    // If we had already created the column then make sure that 
                    // the key is the same. 
                    this.createdColumn.Key = columnName;
                }

                // Assing the newly selected data tyle and formula if any.
                this.createdColumn.DataType = (Type)item.DataValue;
                this.createdColumn.Formula = this.FormulaTextBox.Text;
                this.createdColumn.Format = this.FormatTextBox.Text;
                if (!string.IsNullOrEmpty(this.FormulaTextBox.Text.Trim()))
                {
                    NewColCalcManager.Calculate(this.FormulaTextBox.Text);
                }

                ////For validate formula
                //if (null == this.createdColumn.Layout.Grid.CalcManager)
                //{
                //    this.createdColumn.Layout.Grid.CalcManager = new UltraCalcManager();
                //}

                //NewColCalcManager.Calculate(this.FormulaTextBox.Text);
                if (this.LeftRadioButton.Checked)
                {
                    this.createdColumn.CellAppearance.TextHAlign = Infragistics.Win.HAlign.Left;
                }
                else if (this.RightRadioButton.Checked)
                {
                    this.createdColumn.CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                }
                else if (this.CenterRadioButton.Checked)
                {
                    this.createdColumn.CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, SharedFunctions.GetResourceString("F9035InvalidFormat"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
               // this.createdColumn.DataType = null;
               // this.createdColumn.Formula = string.Empty;
               // this.createdColumn.Format = string.Empty;
               
                this.FormulaTextBox.Text = string.Empty;
                //// ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return true;
        }

        #endregion // CreateColumnHelper

        /// <summary>
        /// To assgine the F9035 Add new value on Form Load
        /// </summary>
        private void AssignColumnValues()
        {
            if (this.assignaddNewFieldsHashTable.Count != 0)
            {
                ////added by Vijayakumar on 25/08/2007

                if (this.assignaddNewFieldsHashTable["KeyName"] != null)
                {
                    this.FieldNameTextBox.Text = this.assignaddNewFieldsHashTable["KeyName"].ToString();
                }

                if (this.assignaddNewFieldsHashTable["Type"] != null)
                {
                    /* DataType string     TypeComboBox SelcetdIndex    TypeComboBox Display value                      
                     * System.String               2                            Text
                     * System.Int32                1                            Numberic
                     * System.DateTime             0                            Date                     
                    */

                    if (this.assignaddNewFieldsHashTable["Type"].ToString().Equals("System.String"))
                    {
                        this.TypeCombo.SelectedIndex = 2;
                    }
                    else if (this.assignaddNewFieldsHashTable["Type"].ToString().Equals("System.Int32"))
                    {
                        this.TypeCombo.SelectedIndex = 1;
                    }
                    else if (this.assignaddNewFieldsHashTable["Type"].ToString().Equals("System.DateTime"))
                    {
                        this.TypeCombo.SelectedIndex = 0;
                    }
                }

                /////here check condtion to lock the FormatTextBox text box basedon Datatype
                if (!string.IsNullOrEmpty(this.TypeCombo.SelectedItem.DisplayText))
                {
                    if (this.TypeCombo.SelectedItem.DisplayText.Equals("Number") || this.TypeCombo.SelectedItem.DisplayText.Equals("Date"))
                    {
                        this.FormatTextBox.LockKeyPress = false;
                        this.label3.Enabled = true;

                        /////here the formate text box is load based on the datatype
                        if (this.assignaddNewFieldsHashTable["Format"] != null)
                        {
                            this.FormatTextBox.Text = this.assignaddNewFieldsHashTable["Format"].ToString();
                        }
                    }
                    else
                    {
                        this.FormatTextBox.LockKeyPress = true;
                        this.label3.Enabled = false;
                    }
                }

                if (this.assignaddNewFieldsHashTable["Formula"] != null)
                {
                    this.FormulaTextBox.Text = this.assignaddNewFieldsHashTable["Formula"].ToString();
                }

                if (this.assignaddNewFieldsHashTable["Alignment"] != null)
                {
                    ////this.assignaddNewFieldsHashTable["Alignment"].ToString();

                    if (this.assignaddNewFieldsHashTable["Alignment"].ToString().Equals("Left") || this.assignaddNewFieldsHashTable["Alignment"].ToString().Equals("Default"))
                    {
                        this.LeftRadioButton.Checked = true;
                    }
                    else if (this.assignaddNewFieldsHashTable["Alignment"].ToString().Equals("Right"))
                    {
                        this.RightRadioButton.Checked = true;
                    }
                    else if (this.assignaddNewFieldsHashTable["Alignment"].ToString().Equals("Center"))
                    {
                        this.CenterRadioButton.Checked = true;
                    }
                }
            }
        }

        /// <summary>
        /// Loads the type combo.
        /// </summary>
        private void LoadTypeCombo()
        {
            object[] arr = new object[]
             {
                  typeof(DateTime), "Date",
                  typeof(string),  "Text",
                  typeof(int),  "Number",
             };

            this.TypeCombo.Items.Clear();

            for (int i = 0; i < arr.Length; i += 2)
            {
                Type type = (Type)arr[i];
                string description = arr[i + 1].ToString();
                this.TypeCombo.Items.Add(type, description);
            }

            ValueList vl = this.TypeCombo.Items[0].ValueList;
            vl.DisplayStyle = ValueListDisplayStyle.DisplayText;
            vl.SortStyle = ValueListSortStyle.Ascending;

            //// Select a default type.
            if (this.TypeCombo.Items.Count > 0)
            {
                this.TypeCombo.SelectedIndex = 1;
            }
        }

        /// <summary>
        /// Modifies the column data value.
        /// </summary>
        /// <returns>Bool</returns>
        private bool ModifyColumnDataValue()
        {
            if (this.columnIndex >= 0)
            {
                //if (this.band.Columns[this.columnIndex].Key.Contains(this.FieldNameTextBox.Text.Trim()))
                //{
                //    MessageBox.Show(this, SharedFunctions.GetResourceString("ExistsColumnName"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return false;
                //}
                //else
                //{
                string columnName = this.FieldNameTextBox.Text;

                //try
                //{

                if (null == this.band.Columns[this.columnIndex].Layout.Grid.CalcManager)
                {
                    this.band.Columns[this.columnIndex].Layout.Grid.CalcManager = new UltraCalcManager();
                }

                try
                {
                    NewColCalcManager.Calculate(this.FormulaTextBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, SharedFunctions.GetResourceString("F9035InvalidFormat"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // this.createdColumn.DataType = null;
                    this.FormulaTextBox.Text = string.Empty;
                    return false;
                    //// ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }

                this.ToSetBandvalues();
  
                this.band.Columns[this.columnIndex].Formula = this.FormulaTextBox.Text.Trim();
                //}
                //catch (Exception ex)
                //{
                //}
                this.band.Columns[this.columnIndex].Format = this.FormatTextBox.Text.Trim();
                this.band.Columns[this.columnIndex].Key = this.FieldNameTextBox.Text.Trim();

                //this.ToSetBandvalues();
                return true;
                //}
            }

            return false;
        }

        /// <summary>
        /// To set the band Values
        /// </summary>
        private void ToSetBandvalues()
        {
            /* DataType string     TypeComboBox SelcetdIndex    TypeComboBox Display value                      
                   * System.String               2                            Text
                   * System.Int32                1                            Numberic
                   * System.DateTime             0                            Date                     
            */

            if (this.TypeCombo.SelectedIndex == 2)
            {
                this.band.Columns[this.columnIndex].DataType = typeof(string);
            }
            else if (this.TypeCombo.SelectedIndex == 1)
            {
                this.band.Columns[this.columnIndex].DataType = typeof(Int32);
            }
            else if (this.TypeCombo.SelectedIndex == 0)
            {
                this.band.Columns[this.columnIndex].DataType = typeof(DateTime);
            }

            if (this.LeftRadioButton.Checked)
            {
                this.band.Columns[this.columnIndex].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Left;
            }
            else if (this.RightRadioButton.Checked)
            {
                this.band.Columns[this.columnIndex].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
            }
            else if (this.CenterRadioButton.Checked)
            {
                this.band.Columns[this.columnIndex].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Center;
            }
        }

        #endregion  Methods

        #region Events

        /// <summary>
        /// Handles the TextChanged event of the FieldNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FieldNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.flagFormLoadOnprocess)
            {
                this.flagUnSavedChangesExists = true;
                //if (!this.flagFormDataModify)
                //{
                //    this.newColumnCreated = true;
                //}
            }
        }

        /// <summary>
        /// Handles the Leave event of the FieldNameTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FieldNameTextBox_Leave(object sender, EventArgs e)
        {
            //bool isValidValue = IsValid(this.FieldNameTextBox.Text);

            //if (!isValidValue)
            //{
            //    MessageBox.Show("Give Valid Column Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //this.FieldNameTextBox.Focus();
            //}
            this.FieldNameTextBox.Text = this.FieldNameTextBox.Text.Trim();
        }
        /// <summary>
        /// Handles the CheckedChanged event of the RadioButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.flagFormLoadOnprocess)
            {
                this.flagUnSavedChangesExists = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the F9035CancelButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9035CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.avoidFormClose = false;
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.Display, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the F9035OKButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9035OKButton_Click(object sender, EventArgs e)
        {
            bool isValidValue = IsValid(this.FieldNameTextBox.Text);

            if (!isValidValue)
            {
                MessageBox.Show("Enter Valid Field Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //this.FieldNameTextBox.Focus();
                this.flagUnSavedChangesExists = true;
                this.avoidFormClose = true;
            }
            else
            {
                if (!this.newColumnCreated)//(this.formatChanged && this.newColumnCreated)
                {
                    ////this.SetFormat();
                    if (string.IsNullOrEmpty(this.FieldNameTextBox.Text.Trim()) || string.IsNullOrEmpty(this.FormulaTextBox.Text.Trim()) || string.IsNullOrEmpty(this.TypeCombo.Text.Trim()))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.avoidFormClose = true;
                        this.flagUnSavedChangesExists = false;
                    }
                    else
                    {
                        bool columnCreated = this.CreateColumnHelper();
                        if (columnCreated && !string.IsNullOrEmpty(this.FormulaTextBox.Text.Trim()))
                        {
                            this.flagUnSavedChangesExists = false;
                            this.avoidFormClose = false;
                        }
                        else
                        {
                            this.flagUnSavedChangesExists = false;
                            this.avoidFormClose = true;
                        }
                    }
                }

                ////To modify the Current Data values
                if (this.flagFormDataModify || this.flagUnSavedChangesExists)
                {
                    if (string.IsNullOrEmpty(this.FieldNameTextBox.Text.Trim()) || string.IsNullOrEmpty(this.FormulaTextBox.Text.Trim()) || string.IsNullOrEmpty(this.TypeCombo.Text.Trim()))
                    {
                        MessageBox.Show(SharedFunctions.GetResourceString("RequiredField"), ConfigurationWrapper.ApplicationSave, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.avoidFormClose = true;
                        this.flagUnSavedChangesExists = false;
                    }
                    else
                    {
                        bool modified = this.ModifyColumnDataValue();

                        if (modified)
                        {
                            this.flagUnSavedChangesExists = false;
                            ///this.flagFormDataModify = false;
                            this.avoidFormClose = false;
                        }
                        else
                        {
                            this.flagUnSavedChangesExists = true;
                            this.avoidFormClose = true;
                        }
                    }
                }
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
        /// Handles the TextChanged event of the FormatTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FormatTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.flagFormLoadOnprocess)
            {
                this.formatChanged = true;
                this.flagUnSavedChangesExists = true;
            }
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the TypeCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TypeCombo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.formatChanged = true;
            this.flagUnSavedChangesExists = true;
            if (this.TypeCombo.SelectedItem.DisplayText.Equals("Number") || this.TypeCombo.SelectedItem.DisplayText.Equals("Date"))
            {
                this.FormatTextBox.Text = string.Empty;
                this.label3.Enabled = true;
                this.FormatTextBox.LockKeyPress = false;
            }
            else
            {
                this.FormatTextBox.Text = string.Empty;
                this.label3.Enabled = false;
                this.FormatTextBox.LockKeyPress = true;
            }
        }

        /// <summary>
        /// Handles the Enter event of the TypeCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TypeCombo_Enter(object sender, EventArgs e)
        {
            TypeCombo.Parent.BackColor = Color.FromArgb(255, 255, 121);
        }

        /// <summary>
        /// Handles the Leave event of the TypeCombo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void TypeCombo_Leave(object sender, EventArgs e)
        {
            TypeCombo.Parent.BackColor = Color.White;
        }

        /// <summary>
        /// Handles the FormClosing event of the F9035 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.Windows.Forms.FormClosingEventArgs"/> instance containing the event data.</param>
        private void F9035_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool formClose = true;

            if (!this.avoidFormClose)
            {
                if (this.flagUnSavedChangesExists)
                {
                    ////here when new column is added 
                    if (this.formatChanged && this.newColumnCreated)
                    {
                        ////When unsaved changes exists and F9035 form cancel button is clicked following message is raised
                        if (MessageBox.Show(SharedFunctions.GetResourceString("Discard"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (this.createdColumn != null)
                            {
                                this.band.Columns.Remove(this.createdColumn);
                            }

                            this.flagUnSavedChangesExists = false;
                            e.Cancel = false;
                        }
                        else
                        {
                            ////cancel will not close the form
                            e.Cancel = true;
                        }

                        formClose = false;
                    }

                    ////When unsaved changes exists and F9035 form cancel button is clicked following message is raised
                    if (formClose)
                    {
                        if (this.flagFormDataModify)
                        {
                            if (MessageBox.Show(SharedFunctions.GetResourceString("Discard"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                this.AssignColumnValues();
                                this.flagFormDataModify = false;
                                this.flagUnSavedChangesExists = false;
                                e.Cancel = false;
                            }
                            else
                            {
                                ////cancel will not close the form
                                e.Cancel = true;
                            }

                            formClose = false;
                        }
                    }

                    if (formClose)
                    {
                        /////when Unsaved change exists
                        if (MessageBox.Show(SharedFunctions.GetResourceString("Discard"), ConfigurationWrapper.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.flagUnSavedChangesExists = false;
                            this.flagFormDataModify = false;
                            this.flagUnSavedChangesExists = false;
                            e.Cancel = false;
                        }
                        else
                        {
                            ////cancel will not close the form
                            e.Cancel = true;
                        }

                        formClose = false;
                    }
                }
            }
            else
            {
                this.avoidFormClose = false;
                this.flagUnSavedChangesExists = true;
                ////cancel will not close the form
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Handles the Load event of the F9035 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void F9035_Load(object sender, EventArgs e)
        {
            this.flagFormLoadOnprocess = true;

            this.availableTypeData = new CommonData();

            ////to Load the dataType combo box
            this.LoadTypeCombo();

            if (this.flagFormDataModify)
            {
                this.AssignColumnValues();
                this.newColumnCreated = true;
            }
            else
            {
                //// select  defualt radio button
                if (!string.IsNullOrEmpty(this.TypeCombo.SelectedItem.DisplayText))
                {
                    if (this.TypeCombo.SelectedItem.DisplayText.Equals("Number") || this.TypeCombo.SelectedItem.DisplayText.Equals("Date"))
                    {
                        this.FormatTextBox.LockKeyPress = false;
                        this.label3.Enabled = true;
                    }
                    else
                    {
                        this.FormatTextBox.LockKeyPress = true;
                        this.label3.Enabled = false;
                    }
                }

                this.LeftRadioButton.Checked = true;
            }

            this.flagFormLoadOnprocess = false;
        }

        /// <summary>
        /// Handles the EditorButtonClick event of the FormulaTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinEditors.EditorButtonEventArgs"/> instance containing the event data.</param>
        private void FormulaTextBox_EditorButtonClick(object sender, Infragistics.Win.UltraWinEditors.EditorButtonEventArgs e)
        {
            if (!this.flagFormDataModify)
            {
                this.CreateColumnHelper();

                try
                {
                    if (null == this.createdColumn)
                    {
                        return;
                    }
                    else
                    {
                        //this.newColumnCreated = true;
                        this.flagUnSavedChangesExists = true;
                    }

                    // In order to design a formula, CalcManager property of the grid must
                    // be assigned an instance of a calc manager. If none is assigned then
                    // create a new one and assign it.

                    if (null == this.createdColumn.Layout.Grid.CalcManager)
                    {
                        this.createdColumn.Layout.Grid.CalcManager = new UltraCalcManager();
                    }
                   
                    this.ShowFormulaBuilderDialog(this.createdColumn as IFormulaProvider);
                    ////Ends here
                    this.formatChanged = true;
                    this.flagUnSavedChangesExists = true;
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
                }
            }
            else
            {
                try
                {
                    //// In order to design a formula, CalcManager property of the grid must
                    //// be assigned an instance of a calc manager. If none is assigned then
                    //// create a new one and assign it.
                    if (null == this.band.Columns[this.columnIndex].Layout.Grid.CalcManager)
                    {
                        this.band.Columns[this.columnIndex].Layout.Grid.CalcManager = new UltraCalcManager();
                    }

                    this.ShowFormulaBuilderDialog(this.band.Columns[this.columnIndex] as IFormulaProvider);
                    ////Ends here
                    this.formatChanged = true;
                    this.flagUnSavedChangesExists = true;
                }
                catch (Exception ex)
                {
                    ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this);
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

        ////Added by Latha
        #region ShowFormulaBuilderDialog method

        /// <summary>
        /// Shows the formula builder dialog.
        /// </summary>
        /// <param name="formulaProvider">The formula provider.</param>
        private void ShowFormulaBuilderDialog(IFormulaProvider formulaProvider)
        {
            ////Declare a new FormulaBuilderDialog
            FormulaBuilderDialog formulaBuilderDialog = new FormulaBuilderDialog(formulaProvider);
            formulaBuilderDialog.FunctionInitializing += new FunctionInitializingEventHandler(this.FormulaBuilder_FunctionInitializing);
            formulaBuilderDialog.OperandInitializing += new OperandInitializingEventHandler(formulaBuilderDialog_OperandInitializing);

            try
            {
                //// Show the dialog
                DialogResult dialResult = formulaBuilderDialog.ShowDialog(this);

                if (DialogResult.OK == dialResult)
                {
                    if (!this.flagFormDataModify)
                    {
                        this.FormulaTextBox.Text = formulaBuilderDialog.Formula;
                        this.createdColumn.Formula = this.FormulaTextBox.Text;
                        this.createdColumn.Format = this.FormatTextBox.Text;

                        object type = this.createdColumn.DataType.FullName;
                    }
                    else
                    {
                        this.FormulaTextBox.Text = formulaBuilderDialog.Formula;
                        this.band.Columns[this.columnIndex].Formula = this.FormulaTextBox.Text;
                        this.band.Columns[this.columnIndex].Format = this.FormatTextBox.Text;
                    }
                }
            }
            finally
            {
                //// Disconnect from the events
                formulaBuilderDialog.FunctionInitializing -= new FunctionInitializingEventHandler(this.FormulaBuilder_FunctionInitializing);
            }
        }

        private void formulaBuilderDialog_OperandInitializing(object sender, OperandInitializingEventArgs e)
        {
            //throw new NotImplementedException();
            if (e.OperandName.ToString().Equals("\\ "))
            {
                e.Cancel = true;
            }
        }

        #endregion ShowFormulaBuilderDialog method

        #region FormulaBuilder_FunctionInitializing
        //// This event fires for each function added to the list
        //// of functions in the FormulaBuilder and provides the 
        //// opportunity to cancel them. 

        /// <summary>
        /// Handles the FunctionInitializing event of the formulaBuilder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:Infragistics.Win.UltraWinCalcManager.FormulaBuilder.FunctionInitializingEventArgs"/> instance containing the event data.</param>
        private void FormulaBuilder_FunctionInitializing(object sender, FunctionInitializingEventArgs e)
        {
            switch (e.Function.Category)
            {
                case "Financial":
                    e.Cancel = true;
                    break;
                case "Engineering":
                    e.Cancel = true;
                    break;
                case "LookupAndReference":
                    e.Cancel = true;
                    break;
                case "DateAndTime":
                    if (!e.Function.Name.Equals("datediff") && !e.Function.Name.Equals("month")
                        && !e.Function.Name.Equals("year") && !e.Function.Name.Equals("now"))
                    {
                        e.Cancel = true;
                    }
                    break;
                case "Information":
                    if (!e.Function.Name.Equals("isnumber") && !e.Function.Name.Equals("null"))
                    {
                        e.Cancel = true;
                    }
                    break;
                case "Logical":
                    if (e.Function.Name.Equals("TRUE"))
                    {
                        e.Cancel = true;
                    }
                    break;
                case "Math":
                    if (!e.Function.Name.Equals("abs") && !e.Function.Name.Equals("mod")
                        && !e.Function.Name.Equals("pi") && !e.Function.Name.Equals("round")
                        && !e.Function.Name.Equals("sqrt"))
                    {
                        e.Cancel = true;
                    }
                    break;
                case "Statistical":
                    e.Cancel = true;
                    break;
                case "TextAndData":
                    if (e.Function.Name.Equals("char") || e.Function.Name.Equals("code"))
                    {
                        e.Cancel = true;
                    }
                    break;
            }
        }

        #endregion FormulaBuilder_FunctionInitializing

        /// <summary>
        /// Handles the TextChanged event of the FormulaTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="T:System.EventArgs"/> instance containing the event data.</param>
        private void FormulaTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.flagFormLoadOnprocess)
            {
                this.hasFormulaChanged = true;
                this.formatChanged = true;
                this.flagUnSavedChangesExists = true;
            }
        }

        private void NewColCalcManager_FormulaSyntaxError(object sender, FormulaSyntaxErrorEventArgs e)
        {

        }

        ////Ends here
        #endregion Events
    }
}