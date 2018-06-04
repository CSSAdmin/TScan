// -------------------------------------------------------------------------------------------------
// <copyright file="ImportFile.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
// Morgage Import Entry Details
// </summary>
// -------------------------------------------------------------------------------------------------
namespace TerraScan.BusinessEntities 
{
    using System.Data;
    using System.Collections;

    /// <summary>
    /// Provides methods for inserting mortgage import entry record
    /// </summary>
    public partial class ImportFile
    {
        /// <summary>
        /// Inserts mortgage import entry record
        /// </summary>
        /// <param name="mortgageImportDictionary">The collection contains columnname and value</param>
        /// <param name="lineIndex">The index of the file line</param>
        public void InsertMortgageImportEntry(IDictionary mortgageImportDictionary, int lineIndex)
        {            
            IDictionaryEnumerator enumerator = mortgageImportDictionary.GetEnumerator();
            string fieldValue = string.Empty;
            int maxLength = -1;
            string columnName = string.Empty;
            DataRow dr = this.tableMortgageImportEntry.NewRow();
            
            while (enumerator.MoveNext())
            {
                columnName = enumerator.Key.ToString();
                maxLength = this.tableMortgageImportEntry.Columns[columnName].MaxLength;
                fieldValue = enumerator.Value.ToString();
                if (maxLength > 0 && fieldValue.Length > maxLength)
                {
                    fieldValue = fieldValue.Remove(maxLength);
                }

                dr[columnName] = fieldValue;
            }

            dr[this.MortgageImportEntry.FileLineColumn] = lineIndex;

            this.tableMortgageImportEntry.Rows.Add(dr);
        }
    }
}
