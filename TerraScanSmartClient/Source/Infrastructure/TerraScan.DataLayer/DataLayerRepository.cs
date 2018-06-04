namespace TerraScan.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;
    using System.Data.SqlClient;
    public class DataLayerRepository
    {
        #region Validation Methods
        /// <summary>
        /// Checks for SQL injection.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        /// <returns></returns>
        public static Boolean CheckInputParams(IDictionary userInput)
        {
            bool isValid = false;
            string[] sqlCheckList = {"';delete","';insert","';update","';alter","';drop","';truncate","';create","';enable","';execute","';exec",
                                       "';xp_",";restore",";backup","';--","';sp_executesql","';rename","';kill"
                                       };

            foreach (var item in userInput)
            {
                for (int i = 0; i <= sqlCheckList.Length - 1; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(item).Trim()) && (!Convert.ToString(item).Trim().ToLower().Equals("null")))
                    {
                        if (item.ToString().ToLower().Contains(sqlCheckList[i]))
                        {
                            isValid = true;
                        }
                    }
                }
            }
            return isValid;
        }

        /// <summary>
        /// Validates for SQL injection.
        /// </summary>
        /// <param name="userInput">The user input.</param>
        /// <returns></returns>
        public static Boolean ValidateInputParams(SqlParameter[] userInput)
        {
            bool isValid = false;
            string[] sqlCheckList = {"';delete","';insert","';update","';alter","';drop","';truncate","';create","';enable","';execute","';exec",
                                       "';xp_",";restore",";backup","';--","';sp_executesql","';rename","';kill"
                                       };

            // var invalidChar = userInput.(x => sqlCheckList.Select(y => y).Contains(x.ToString()));
            foreach (var item in userInput)
            {
                for (int i = 0; i <= sqlCheckList.Length - 1; i++)
                {
                    if (!string.IsNullOrEmpty(Convert.ToString(item).Trim()) && (!Convert.ToString(item).Trim().ToLower().Equals("null")))
                    {
                        if (item.ToString().ToLower().Contains(sqlCheckList[i]))
                        {
                            isValid = true;
                        }
                    }
                }
            }
            return isValid;
        }
        #endregion
    }
}
