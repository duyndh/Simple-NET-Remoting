using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteClass
{
    //[Serializable]
    public class RemoteClass : MarshalByRefObject
    {
        private int _count = 0;

        public RemoteClass()
        {
        }

        public string Ping()
        {
            _count++;
            return "Pong";
        }

        public int GetCount()
        {
            return _count;
        }
    }
}
