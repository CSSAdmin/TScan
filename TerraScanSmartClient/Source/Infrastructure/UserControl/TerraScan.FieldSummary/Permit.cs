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
    public partial class PermitUserControl : UserControl
    {

        private DataTable permitDataSet;

        #region properities
        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public DataTable PermitDataSet
        {
            get
            {
                return this.permitDataSet;
            }
            set
            {
                this.permitDataSet = value;
            }
        }
        #endregion properities

        public PermitUserControl()
        {
            InitializeComponent();
        }

        private void CustomizePermitGridView()
        {
            this.PermitGridView.AutoGenerateColumns = false;
            this.PermitGridView.AllowEmptyRows = true; 
            DataGridViewColumnCollection columns = this.PermitGridView.Columns;
            columns[0].DataPropertyName = "ParcelId";
            columns[1].DataPropertyName = "ParcelNumber";
            columns[2].DataPropertyName = "EventTypeId";
            columns[3].DataPropertyName = "EventDate";
            columns[4].DataPropertyName = "Descr";
            columns[5].DataPropertyName = "PermitNumber";
            columns[6].DataPropertyName = "Description";
            columns[7].DataPropertyName = "Visited";
            columns[8].DataPropertyName = "Closed";
            columns[9].DataPropertyName = "EstValue";
            columns[10].DataPropertyName = "EventId";

            this.ParcelId.DisplayIndex = 0;
            this.ParcelNumber.DisplayIndex = 1;
            this.EventTypeId.DisplayIndex = 2;
            this.EventDate.DisplayIndex = 3;
            this.PermitNumber.DisplayIndex = 4;
            this.Description.DisplayIndex = 5;
            this.Visited.DisplayIndex = 6;
            this.Closed.DisplayIndex = 7;
            this.EstValue.DisplayIndex = 8;
            this.Descr.DisplayIndex = 9;
           this.EventId.DisplayIndex = 10; 

            //this.Opened.Width = 100;
            //this.Permit.Width = 148;
            //this.Description.Width = 200;
            //this.Visited.Width = 100;
            //this.Closed.Width = 100;
            //this.Amount.Width = 100;



        }

        private void PermitUserControl_Load(object sender, EventArgs e)
        {
            if (this.permitDataSet != null)
            {
                this.CustomizePermitGridView();
                this.PermitGridView.DataSource = this.permitDataSet.DefaultView;
                if (this.PermitGridView.OriginalRowCount > 8)
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

        private void PermitGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
