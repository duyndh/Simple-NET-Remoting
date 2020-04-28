using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteClass
{
    public class RemoteClass : MarshalByRefObject
    {
        private int _count = 0;

        public RemoteClass()
        {
        }

        public int Increase(int x)
        {
            _count++;
            return x + 1;           
        }

        public int GetCount()
        {
            return _count;
        }
    }
}
