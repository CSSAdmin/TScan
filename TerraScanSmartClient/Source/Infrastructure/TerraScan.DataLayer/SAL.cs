
//-------------------------------------------------------------------------------------------------
// <copyright file="SAL.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	A class to hold methods to access the ado.net	
// </summary>
//-------------------------------------------------------------------------------------------------
//**********************************************************************************
// Description:	Sql Access Layer Class
// Author:		Thilak Raj
// Date:		20 Oct 2005
//**********************************************************************************
// Change History
//**********************************************************************************
// Date				Author			Description
// ----------		---------		----------------------------------------------------------
// 20 Oct 2005		Thilak Raj	Written the data access methods which connect the ado.net
// 
// 
// 
//*********************************************************************************/



namespace TerraScan.DataLayer
{
    using System;
    using System.Collections;
    using System.Configuration;
    using System.Data;
    using System.Windows.Forms;

    /// <summary>
    /// Sql Access Layer.
    /// </summary>
    public class SAL
    {
        //private static readonly string tempString = ConfigurationManager.AppSettings["ConnectionString"];
        /// <summary>
        /// Initializes a new instance of the <see cref="T:SAL"/> class.
        /// </summary>
        public SAL()
        {
        }

        #region Public Members



        /// <summary>
        /// Excecutes the SP.
        /// </summary>
        /// <param name="localStr">The local STR.</param>
        /// <param name="procDetail">The proc detail.</param>
        /// <param name="listParamItems">The list param items.</param>
        /// <returns></returns>
        public static int LoadProcedureItem(string procDetail, IDictionary listParamItems)
        {
            try
            {
                if (!DataLayerRepository.CheckInputParams(listParamItems))
                {
                    return TerraScan.UtilityWrapper.UtilityWrapper.WrapperSALExcecuteSP(procDetail, listParamItems);
                }
                else
                { 
                    return 0;
                }

            }
            catch (Exception )
            {
                throw ;
            }
        }

        /// <summary>
        /// Fetches the sp object.
        /// </summary>
        /// <param name="localStr">The local STR.</param>
        /// <param name="procDetail">The proc detail.</param>
        /// <param name="paramAndValue">The param and value.</param>
        /// <returns></returns>
        public static object FetchSpObject(string procDetail, IDictionary listParamItems)
        {
            try
            {
                if (!DataLayerRepository.CheckInputParams(listParamItems))
                {
                    return TerraScan.UtilityWrapper.UtilityWrapper.WrapperSALFetchSpObject(procDetail, listParamItems);
                }
                else
                { 
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Fetches the data set.
        /// </summary>
        /// <param name="localStr">The local STR.</param>
        /// <param name="procDetail">The proc detail.</param>
        /// <param name="paramAndValue">The param and value.</param>
        /// <returns></returns>
        public static DataSet FetchDataSet(string procDetail, IDictionary listParamItems)
        {
            try
            {
                if (!DataLayerRepository.CheckInputParams(listParamItems))
                {
                    return TerraScan.UtilityWrapper.UtilityWrapper.WrapperSALFetchDataSet(procDetail, listParamItems);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw ;
            }
        }


        /// <summary>
        /// Fetches the data set.
        /// </summary>
        /// <param name="localStr">The local STR.</param>
        /// <param name="procDetails">The proc details.</param>
        /// <returns></returns>
        public static DataSet FetchDataSet(string procDetails)
        {
            try
            {
                return TerraScan.UtilityWrapper.UtilityWrapper.WrapperSALFetchDataSet(procDetails);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Fetches the data table.
        /// </summary>
        /// <param name="localStr">The local STR.</param>
        /// <param name="procDetails">The proc details.</param>
        /// <param name="paramAndValue">The param and value.</param>
        /// <returns></returns>
        public static DataTable FetchDataTable(string procDetails, IDictionary listParamItems)
        {
            try
            {
                if (!DataLayerRepository.CheckInputParams(listParamItems))
                {
                    return TerraScan.UtilityWrapper.UtilityWrapper.WrapperSALFetchDataTable(procDetails, listParamItems);
                }
                else
                { 
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        /// <summary>
        /// Fetches the data table.
        /// </summary>
        /// <param name="localStr">The local STR.</param>
        /// <param name="procDetails">The proc details.</param>
        /// <returns></returns>
        public static DataTable FetchDataTable(string procDetails)
        {
            try
            {
                return TerraScan.UtilityWrapper.UtilityWrapper.WrapperSALFetchDataTable(procDetails);

            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion     
    }
}
