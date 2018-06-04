using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;



namespace TerrascanRegistryRightsProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (!EventLog.SourceExists("TerraScan"))
                {
                    EventLog.CreateEventSource("TerraScan", "TerraScan");
                }
            }
            catch (Exception ex)
            {
                

            }
        }
    }
}
