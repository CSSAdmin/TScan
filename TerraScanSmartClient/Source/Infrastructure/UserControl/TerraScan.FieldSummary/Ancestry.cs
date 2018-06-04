using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TerraScan.FieldSummary
{
    public partial class Ancestry : UserControl
    {
        private DataTable ancestryDataSet;


        #region properities
        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public DataTable AncestryDataSet
        {
            get
            {
                return this.ancestryDataSet;
            }
            set
            {
                this.ancestryDataSet = value;
            }
        }
        #endregion properities

        public Ancestry()
        {
            InitializeComponent();
        }

        private void CustomizeAncestryGridView()
        {
            this.AncestryGridView.AutoGenerateColumns = false;
            this.AncestryGridView.AllowEmptyRows = true;
            DataGridViewColumnCollection columns = this.AncestryGridView.Columns;

            columns[0].DataPropertyName = "EventDate";
            columns[1].DataPropertyName = "EventType";
            columns[2].DataPropertyName = "Relation";
            columns[3].DataPropertyName = "ParcelNumber";
            columns[4].DataPropertyName = "Status";
            columns[5].DataPropertyName = "ProcessedBy";
            columns[6].DataPropertyName = "EventTypeID";
            columns[7].DataPropertyName = "EventID";
            columns[8].DataPropertyName = "ParcelID";


            this.EventDate.DisplayIndex = 0;
            this.EventType.DisplayIndex = 1;
            this.Relation.DisplayIndex = 2;
            this.ParcelNumber.DisplayIndex = 3;
            this.Status.DisplayIndex = 4;
            this.ProcessedBy.DisplayIndex = 5;
            this.EventTypeID.DisplayIndex = 6;
            this.EventID.DisplayIndex = 7;
            this.ParcelID.DisplayIndex = 8;

            //this.EventDate.Width = 90;
            //this.EventType.Width = 150;
            //this.Relation.Width = 75;
            //this.ParcelNumber.Width = 200;
            //this.Status.Width = 75;
            //this.ProcessedBy.Width = 150;
        }

        private void Ancestry_Load(object sender, EventArgs e)
        {
            if (this.ancestryDataSet != null)
            {
                this.CustomizeAncestryGridView();
                this.AncestryGridView.DataSource = this.ancestryDataSet.DefaultView;
                if (this.AncestryGridView.OriginalRowCount > 8)
                {
                    this.verticalScrollBar.Visible = false;
                    this.verticalScrollBar.Enabled = false;
                }
                else
                {
                    this.verticalScrollBar.Visible = true;
                    this.verticalScrollBar.Enabled = false;
                }
            }
        }
    }
}
