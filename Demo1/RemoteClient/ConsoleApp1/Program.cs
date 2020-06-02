// SingleCall / Singleton / ClientAO 
//#define Singleton

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
        private const int PORT = 8089;
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
            var serverIp = args[1];

            RemoteClass.RemoteClass remoteObject = null;

            if (type != Types.ClientAO)
            {
                // Server Activated Objects
                remoteObject = (RemoteClass.RemoteClass)Activator.GetObject(
                        typeof(RemoteClass.RemoteClass),
                        string.Format("tcp://{0}:{1}/{2}", serverIp, PORT.ToString(), APP_NAME));
            }
            else
            {
                // Client Activated Objects
                RemotingConfiguration.RegisterActivatedClientType(
                    typeof(RemoteClass.RemoteClass),
                    string.Format("tcp://{0}:{1}/{2}", serverIp, PORT.ToString(), APP_NAME));
                remoteObject = new RemoteClass.RemoteClass();

            }

            while (true)
            {                
                Console.WriteLine(remoteObject.Ping());
                Console.WriteLine("n call: " + remoteObject.GetCount().ToString());
                Console.ReadLine();
            }
        }
    }
}
