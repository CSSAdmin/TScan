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
    public partial class History : UserControl
    {
        private DataTable historyDataSet;


        #region properities
        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public DataTable HistoryDataSet
        {
            get
            {
                return this.historyDataSet;
            }
            set
            {
                this.historyDataSet = value;
            }
        }
        #endregion properities

        public History()
        {
            InitializeComponent();
        }

        private void CustomizeHistoryGridView()
        {
            this.HistoryGridView.AutoGenerateColumns = false;
            this.HistoryGridView.AllowEmptyRows = true;
            DataGridViewColumnCollection columns = this.HistoryGridView.Columns;
            columns[0].DataPropertyName = "parNum";
            columns[1].DataPropertyName = "rollyear";
            columns[2].DataPropertyName = "posttype";
            columns[3].DataPropertyName = "origval";
            columns[4].DataPropertyName = "origtax";
            columns[5].DataPropertyName = "statementid";
            columns[6].DataPropertyName = "balanceD"; 

            this.parNum.DisplayIndex = 0;
            this.rollyear.DisplayIndex = 1;
            this.posttype.DisplayIndex = 2;
            this.origval.DisplayIndex = 3;
            this.origtax.DisplayIndex = 4;
            this.statementid.DisplayIndex = 5;
            this.balanceD.DisplayIndex = 6; 


            //this.RelatedStatement.Width = 187;
            //this.PostType.Width = 140;
            //this.AssessedValue.Width = 140;
            //this.TaxAmount.Width = 140;
            //this.BalanceDue.Width = 140;
            


        }

        private void History_Load(object sender, EventArgs e)
        {
            if (this.historyDataSet != null)
            {
                this.CustomizeHistoryGridView();
                this.HistoryGridView.DataSource = this.historyDataSet.DefaultView;
                if (this.HistoryGridView.OriginalRowCount > 8)
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

        private void HistoryGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }
    }
}
