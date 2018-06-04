namespace D1500
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using TerraScan.UI.Controls;
    using Microsoft.Practices.CompositeUI.UIElements;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI.EventBroker;
    using Microsoft.Practices.CompositeUI.Utility;
    using Microsoft.Practices.ObjectBuilder;
    using TerraScan.BusinessEntities;
    using TerraScan.Common;
    using TerraScan.Utilities;
    using System.IO;

    public partial class F1407 : Form
    {

        private string baseFormData;
        /// <summary>
        /// Command Result
        /// </summary>
        private string commandResult;

        private string TypeId=string.Empty;
        private string StatusId = string.Empty;
        private string note = string.Empty;
        private string sourceId = string.Empty;
        DataTable dTable = new DataTable();
        DataSet ds = new DataSet();
        DataSet distinctDataSet = new DataSet();
        DataTable distinctVals;


        /// <summary>
        /// controller F1503Controller
        /// </summary>
        private F1407Controller form1407Control;

        F14062StatementPullListData.f1407_GetPullListStatusDataTable StatusTypeDataTable = new F14062StatementPullListData.f1407_GetPullListStatusDataTable();
        F14062StatementPullListData.f1407_GetPullListTypeDataTable TypeDataTable = new F14062StatementPullListData.f1407_GetPullListTypeDataTable();


        #region Property

        /// <summary>
        /// For 1503Control
        /// </summary>
        [CreateNew]
        public F1407Controller Form1407Control
        {
            get { return this.form1407Control as F1407Controller; }
            set { this.form1407Control = value; }
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

        #region EventPublication

        /// <summary>
        /// event publication for Show the child form 
        /// </summary>
        [EventPublication(EventTopics.D9001_ShellForm_NavigationPanelSmartPart_ShowForm, PublicationScope.Global)]
        public event EventHandler<DataEventArgs<FormInfo>> ShowForm;



        #endregion EventPublication

        public F1407()
        {
            InitializeComponent();
        }

        public F1407(DataSet saveContent)
        {
            InitializeComponent();                    
           this.ds = saveContent;
        }
        private void F1407_Load(object sender, EventArgs e)
        {
          
          this.LoadStatusCombo();
          this.LoadTypeCombo();
          if (TypeComboBox.SelectedIndex >= 0 && this.StatusComboBox.SelectedIndex >= 0)
          {
              this.SaveButton.Enabled = true;
          }
          else
          {
              this.SaveButton.Enabled = false;
          }
            //To retain the previous values to the form
          //if (ds.Tables[0].Rows.Count > 0)
          //{
          //   this.NoteTextBox.Text= this.ds.Tables[0].Rows[0]["Note"].ToString() ;
          //   this.TypeComboBox.SelectedValue=  this.ds.Tables[0].Rows[0]["TypeID"] ;
          //   this.StatusComboBox.SelectedValue= this.ds.Tables[0].Rows[0]["StatusID"];
          //}
          //this.TypeComboBox.Focus();
        }

        private void LoadStatusCombo()
        {
            this.StatusTypeDataTable = this.form1407Control.WorkItem.F1407_GetPullListStatus().f1407_GetPullListStatus;
            if (this.StatusTypeDataTable.Rows.Count > 0)
            {
                this.StatusComboBox.DataSource = this.StatusTypeDataTable.DefaultView;
                this.StatusComboBox.DisplayMember = this.StatusTypeDataTable.StatusNameColumn.ColumnName;
                this.StatusComboBox.ValueMember = this.StatusTypeDataTable.StatusIDColumn.ColumnName;
            }
        }

        private void LoadTypeCombo()
        {
            this.TypeDataTable = this.form1407Control.WorkItem.F1407_GetPullListType().f1407_GetPullListType;
            if (this.TypeDataTable.Rows.Count > 0)
            {
                this.TypeComboBox.DataSource = this.TypeDataTable.DefaultView;
                this.TypeComboBox.DisplayMember = this.TypeDataTable.TypeNameColumn.ColumnName;
                this.TypeComboBox.ValueMember = this.TypeDataTable.TypeIDColumn.ColumnName;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ds.Tables[0].Rows.Count > 0)
                {
                    string finalDataSet = string.Empty;
                    this.dTable = this.ds.Tables[0];
                    for (int i = 0; i < this.ds.Tables[0].Rows.Count; i++)
                    {
                        this.ds.Tables[0].Rows[i]["Note"] = this.NoteTextBox.Text;
                        this.ds.Tables[0].Rows[i]["TypeID"] = this.TypeComboBox.SelectedValue;
                        this.ds.Tables[0].Rows[i]["StatusID"] = this.StatusComboBox.SelectedValue;
                    }
                    dTable.AcceptChanges();
                    //DataView view = new DataView(dTable);
                    //this.distinctVals = view.ToTable(true, "TypeID", "StatusID", "ParcelNumber", "StatementNumber", "PostTypeID", "TaxpayerName","Note","SourceID","OwnerID");
                    //this.distinctDataSet.Tables.Add(distinctVals);
                    //finalDataSet = this.distinctDataSet.GetXml();
                    //finalDataSet = finalDataSet.Replace("NewDataSet", "Root");
                    //this.distinctVals = this.distinctDataSet.Tables[0];
                    this.form1407Control.WorkItem.F14062_SaveGridDetails(ds.GetXml(), Convert.ToInt32(TerraScanCommon.UserId));
                }
            }
            catch (Exception ex)
            {

                ExceptionManager.ManageException(ex, ExceptionManager.ActionType.CloseCurrentForm, this.ParentForm);
            }
             
        }


        //private string RemoveXMLItem(ArrayList removeListIds)
        //{
        //    DataTable dt = new DataTable("Table");
        //    dt.Columns.Add("ParcelNumber", typeof(String));
        //    foreach (var r in removeListIds)
        //    {
        //        dt.Rows.Add(r);
        //    }
        //    dt = dt.DefaultView.ToTable("ParcelNumber");

        //    dt.AcceptChanges();
        //    MemoryStream str = new MemoryStream();
        //    dt.WriteXml(str, true);
        //    str.Seek(0, SeekOrigin.Begin);
        //    StreamReader sr = new StreamReader(str);
        //    string removexmlstr;
        //    removexmlstr = sr.ReadToEnd();
        //    removexmlstr = removexmlstr.Replace("DocumentElement", "Root");
        //    // xmlstr = xmlstr.Replace("ParcelTable", "Table");
        //    return (removexmlstr);
        //}

        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TypeComboBox.SelectedIndex >= 0 && this.StatusComboBox.SelectedIndex >= 0)
            {
                this.SaveButton.Enabled = true;
            }
            else
            {
                this.SaveButton.Enabled = false;
            }
        }

        private void StatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TypeComboBox.SelectedIndex >= 0 && this.StatusComboBox.SelectedIndex >= 0)
            {
                this.SaveButton.Enabled = true;
            }
            else
            {
                this.SaveButton.Enabled = false;
            }

        }
    }
}
