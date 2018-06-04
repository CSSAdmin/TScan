namespace TerraScan.BusinessEntities {

    using System.Data;

    partial class ExciseTaxStatementData
    {
        /// <summary>
        /// Initializes the excise tax receipt with the default value.
        /// </summary>
        public void InitializeExciseTaxReceipt()
        {            
            this.tableGetExciseTaxReceipt.Rows.Clear();
            ////initialize three rows
            for (int i = 0; i < 3; i++)
            {
                DataRow dr = this.tableGetExciseTaxReceipt.NewRow();
                this.tableGetExciseTaxReceipt.Rows.Add(dr);
            }

            this.tableGetExciseTaxReceipt.Rows[0][this.tableGetExciseTaxReceipt.ItemColumn] = "Tax";
            this.tableGetExciseTaxReceipt.Rows[0][this.tableGetExciseTaxReceipt.FeeTypeColumn] = "Technology";
            this.tableGetExciseTaxReceipt.Rows[1][this.tableGetExciseTaxReceipt.ItemColumn] = "Interest";
            this.tableGetExciseTaxReceipt.Rows[1][this.tableGetExciseTaxReceipt.FeeTypeColumn] = "Transaction";
            this.tableGetExciseTaxReceipt.Rows[2][this.tableGetExciseTaxReceipt.ItemColumn] = "Penalty";

            this.tableGetExciseTaxReceipt.AcceptChanges();
        }
    }
}
