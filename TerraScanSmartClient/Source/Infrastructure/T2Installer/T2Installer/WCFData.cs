//--------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Congruent">
//		Copyright (c) Congruent Info-Tech.  All rights reserved.
// </copyright>
// <summary>
//	This file Class for WCFData.
// </summary>
//----------------------------------------------------------------------------------------------
// Change History
//**********************************************************************************
// Date			    Author		       Description
// ----------		---------		   ---------------------------------------------------------
// JAN 11 2016      Priyadharshini      Created
//*********************************************************************************
using System;

namespace T2Installer
{
    /// <summary>
    /// this class uesed to hold the WCF Properties.
    /// </summary>
    public static class WCFData
    {
        /// <summary>
        /// Variable to hold the Domain name.
        /// </summary>
        public static string DomainName { get; set; }

        /// <summary>
        /// Variable to hold the LDAP.
        /// </summary>
        public static string LDAP { get; set; }

        /// <summary>
        /// Variable to hold the User Name.
        /// </summary>
        public static string UserName { get; set; }
        
        /// <summary>
        /// Variable to hold the Password.
        /// </summary>
        public static string Password { get; set; }

        /// <summary>
        /// Variable to hold the DB Server Name.
        /// </summary>
        public static string DBServerName { get; set; }

        /// <summary>
        /// Variable to hold the Db Name.        
        /// </summary>
        public static string DBName { get; set; }

        /// <summary>
        /// Variable to hold the Handle.
        /// </summary>
        public static IntPtr handle {get;set;}
        
        /// <summary>
        /// Variable to hold the Wcf Handle.
        /// </summary>
        public static IntPtr wcfhandle { get; set; }       
    }   
}
