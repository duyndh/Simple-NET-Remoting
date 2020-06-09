// SingleCall / Singleton / ClientAO 
//#define Singleton

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
        //private const int PORT = 8089;
        private const string APP_NAME = "test";

        enum Types
        {
            SingleCall = 1,
            Singleton = 2,
            ClientAO = 3
        }

        static void Main(string[] args)
        {
            var type = (Types)Int32.Parse(args[0]);
            var port = Int32.Parse(args[1]);

            // using TCP protocol
            TcpChannel channel = new TcpChannel(port);
            ChannelServices.RegisterChannel(channel);

            if (type != Types.ClientAO)
            {
                if (type == Types.SingleCall)
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
