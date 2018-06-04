namespace TerraScan.BusinessEntities {

    using System.Data;
    using System.Collections; 

    partial class CommonData
    {
        /// <summary>
        /// default - Loads the yes no value in the combobox.
        /// </summary>
        public void LoadYesNoValue()
        {
            this.tableComboBoxDataTable.Rows.Clear();
            ////initialize two rows
            for (int i = 0; i < 2; i++)
            {
                DataRow dr = this.tableComboBoxDataTable.NewRow();               
                this.tableComboBoxDataTable.Rows.Add(dr);
            }

            this.tableComboBoxDataTable.Rows[0][this.tableComboBoxDataTable.KeyIdColumn] = 1;
            this.tableComboBoxDataTable.Rows[0][this.tableComboBoxDataTable.KeyNameColumn] = "Yes";
            this.tableComboBoxDataTable.Rows[1][this.tableComboBoxDataTable.KeyIdColumn] = 0;
            this.tableComboBoxDataTable.Rows[1][this.tableComboBoxDataTable.KeyNameColumn] = "No";

            this.tableComboBoxDataTable.AcceptChanges();
        }



        /// <summary>
        /// default - Loads the yes no value in the combobox in uppercase.
        /// </summary>
        public void LoadYesNoValueUpperCase()
        {
            this.tableComboBoxDataTable.Rows.Clear();
            ////initialize two rows
            for (int i = 0; i < 2; i++)
            {
                DataRow dr = this.tableComboBoxDataTable.NewRow();
                this.tableComboBoxDataTable.Rows.Add(dr);
            }

            this.tableComboBoxDataTable.Rows[0][this.tableComboBoxDataTable.KeyIdColumn] = 1;
            this.tableComboBoxDataTable.Rows[0][this.tableComboBoxDataTable.KeyNameColumn] = "YES";
            this.tableComboBoxDataTable.Rows[1][this.tableComboBoxDataTable.KeyIdColumn] = 0;
            this.tableComboBoxDataTable.Rows[1][this.tableComboBoxDataTable.KeyNameColumn] = "NO";

            this.tableComboBoxDataTable.AcceptChanges();
        }


        /// <summary>
        /// default - Loads the yes no value in the combobox.
        /// </summary>
        public void LoadGeneralComboData(Hashtable dataValues)
        {
            this.tableComboBoxDataTable.Rows.Clear();
            int rowID = 0;
            int keyId = 0 ;
         
            ////initialize two rows
         foreach (DictionaryEntry ComboRowValue in dataValues)
         {
             DataRow ComboBoxRow = this.tableComboBoxDataTable.NewRow();
             this.tableComboBoxDataTable.Rows.Add(ComboBoxRow);
             int.TryParse(ComboRowValue.Value.ToString(),out keyId);
             this.tableComboBoxDataTable.Rows[rowID][this.tableComboBoxDataTable.KeyIdColumn] = keyId;
             this.tableComboBoxDataTable.Rows[rowID][this.tableComboBoxDataTable.KeyNameColumn] = ComboRowValue.Key.ToString() ;
             rowID++;
         }
            
            this.tableComboBoxDataTable.AcceptChanges();
        }

    }
}
