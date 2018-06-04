

namespace D3230
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    public partial class ListBoxForm : Form
    {

        #region Variable

        /// <summary>
        /// fileMissing
        /// </summary>
        private bool fileMissing;

               /// <summary>
        /// Missed AttachmentDataTable
        /// </summary>
        private DataTable missedAttachmentTable;

        /// <summary>
        /// Missed Sketch DataTable
        /// </summary>
        private DataTable missedSketchTable; 

        #endregion Variable


        public ListBoxForm()
        {
            InitializeComponent();
        }

        #region Constructor

        /// <summary>
        /// Progressform
        /// </summary>
        public ListBoxForm(bool value)
        {
            this.InitializeComponent();
            this.fileMissing = value;
            this.OkButton.Enabled = true; 
        }

        #endregion



        #region Property

        public DataTable MissedAttachTable
        {
            set
            {
                this.missedAttachmentTable = value;
                this.OkButton.Enabled = true;
                this.CustomizeAttachmentGridView(); 
              this.MissedAttachmentsGridView.DataSource = this.missedAttachmentTable.DefaultView;
              if (this.MissedAttachmentsGridView.OriginalRowCount > 5)
              {
                  this.verticalScrollBar.Visible = false;
              }
            }

            get
            {
                return this.missedAttachmentTable;
            }
        }
        /// <summary>
        /// Gets or sets the process duration.
        /// </summary>
        /// <value>The process duration.</value>
        public DataTable MissedSketchTable
        {
            set
            {
                this.missedSketchTable = value;
                this.OkButton.Enabled = true;
                this.MissedSketchDataGridView.DataSource = this.missedSketchTable.DefaultView;
                if (this.MissedSketchDataGridView.OriginalRowCount > 5)
                {
                    this.verticalScrollBar1.Visible = false;
                }
                this.OkButton.Enabled = true;
            }

            get
            {
                return this.missedSketchTable;
            }
        }


        #endregion property

        #region Methods

        /// <summary>
        /// AttachmentGridView
        /// </summary>
        private void CustomizeAttachmentGridView()
        {
            this.MissedAttachmentsGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.MissedAttachmentsGridView.Columns;
            columns[0].DataPropertyName = "FileID";
            columns[1].DataPropertyName = "AURL";
            columns[2].DataPropertyName = "Form";
            columns[3].DataPropertyName = "KeyID";
            columns[4].DataPropertyName = "Extension";
            columns[5].DataPropertyName = "FileTypeID";
            columns[6].DataPropertyName = "Source";
            columns[7].DataPropertyName = "IsPrimary";
            columns[8].DataPropertyName = "Description";
            columns[9].DataPropertyName = "EventDate";
            columns[10].DataPropertyName = "UserID";
            columns[11].DataPropertyName = "IsPublic";
            columns[12].DataPropertyName = "IsRoll";
            columns[13].DataPropertyName = "ModuleID";
            columns[14].DataPropertyName = "InsertedBy";
            columns[15].DataPropertyName = "InsertedDate";
            columns[16].DataPropertyName = "UpdatedBy";
            columns[17].DataPropertyName = "IsDeleted";
            columns[18].DataPropertyName = "LinkType";
            columns[19].DataPropertyName = "PFileID";
            columns[20].DataPropertyName = "UpdatedDate";
            columns[21].DataPropertyName = "SketchPage";
            
            this.FileID.DisplayIndex = 0;
            this.AURL.DisplayIndex = 1;
            this.Form.DisplayIndex = 2;
            this.KeyID.DisplayIndex = 3;
            this.Extension.DisplayIndex = 4;
            this.FileTypeID.DisplayIndex = 5;
            this.Source.DisplayIndex = 6;
            this.IsPrimary.DisplayIndex = 7;
            this.Description.DisplayIndex = 8;
            this.EventDate.DisplayIndex = 9;
            this.UserID.DisplayIndex = 10;
            this.IsPublic.DisplayIndex = 11;
            this.IsRoll.DisplayIndex = 12;
            this.ModuleID.DisplayIndex = 13;
            this.InsertedBy.DisplayIndex = 14;
            this.InsertedDate.DisplayIndex = 15;
            this.UpdatedBy.DisplayIndex = 16;
            this.IsDeleted.DisplayIndex = 17;
            this.LinkType.DisplayIndex = 18;
            this.PFileID.DisplayIndex = 19;
            this.UpdatedDate.DisplayIndex = 20;
            this.SketchPage.DisplayIndex = 21;
 
         
        }


        /// <summary>
        /// SketchGridView
        /// </summary>
        private void CustomizeSketchGridView()
        {
            this.MissedSketchDataGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.MissedSketchDataGridView.Columns;
            columns[0].DataPropertyName = "FileID";
            columns[1].DataPropertyName = "AURL";
            columns[2].DataPropertyName = "Form";
            columns[3].DataPropertyName = "KeyID";
            columns[4].DataPropertyName = "Extension";
            columns[5].DataPropertyName = "FileTypeID";
            columns[6].DataPropertyName = "Source";
            columns[7].DataPropertyName = "IsPrimary";
            columns[8].DataPropertyName = "Description";
            columns[9].DataPropertyName = "EventDate";
            columns[10].DataPropertyName = "UserID";
            columns[11].DataPropertyName = "IsPublic";
            columns[12].DataPropertyName = "IsRoll";
            columns[13].DataPropertyName = "ModuleID";
            columns[14].DataPropertyName = "InsertedBy";
            columns[15].DataPropertyName = "InsertedDate";
            columns[16].DataPropertyName = "UpdatedBy";
            columns[17].DataPropertyName = "IsDeleted";
            columns[18].DataPropertyName = "LinkType";
            columns[19].DataPropertyName = "PFileID";
            columns[20].DataPropertyName = "UpdatedDate";
            columns[21].DataPropertyName = "SketchPage";

            this.dataGridViewTextBoxColumn1.DisplayIndex = 0;
            this.dataGridViewTextBoxColumn2.DisplayIndex = 1;
            this.dataGridViewTextBoxColumn3.DisplayIndex = 2;
            this.dataGridViewTextBoxColumn4.DisplayIndex = 3;
            this.dataGridViewTextBoxColumn5.DisplayIndex = 4;
            this.dataGridViewTextBoxColumn6.DisplayIndex = 5;
            this.dataGridViewTextBoxColumn7.DisplayIndex = 6;
            this.dataGridViewTextBoxColumn8.DisplayIndex = 7;
            this.dataGridViewTextBoxColumn9.DisplayIndex = 8;
            this.dataGridViewTextBoxColumn10.DisplayIndex = 9;
            this.dataGridViewTextBoxColumn11.DisplayIndex = 10;
            this.dataGridViewTextBoxColumn12.DisplayIndex = 11;
            this.dataGridViewTextBoxColumn13.DisplayIndex = 12;
            this.dataGridViewTextBoxColumn14.DisplayIndex = 13;
            this.dataGridViewTextBoxColumn15.DisplayIndex = 14;
            this.dataGridViewTextBoxColumn16.DisplayIndex = 15;
            this.dataGridViewTextBoxColumn17.DisplayIndex = 16;
            this.dataGridViewTextBoxColumn18.DisplayIndex = 17;
            this.dataGridViewTextBoxColumn19.DisplayIndex = 18;
            this.dataGridViewTextBoxColumn20.DisplayIndex = 19;
            this.dataGridViewTextBoxColumn21.DisplayIndex = 20;
            this.dataGridViewTextBoxColumn22.DisplayIndex = 21;

        }
        #endregion Methods
        /// <summary>
        /// OK Button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkButton_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void ListBoxForm_Load(object sender, EventArgs e)
        {
            if (this.fileMissing)
            {
                if (this.missedAttachmentTable != null)
                {
                    this.CustomizeAttachmentGridView();
                    if (this.missedAttachmentTable.Rows.Count > 0)
                    {
                        this.MissedAttachmentsGridView.DataSource = this.missedAttachmentTable.DefaultView;
                        if (this.MissedAttachmentsGridView.OriginalRowCount > 5)
                        {
                            this.verticalScrollBar.Visible = false;
                        }
                    }

                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        this.MissedAttachmentsGridView.Rows.Add();
                    }
                }
                if (this.missedSketchTable != null)
                {
                    this.CustomizeSketchGridView();
                    if (this.missedSketchTable.Rows.Count > 0)
                    {
                        this.MissedSketchDataGridView.DataSource = this.missedSketchTable.DefaultView;
                        if (this.MissedSketchDataGridView.OriginalRowCount > 5)
                        {
                            this.verticalScrollBar1.Visible = false;
                        }
                    }
                    
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        this.MissedSketchDataGridView.Rows.Add();
                    }
                     
                }
                this.OkButton.Enabled = true; 
                this.Show();
            }
        }
    }
}
