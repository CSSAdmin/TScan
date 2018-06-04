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
    public partial class OwnerShipUserControl : UserControl
    {
        private DataTable ownerShipDataSet;


        #region properities
        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public DataTable OwnerShipDataSet
        {
            get
            {
                return this.ownerShipDataSet;
            }
            set
            {
                this.ownerShipDataSet = value;
            }
        }

        #endregion properities

        public OwnerShipUserControl()
        {
            InitializeComponent();
        }

        private void CustomizeOwnerShipGridView()
        {
            this.OwnershipGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.OwnershipGridView.Columns;
            columns[0].DataPropertyName = "LastFirst";
            columns[1].DataPropertyName = "OwnerPercent";
            columns[2].DataPropertyName = "OwnerType";
            columns[3].DataPropertyName = "OwnerID";
            columns[4].DataPropertyName = "ParcelID";
            columns[5].DataPropertyName = "IsPrimary";
            columns[6].DataPropertyName = "IsTaxPayer";
            columns[7].DataPropertyName = "BackgroundColor";
           
            this.LastFirst.DisplayIndex = 0;
            this.OwnerPercent.DisplayIndex = 1;
            this.OwnerType.DisplayIndex = 2;
            this.OwnerID.DisplayIndex = 3;
            this.ParcelID.DisplayIndex = 4;
            this.IsPrimary.DisplayIndex = 5;
            this.IsTaxPayer.DisplayIndex = 6;
            this.BackgroundColor.DisplayIndex = 7;



            //this.Owner.Width = 265;
            //this.percentage.Width = 100;
            //this.OwnerType.Width = 175;
            //this.Primary.Width = 100;
            //this.TaxPayer.Width = 100;
            
        }

        private void OwnerShipUserControl_Load(object sender, EventArgs e)
        {
            if (this.ownerShipDataSet != null)
            {
                this.CustomizeOwnerShipGridView();
                this.OwnershipGridView.DataSource = this.ownerShipDataSet.DefaultView;
                if (this.OwnershipGridView.OriginalRowCount > 8)
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

        private void OwnershipGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (this.OwnershipGridView.OriginalRowCount > 0)
            {
                //Decimal outDecimal;
                //if (e.ColumnIndex == this.OwnershipGridView.Columns["OwnerPercent"].Index)
                //{
                    
                //         string val = e.Value.ToString();
                //         if (Decimal.TryParse(val, out outDecimal))
                //         {
                //             decimal Percent;
                //             Percent = outDecimal * 100;
                //             e.Value = Percent;
                //         }
                //    }
                }

            }


        }
    }

