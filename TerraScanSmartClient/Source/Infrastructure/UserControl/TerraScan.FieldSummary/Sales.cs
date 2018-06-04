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
    public partial class SalesUserControl : UserControl
    {

        private DataTable saleDataSet;

        #region properities
        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public DataTable SaleDataSet
        {
            get
            {
                return this.saleDataSet;
            }
            set
            {
                this.saleDataSet = value;
            }
        }
        #endregion properities


        public SalesUserControl()
        {
            InitializeComponent();
        }


        private void CustomizeSaleGridView()
        {
            this.SalesGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.SalesGridView.Columns;
            columns[0].DataPropertyName = "SaleDate";
            columns[1].DataPropertyName = "BookPage";
            columns[2].DataPropertyName = "ttlprcls";
            columns[3].DataPropertyName = "Grantor";
            columns[4].DataPropertyName = "SalePrice";
            columns[5].DataPropertyName = "SaleStudy";
            columns[6].DataPropertyName = "Grantee";
            columns[7].DataPropertyName = "EventID";
            columns[8].DataPropertyName = "ThrowAway";
            columns[9].DataPropertyName = "SaleID";
            columns[10].DataPropertyName = "SalePrice1";

            this.SaleDate.DisplayIndex = 0;
            this.BookPage.DisplayIndex = 1;
            this.ttlprcls.DisplayIndex = 2;
            this.Grantor.DisplayIndex = 3;
            this.SalePrice.DisplayIndex = 4;
            this.SaleStudy.DisplayIndex = 5;
            this.Grantee.DisplayIndex = 6;
            this.EventID.DisplayIndex = 7;
            this.ThrowAway.DisplayIndex = 8;
            this.SaleID.DisplayIndex = 9;
            this.SalePrice1.DisplayIndex = 10;

            //this.SaleDate.Width = 90;
            //this.BookPage.Width = 100;
            //this.Parcels.Width =78;
            //this.Grant.Width =275;
            //this.SalePrice.Width =100;
            //this.SalesStudy.Width =100;


            
        }

        private void SalesUserControl_Load(object sender, EventArgs e)
        {
            if(this.saleDataSet!=null)
            {
                    this.CustomizeSaleGridView();
                    this.saleDataSet.AcceptChanges();
                    //DataTable ds;
                    //ds = this.saleDataSet.DefaultView.ToTable();   
                    this.SalesGridView.DataSource =this.saleDataSet.DefaultView; 
                    if (this.SalesGridView.OriginalRowCount > 8)
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
