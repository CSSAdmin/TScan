//--------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file Class for TreasurerData.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// JAN 11 2016      Priyadharshini      Created
//*********************************************************************************/using System;
namespace T2Installer
{
    /// <summary>
    /// this class used to hold the treasurer Properties.
    /// </summary>
    public static class TreasurerData
    {
        /// <summary>
        /// Variable to hold the Web Service Url.
        /// </summary>
        public static string WCFServiceURL { get; set; }

        /// <summary>
        /// Variable to hold the install Url.
        /// </summary>
        public static string InstallURL { get; set; }

        /// <summary>
        /// Variable to hold the MS wcf Service Url.
        /// </summary>
        public static string MSWCFServiceURL { get; set; }
    }
}
