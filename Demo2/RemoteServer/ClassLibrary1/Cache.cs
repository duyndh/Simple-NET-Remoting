using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotableObjects
{
    public class Cache
    {
        private static Cache instance;
        private static IObserver Observer;

        private Cache() { }

		public static void Attach(IObserver observer)
		{
			Observer = observer;
		}
		public static Cache Instance
		{
			get
			{
				if (instance == null)
					instance = new Cache();

				return instance;
			}
		}

		public string SendNotify(EAction action, string text)
		{
			return Observer.Notify(action, text);
		}
	}
}
