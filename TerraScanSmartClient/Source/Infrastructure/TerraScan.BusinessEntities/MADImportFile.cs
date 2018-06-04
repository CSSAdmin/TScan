using System.Collections;
using System.Data;
using System;
using System.Windows.Forms;
namespace TerraScan.BusinessEntities {
    
    
    public partial class MADImportFile {
        /// <summary>
        /// Inserts MAD import entry record
        /// </summary>
        /// <param name="MADImportDictionary">The collection contains columnname and value</param>
        /// <param name="lineIndex">The index of the file line</param>
        public void InsertMADImportEntry(IDictionary MADImportDictionary, int lineIndex)
        {
            try
            {
                IDictionaryEnumerator enumerator = MADImportDictionary.GetEnumerator();
                string fieldValue = string.Empty;
                int maxLength = -1;
                string columnName = string.Empty;
                DataRow dr = this.tableMADImportEntry.NewRow();

                while (enumerator.MoveNext())
                {
                    columnName = enumerator.Key.ToString();
                    maxLength = this.tableMADImportEntry.Columns[columnName].MaxLength;
                    fieldValue = Convert.ToString(enumerator.Value);
                    if (maxLength > 0 && fieldValue.Length > maxLength)
                    {
                        fieldValue = fieldValue.Remove(maxLength);
                    }

                    if (fieldValue == "")
                    {

                    }
                    else
                    {
                        dr[columnName] = fieldValue;
                    }
                }

               // dr[this.PermitImportEntry.LineColumn] = lineIndex;

                this.tableMADImportEntry.Rows.Add(dr);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Data given");
            }
        }
    }
}
namespace TerraScan.BusinessEntities.MADImportFileTableAdapters
{
    
    
    public partial class MADImportFile {


    }
}
