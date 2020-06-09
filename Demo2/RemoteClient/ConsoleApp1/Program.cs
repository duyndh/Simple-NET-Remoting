// SingleCall / Singleton / ClientAO 
#define ClientAO

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace RemoteClient
{
    class Program
    {
        private const string APP_NAME = "RemoteTools";

        static string GetInitData()
        {
            // Get MAC address
            string address = string.Empty;
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet && nic.OperationalStatus == OperationalStatus.Up)
                {
                    address = BitConverter.ToString(nic.GetPhysicalAddress().GetAddressBytes());
                    break;
                }
            }

            // Get other info
            string info = string.Empty;
            {
                info += string.Format("Physical Address: {0}\n", address);
                info += String.Format("Machine Name: {0}\n", Environment.MachineName);
                info += String.Format("OS Version: {0}\n", Environment.OSVersion.ToString());
                info += String.Format("Processor count: {0}\n", Environment.ProcessorCount.ToString());
                info += String.Format("User Domain Name: {0}\n", Environment.UserDomainName);
                info += String.Format("User Name: {0}\n", Environment.UserName);
            }

            return info;
        }

        static void Main(string[] args)
        {
            var serverIp = args[0];

            var port = Int32.Parse(args[1]);

#if !(ClientAO)

            // Server Activated Objects
            var remoteObject = (RemotableObjects.RemoteClass)Activator.GetObject(
                typeof(RemotableObjects.RemoteClass),
                string.Format("tcp://{0}:{1}/{2}", serverIp, PORT.ToString(), APP_NAME));

#else
            // Client Activated Objects
            RemotingConfiguration.RegisterActivatedClientType(
                typeof(RemotableObjects.RemoteClass),
                string.Format("tcp://{0}:{1}/{2}", serverIp, port.ToString(), APP_NAME));
            var remoteObject = new RemotableObjects.RemoteClass();
#endif

            var info = GetInitData();
            if (remoteObject.Init(info) == null)
                return;

            
            while (true)
            {
                string command = remoteObject.GetCommand();
                if (command == null)
                    break;

                if (command.Length > 0)
                {
                    int iSpace = command.IndexOf(' ');

                    string fileName = string.Empty;
                    string arguments = string.Empty;

                    if (iSpace == -1)
                    {
                        fileName = command;
                    }
                    else
                    {
                        fileName = command.Substring(0, iSpace);
                        arguments = command.Substring(iSpace + 1);
                    }

                    Process.Start(fileName, arguments);
                }

                Thread.Sleep(1000);
            }

            remoteObject.Terminate();
        }
    }
}
