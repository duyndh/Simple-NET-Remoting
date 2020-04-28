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
        private const int PORT = 8085;

        static void Main(string[] args)
        {
            var remotableObject = new RemoteClass.RemoteClass();

            // using TCP protocol
            TcpChannel channel = new TcpChannel(PORT);
            ChannelServices.RegisterChannel(channel);

            // Register
            RemotingConfiguration.RegisterWellKnownServiceType(
               typeof(RemoteClass.RemoteClass), 
               "test",
               WellKnownObjectMode.SingleCall);

            Console.Write("Sever is running ...");
            Console.Read();
        }
    }
}
