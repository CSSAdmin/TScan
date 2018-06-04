using System;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using TerraScan.Common;

// A WCF service consists of a contract (defined below), 
// a class which implements that interface, and configuration 
// entries that specify behaviors and endpoints associated with 
// that implementation (see <system.serviceModel> in your application
// configuration file).

internal class MyServiceHost
{
    internal static ServiceHost myServiceHost = null;

    internal static void StartService()
    {
        //Consider putting the baseAddress in the configuration system
        //and getting it here with AppSettings
        //Uri baseAddress = new Uri("http://localhost:8093/ExternalComponentService");

        //Consider putting the baseAddress in the configuration system
        //and getting it here with AppSettings
        //Uri baseAddress = new Uri("http://localhost:8092/service1");
        //Uri baseAddress = new Uri("net.tcp://192.168.60.89:4509/ExternalComponentServices.svc");
        Uri baseAddress = new Uri("net.tcp://localhost:4509/ExternalComponentServices.svc");
        ////Uri baseAddress2 = new Uri("http://localhost:1265/ExternalComponentService.svc");


        //Instantiate new ServiceHost 
        myServiceHost = new ServiceHost(typeof(TerraScan.UI.ExternalComponentService), baseAddress);
        //// myServiceHost.AddServiceEndpoint((typeof(TerraScan.UI.IService1)), new NetNamedPipeBinding(NetNamedPipeSecurityMode.None), baseAddress);

        //Open myServiceHost
        myServiceHost.Open();
    }

    internal static void StopService()
    {
        //Call StopService from your shutdown logic (i.e. dispose method)
        if (myServiceHost.State != CommunicationState.Closed)
            myServiceHost.Close();
    }
}