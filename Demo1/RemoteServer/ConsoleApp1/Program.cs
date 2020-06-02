// SingleCall / Singleton / ClientAO 
//#define SingleCall

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

        enum Types
        {
            SingleCall = 1,
            Singleton = 2,
            ClientAO = 3
        }

        const Types REMOTE_TYPE = Types.Singleton;

        static void Main(string[] args)
        {
            //var remotableObject = new RemoteClass.RemoteClass();

            // using TCP protocol
            TcpChannel channel = new TcpChannel(PORT);
            ChannelServices.RegisterChannel(channel);

            if (REMOTE_TYPE != Types.ClientAO)
            {
                if (REMOTE_TYPE == Types.SingleCall)
                {
                    // Server Activated Objects
                    RemotingConfiguration.RegisterWellKnownServiceType(
                       typeof(RemoteClass.RemoteClass),
                       APP_NAME,
                       WellKnownObjectMode.SingleCall);
                }
                else
                {
                    // Server Activated Objects
                    RemotingConfiguration.RegisterWellKnownServiceType(
                       typeof(RemoteClass.RemoteClass),
                       APP_NAME,
                       WellKnownObjectMode.Singleton);
                }
            }
            else
            {
                // Client Activated Objects
                RemotingConfiguration.ApplicationName = APP_NAME;
                RemotingConfiguration.RegisterActivatedServiceType(typeof(RemoteClass.RemoteClass));
            }
            
            Console.Write("Sever is running ...");
            Console.Read();
        }
    }
}
