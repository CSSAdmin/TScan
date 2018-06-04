//--------------------------------------------------------------------------------------------
// <copyright file="IServiceImplimentation.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file contains methods for the Owner Recipting.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		  ---------------------------------------------------------
// 2 May 07		    Guhan S	           Created
//*********************************************************************************/

namespace TerraScan.MSWCFService.ServiceContract
{
    using System.ServiceModel;
    using System.Collections.Generic;
    using System.Data;
    using System;
    using System.Collections;
    using RE7Engine;

    /// <summary>
    /// IServiceImplimentation
    /// </summary>
    [ServiceContractAttribute(Namespace = "http://TerraScan.MSWCFService.ServiceContract", Name = "IServiceImplimentation")]
    public interface IServiceImplimentation
    {
        /// <summary>
        /// F36000_s the get HTC XML.
        /// </summary>
        /// <returns>HillsideZoneCollection</returns>
        ////[OperationContract]
        ////T2RE7Engine.T2RE7EngineInterFace.HillsideZoneCollection F36000_GetHTCXml();

        /// <summary>
        /// F36000_s the save marshall swift.
        /// </summary>
        /// <param name="saveElement">The save element.</param>
        /// <param name="componentXml">The component XML.</param>
        [OperationContract]
        void F36000_SaveMarshallSwift(Hashtable fieldHashTable, string componentXml, string groupXML, bool newMode);
        
        /// <summary>
        /// F36000_s the calculate RCN.
        /// </summary>
        /// <param name="fieldHashTable">The field hash table.</param>
        /// <param name="componentXml">The component XML.</param>
        /// <returns>string</returns>
        [OperationContract]
        string F36000_CalculateRCN(Hashtable fieldHashTable, string componentXml, string groupXML, bool newMode);

        /// <summary>
        /// Assigns the R e7 object.
        /// </summary>
        /// <param name="fieldHashTable">The field hash table.</param>
        /// <param name="componentXml">The component XML.</param>
        /// <returns>RE7Engine.Estimate</returns>
        [OperationContract]
        RE7Engine.Estimate AssignRE7Object(Hashtable fieldHashTable, string componentXml, string groupXML, bool newMode);

        /// <summary>
        /// GetEstimateObject
        /// </summary>
        /// <param name="estimateId"></param>
        /// <returns>int</returns>
        /// <returns>string</returns>
        [OperationContract]
        Hashtable F36000_GetEstimateObject(int estimateId, string connectionString);

        /// <summary>
        /// F36001_s the calculate RCN.
        /// </summary>
        /// <param name="saveElementsHashTable">The save elements hash table.</param>
        /// <returns></returns>
        [OperationContract]
        string F36001_CalculateRcn(Hashtable saveElementsHashTable);
    }
}
