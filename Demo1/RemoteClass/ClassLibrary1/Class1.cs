// MBR / MBV
#define MBR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteClass
{
#if (MBV)
    [Serializable]
    public class RemoteClass
#else
    public class RemoteClass : MarshalByRefObject
#endif
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
