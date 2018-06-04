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
    public partial class Correction : UserControl
    {
        private DataTable correctionDataSet;

        
        #region properities
        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>The label text.</value>
        public DataTable CorrectionDataSet
        {
            get
            {
                return this.correctionDataSet;
            }
            set
            {
                this.correctionDataSet = value;
            }
        }
        #endregion properities


        public Correction()
        {
            InitializeComponent();
        }

        private void CustomizeCorrectionGridView()
        {
            this.CorrectionGridView.AutoGenerateColumns = false;
            DataGridViewColumnCollection columns = this.CorrectionGridView.Columns;
            columns[0].DataPropertyName = "Date";
            columns[1].DataPropertyName = "Type";
            columns[2].DataPropertyName = "prvval";
            columns[3].DataPropertyName = "newval";
            columns[4].DataPropertyName = "origtax";
            columns[5].DataPropertyName = "newtax";
            columns[6].DataPropertyName = "change";
            columns[7].DataPropertyName = "Note";

            this.Date.DisplayIndex = 0;
            this.Type.DisplayIndex = 1;
            this.prvval.DisplayIndex = 2;
            this.newval.DisplayIndex = 3;
            this.origtax.DisplayIndex = 4;
            this.newtax.DisplayIndex = 5;
            this.change.DisplayIndex = 6;
            this.Note.DisplayIndex = 7;
            
              

            //this.Date.Width = 100;
            //this.Type.Width = 140;
            //this.PriorValue.Width = 100;
            //this.NewTax.Width = 100;
            //this.NewValue.Width = 100;
            //this.PriorTax.Width = 100;
            //this.Change.Width = 100;
        }

        private void Correction_Load(object sender, EventArgs e)
        {
            if (this.correctionDataSet != null)
            {
                this.CustomizeCorrectionGridView();
                this.correctionDataSet.AcceptChanges();

                
                this.CorrectionGridView.DataSource = this.correctionDataSet.DefaultView;
                if (this.CorrectionGridView.OriginalRowCount > 8)
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
