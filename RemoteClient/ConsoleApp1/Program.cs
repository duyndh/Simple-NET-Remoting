using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace RemoteClient
{
    class Program
    {
        private const string serverIp = "192.168.11.102";
        private const int PORT = 8085;

        static void Main(string[] args)
        {
            while (true)
            {
                //TcpChannel channel = new TcpChannel();
                //ChannelServices.RegisterChannel(channel);

                // Create an instance of the remote object
                var remoteObject = (RemoteClass.RemoteClass)Activator.GetObject(
                  typeof(RemoteClass.RemoteClass),
                    "tcp://" + serverIp + ":" + PORT.ToString() + "/test");
                
                Console.Write("x = ");
                int x = Int32.Parse(Console.ReadLine());

                Console.WriteLine("x + 1 = " + remoteObject.Increase(x).ToString());
                Console.WriteLine("n call = " + remoteObject.GetCount().ToString());
            }
        }
    }
}
