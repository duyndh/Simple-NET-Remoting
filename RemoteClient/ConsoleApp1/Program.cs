using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //TcpChannel ch = new TcpChannel();
            while (true)
            {
                RemoteClass.RemoteClass obj = new RemoteClass.RemoteClass();
                obj = (RemoteClass.RemoteClass)Activator.GetObject(typeof(RemoteClass.RemoteClass), "tcp://localhost:8085/test");

                var inputStr = Console.ReadLine();

                Console.Write("x = ");
                int x = Int32.Parse(Console.ReadLine());
                Console.Write("y = ");
                int y = Int32.Parse(Console.ReadLine());

                Console.WriteLine("x + y = " + obj.Sum(x, y).ToString());
            }
        }
    }
}
