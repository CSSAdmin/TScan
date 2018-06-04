
namespace D91000
{

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Web.Services.Protocols;
    using System.Data;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.SmartParts;
    using TerraScan.BusinessEntities;
    using System.Configuration;
    using TerraScan.UI.Controls;
    using TerraScan.Common;
    using TerraScan.Common.Reports;
    using TerraScan.Utilities;
    using System.IO;

    public partial class F9105 : Form
    {

        /// <summary>
        /// controller F1502Controller
        /// </summary>
        private F9105Controller form9105Control;

        F96000OwnerManagementData CountryDetailsData = new F96000OwnerManagementData();

        private CommonData activeComboData = new CommonData();
        private CommonData activeComboGridData = new CommonData();

        /// <summary>
        /// the typed dataset for yes no combo box
        /// </summary>
        private CommonData priorityComboGridData = new CommonData();        

        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        private int OwnerID;

        DataTable BaseTable = new DataTable();
        private int parentForm;
        public F9105()
        {
            InitializeComponent();
        }

        public F9105(DataTable CopyTable,int parentFormId)
        {
            InitializeComponent();
            this.BaseTable = CopyTable;
            this.parentForm = parentFormId;
        }

        #region Property

        /// <summary>
        /// For F8901Control
        /// </summary>
        [CreateNew]
        public F9105Controller Form9105Control
        {
            get { return this.form9105Control as F9105Controller; }
            set { this.form9105Control = value; }
        }       

        /// <summary>
        /// Gets or sets the command result.
        /// </summary>
        /// <value>The command result.</value>
        public string CommandResult
        {
            get { return commandResult; }
            set { commandResult = value; }
        }

        #endregion Property

        #region Events

        /// <summary>
        /// Handles the Load event of the F9105 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void F9105_Load(object sender, EventArgs e)
        {
            this.CustomizeCombobox();
            this.LoadCountryComboBox();
            this.LoadFormDetails();
            if (!string.IsNullOrEmpty(this.LastNameTextBox.Text.Trim()))
            {
                this.CreateButton.Enabled = true;
            }
            else
            {
                this.CreateButton.Enabled = false;
            }
            this.CancelButton.Enabled = true;
        }

        /// <summary>
        /// Handles the Click event of the CreateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void CreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.BaseTable.Rows.Count > 0)
                {
                    this.BaseTable.Rows[0]["LastName"] = this.LastNameTextBox.Text.Trim();
                    this.BaseTable.Rows[0]["FirstName"] = this.FirstNameTextBox.Text.Trim();
                    this.BaseTable.Rows[0]["OwnerCode"] = this.OwnerCodeTextBox.Text.Trim();
                    this.BaseTable.Rows[0]["Address1"] = this.Address1TextBox.Text.Trim();
                    this.BaseTable.Rows[0]["Address2"] = this.Address2TextBox.Text.Trim();
                    this.BaseTable.Rows[0]["City"] = this.CityTextBox.Text.Trim();
                    this.BaseTable.Rows[0]["State"] = this.StateTextBox.Text.Trim();
                    this.BaseTable.Rows[0]["Zip"] = this.ZipCodeTextBox.Text.Trim();
                    this.BaseTable.Rows[0]["Phone"] = this.PhoneNumberTextBox.Text.Trim();
                    this.BaseTable.Rows[0]["Email"] = this.EmailAddressTextBox.Text.Trim();
                    if (this.CountryComboBox.SelectedValue != null)
                    {
                        this.BaseTable.Rows[0]["CountryID"] = this.CountryComboBox.SelectedValue;
                    }
                    else
                    {
                        this.BaseTable.Columns.Remove("CountryID");
                    }
                    this.BaseTable.Rows[0]["IsActive"] = this.ActiveCombo.SelectedValue;
                    this.BaseTable.AcceptChanges();
                    string copyData = ConvertToXML(this.BaseTable);
                    int returnValue = this.form9105Control.WorkItem.F9105_ExecuteCopyName(copyData, TerraScanCommon.UserId);                    
                    this.commandResult = returnValue.ToString();
                    this.DialogResult = DialogResult.OK;
                    this.Close(); 
                }
            }           
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }

        /// <summary>
        /// Converts to XML.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        private string ConvertToXML(DataTable table)
        {
            DataTable dt = table.Copy();
            dt.Namespace = "";
            dt.TableName = "Table";
            MemoryStream str = new MemoryStream();
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string xmlstr;
            xmlstr = sr.ReadToEnd();
            xmlstr = xmlstr.Replace("DocumentElement", "Root");
            return (xmlstr);
        }

        /// <summary>
        /// Customizes the combobox.
        /// </summary>
        private void CustomizeCombobox()
        {
            ////which loads yes, no value to the DataSet
            this.activeComboData.LoadYesNoValue();
            this.activeComboGridData.LoadYesNoValue();
            this.priorityComboGridData.LoadYesNoValue();
            this.ActiveCombo.DataSource = this.activeComboData.ComboBoxDataTable;
            this.ActiveCombo.ValueMember = this.activeComboData.ComboBoxDataTable.KeyIdColumn.ColumnName;
            this.ActiveCombo.DisplayMember = this.activeComboData.ComboBoxDataTable.KeyNameColumn.ColumnName;
            this.ActiveCombo.SelectedValue = 1;
        }

        /// <summary>
        /// Cencel Button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CencelButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        } 
        #endregion

        #region Methods

        /// <summary>
        /// Loads the form details.
        /// </summary>
        internal void LoadFormDetails()
        {
            try
            {
                if (this.BaseTable.Rows.Count > 0)
                {
                    this.OwnerID = Convert.ToInt32(this.BaseTable.Rows[0]["OwnerID"].ToString());
                    if (!string.IsNullOrEmpty(this.BaseTable.Rows[0]["LastName"].ToString()))
                    {
                        this.LastNameTextBox.Text = this.BaseTable.Rows[0]["LastName"].ToString();
                    }
                    else
                    {
                        this.LastNameTextBox.Text = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(this.BaseTable.Rows[0]["FirstName"].ToString()))
                    {
                        this.FirstNameTextBox.Text = this.BaseTable.Rows[0]["FirstName"].ToString();
                    }
                    else
                    {
                        this.FirstNameTextBox.Text = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(this.BaseTable.Rows[0]["OwnerCode"].ToString()))
                    {
                        this.OwnerCodeTextBox.Text = this.BaseTable.Rows[0]["OwnerCode"].ToString();
                    }
                    else
                    {
                        this.OwnerCodeTextBox.Text = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(this.BaseTable.Rows[0]["Address1"].ToString()))
                    {
                        this.Address1TextBox.Text = this.BaseTable.Rows[0]["Address1"].ToString();
                    }
                    else
                    {
                        this.Address1TextBox.Text = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(this.BaseTable.Rows[0]["Address2"].ToString()))
                    {
                        this.Address2TextBox.Text = this.BaseTable.Rows[0]["Address2"].ToString();
                    }
                    else
                    {
                        this.Address2TextBox.Text = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(this.BaseTable.Rows[0]["City"].ToString()))
                    {
                        this.CityTextBox.Text = this.BaseTable.Rows[0]["City"].ToString();
                    }
                    else
                    {
                        this.CityTextBox.Text = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(this.BaseTable.Rows[0]["State"].ToString()))
                    {
                        this.StateTextBox.Text = this.BaseTable.Rows[0]["State"].ToString();
                    }
                    else
                    {
                        this.StateTextBox.Text = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(this.BaseTable.Rows[0]["Zip"].ToString()))
                    {
                        this.ZipCodeTextBox.Text = this.BaseTable.Rows[0]["Zip"].ToString();
                    }
                    else
                    {
                        this.ZipCodeTextBox.Text = string.Empty;
                    }

                    //this.LoadActiveCombo();

                    if (!string.IsNullOrEmpty(this.BaseTable.Rows[0]["Phone"].ToString()))
                    {
                        this.PhoneNumberTextBox.Text = this.BaseTable.Rows[0]["Phone"].ToString();
                    }
                    else
                    {
                        this.PhoneNumberTextBox.Text = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(this.BaseTable.Rows[0]["Email"].ToString()))
                    {
                        this.EmailAddressTextBox.Text = this.BaseTable.Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        this.EmailAddressTextBox.Text = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(this.BaseTable.Rows[0]["CountryID"].ToString()))
                    {
                        this.CountryComboBox.SelectedValue = this.BaseTable.Rows[0]["CountryID"].ToString();
                    }
                    else
                    {
                        this.CountryComboBox.SelectedIndex = -1;
                        this.CountryComboBox.Text = string.Empty;
                        this.CountryComboBox.SelectedText = string.Empty;
                    }

                    if (Convert.ToBoolean(this.BaseTable.Rows[0]["IsActive"]))
                    {
                        this.ActiveCombo.SelectedValue = 1;
                    }
                    else
                    {
                        this.ActiveCombo.SelectedValue = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
            
        }

        /// <summary>
        /// Loads the country combo box.
        /// </summary>
        private void LoadCountryComboBox()
        {
            try
            {
                this.CountryDetailsData = this.form9105Control.WorkItem.F96000_CountryComboDetails();
                if (this.CountryDetailsData.f96000_pclst_Country.Rows.Count > 0)
                {
                    this.CountryComboBox.DataSource = this.CountryDetailsData.f96000_pclst_Country;
                    this.CountryComboBox.ValueMember = this.CountryDetailsData.f96000_pclst_Country.CountryIDColumn.ColumnName;
                    this.CountryComboBox.DisplayMember = this.CountryDetailsData.f96000_pclst_Country.CountryNameColumn.ColumnName;

                    this.CountryComboBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                    this.CountryComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
        }
        
        #endregion

        private void LastNameTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.LastNameTextBox.Text.Trim()))
            {
                this.CreateButton.Enabled = false;
            }
            else
            {
                this.CreateButton.Enabled = true;;
            }
        }

    }
}
