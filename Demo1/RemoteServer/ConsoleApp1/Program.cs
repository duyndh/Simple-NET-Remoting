// SingleCall / Singleton / ClientAO 
#define Singleton

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace RemoteServer
{
    class Program
    {
        private const int PORT = 8089;
        private const string APP_NAME = "test";

        static void Main(string[] args)
        {
            //var remotableObject = new RemoteClass.RemoteClass();

            // using TCP protocol
            TcpChannel channel = new TcpChannel(PORT);
            ChannelServices.RegisterChannel(channel);

#if !(ClientAO)

            // Server Activated Objects
            RemotingConfiguration.RegisterWellKnownServiceType(
               typeof(RemoteClass.RemoteClass),
               APP_NAME,
#if (SingleCall)
               WellKnownObjectMode.SingleCall);
#else
               WellKnownObjectMode.Singleton);
#endif


#else
            // Client Activated Objects
            RemotingConfiguration.ApplicationName = APP_NAME;
            RemotingConfiguration.RegisterActivatedServiceType(typeof(RemoteClass.RemoteClass));
#endif
            
            Console.Write("Sever is running ...");
            Console.Read();
        }
    }
}
